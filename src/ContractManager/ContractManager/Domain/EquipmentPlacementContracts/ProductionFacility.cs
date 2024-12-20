using ContractManager.Domain.Common;

namespace ContractManager.Domain.EquipmentPlacementContracts;

public class ProductionFacility : Entity
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public double StandardArea { get; set; }

    public List<EquipmentPlacementContract> Contracts { get; set; } = [];

    private ProductionFacility() { }

    public static ProductionFacility Create(string code, string name, double standardArea)
    {
        return new ProductionFacility
        {
            Code = code,
            Name = name,
            StandardArea = standardArea
        };
    }

    public bool HasEnoughArea(double equipmentArea, int quantity)
    {
        return equipmentArea * quantity <= StandardArea;
    }
}