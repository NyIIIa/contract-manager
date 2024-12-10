using ContractManager.Infrastructure.Common.Persistence;
using MediatR;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace ContractManager.Application.EquipmentPlacementContracts.Queries.GetList
{
    public class GetEquipmentPlacementContractListQueryHandler(AppDbContext dbContext)
        : IRequestHandler<GetEquipmentPlacementContractListQuery, ErrorOr<List<EquipmentPlacementContractDto>>>
    {
        public async Task<ErrorOr<List<EquipmentPlacementContractDto>>> Handle(GetEquipmentPlacementContractListQuery request, CancellationToken cancellationToken)
        {
            var result = await dbContext.EquipmentPlacementContracts
                .Select(c => new EquipmentPlacementContractDto(
                    c.ProductionFacility!.Name, 
                    c.Equipment!.Name, 
                    c.NumberOfEquipmentUnits))
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}