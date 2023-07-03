using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Products>
    {
     public ProductsWithTypesAndBrandsSpecification()
     {
             AddInclude(x=> x.ProductBrand);
             AddInclude(x=> x.ProductType);
     }

     public ProductsWithTypesAndBrandsSpecification(int id) 
       : base(x => x.Id == id)
     {
            AddInclude(x=> x.ProductBrand);
            AddInclude(x=> x.ProductType);
     }  
  
  }

}