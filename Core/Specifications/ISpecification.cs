using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;
using System;
using System.Linq.Expressions;


namespace Core.Specifications
{
    public interface ISpecification<T>
    {
      Expression<Func<T, bool>> Criteria { get; }
     
      List<Expression<Func<T, object>>> Includes { get; }
   
    }
}