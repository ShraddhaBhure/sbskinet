using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContext :DbContext
    {

      public StoreContext(DbContextOptions<StoreContext> options) 
      : base(options)
      {
      }


      public DbSet<Product> Product { get; set; }


     } 
}