using ErrorOr;

namespace ContractManager.Domain.EquipmentPlacementContracts.Errors
{
    public static class ProductionFacilityErrors
    {
        public static Error CodeNotFound { get; } = Error.Validation(
            code: "ProductionFacility.CodeNotFound",
            description: "Cannot find the specified production facility's code!");

        public static Error DeficientArea { get; } = Error.Validation(
            code: "ProductionFacility.DeficientArea",
            description: "The available area in the production facility is insufficient " +
                         "to accommodate the specified equipment in the required quantity!");
    }
}