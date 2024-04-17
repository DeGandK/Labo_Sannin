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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProductCreateForm),StatusCodes.Status400BadRequest)]
        public IActionResult Create(ProductCreateForm form)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _productService.Create(form.ToDOMAIN());
            return Ok();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Delete (int id)
        {
            _productService.Delete(id);
            return Ok();
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ProductCreateForm)),
        //    StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromBody]ProductCreateForm p,[FromRoute]int id)
        {
            if(!ModelState.IsValid) return BadRequest();
            Product product = p.ToDOMAIN();
            product.ProductID = id;
            _productService.Update(product);

            return Ok();
        }







    }
}
