using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.StorageCore.Entities.Ref1c.Boxes;

public sealed class SqlBoxValidator : SqlTableValidator<BoxEntity>
{
    public SqlBoxValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.Weight)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
    }
}