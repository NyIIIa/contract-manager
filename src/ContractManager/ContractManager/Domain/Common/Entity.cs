namespace ContractManager.Domain.Common;

public abstract class Entity
{
    protected readonly List<IDomainEvent> _domainEvents = [];
    
    public Guid Id { get; private init; }

    protected Entity()
    {   
        Id = Guid.NewGuid();
    }
    
    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        _domainEvents.Clear();

        return copy;
    }
}