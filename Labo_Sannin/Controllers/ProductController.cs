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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        /// <summary>
        /// Cette méthode permet d'obtenir la liste de tous les produits
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_productService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Cette méthode permet d'obtenir les caractéristiques d'un produit grâce à l'id rentré en paramètre
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_productService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Cette méthode permet de créer un produit
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Authorize("adminPolicy")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProductCreateForm),StatusCodes.Status400BadRequest)]
        public IActionResult Create(ProductCreateForm form)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                _productService.Create(form.ToDOMAIN());
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Permet de supprimer un produit en entrant son id en paramètre
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize("adminPolicy")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete (int id)
        {
            try
            {
                _productService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Cette méthode permet de modifier un produit via son ID
        /// </summary>
        /// <param name="p"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize("adminPolicy")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ProductCreateForm)),
        //    StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromBody]ProductCreateForm p,[FromRoute]int id)
        {
            if(!ModelState.IsValid) return BadRequest();
            try
            {
                Product product = p.ToDOMAIN();
                product.ProductID = id;
                _productService.Update(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
