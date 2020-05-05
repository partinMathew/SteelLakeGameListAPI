using AutoMapper;
using SteelLakeGameListAPI.Domain;
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
    }
}
