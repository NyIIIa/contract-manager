using ErrorOr;
using MediatR;

namespace ContractManager.Application.EquipmentPlacementContracts.Commands.Create
{
    public record CreateEquipmentPlacementContractCommand(
        string ProductionFacilityCode,
        string EquipmentCode,
        int EquipmentQuantity) : IRequest<ErrorOr<Success>>;
}