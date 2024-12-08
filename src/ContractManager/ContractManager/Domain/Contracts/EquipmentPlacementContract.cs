using ContractManager.Domain.Common;

namespace ContractManager.Domain.Contracts;

public class EquipmentPlacementContract : Entity
{
    public Guid ProductionFacilityId { get; set; }

    public ProductionFacility? ProductionFacility { get; set; }

    public Guid EquipmentId { get; set; }

    public Equipment? Equipment { get; set; }

    public int NumberOfEquipmentUnits { get; set; }
}