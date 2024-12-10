using ContractManager.Domain.Common;

namespace ContractManager.Domain.EquipmentPlacementContracts.Events
{
    public record EquipmentPlacementContractCreatedEvent(
        Guid ContractId, 
        Guid ProductionFacilityId, 
        Guid EquipmentId) : IDomainEvent;
}