using HiveGame.BusinessLogic.Models;
using HiveGame.BusinessLogic.Models.Requests;
using HiveGame.BusinessLogic.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace HiveGameAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;

        public GameController(IGameRepository repository)
        {
            _gameRepository = repository;
        }

        // GET: /Game
        [HttpGet]
        public ActionResult<IEnumerable<Game>> GetAllGames()
        {
            var games = _gameRepository.GetAll();
            return Ok(games);
        }

        // GET: /Game/{gameId}
        /// <summary>
        /// GET: /Game/{gameId}
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        // Mo¿na u¿yæ komentarza typu summary do  opisu danego kontrolera
        // Do zastanowienia siê czy nie powinno byæ InternalServerError(500) zamiast NotFound(404)
        // return StatusCode(500, $"Game with ID {gameId} not found.");
        [HttpGet("{gameId}")]
        public ActionResult<Game> GetGameById(string gameId)
        {
            var game = _gameRepository.GetByGameId(gameId);

            if (game == null)
            {
                return NotFound($"Game with ID {gameId} not found.");
            }

            return Ok(game);
        }

        // GET: /Game/player/{playerId}
        // Do zastanowienia siê czy nie powinno byæ InternalServerError(500) zamiast NotFound(404)
        // return StatusCode(500, $"No game found for player with ID {playerId}");
        [HttpGet("player/{playerId}")]
        public ActionResult<Game> GetGameByPlayerId(string playerId)
        {
            var game = _gameRepository.GetByPlayerId(playerId);

            if (game == null)
            {
                return NotFound($"No game found for player with ID {playerId}.");
            }

            return Ok(game);
        }

        // POST: /Game
        // Do zastanowienia siê czy nie powinno byæ InternalServerError(500) zamiast NotFound(404)
        // return StatusCode(500, "Invalid game data.");
        [HttpPost]
        public ActionResult AddGame([FromBody] Game game)
        {
            if (game == null)
            {
                return BadRequest("Invalid game data.");
            }

            _gameRepository.Add(game);

            return CreatedAtAction(nameof(GetGameById), new { gameId = game.Id }, game);
        }

        // DELETE: /Game/{gameId}
        // Do zastanowienia siê czy nie powinno byæ InternalServerError(500) zamiast NotFound(404)
        // return StatusCode(500, "Invalid game data.");
        [HttpDelete("{gameId}")]
        public ActionResult DeleteGame(string gameId)
        {
            var success = _gameRepository.Remove(gameId);

            if (!success)
            {
                return NotFound($"Game with ID {gameId} not found.");
            }

            return NoContent();
        }
    }
}