using System.Net;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly  IHostEnvironment _env;
    //  public JsonNamingPolicy PropertyNamingPolicy { get; private set; }
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
        IHostEnvironment env)
        {
             _logger = logger;
             _next = next;
             _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try 
            {
                await _next(context);
            }
              catch(Exception  ex)
              {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

              var response = _env.IsDevelopment()
              ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
              : new ApiException((int)HttpStatusCode.InternalServerError);

             var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
             var json = JsonSerializer.Serialize(response, options);

             await context.Response.WriteAsync(json);

            }



        }
    }
}