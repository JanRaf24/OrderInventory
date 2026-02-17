using FluentValidation;
using OrderInventory.DTOs;

namespace OrderInventory.Validations
{
    public class InventoryRequestValidator : AbstractValidator<InventoryRequest>
    {
        public InventoryRequestValidator()
        {
            RuleFor(x => x.Sku).NotEmpty().WithMessage("SKU is required.");
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantity must be zero or greater.");
        }
    }
}
