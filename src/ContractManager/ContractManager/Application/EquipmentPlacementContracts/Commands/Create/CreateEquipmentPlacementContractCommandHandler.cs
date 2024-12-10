using ContractManager.Domain.EquipmentPlacementContracts;
using ContractManager.Domain.EquipmentPlacementContracts.Errors;
using ContractManager.Infrastructure.Common.Persistence;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContractManager.Application.EquipmentPlacementContracts.Commands.Create
{
    public class CreateEquipmentPlacementContractCommandHandler(AppDbContext dbContext)
        : IRequestHandler<CreateEquipmentPlacementContractCommand, ErrorOr<Success>>
    {
        public async Task<ErrorOr<Success>> Handle(CreateEquipmentPlacementContractCommand request, CancellationToken cancellationToken)
        {
            var productionFacility = await dbContext.ProductionFacilities
                .FirstOrDefaultAsync(pf => pf.Code.Trim().ToLower() == request.ProductionFacilityCode.Trim().ToLower(), 
                    cancellationToken);
            
            if (productionFacility is null)
            {
                return ProductionFacilityErrors.CodeNotFound;
            }
            
            var equipment = await dbContext.Equipments
                .FirstOrDefaultAsync(e => e.Code.Trim().ToLower() == request.EquipmentCode.Trim().ToLower(),
                    cancellationToken);
            
            if (equipment is null)
            {
                return EquipmentErrors.CodeNotFound;
            }
            
            if (!productionFacility.HasEnoughArea(equipment.Area, request.EquipmentQuantity))
            {
                return ProductionFacilityErrors.DeficientArea;
            }
            
            var equipmentPlacementContract = EquipmentPlacementContract.Create(
                    productionFacility.Id, 
                    equipment.Id, 
                    request.EquipmentQuantity);
            
            dbContext.EquipmentPlacementContracts.Add(equipmentPlacementContract);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}