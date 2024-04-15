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
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly TokenGenerator _tokenGenerator;

        public AuthController(IUserService userService, TokenGenerator tokenGenerator)
        {
            _userService = userService;
            _tokenGenerator = tokenGenerator;

        }
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

        [HttpPost("register")]
        public IActionResult Register(UserRegisterForm form)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                _userService.Register(form.Nom,form.Prenom,form.Email, form.Password,form.Telephone,form.Adresse);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("update")]
        public IActionResult Update(UserUpdateForm form) 
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                _userService.Update()
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
