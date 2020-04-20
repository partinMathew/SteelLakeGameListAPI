using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using SteelLakeGameListAPI.Mappers;
using SteelLakeGameListAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace SteelLakeGameListAPI.Controllers
{
    public class GamesController
    {
        private IMapGames _mapper;
        private readonly IDistributedCache _distributedCache;

        public GamesController(IMapGames mapper, IDistributedCache distributedCache)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }

        [HttpGet("games/{id:Guid}", Name = "games#getagame")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetAGameResponse>> GetGameById(Guid id)
        {

            GetAGameResponse response = await _mapper.GetGameById(id);

            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddSeconds(45));
            //var key = $"game#{id}";
            //var encodedResponse = 
            //await _distributedCache.SetAsync(key,response.)

            if (response == null)
            {
                return new NotFoundResult();
            }
            else
            {
                return new OkObjectResult(response);
            }

            
            //var time = await Cache.GetAsync("time");
            //string newTime = null;
            //if (time == null)
            //{
            //    newTime = DateTime.Now.ToLongTimeString();
            //    var encodedTime = Encoding.UTF8.GetBytes(newTime);
            //    var options = new DistributedCacheEntryOptions()
            //        .SetAbsoluteExpiration(DateTime.Now.AddSeconds(15));
            //    await Cache.SetAsync("time", encodedTime, options);
            //}
            //else
            //{
            //    newTime = Encoding.UTF8.GetString(time);
            //}
            //return Ok($"Ok, it is now {newTime}");
        }
    }
}
