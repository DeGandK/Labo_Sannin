using Labo_BLL.Interfaces;
using Labo_Domain.Models;
using Labo_Sannin_API.Models;
using Labo_Sannin_API.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labo_Sannin_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandRowController : ControllerBase
    {
        private readonly ICommandRowService _commandRowService;
        public CommandRowController(ICommandRowService commandRowService)
        {
            _commandRowService = commandRowService;
        }
        /// <summary>
        /// Récupération de la liste de commande
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize("isConnectedPolicy")]
        [HttpGet("{CommandID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetByCommandId(int CommandId)
        {
            try
            {
            return Ok(_commandRowService.GetByCommandId(CommandId));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
