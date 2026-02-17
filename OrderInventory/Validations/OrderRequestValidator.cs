using FluentValidation;
using OrderInventory.DTOs;

namespace OrderInventory.Validations
{

    public class OrderRequestValidator : AbstractValidator<OrderRequest>
    {
        public OrderRequestValidator()
        {
            RuleFor(x => x.Sku).NotEmpty().WithMessage("SKU is required.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }
}
