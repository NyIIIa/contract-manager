using ContractManager.Domain.EquipmentPlacementContracts.Errors;
using ContractManager.Domain.EquipmentPlacementContracts.Events;
using ContractManager.Infrastructure.Common.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContractManager.Application.EquipmentPlacementContracts.Events
{
    public class EquipmentPlacementContractCreatedEventHandler(AppDbContext dbContext)
        : INotificationHandler<EquipmentPlacementContractCreatedEvent>
    {
        public async Task Handle(EquipmentPlacementContractCreatedEvent notification, CancellationToken cancellationToken)
        {
            var contractInfo = await dbContext.EquipmentPlacementContracts
                .Where(c => c.Id == notification.ContractId)
                .Select(c => new
                {
                    ProdcutionFacilityName = c.ProductionFacility!.Name,
                    EquipmentName = c.Equipment!.Name,
                    c.NumberOfEquipmentUnits
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (contractInfo is null)
            {
                Console.WriteLine(EquipmentPlacementContractErrors.IdNotFound.Description);
                return;
            }

            Console.WriteLine(contractInfo);
        }
    }
}