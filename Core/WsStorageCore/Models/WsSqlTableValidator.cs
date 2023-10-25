namespace WsStorageCore.Models;

[DebuggerDisplay("{ToString()}")]
public class WsSqlTableValidator<T> : AbstractValidator<T> where T : WsSqlEntityBase
{
    protected WsSqlTableValidator(bool isCheckIdentity, bool isCheckCreateDt, bool isCheckChangeDt)
    {
        if (isCheckIdentity)
            RuleFor(item => item.Identity).SetValidator(new WsSqlFieldIdentityValidator());
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