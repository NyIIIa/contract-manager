using ErrorOr;

namespace ContractManager.Domain.EquipmentPlacementContracts.Errors
{
    public static class EquipmentPlacementContractErrors
    {
        public static Error IdNotFound { get; } = Error.Validation(
            code: "EquipmentPlacementContract.IdNotFound",
            description: "Cannot find a contract with the specified id!");
    }
}