﻿using Labo_BLL.Interfaces;
using Labo_Domain.Models;
using Labo_Sannin_API.Models;
using Labo_Sannin_API.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labo_Sannin_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly TokenGenerator _tokenGenerator;
        public AuthController(IUserService userService, TokenGenerator tokenGenerator)
        {
            _userService = userService;
            _tokenGenerator = tokenGenerator;
        }
        /// <summary>
        /// Log sur le site
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login(UserLoginForm loginInfo)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                User connectedUser = _userService.Login(loginInfo.Email, loginInfo.Password);
                string token = _tokenGenerator.GenerateToken(connectedUser);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Enregistrement d'un User
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult Register(UserRegisterForm form)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                _userService.Register(form.Nom,form.Prenom,form.Email,form.Password,form.Telephone,form.Adresse);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Mise à jour du Profil
        /// </summary>
        /// <param name="form"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("update")]
        public IActionResult Update([FromBody]UserUpdateForm form,[FromRoute] int id) 
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                User u = form.ToDOMAIN();
                u.UserID = id;
                _userService.Update(u);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Récupération de tous les utilisateurs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_userService.GetAll());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
