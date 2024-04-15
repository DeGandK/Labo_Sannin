using Labo_BLL.Interfaces;
using Labo_Sannin_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labo_Sannin_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService
            _productService;
        public ProductController(IProductService productService)

        {
            _productService = productService;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            return Ok(_productService.GetAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetById(int id)
        {
            return Ok(_productService.GetById(id));
        }

        public IActionResult Create(ProductCreateForm form)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _productService.Create(form.ToDAL());
            return Ok();
        }



    }
}
