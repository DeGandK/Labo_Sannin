﻿using Labo_BLL.Interfaces;
using Labo_BLL.Models;
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
    public class CommandController : ControllerBase
    {
        private readonly ICommandService _commandService;
        private readonly ICommandRowService _commandRowService;
        private readonly IProductService _productService;
        public CommandController(ICommandService commandService, ICommandRowService commandRowService, IProductService productService)
        {
            _commandService = commandService;
            _commandRowService = commandRowService;
            _productService = productService;
        }
        /// <summary>
        /// Fournit la liste des commandes
        /// </summary>
        /// <returns>Un IEnumerable de Command</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_commandService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Récupération des commandes par User
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [Authorize("isConnectedPolicy")]
        [HttpGet("{UserID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCommandsbyUserID([FromRoute] int UserID)
        {
            try
            {
                return Ok(_commandService.GetCommandsByUserID(UserID));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Création de commande
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Authorize("isConnectedPolicy")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CommandCreateForm form)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                _commandService.Create(form.ToBLL());
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Cette méthode sert à valider la commande en fonction de son état de paiement
        /// </summary>
        /// <param name="CommandId"></param>
        /// <returns></returns>
        [Authorize("adminPolicy")]
        [HttpPost("{CommandId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult IsValid(int CommandId)
        {
            // Ici il faut trouver le moyen de savoir si la commande a été payée ou pas... Paypal? Bancontact? etc
            try
            {
                bool isPaid = _commandService.CheckIsPaid(CommandId);
                bool IsValid = _commandService.IsValid(CommandId, isPaid);
                if (IsValid)
                {
                    _commandService.StockAchat(CommandId);
                    return Ok("Commande validée avec succès");
                }
                else
                {
                    _commandService.DeleteCommande(CommandId);
                    return Ok("La commande a été annulée car le paiement a échoué");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize("adminPolicy")]
        [HttpPut("{CommandID}")]
        public IActionResult ValiderCommande(int CommandId) 
        {
            try
            {
                _commandService.ValiderCommande(CommandId);
                return Ok();
            }
            catch (Exception ex )
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
