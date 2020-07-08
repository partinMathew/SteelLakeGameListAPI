using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SteelLakeGameListAPI.Domain;
using SteelLakeGameListAPI.Extensions;
using SteelLakeGameListAPI.Mappers;
using SteelLakeGameListAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace SteelLakeGameListAPI.Controllers
{
    public class ExpansionsController : GamesListControllerBase
    {
        private IMapExpansions _expansionsMapper;
        private IMapGames _gamesMapper;
       
        private readonly ILogger<ExpansionsController> _logger;

        public ExpansionsController(IMapExpansions expansionsMapper, IMapGames gamesMapper, ILogger<ExpansionsController> logger)
        {
            _expansionsMapper = expansionsMapper;
            _gamesMapper = gamesMapper;
            _logger = logger;
        }






        /// <summary>
        /// Get all expansions associated with a given game
        /// </summary>
        /// <param name="gameId">The GUID of the game associated with the expansions</param>
        /// <returns>A list of the expansions for the selected game, or not found</returns>
        [HttpGet("games/{gameId:Guid}/expansions", Name = "game-expansions#getallexpansions")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<GetExpansionsResponse>> GetExpansions(Guid gameId)
        {
            var game = await _gamesMapper.GetGameById(gameId);
            if (game == null)
            {
                return NotFound();
            }

            GetExpansionsResponse response = await _expansionsMapper.GetExpansionsByGameId(gameId);                
            
            return Ok(response);
        }

        /// <summary>
        /// Get a given expansion
        /// </summary>
        /// <param name="gameId">The GUID of the game associated with the expansion</param>
        /// <param name="expansionId">The GUID of the expansion you wish to find</param>
        /// <returns>The expansion you selected, or not found</returns>
        [HttpGet("games/{gameId:Guid}/expansions/{expansionId:Guid}", Name = "game-expansions#getanexpansion")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<GetAnExpansionResponse>> GetExpansions(Guid gameId, Guid expansionId)
        {
            var game = await _gamesMapper.GetGameById(gameId);
            if (game == null)
            {
                return NotFound();
            }

            GetAnExpansionResponse response = await _expansionsMapper.GetAnExpansion(gameId, expansionId);
            if (response == null)
            {
                return NotFound();
            }
           

            return Ok(response);
        }

        /// <summary>
        /// Add an expansion to a game
        /// </summary>
        /// <param name="gameId">The id of the game associated with expansion you are adding</param>
        /// <param name="request">An expansion to add to the database</param>
        /// <returns>The expansion you created</returns>
        [HttpPost("games/{gameId:Guid}/expansions", Name = "game-expansions#addanexpansion")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ValidateModel]
        public async Task<ActionResult> AddAnExpansion(Guid gameId, [FromBody] PostExpansionRequest request)
        {
            GetAnExpansionResponse response = await _expansionsMapper.AddAnExpansion(gameId, request);
            
            _logger.LogInformation("Added expansion to database: " + request.Title + " Game ID: " + gameId);


            return CreatedAtRoute("game-expansions#getanexpansion", new { gameId = gameId, expansionId = response.Id }, response);
        }

        /// <summary>
        /// Remove a expansion from a given game
        /// </summary>
        /// <param name="gameId">The id of the game association with the expansion you wish to delete</param>
        /// <param name="expansionId">The id of the expansion you wish to remove</param>
        [HttpDelete("games/{gameId:Guid}/expansions/{expansionId:Guid}", Name = "game-expansions#deleteanexpansion")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteAnExpansion(Guid gameId, Guid expansionId)
        {
            await _expansionsMapper.Remove(gameId, expansionId);

            _logger.LogInformation("Removed expansion with id " + expansionId.ToString() + " Game Id: " + gameId);

            return NoContent();
        }

        /// <summary>
        /// Updates a given expansion by <paramref name="expansionId"/> associated with a given <paramref name="gameId"/> with the new information you pass in <paramref name="request"/>
        /// </summary>
        /// <param name="gameId">The id of the game associated with the expansion you wish to update</param>
        /// <param name="expansionId">The id of the expansion you wish to update</param>
        /// <param name="request">The updated object</param>
        [HttpPut("games/{gameId:Guid}/expansions/{expansionId:Guid}", Name = "game-expansions#updateanexpansion")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ValidateModel]
        public async Task<ActionResult> UpdateAnExpansion(Guid gameId, Guid expansionId, [FromBody] UpdateExpansionRequest request)
        {
            bool didUpdate = await _expansionsMapper.UpdateExpansion(gameId, expansionId, request);
            return this.Either<NoContentResult, NotFoundResult>(didUpdate);
        }

    }
}
