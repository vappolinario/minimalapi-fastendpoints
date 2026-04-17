using FastEndpoints;
using FluentValidation;
using OrdersAPI.Dtos;

namespace OrdersAPI.Validators
{
    public class CreateOrderValidator : Validator<CreateOrderRequest>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.TotalCost).GreaterThanOrEqualTo(0);
        }
    }
}
