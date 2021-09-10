using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Application.Errors;

namespace API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private RequestDelegate Next { get; }
        private readonly ILogger<ErrorHandlingMiddleware> Logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            Next = next;
            Logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                //terminamos nuestra logica y llamamos al siguiente middleware
                await Next( context );
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync( context, ex, Logger );
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<ErrorHandlingMiddleware> logger)
        {

            object errors = null;

            switch (ex)
            {
                case RestException re:
                    Logger.LogError( ex, "REST ERROR" );
                    errors = re.Errors;
                    context.Response.StatusCode = (int)re.Code;
                    break;
                case Exception e:
                    Logger.LogError( ex, "SERVER ERROR" );
                    errors = string.IsNullOrEmpty( e.Message ) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";

            if( errors != null )
            {
                var result = JsonSerializer.Serialize( new { errors } );
                await context.Response.WriteAsync( result );

            }
            
        }
    }
}