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
using API.Errors;
using API.Helpers;

namespace API.Controllers
{

    public class ProductsController :BaseApiController
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
           _mapper = mapper;
           _productsRepo = productsRepo;
           _productBrandRepo = productBrandRepo;
           _productTypeRepo = productTypeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
           [FromQuery]ProductSpecParams productParams)
        {
           var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

           var countSpec = new ProductWithFiltersForCountSpecification(productParams);

           var totalItems = await _productsRepo.CountAsync(countSpec);

           var products = await _productsRepo.ListAsync(spec);

            var data = _mapper
           .Map<IReadOnlyList<Products>, IReadOnlyList<ProductToReturnDto>>(products);

           return Ok( new Pagination<ProductToReturnDto>(productParams.PageIndex,
           productParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
           var spec = new ProductsWithTypesAndBrandsSpecification(id);

           var product = await _productsRepo.GetEntityWithSpec(spec);
             if(product == null) return NotFound(new ApiResponse(404));
           return _mapper.Map<Products,ProductToReturnDto>(product);

        }



         [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
         return Ok(await _productBrandRepo.ListAllAsync());

        }
          [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
        
           return Ok(await _productTypeRepo.ListAllAsync());
        }
    
    }
}
