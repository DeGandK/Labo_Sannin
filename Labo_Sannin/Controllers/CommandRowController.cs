using Labo_BLL.Interfaces;
using Labo_Domain.Models;
using Labo_Sannin_API.Models;
using Labo_Sannin_API.Tools;
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
        [HttpGet]
        public IActionResult GetByCommandId(int id)
        {
            return Ok(_commandRowService.GetByCommandId(id));
        }
        ///// <summary>
        ///// Création d'une ligne avec un produit
        ///// </summary>
        ///// <param name="form"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public IActionResult Create(CommandRowCreateForm form) 
        //{
        //    if (!ModelState.IsValid) return BadRequest();
        //    _commandRowService.Create(form);
        //    return Ok();
        //}
    }
}
