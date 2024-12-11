using ContractManager.Domain.Common;
using ContractManager.Domain.EquipmentPlacementContracts;
using ContractManager.Infrastructure.Common.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContractManager.Infrastructure.Common.Persistence;

public class AppDbContext(DbContextOptions options, IPublisher publisher, IHttpContextAccessor httpContextAccessor) : DbContext(options)
{
    public virtual DbSet<ProductionFacility> ProductionFacilities { get; set; } = null!;

    public virtual DbSet<Equipment> Equipments { get; set; } = null!;

    public virtual DbSet<EquipmentPlacementContract> EquipmentPlacementContracts { get; set; } = null!;
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = ChangeTracker.Entries<Entity>()
            .SelectMany(entry => entry.Entity.PopDomainEvents())
            .ToList();
        
        if (IsUserWaitingOnline())
        {
            AddDomainEventsToOfflineProcessingQueue(domainEvents);
            return await base.SaveChangesAsync(cancellationToken);
        }
        
        await PublishDomainEvents(domainEvents);
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
    
    private bool IsUserWaitingOnline() => httpContextAccessor.HttpContext is not null;
    
    private async Task PublishDomainEvents(List<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }
    }

    private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
    {
        Queue<IDomainEvent> domainEventsQueue =
            httpContextAccessor.HttpContext!.Items.TryGetValue(EventualConsistencyMiddleware.DomainEventsKey, out var value)
            && value is Queue<IDomainEvent> existingDomainEvents
                ? existingDomainEvents
                : new();

        domainEvents.ForEach(domainEventsQueue.Enqueue);
        httpContextAccessor.HttpContext.Items[EventualConsistencyMiddleware.DomainEventsKey] = domainEventsQueue;
    }
}