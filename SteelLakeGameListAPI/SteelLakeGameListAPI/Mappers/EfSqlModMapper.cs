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

        public async Task<GetModsResponse> GetModsByGameId(Guid gameId)
        {
            GetModsResponse response = new GetModsResponse
            {
                Data = await _context.Games.Where(g => g.Id == gameId).Select(g => _mapper.Map<ModSummaryItem>(g.Mods)).ToListAsync()
            };
            response.TotalMods = response.Data.Count;
            response.HasMods = response.Data.Count >= 1;

            return response;
        }
    }
}
