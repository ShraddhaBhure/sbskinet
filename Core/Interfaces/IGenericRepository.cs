using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using Core.Specifications;
using Core.Entities;



namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T :BaseEntity
    {
       Task<T> GetByIdAsync(int id);
       Task<IReadOnlyList<T>> ListAllAsync(); 
       Task<T> GetEntityWithSpec(ISpecification<T> spec); 
       Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

      Task<int> CountAsync(ISpecification<T> spec);
    }
}