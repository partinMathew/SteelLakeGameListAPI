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

        public async Task<GetAModResponse> GetAMod(Guid gameId, Guid modId)
        {
            var game = await _context.Games.Where(g => g.Id == gameId).Include(g => g.Mods).FirstOrDefaultAsync();

            GetAModResponse response = _mapper.Map<GetAModResponse>(game.Mods.Select(m => m.Id == modId));
            
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
    }
}
