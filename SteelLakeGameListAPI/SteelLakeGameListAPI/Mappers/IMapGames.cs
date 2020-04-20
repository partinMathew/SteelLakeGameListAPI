using SteelLakeGameListAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Mappers
{
    public interface IMapGames
    {
        Task<GetAGameResponse> GetGameById(Guid id);
    }
}
