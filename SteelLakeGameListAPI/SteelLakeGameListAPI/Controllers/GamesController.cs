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
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace SteelLakeGameListAPI.Controllers
{
    public class GamesController : Controller
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
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<GetAGameResponse>> GetGameById(Guid id)
        {
            var key = $"game#{id}";
            var value = await _distributedCache.GetStringAsync(key);

            GetAGameResponse response;

            if (value != null)
            {
                response = JsonConvert.DeserializeObject<GetAGameResponse>(value);
            }
            else
            {
                response = await _mapper.GetGameById(id);
                if (response == null)
                {
                    return NotFound();
                }
                var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddSeconds(60));
                var serializedReponse = JsonConvert.SerializeObject(response);
                await _distributedCache.SetStringAsync(key, serializedReponse, options);

            }
            return Ok(response);
        }

        [HttpGet("games", Name = "games#getallgames")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<GetGamesResponse>> GetAllGames()
        {
            var key = "allGames";
            var value = await _distributedCache.GetStringAsync(key);

            GetGamesResponse response;

            if (value != null)
            {
                response = JsonConvert.DeserializeObject<GetGamesResponse>(value);
            }
            else
            {
                response = await _mapper.GetAllGames();
                if(response == null)
                {
                    return NotFound();
                }
                var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddSeconds(30));
                var serializedResponse = JsonConvert.SerializeObject(response);
                await _distributedCache.SetStringAsync(key, serializedResponse, options);
            }

            return Ok(response);
        }

        //[HttpPost("games", Name = "games#addagame")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult> AddAGame([FromBody] PostGamesRequest request)
        //{
            
        //}
    }
}
