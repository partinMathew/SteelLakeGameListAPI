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
    public class ModsController : GamesListControllerBase
    {
        private IMapMods _modsMapper;
        private IMapGames _gamesMapper;
        private readonly ILogger<ModsController> _logger;

        public ModsController(IMapMods modsMapper, IMapGames gamesMapper, ILogger<ModsController> logger)
        {
            _modsMapper = modsMapper;
            _gamesMapper = gamesMapper;
            _logger = logger;
        }




        /// <summary>
        /// Get all mods associated with a given game
        /// </summary>
        /// <param name="gameId">The GUID of the game associated with the mods</param>
        /// <returns>A list of the mods for the selected game, or not found</returns>
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

            GetModsResponse response = await _modsMapper.GetModsByGameId(gameId);
            
            return Ok(response);
        }

        /// <summary>
        /// Get a given mod
        /// </summary>
        /// <param name="gameId">The GUID of the game associated with the mod</param>
        /// <param name="modId">The GUID of the mod you wish to find</param>
        /// <returns>The mod you selected, or not found</returns>
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

            GetAModResponse response = await _modsMapper.GetAMod(gameId, modId);
            if (response == null)
            {
                return NotFound();
            }
           
            return Ok(response);
        }

        /// <summary>
        /// Add a mod to a game
        /// </summary>
        /// <param name="gameId">The id of the game associated with mod you are adding</param>
        /// <param name="request">A mod to add to the database</param>
        /// <returns>The mod you created</returns>
        [HttpPost("games/{gameId:Guid}/mods", Name = "game-mods#addamod")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ValidateModel]
        public async Task<ActionResult> AddAMod(Guid gameId, [FromBody] PostModRequest request)
        {
            GetAModResponse response = await _modsMapper.AddAMod(gameId, request);

            _logger.LogInformation("Added mod to database: " + request.Title + " Game ID: " + gameId);


            return CreatedAtRoute("game-mods#getamod", new { gameId = gameId, modId = response.Id }, response);
        }

        /// <summary>
        /// Remove a mod from a given game
        /// </summary>
        /// <param name="gameId">The id of the game association with the mod you wish to delete</param>
        /// <param name="modId">The id of the mod you wish to remove</param>
        [HttpDelete("games/{gameId:Guid}/mods/{modId:Guid}", Name = "game-mods#deleteamod")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteAMod(Guid gameId, Guid modId)
        {
            await _modsMapper.Remove(gameId, modId);

            _logger.LogInformation("Removed mod with id " + modId.ToString() + " Game Id: " + gameId);

            return NoContent();
        }

        /// <summary>
        /// Updates a given mod by <paramref name="modId"/> associated with a given <paramref name="gameId"/> with the new information you pass in <paramref name="request"/>
        /// </summary>
        /// <param name="gameId">The id of the game associated with the mod you wish to update</param>
        /// <param name="modId">The id of the mod you wish to update</param>
        /// <param name="request">The updated object</param>
        [HttpPut("games/{gameId:Guid}/mods/{modId:Guid}", Name = "game-mods#updateamod")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ValidateModel]
        public async Task<ActionResult> UpdateAMod(Guid gameId, Guid modId,  [FromBody] UpdateModRequest request)
        {
            bool didUpdate = await _modsMapper.UpdateMod(gameId, modId, request);
            return this.Either<NoContentResult, NotFoundResult>(didUpdate);
        }

    }
}
