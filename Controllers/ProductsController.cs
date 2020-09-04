using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ActionResult.Models;

namespace ActionResult.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new List<Product>()
        {
            new Product
            {
                Id=1006368,
                Name= "Austin",
                Description= "Thermometer",
                Price= 399

            },
             new Product
            {
                Id=1006368,
                Name= "Austin",
                Description= "Thermometer",
                Price= 399

            },
              new Product
            {
                Id=1006368,
                Name= "Austin",
                Description= "Thermometer",
                Price= 399

            }
        };
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return products;
        }
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = products.Find(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
        [HttpPost]
        public ActionResult Post([FromBody] Product product)
        {
            if (products.Exists(p => p.Id == product.Id))
            {
                return Conflict();
            }
            products.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, products);
        }
        [HttpDelete("{id}")]
        public ActionResult<IEnumerable<Product>> Delete(int id)
        {
            var product = products.Where(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            products = products.Except(product).ToList();
            return products;
        }
        [HttpPut("{id}")]
        public ActionResult<IEnumerable<Product>> Put(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            var existingProduct = products.Where(p => p.Id == id);
            products = products.Except(existingProduct).ToList();
            products.Add(product);
            return products;
        }
    }
}
