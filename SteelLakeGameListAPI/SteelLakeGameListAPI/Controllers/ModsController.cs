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
        private IMapMods _mapper;
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<ModsController> _logger;

        public ModsController(IMapMods mapper, IDistributedCache distributedCache, ILogger<ModsController> logger) : base(distributedCache)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
            // TODO: check if the game exists, return 404 if not


            var key = $"game-mods#{gameId}";
            var value = await _distributedCache.GetStringAsync(key);

            GetModsResponse response;

            if(value != null)
            {
                response = JsonConvert.DeserializeObject<GetModsResponse>(value);
            }
            else
            {
                response = await _mapper.GetModsByGameId(gameId);
                await CacheResponse(key, 60, response);
            }

            return Ok(response);
        }

    }
}
