using ContractManager.Infrastructure.Common.Middleware;

namespace ContractManager.Infrastructure
{
    public static class RequestPipeline
    {
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseMiddleware<EventualConsistencyMiddleware>();
            app.UseMiddleware<ApiKeyAuthenticationMiddleware>();
            return app;
        }
    }
}