using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace MiddlewareDemo.Web.Middlewares
{
    public class GlobalExeptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExeptionHandlingMiddleware> _logger;

        public GlobalExeptionHandlingMiddleware(ILogger<GlobalExeptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server Error",
                    Title = "Server Error",
                    Detail= "An internal server error has occurred"
                };

                string json = JsonSerializer.Serialize(problem);
                await context.Response.WriteAsync(json);
                context.Response.ContentType= "application/json";
            }
        }
    }
}
