using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;



namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository 
    {  
       private readonly StoreContext _context;


       public ProductRepository(StoreContext context)
       {
           _context = context;
       }

       public async Task<Products> GetProductByIdAsync(int id)
       {
         return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .FirstOrDefaultAsync(p => p.Id == id);
       }

      public async Task<IReadOnlyList<Products>> GetProductAsync()
       {

      //  var typeId=1;

       // var products=_context.Products.where(x=>x.ProductTypeId == typeId).Include(x=>x.ProductType).ToListAsync();

        return await _context.Products
            .Include(p => p.ProductType)
            .Include(p => p.ProductBrand)
            .ToListAsync();
       }

      public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
       {
         return await _context.ProductBrands.ToListAsync();
       }

      public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
       {
         return await _context.ProductTypes.ToListAsync();
       }



    }
}