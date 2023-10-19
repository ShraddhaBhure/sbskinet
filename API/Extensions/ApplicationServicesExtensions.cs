using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config )
        {

         services.AddDbContext<StoreContext>(opt => opt.UseSqlite(config.GetConnectionString("DefaultConnection")));
         //-------------added by myself--------
         services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("127.0.0.1:6379")); //////----added by myself---
         
          services.AddScoped<IBasketRepository, BasketRepository>();
          services.AddScoped<IProductRepository, ProductRepository>();
          services.AddScoped<ITokenService,TokenService>();
          services.AddScoped<IOrderService,OrderService>();
          services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
          services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
          services.Configure<ApiBehaviorOptions>(options =>
           {
              options.InvalidModelStateResponseFactory = actionContext =>
              {
                var errors = actionContext.ModelState
                .Where(e => e.Value.Errors.Count > 0)
                .SelectMany(x => x.Value.Errors)
                .Select(x => x.ErrorMessage).ToArray();

                 var errorResponse = new AppValidationErrorResponse
                 {
                      Errors = errors
                 };

                return new BadRequestObjectResult(errorResponse);
             };
            });

            services.AddCors(opt =>
            {
                 opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
           });

          return services;
        }
    }
}