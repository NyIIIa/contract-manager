using ContractManager.Domain.Common;
using ContractManager.Domain.EquipmentPlacementContracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContractManager.Infrastructure.Common.Persistence;

public class AppDbContext(DbContextOptions options, IPublisher publisher) : DbContext(options)
{
    public virtual DbSet<ProductionFacility> ProductionFacilities { get; set; } = null!;

    public virtual DbSet<Equipment> Equipments { get; set; } = null!;

    public virtual DbSet<EquipmentPlacementContract> EquipmentPlacementContracts { get; set; } = null!;
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = ChangeTracker.Entries<Entity>()
            .SelectMany(entry => entry.Entity.PopDomainEvents())
            .ToList();
        
        await PublishDomainEvents(domainEvents);
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
    
    private async Task PublishDomainEvents(List<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }
    }
}