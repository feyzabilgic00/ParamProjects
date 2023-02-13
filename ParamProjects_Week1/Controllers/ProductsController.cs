using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ParamProjects_Week1.Models;

namespace ParamProjects_Week1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new List<Product>
            {
                new Product { Id = 1, Name = "Monitör", Price = 10000, Stock = 2 },
                new Product { Id = 2, Name = "Laptop", Price = 20000, Stock = 1 },
                new Product { Id = 3, Name = "Mouse", Price = 1000, Stock = 1 },
            };

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return products;
        }

        // GET: api/Products/3
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

        // POST: api/Products
        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product product)
        {
            var maxId = products.Max(p => p.Id);
            product.Id = maxId + 1;
            if (product.Name != "string" && product.Price > 0 && product.Stock > 0)
            {
                products.Add(product);
                return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
            }

            return BadRequest();
        }
        // PUT: api/Products/3
        [HttpPut("{id}")]
        public ActionResult<Product> Put(int id, [FromBody] Product product)
        {
            var currentProduct = products.Find(p => p.Id == id);
            if (currentProduct == null)
            {
                return NotFound();
            }

            currentProduct.Name = product.Name;
            currentProduct.Price = product.Price;
            currentProduct.Stock = product.Stock;

            return currentProduct;
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            var currentProduct = products.Find(p => p.Id == id);
            if (currentProduct == null)
            {
                return NotFound();
            }

            products.Remove(currentProduct);

            return currentProduct;
        }

        //PATCH: api/Products/3
        [HttpPatch("{id}")]
        public ActionResult<Product> Patch(int id, [FromBody] JsonPatchDocument<Product> patchDocument)
        {
            var currentProduct = products.Find(p => p.Id == id);
            if (currentProduct == null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(currentProduct);

            return currentProduct;
        }

        [HttpGet("SortOrderByProductName")]
        public IActionResult SortOrderByProductName([FromQuery] string name)
        {
            List<Product> result = products.OrderBy(p => p.Name).ToList();

            if (!string.IsNullOrEmpty(name))
            {
                result = result.Where(p => p.Name.ToUpper().Contains(name.ToUpper())).ToList();
            }

            return Ok(result);
        }
        [HttpGet("SortOrderByDescendingPrice")]
        public IActionResult SortOrderByDescendingPrice()
        {
            List<Product> result = products.OrderByDescending(p => p.Price).ToList();

            return Ok(result);
        }
    }
}
