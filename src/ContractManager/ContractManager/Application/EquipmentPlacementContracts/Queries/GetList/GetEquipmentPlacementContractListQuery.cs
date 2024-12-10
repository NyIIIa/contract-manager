using ErrorOr;
using MediatR;

namespace ContractManager.Application.EquipmentPlacementContracts.Queries.GetList
{
    public class GetEquipmentPlacementContractListQuery : IRequest<ErrorOr<List<EquipmentPlacementContractDto>>>;
}