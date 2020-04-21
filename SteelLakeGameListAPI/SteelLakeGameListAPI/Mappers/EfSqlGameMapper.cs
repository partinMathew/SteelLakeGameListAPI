using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SteelLakeGameListAPI.Domain;
using SteelLakeGameListAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Mappers
{
    public class EfSqlGameMapper : IMapGames
    {
        private DataContext _context;
        private IMapper _mapper;

        public EfSqlGameMapper(DataContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<GetAGameResponse> AddAGame(PostGameRequest request)
        {
            var game = _mapper.Map<Game>(request);
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<GetAGameResponse>(game);
            return response;
        }

        public async Task<GetGamesResponse> GetAllGames()
        {
            GetGamesResponse response = new GetGamesResponse
            {
                Data = await _context.Games.Select(g => _mapper.Map<GameSummaryItem>(g)).ToListAsync()
            };
            response.TotalGames = response.Data.Count;

            return response;
        }

        public async Task<GetAGameResponse> GetGameById(Guid id)
        {
            var response = await _context.Games
            .Where(g => g.Id == id)
            .Select(g => _mapper.Map<GetAGameResponse>(g))
            .SingleOrDefaultAsync();
            return response;
        }

        public async Task Remove(Guid id)
        {
            Game game = await _context.Games
                        .Where(g => g.Id == id)
                        .SingleOrDefaultAsync();
            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task<bool> UpdateGame(Guid id, UpdateGameRequest request)
        {
            Game originalGame = await _context.Games
                        .Where(g => g.Id == id)
                        .SingleOrDefaultAsync();
            if (originalGame != null)
            {
                _mapper.Map<UpdateGameRequest, Game>(request, originalGame);
                
                await _context.SaveChangesAsync();
                
                return true;
            }
            return false;
        }
    }
}
