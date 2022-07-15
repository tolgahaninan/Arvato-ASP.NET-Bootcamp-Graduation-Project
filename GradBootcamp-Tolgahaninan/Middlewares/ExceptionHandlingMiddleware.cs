using System.Net;
using System.Text.Json;
using GradBootcamp_Tolgahaninan.Models;

namespace GradBootcamp_Tolgahaninan.Middlewares
{
    public class ExceptionHandlingMiddleware // Middleware for exception handling
    {
        private readonly RequestDelegate _next;  // To create instance Of Request Delegate
        private readonly ILogger<ExceptionHandlingMiddleware> _logger; // To create instance Of logger
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger) // Constructor for dependency injection
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext) // This method works as middleware If there is any exception calls HandleExceptionAsync method If there is not continius with next request
        {
            try
            {
                await _next(httpContext); // If there is no exception
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex); // If there is exception
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception) // To handle Exceptions and return responses for specific kind of exceptions
        {
            context.Response.ContentType = "application/json"; // To specify response type of HTTP Context
            var response = context.Response;

            var errorResponse = new ErrorResponse // To create new error respınse
            {
                Success = false
            };
            switch (exception) // Switch case mechanism for different kind of exceptions
            {
                case ApplicationException ex:
                    if (ex.Message.Contains("Invalid token")) // 401 - 403 Errors
                    {
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        errorResponse.Message = ex.Message;
                        errorResponse.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    }
                    response.StatusCode = (int)HttpStatusCode.BadRequest; // 400 Error
                    errorResponse.Message = ex.Message;
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException ex:
                    response.StatusCode = (int)HttpStatusCode.NotFound; // 404 Error
                    errorResponse.Message = ex.Message;
                    errorResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500 Error
                    errorResponse.Message = "Internal Server errors. Check Logs!";
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            _logger.LogError(exception.Message);
            var result = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(result);
        }
    }
}