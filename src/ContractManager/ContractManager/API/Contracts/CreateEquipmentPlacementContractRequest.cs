namespace ContractManager.API.Contracts
{
    public record CreateEquipmentPlacementContractRequest(
        string ProductionFacilityCode,
        string EquipmentCode,
        int EquipmentQuantity);
}