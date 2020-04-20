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
    }
}
