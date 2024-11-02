using HiveGame.BusinessLogic.Models;
using HiveGame.BusinessLogic.Models.Requests;
using HiveGame.BusinessLogic.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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