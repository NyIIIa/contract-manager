using ContractManager.Domain.Common;

namespace ContractManager.Domain.Contracts;

public class ProductionFacility : Entity
{
    public int Code { get; set; }

    public string Name { get; set; } = null!;

    public double StandardArea { get; set; }

    public List<EquipmentPlacementContract> Contracts { get; set; } = [];
}