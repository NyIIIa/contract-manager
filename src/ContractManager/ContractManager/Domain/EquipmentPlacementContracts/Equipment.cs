using ContractManager.Domain.Common;

namespace ContractManager.Domain.EquipmentPlacementContracts;

public class Equipment : Entity
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public double Area { get; set; }
    
    public List<EquipmentPlacementContract> Contracts { get; set; } = [];

    private Equipment() {}
    
    public static Equipment Create(string code, string name, double area)
    {
        return new Equipment
        {
            Code = code,
            Name = name,
            Area = area
        };
    }
}