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
using SteelLakeGameListAPI.Extensions;

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


        /// <summary>
        /// Get a game from the database
        /// </summary>
        /// <param name="id">The id of the game you are searching for</param>
        /// <returns>The game you searched for, or a 404 if it does not exist.</returns>
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


        /// <summary>
        /// Get all games from the database
        /// </summary>
        /// <returns>A list of all games in the database</returns>
        [HttpGet("games", Name = "games#getallgames")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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


        /// <summary>
        /// Add a game to the database
        /// </summary>
        /// <param name="request">A game to add to the database</param>
        /// <returns>A list of all games in the database</returns>
        [HttpPost("games", Name = "games#addagame")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ValidateModel]
        public async Task<ActionResult> AddAGame([FromBody] PostGameRequest request)
        {
            GetAGameResponse response = await _mapper.AddAGame(request);
            
            // We can cache this object for subsequent GET requests for the same object
            var key = $"game#{response.Id}";
            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddSeconds(150)); //Can cache this for longer, as it is a brand new object, and less likely to change
            var serializedResponse = JsonConvert.SerializeObject(response);
            await _distributedCache.SetStringAsync(key, serializedResponse, options);

            return CreatedAtRoute("games#getagame", new { id = response.Id }, response);
        }

        /// <summary>
        /// Remove a game from the database
        /// </summary>
        /// <param name="id">The id of the game you wish to remove</param>
        [HttpDelete("games/{id:Guid}", Name = "games#deleteagame")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteAGame(Guid id)
        {
            await _mapper.Remove(id);
            await _distributedCache.RemoveAsync($"game#{id}");
            return NoContent();
        }


        /// <summary>
        /// Updates a given game by <paramref name="id"/> with the new inforamtion you pass in <paramref name="request"/>
        /// </summary>
        /// <param name="id">The id of the game you wish to update</param>
        /// <param name="request">The updated object</param>
        [HttpPut("games/{id:Guid}", Name = "games#updateagame")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ValidateModel]
        public async Task<ActionResult> UpdateAGame(Guid id, [FromBody] UpdateGameRequest request)
        {
            bool didUpdate = await _mapper.UpdateGame(id, request);
            return this.Either<NoContentResult, NotFoundResult>(didUpdate);           
        }
    }
}
