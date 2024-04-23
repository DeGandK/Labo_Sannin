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
    public class CommandController : ControllerBase
    {
        private readonly ICommandService _commandService;
        private readonly ICommandRowService _commandRowService;
        public CommandController(ICommandService commandService, ICommandRowService commandRowService)
        {
            _commandService = commandService;
            _commandRowService = commandRowService;
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
        /// <summary>
        /// Récupération de commande par User
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet("{UserID}")]
        public IActionResult GetCommandsbyUserID([FromRoute] int UserID)
        {
            return Ok(_commandService.GetCommandsByUserID(UserID));
        }
        /// <summary>
        /// Création de commande
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody] CommandCreateForm form)
        {
            if (!ModelState.IsValid) return BadRequest();

            _commandService.Create(form.ToBLL());
            return Ok();
        }

        /// <summary>
        /// Cette méthode sert à valider la commande en fonction de son état de paiement
        /// </summary>
        /// <param name="CommandId"></param>
        /// <returns></returns>
        [HttpPost("{CommandId}")]
        public IActionResult IsValid(int CommandId)
        {
            // Ici il faut trouver le moyen de savoir si la commande a été payée ou pas... Paypal? Bancontact? ect
            bool isPaid = _commandService.CheckIsPaid(CommandId);

            bool IsValid = _commandService.IsValid(CommandId, isPaid);

            if (IsValid)
            {
                _commandService.ValiderCommande(CommandId);
                return Ok("Commande validée avec succès");
            }
            else
            {
                _commandService.DeleteCommande(CommandId);
                return Ok("La commande a été annulée car le paiement a échoué");
            }
        }
    }
}
