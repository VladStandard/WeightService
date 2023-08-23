// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Models;

[DebuggerDisplay("{ToString()}")]
public class WsSqlTableValidator<T> : AbstractValidator<T> where T : WsSqlTableBase
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