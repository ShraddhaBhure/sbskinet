using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Core.Interfaces
{
    public interface IProductRepository 
    {
       Task<Products> GetProductByIdAsync(int id);
       
       Task<IReadOnlyList<Products>> GetProductAsync();
      
      Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync();
       
       Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
   
    }
}