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
    public class EfSqlModMapper : IMapMods
    {
        private DataContext _context;
        private IMapper _mapper;

        public EfSqlModMapper(DataContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<GetAModResponse> AddAMod(Guid gameId, PostModRequest request)
        {
            var mod = _mapper.Map<Mod>(request);
            var game = await _context.Games.Where(g => g.Id == gameId).Include(g => g.Mods).FirstOrDefaultAsync();
            game.Mods.Add(mod);
            await _context.SaveChangesAsync();
            var response = _mapper.Map<GetAModResponse>(mod);
            return response;
        }

        public async Task<GetAModResponse> GetAMod(Guid gameId, Guid modId)
        {
            var game = await _context.Games.Where(g => g.Id == gameId).Include(g => g.Mods).FirstOrDefaultAsync();

            GetAModResponse response = _mapper.Map<GetAModResponse>(game.Mods.Where(m => m.Id == modId).FirstOrDefault());
            
            return response;
        }

        public async Task<GetModsResponse> GetModsByGameId(Guid gameId)
        {
            var game = await _context.Games.Where(g => g.Id == gameId).Include(g => g.Mods).FirstOrDefaultAsync();

            GetModsResponse response = new GetModsResponse
            {
                Data = _mapper.Map<ICollection<Mod>, List<ModSummaryItem>>(game.Mods)
            };
            response.TotalMods = response.Data.Count;
            response.HasMods = response.Data.Count >= 1;

            return response;
        }

        public async Task Remove(Guid gameId, Guid modId)
        {
            Game game = await _context.Games
                        .Where(g => g.Id == gameId).Include(g => g.Mods)
                        .SingleOrDefaultAsync();
            if (game != null)
            {
                var itemToRemove = game.Mods.Single(m => m.Id == modId);
                game.Mods.Remove(itemToRemove);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<bool> UpdateMod(Guid gameId, Guid modId, UpdateModRequest request)
        {
            Game originalGame = await _context.Games
                        .Where(g => g.Id == gameId).Include(g => g.Mods)
                        .SingleOrDefaultAsync();

            Mod originalMod = originalGame.Mods.Where(m => m.Id == modId).FirstOrDefault();
            if (originalMod != null)
            {
                _mapper.Map<UpdateModRequest, Mod>(request, originalMod);

                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }
    }
}
