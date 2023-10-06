using API.Errors;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration config )
        {


       services.AddDbContext<AppIdentityDbContext>(opt => opt.UseSqlite(config.GetConnectionString("IdentityConnection")));
      
      services.AddIdentityCore<AppUser>(opt => 
         {
           ////add identity options here
         })
         .AddEntityFrameworkStores<AppIdentityDbContext>()
         .AddSignInManager<SignInManager<AppUser>>();
         
         services.AddAuthentication();
         services.AddAuthorization();

         return services;


        }
  }
}