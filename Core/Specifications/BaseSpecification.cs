using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;


namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public  BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
           Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria {get; }

        public List<Expression<Func<T, object>>> Includes {get; } =
            new List<Expression <Func<T, object>>>();
        
        
        
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
         Includes.Add(includeExpression);
        }
        
    }
}