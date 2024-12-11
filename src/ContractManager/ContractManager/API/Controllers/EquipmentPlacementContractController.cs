using ContractManager.API.Contracts;
using ContractManager.Application.EquipmentPlacementContracts.Commands.Create;
using ContractManager.Application.EquipmentPlacementContracts.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContractManager.API.Controllers
{
    [Route("api/equipmentPlacementContracts")]
    public class EquipmentPlacementContractController(ISender sender) : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateEquipmentPlacementContractAsync(CreateEquipmentPlacementContractRequest request)
        {
            var command = new CreateEquipmentPlacementContractCommand(
                request.ProductionFacilityCode, 
                request.EquipmentCode, 
                request.EquipmentQuantity);

            var result = await sender.Send(command);
            
            return result.Match(
                _ => Created(), 
                Problem);
        }

        [HttpGet]
        public async Task<IActionResult> GetEquipmentPlacementContractsAsync()
        {
            var query = new GetEquipmentPlacementContractListQuery();

            var result = await sender.Send(query);
            
            return result.Match(
                equipmentPlacementsContracts => Ok(equipmentPlacementsContracts),
                Problem);
        }
    }
}