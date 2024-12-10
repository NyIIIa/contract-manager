using FluentValidation;

namespace ContractManager.Application.EquipmentPlacementContracts.Commands.Create
{
    public class CreateEquipmentPlacementContractCommandValidator : AbstractValidator<CreateEquipmentPlacementContractCommand>
    {
        public CreateEquipmentPlacementContractCommandValidator()
        {
            RuleFor(c => c.ProductionFacilityCode)
                .NotEmpty().WithMessage("The production facility's code shouldn't be empty.");
            
            RuleFor(c => c.EquipmentCode)
                .NotEmpty().WithMessage("The equipment's code shouldn't be empty.");
            
            RuleFor(c => c.EquipmentQuantity)
                .Must(quantity => quantity > 0).WithMessage("The equipment's quantity should be greater than 0.");
        }
    }
}