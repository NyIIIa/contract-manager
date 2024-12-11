using ErrorOr;

namespace ContractManager.Domain.EquipmentPlacementContracts.Errors
{
    public static class EquipmentErrors
    {
        public static Error CodeNotFound { get; } = Error.Validation(
            code: "Equipment.CodeNotFound",
            description: "Cannot find the specified equipment's code!");
    }
}