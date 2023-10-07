using System.Text;
using API.Errors;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
          
         services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme )
         .AddJwtBearer(options =>
         {
            options.TokenValidationParameters = new  TokenValidationParameters
            {
                  ValidateIssuerSigningKey=true,
                  IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                  ValidIssuer = config["Token:Issuer"],
                  ValidateIssuer = true
            };
         });

         services.AddAuthorization();

         return services;


        }
  }
}