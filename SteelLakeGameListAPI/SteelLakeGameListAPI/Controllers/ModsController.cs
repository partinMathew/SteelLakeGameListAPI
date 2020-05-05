using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using SteelLakeGameListAPI.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Controllers
{
    public class ModsController : ControllerBase
    {
        private IMapMods _mapper;
        private readonly IDistributedCache _distributedCache;

        public ModsController(IMapMods mapper, IDistributedCache distributedCache)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }

        //[HttpGet("games/{id:Guid}/mods", Name = "game-mods#getallmods")]
        //[Produces("application/json")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        //public async Task<ActionResult<GetModsResponse>> GetMoods(Guid gameId)
        //{
            
        //}

    }
}
