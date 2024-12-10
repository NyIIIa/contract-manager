
namespace ContractManager.Infrastructure.Common.Middleware
{
    public class ApiKeyAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        private const string ApiKeyHeaderName = "X-Api-Key";
        
        public async Task InvokeAsync(HttpContext context)
        {
            var apiKey = context.Request.Headers[ApiKeyHeaderName];

            if (!IsApiKeyValid(apiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid api-key!");
                return;
            }
            
            await next.Invoke(context);
        }

        private bool IsApiKeyValid(string? apiKey)
        {
            return !string.IsNullOrEmpty(apiKey) && configuration["Authentication" + ":" + ApiKeyHeaderName] == apiKey;
        }
    }
}