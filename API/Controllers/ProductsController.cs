using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Add this using directive
using Core.Interfaces; 
using Core.Specifications;
using API.Dtos;
using AutoMapper;

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
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Products> productsRepo,
        IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType>
       productTypeRepo, IMapper mapper )
        { 
           //  _repo = repo;
           // _context = context;
           _mapper = mapper;
           _productsRepo = productsRepo;
           _productBrandRepo = productBrandRepo;
           _productTypeRepo = productTypeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
       
           var spec = new ProductsWithTypesAndBrandsSpecification();
           var products = await _productsRepo.ListAsync(spec);
           return Ok(_mapper
           .Map<IReadOnlyList<Products>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id) 
        {
           var spec = new ProductsWithTypesAndBrandsSpecification(id); 

           var product = await _productsRepo.GetEntityWithSpec(spec);

           return _mapper.Map<Products,ProductToReturnDto>(product);
          //  return new ProductToReturnDto
         //   {
           //   Id = product.Id,
           //   Name = product.Name,
           //   Description = product.Description,
           //   PictureUrl = product.PictureUrl,
            //  Price = product.Price,
            //  ProductBrand  = product.ProductBrand.Name,
            //  ProductType = product.ProductType.Name
        //   };
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
