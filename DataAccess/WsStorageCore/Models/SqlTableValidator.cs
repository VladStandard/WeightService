namespace WsStorageCore.Models;

[DebuggerDisplay("{ToString()}")]
public class SqlTableValidator<T> : AbstractValidator<T> where T : SqlEntityBase
{
    protected SqlTableValidator(bool isCheckIdentity, bool isCheckCreateDt, bool isCheckChangeDt)
    {
        if (isCheckIdentity)
            RuleFor(item => item.Identity).SetValidator(new SqlFieldIdentityValidator());
        if (isCheckCreateDt)
            RuleFor(item => item.CreateDt)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
        if (isCheckChangeDt)
            RuleFor(item => item.ChangeDt)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
    }
}