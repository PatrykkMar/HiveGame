using HiveGame.BusinessLogic.Models.Requests;
using HiveGame.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace HiveGameAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HiveGameController : ControllerBase
    {
        //TODO: Result classes
        private readonly IHiveGameService _hiveGameService;

        public HiveGameController(IHiveGameService hiveGameService)
        {
            _hiveGameService = hiveGameService;
        }

        [HttpGet("grid")]
        public IActionResult GetTestGrid()
        {
            var result = _hiveGameService.GetTestGrid();
            return Ok(new{ Vertices = result });
        }

        [HttpGet("gridPrint")]
        public IActionResult GetTestGridPrint()
        {
            var result = _hiveGameService.GetTestGridPrint();
            return Ok(new { Vertices = result });
        }

        [HttpPost("move")]
        public IActionResult Move([FromBody] MoveInsectRequest request)
        {
            var result = _hiveGameService.Move(request);
            return Ok(result);
        }

        [HttpPost("put")]
        public IActionResult Put([FromBody] PutInsectRequest request)
        {
            var result = _hiveGameService.Put(request);
            return Ok(result);
        }

        [HttpPost("putFirstInsect")]
        public IActionResult PutFirstInsect([FromBody] PutFirstInsectRequest request)
        {
            var result = _hiveGameService.PutFirstInsect(request);
            return Ok(result);
        }
    }
}