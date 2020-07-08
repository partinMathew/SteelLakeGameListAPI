using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SteelLakeGameListAPI.Mappers;
using SteelLakeGameListAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Controllers
{
    public class ModsController : GamesListControllerBase
    {
        private IMapMods _modsMapper;
        private IMapGames _gamesMapper;
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<ModsController> _logger;

        public ModsController(IMapMods modsMapper, IMapGames gamesMapper, IDistributedCache distributedCache, ILogger<ModsController> logger) : base(distributedCache)
        {
            _modsMapper = modsMapper ?? throw new ArgumentNullException(nameof(modsMapper));
            _gamesMapper = gamesMapper ?? throw new ArgumentNullException(nameof(gamesMapper));
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("games/{gameId:Guid}/mods", Name = "game-mods#getallmods")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<GetModsResponse>> GetMods(Guid gameId)
        {
            var game = await _gamesMapper.GetGameById(gameId);
            if (game == null)
            {
                return NotFound();
            }

            var key = $"game-mods#{gameId}";
            var value = await _distributedCache.GetStringAsync(key);

            GetModsResponse response;

            if(value != null)
            {
                response = JsonConvert.DeserializeObject<GetModsResponse>(value);
            }
            else
            {
                response = await _modsMapper.GetModsByGameId(gameId);
                await CacheResponse(key, 60, response);
            }

            return Ok(response);
        }

        [HttpGet("games/{gameId:Guid}/mods/{modId:Guid}", Name = "game-mods#getamod")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<GetAModResponse>> GetMods(Guid gameId, Guid modId)
        {
            var game = await _gamesMapper.GetGameById(gameId);
            if (game == null)
            {
                return NotFound();
            }

            var key = $"game-mods#{gameId}%{modId}";
            var value = await _distributedCache.GetStringAsync(key);

            GetAModResponse response;

            if (value != null)
            {
                response = JsonConvert.DeserializeObject<GetAModResponse>(value);
            }
            else
            {
                response = await _modsMapper.GetAMod(gameId, modId);
                await CacheResponse(key, 60, response);
            }

            return Ok(response);
        }

    }
}
