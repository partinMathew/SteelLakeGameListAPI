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
using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Logging;

namespace SteelLakeGameListAPI.Controllers
{
    public class GamesController : GamesListControllerBase
    {
        private IMapGames _mapper;
       
        private readonly ILogger<GamesController> _logger;

        public GamesController(IMapGames mapper, ILogger<GamesController> logger)
        {
            _mapper = mapper;
            _logger = logger;
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
            GetAGameResponse response = await _mapper.GetGameById(id);
            if (response == null)
            {
                return NotFound();
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
            GetGamesResponse response = await _mapper.GetAllGames();
            if(response == null)
            {
                return NotFound();
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
            
            _logger.LogInformation("Added game to database: " + request.Title);


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

            _logger.LogInformation("Removed game with id " + id.ToString());

            return NoContent();
        }


        /// <summary>
        /// Updates a given game by <paramref name="id"/> with the new information you pass in <paramref name="request"/>
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
