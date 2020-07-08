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
    public class EfSqlExpansionMapper : IMapExpansions
    {
        private DataContext _context;
        private IMapper _mapper;

        public EfSqlExpansionMapper(DataContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<GetAnExpansionResponse> AddAnExpansion(Guid gameId, PostExpansionRequest request)
        {
            var expansion = _mapper.Map<Expansion>(request);
            var game = await _context.Games.Where(g => g.Id == gameId).Include(g => g.Expansions).FirstOrDefaultAsync();
            game.Expansions.Add(expansion);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<GetAnExpansionResponse>(expansion);
            return response;
        }

        public async Task<GetAnExpansionResponse> GetAnExpansion(Guid gameId, Guid expansionId)
        {
            var game = await _context.Games.Where(g => g.Id == gameId).Include(g => g.Expansions).FirstOrDefaultAsync();

            GetAnExpansionResponse response = _mapper.Map<GetAnExpansionResponse>(game.Expansions.Where(e => e.Id == expansionId).FirstOrDefault());

            return response;
        }

        public async Task<GetExpansionsResponse> GetExpansionsByGameId(Guid gameId)
        {
            var game = await _context.Games.Where(g => g.Id == gameId).Include(g => g.Expansions).FirstOrDefaultAsync();

            GetExpansionsResponse response = new GetExpansionsResponse
            {
                Data = _mapper.Map<ICollection<Expansion>, List<ExpansionSummaryItem>>(game.Expansions)
            };
            response.TotalExpansions = response.Data.Count;
            response.HasExpansions = response.Data.Count >= 1;

            return response;
        }

        public async Task Remove(Guid gameId, Guid expansionId)
        {
            Game game = await _context.Games
                        .Where(g => g.Id == gameId).Include(g => g.Expansions)
                        .SingleOrDefaultAsync();
            if (game != null)
            {
                var itemToRemove = game.Expansions.Single(m => m.Id == expansionId);
                game.Expansions.Remove(itemToRemove);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<bool> UpdateExpansion(Guid gameId, Guid expansionId, UpdateExpansionRequest request)
        {
            Game originalGame = await _context.Games
                        .Where(g => g.Id == gameId).Include(g => g.Expansions)
                        .SingleOrDefaultAsync();

            Expansion originalExpansion = originalGame.Expansions.Where(m => m.Id == expansionId).FirstOrDefault();
            if (originalExpansion != null)
            {
                _mapper.Map<UpdateExpansionRequest, Expansion>(request, originalExpansion); 

                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}
