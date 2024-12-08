using ContractManager.Domain.Common;

namespace ContractManager.Domain.Contracts;

public class Equipment : Entity
{
    public int Code { get; set; }

    public string Name { get; set; } = null!;

    public double Area { get; set; }
    
    public List<EquipmentPlacementContract> Contracts { get; set; } = [];
}