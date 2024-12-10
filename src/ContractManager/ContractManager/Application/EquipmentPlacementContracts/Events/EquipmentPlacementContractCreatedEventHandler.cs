using ContractManager.Domain.EquipmentPlacementContracts.Errors;
using ContractManager.Domain.EquipmentPlacementContracts.Events;
using ContractManager.Infrastructure.Common.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ILogger = Serilog.ILogger;

namespace ContractManager.Application.EquipmentPlacementContracts.Events
{
    public class EquipmentPlacementContractCreatedEventHandler(AppDbContext dbContext, ILogger logger)
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
                logger.Information(EquipmentPlacementContractErrors.IdNotFound.Description);
                return;
            }

            logger.Information($"Successfully created contract - " +
                               $"Production facility: {contractInfo.ProdcutionFacilityName} ||" +
                               $"Equipment: {contractInfo.EquipmentName} ||" +
                               $"NumberOfEquipmentUnits:  {contractInfo.NumberOfEquipmentUnits}");
        }
    }
}