using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using System.Threading.Tasks;

namespace SpermCatalog.API.MiddleWares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private ILogger<ExceptionHandlerMiddleWare> _logger;
        public ExceptionHandlerMiddleWare(RequestDelegate next, ILogger<ExceptionHandlerMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await GetExceptionData(httpContext, ex);
            }
        }


        private Task GetExceptionData(HttpContext httpContext,Exception ex)
        {
            httpContext.Response.ContentType = "Application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new ExceptionModel
            {
                Message = ex.Message,
                StatusCode = httpContext.Response.StatusCode,
                Datas = ex.Data,
            }));
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlerMiddleWareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleWare>();
        }
    }
}
