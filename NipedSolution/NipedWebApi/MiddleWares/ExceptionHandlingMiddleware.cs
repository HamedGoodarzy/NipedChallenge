
namespace NipedWebApi.MiddleWares
{
    public class ExceptionHandlingMiddleware : IExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                var response = context.Response;
                response.ContentType = "application/json";

                var (statusCode, message) = MapException(ex);

                response.StatusCode = statusCode;

                var problem = new
                {
                    Status = statusCode,
                    Message = message
                };

                await response.WriteAsJsonAsync(problem);
            }
        }

        private (int statusCode, string message) MapException(Exception ex)
        {
            return ex switch
            {
                ArgumentNullException => (StatusCodes.Status400BadRequest, "A required argument was missing."),
                ArgumentException => (StatusCodes.Status400BadRequest, ex.Message),
                KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource not found."),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized access."),
                InvalidOperationException => (StatusCodes.Status409Conflict, "Invalid operation."),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
            };
        }
    }
    public interface IExceptionHandlingMiddleware
    {
        public Task Invoke(HttpContext context);
    }
}
