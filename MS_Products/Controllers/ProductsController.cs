using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS_Products.Models;
using MS_Products.Repository;

namespace MS_Products.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ProductsController : ODataController
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [EnableQuery]
        //[HttpGet]
        public IActionResult Get()
        {
            var products = _productRepository.GetProducts();
            return new OkObjectResult(products);
        }

        [EnableQuery]
        //[HttpGet("{id}", Name = "Get")]
        public IActionResult Get2(int id)
        {
            var a = this.Request;
            var product = _productRepository.GetProductByID(id);
            return new OkObjectResult(product);
        }

        [EnableQuery]
        public IActionResult GetWhere([FromBody] Product product)
        {
            return new OkObjectResult(product);
        }

        [EnableQuery]
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            using (var scope = new TransactionScope())
            {
                _productRepository.InsertProduct(product);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
            }
        }

        [EnableQuery]
        [HttpPut]
        public IActionResult Put([FromBody] Product product)
        {
            if (product != null)
            {
                using (var scope = new TransactionScope())
                {
                    _productRepository.UpdateProduct(product);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [EnableQuery]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productRepository.DeleteProduct(id);
            return new OkResult();
        }
    }
}
