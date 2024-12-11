using ContractManager.Domain.Common;
using ContractManager.Infrastructure.Common.Persistence;
using MediatR;
using ILogger = Serilog.ILogger;

namespace ContractManager.Infrastructure.Common.Middleware
{
    public class EventualConsistencyMiddleware(RequestDelegate next, ILogger logger)
    {
        public const string DomainEventsKey = "DomainEventsKey";

        public async Task InvokeAsync(HttpContext context, IPublisher publisher, AppDbContext dbContext)
        {
            var transaction = await dbContext.Database.BeginTransactionAsync();
            context.Response.OnCompleted(async () =>
            {
                try
                {
                    if (context.Items.TryGetValue(DomainEventsKey, out var value) && value is Queue<IDomainEvent> domainEvents)
                    {
                        while (domainEvents.TryDequeue(out var nextEvent))
                        {
                            await publisher.Publish(nextEvent);
                        }
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                { 
                    logger.Error(ex.Message);
                }
                finally
                {
                    await transaction.DisposeAsync();
                }
            });

            await next(context);
        }
    }
}