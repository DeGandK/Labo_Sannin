using Labo_BLL.Interfaces;
using Labo_Sannin_API.Models;
using Labo_Sannin_API.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labo_Sannin_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly ICommandService _commandService;
        public CommandController(ICommandService commandService)
        {
            _commandService = commandService;
        }
        /// <summary>
        /// Fournit la liste des Commandes
        /// </summary>
        /// <returns>Un IEnumerable de Command</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_commandService.GetAll());
        }
        [HttpGet("{UserID}")]
        public IActionResult GetCommandsbyUserID([FromRoute]int UserID) 
        {
            return Ok(_commandService.GetCommandsByUserID(UserID));
        }
        [HttpPost]
        public IActionResult Create([FromBody] CommandCreateForm form)
        {
            if (!ModelState.IsValid) return BadRequest();

            _commandService.Creat(form.ToDOMAIN());
            return Ok();
        }
    }
}
