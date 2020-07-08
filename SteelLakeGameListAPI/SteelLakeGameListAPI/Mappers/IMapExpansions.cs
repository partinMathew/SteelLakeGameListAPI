using SteelLakeGameListAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Mappers
{
    public interface IMapExpansions
    {
        Task<GetAnExpansionResponse> AddAnExpansion(Guid gameId, PostExpansionRequest request);
        Task<GetAnExpansionResponse> GetAnExpansion(Guid gameId, Guid expansionId);
        Task<GetExpansionsResponse> GetExpansionsByGameId(Guid gameId);
        Task Remove(Guid gameId, Guid expansionId);
        Task<bool> UpdateExpansion(Guid gameId, Guid expansionId, UpdateExpansionRequest request);
    }
}
