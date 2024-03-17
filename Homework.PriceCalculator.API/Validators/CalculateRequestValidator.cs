using FluentValidation;
using Workshop.Api.Requests.V3;

namespace Workshop.Api.Validators;

public class CalculateRequestValidator : AbstractValidator<CalculateRequest>
{
    public CalculateRequestValidator()
    {
        RuleForEach(x => x.Goods)
            .SetValidator(new GoodPropertiesValidator());
    }
}