using ContractManager.Domain.Common;
using ContractManager.Domain.EquipmentPlacementContracts.Events;

namespace ContractManager.Domain.EquipmentPlacementContracts;

public class EquipmentPlacementContract : Entity
{
    public Guid ProductionFacilityId { get; set; }

    public ProductionFacility? ProductionFacility { get; set; }

    public Guid EquipmentId { get; set; }

    public Equipment? Equipment { get; set; }

    public int NumberOfEquipmentUnits { get; set; }

    private EquipmentPlacementContract(){}

    public static EquipmentPlacementContract Create(Guid productionFacilityId, Guid equipmentId, int numberOfEquipmentUnits)
    {
        var contract =  new EquipmentPlacementContract
        {
            ProductionFacilityId = productionFacilityId,
            EquipmentId = equipmentId,
            NumberOfEquipmentUnits = numberOfEquipmentUnits
        };
        
        contract._domainEvents.Add(new EquipmentPlacementContractCreatedEvent(contract.Id, productionFacilityId, equipmentId));

        return contract;
    }
}