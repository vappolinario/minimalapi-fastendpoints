using FastEndpoints;
using FluentValidation;

public class CreateOrderValidator : Validator<CreateOrderRequestDto>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.TotalCost).GreaterThanOrEqualTo(0);
    }
}
