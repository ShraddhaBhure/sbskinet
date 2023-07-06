using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Products>
    {
     public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
     :base(x =>
         (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains
         (productParams.Search)) &&
         (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
         (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
         )
      {
             AddInclude(x=> x.ProductBrand);
             AddInclude(x=> x.ProductType);
             AddOrderBy(x => x.Name);
             ApplyPaging(productParams.PageSize * (productParams.PageIndex -1),
             productParams.PageSize);

             if(!string.IsNullOrEmpty(productParams.Sort))
             {
              switch(productParams.Sort)
               {
                  case "priceAsc":
                     AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                       break;
                     default:
                        AddOrderBy(n =>n.Name);
                        break;
               }
             }
     }

     public ProductsWithTypesAndBrandsSpecification(int id)
       : base(x => x.Id == id)
     {
            AddInclude(x=> x.ProductBrand);
            AddInclude(x=> x.ProductType);
     }
  }

}