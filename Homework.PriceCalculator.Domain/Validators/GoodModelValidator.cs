using FluentValidation;
using Homework.PriceCalculator.Domain.Models;

namespace Homework.PriceCalculator.Domain.Validators;

public class GoodModelValidator : AbstractValidator<GoodModel>
{
    public GoodModelValidator()
    {
        RuleFor(x => x.Weight)
            .GreaterThan(0)
            .LessThan(Int32.MaxValue);
        RuleFor(x => x.Height)
            .GreaterThan(0)
            .LessThan(Int32.MaxValue);
        RuleFor(x => x.Length)
            .GreaterThan(0)
            .LessThan(Int32.MaxValue);
        RuleFor(x => x.Width)
            .GreaterThan(0)
            .LessThan(Int32.MaxValue);
    }
}