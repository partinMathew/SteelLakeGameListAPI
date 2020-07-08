using SteelLakeGameListAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Mappers
{
    public interface IMapMods
    {
        Task<GetModsResponse> GetModsByGameId(Guid gameId);
        Task<GetAModResponse> GetAMod(Guid gameId, Guid modId);
        Task<GetAModResponse> AddAMod(Guid gameId, PostModRequest request);
        Task Remove(Guid gameId, Guid modId);
        Task<bool> UpdateMod(Guid gameId, Guid modId, UpdateModRequest request);
    }
}
