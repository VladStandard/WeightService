using Ws.Domain.Models.Entities.Print;

namespace Ws.Domain.Services.Features.Label.Validators;

internal sealed class LabelNewValidator : AbstractValidator<LabelEntity>
{
    public LabelNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);

        RuleFor(item => item.Zpl)
            .NotEmpty();
        RuleFor(item => item.WeightNet)
            .NotNull();
        RuleFor(item => item.WeightTare)
            .NotNull();
        RuleFor(item => item.BarcodeTop)
            .NotEmpty();
        RuleFor(item => item.BarcodeRight)
            .NotEmpty();
        RuleFor(item => item.BarcodeBottom)
            .NotEmpty();
        RuleFor(item => item.Kneading)
            .NotEmpty();
        RuleFor(item => item.ProductDt)
            .NotEmpty();
        RuleFor(item => item.ExpirationDt)
            .NotEmpty()
            .GreaterThanOrEqualTo(item => item.ProductDt);
        // RuleFor(item => item.Plu)
        //     .SetValidator(new SqlPluValidator(isCheckIdentity));
        // RuleFor(item => item.Line)
        //     .SetValidator(new SqlLineValidator(isCheckIdentity));
    }
}