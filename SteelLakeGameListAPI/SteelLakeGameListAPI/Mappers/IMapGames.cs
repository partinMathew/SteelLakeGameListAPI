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
        Task<GetGamesResponse> GetAllGames();
        Task<GetAGameResponse> AddAGame(PostGameRequest request);
        Task Remove(Guid id);
        Task<bool> UpdateGame(Guid id, UpdateGameRequest request);
    }
}
