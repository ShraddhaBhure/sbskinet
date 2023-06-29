using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Add this using directive

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _context.Product.ToListAsync();
            return products;
          
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) // Change the return type to ActionResult<Product>
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
                return NotFound();

            return product;
        }
    }
}
