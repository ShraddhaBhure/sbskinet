using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Add this using directive
using Core.Interfaces; 
using Core.Specifications;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly  IProductRepository _repo;

       private readonly  IGenericRepository<Products> _productsRepo;
       private readonly  IGenericRepository<ProductBrand> _productBrandRepo;
       private readonly  IGenericRepository<ProductType> _productTypeRepo;
        
        public ProductsController(IGenericRepository<Products> productsRepo,
        IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo)
        { 
           //  _repo = repo;
           // _context = context;

           _productsRepo = productsRepo;
           _productBrandRepo = productBrandRepo;
           _productTypeRepo = productTypeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Products>>> GetProducts()
        {
           // var products = await _repo.GetProductAsync();
           var spec = new ProductsWithTypesAndBrandsSpecification();

           var products = await _productsRepo.ListAsync(spec);
            return Ok(products) ;
          
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProduct(int id) // Change the return type to ActionResult<Product>
// dotnet add package Serilog.AspNetCore
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    // dotnet add package Serilog.Sinks.File
    // .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("Starting web application");

    builder.Host.UseSerilog(); // TODO: Move this line to after 'var builder = ...' line
            
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

{
           //   return await _repo.GetProductByIdAsync(id);;
           
           var spec = new ProductsWithTypesAndBrandsSpecification(id); 
            return await  _productsRepo.GetEntityWithSpec(spec);
        }



         [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
         //   return Ok(await _repo.GetProductBrandAsync());

            return Ok(await _productBrandRepo.ListAllAsync());
          
        }
          [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
         //   return Ok(await _repo.GetProductTypesAsync());
           return Ok(await _productTypeRepo.ListAllAsync());
        }
        
    }
}
