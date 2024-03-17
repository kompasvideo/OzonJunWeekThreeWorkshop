using FluentValidation;
using Workshop.Api.Requests.V3;

namespace Workshop.Api.Validators;

public class GoodPropertiesValidator : AbstractValidator<GoodProperties>
{
    public GoodPropertiesValidator()
    {
        RuleFor(x => x.weight)
            .GreaterThan(0)
            .LessThan(Int32.MaxValue);
        RuleFor(x => x.height)
            .GreaterThan(0)
            .LessThan(Int32.MaxValue);
        RuleFor(x => x.length)
            .GreaterThan(0)
            .LessThan(Int32.MaxValue);
        RuleFor(x => x.width)
            .GreaterThan(0)
            .LessThan(Int32.MaxValue);
    }
}