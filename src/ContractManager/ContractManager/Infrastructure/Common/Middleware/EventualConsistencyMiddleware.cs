using ContractManager.Domain.Common;
using ContractManager.Infrastructure.Common.Persistence;
using MediatR;

namespace ContractManager.Infrastructure.Common.Middleware
{
    public class EventualConsistencyMiddleware(RequestDelegate _next)
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
                catch (Exception)
                {
                    //logging
                }
                finally
                {
                    await transaction.DisposeAsync();
                }
            });

            await _next(context);
        }
    }
}