namespace BookStore2.MiddleWare
{
    public class CustomMiddlewareErrorHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddlewareErrorHandler> _logger;

        public CustomMiddlewareErrorHandler(RequestDelegate next, ILogger<CustomMiddlewareErrorHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogError($"Error in {context.Request.Method}");
            await _next(context);
        }
    }
}
