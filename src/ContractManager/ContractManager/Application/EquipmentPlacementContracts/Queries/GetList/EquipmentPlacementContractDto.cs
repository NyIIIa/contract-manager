namespace ContractManager.Application.EquipmentPlacementContracts.Queries.GetList
{
    public record EquipmentPlacementContractDto(
        string ProductionFacilityName,
        string EquipmentName, 
        int EquipmentQuantity);
}