using FluentValidation;
using Homework.PriceCalculator.Domain.Models;

namespace Homework.PriceCalculator.Domain.Validators;

public class GoodsValidator : AbstractValidator<IReadOnlyCollection<GoodModel>>
{
    public GoodsValidator()
    {
        RuleForEach(x => x)
            .SetValidator(new GoodModelValidator());

        RuleFor(x => x)
            .Must(x => x.Any());
    }
}