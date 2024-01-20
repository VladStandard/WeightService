using Ws.Domain.Models.Entities.Print;

namespace Ws.StorageCore.Entities.Print.Labels;

public sealed class SqlLabelValidator : SqlTableValidator<LabelEntity>
{

    public SqlLabelValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Zpl)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.WeightNet)
            .NotNull();
         RuleFor(item => item.WeightTare)
            .NotNull();
         RuleFor(item => item.BarcodeTop)
            .NotEmpty();
        RuleFor(item => item.BarcodeRight)
            .NotEmpty();
        RuleFor(item => item.BarcodeBottom)
            .NotNull();
    }
}