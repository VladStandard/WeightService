// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables;

/// <summary>
/// Table validation.
/// </summary>
public class WsSqlTableValidator<T> : AbstractValidator<T> where T : WsSqlTableBase
{
    /// <summary>
    /// Constructor.
    /// </summary>
    protected WsSqlTableValidator(bool isCheckCreateDt, bool isCheckChangeDt)
    {
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

    protected bool PreValidateSubEntity<TItem>(TItem? item, ref ValidationResult result) where TItem : WsSqlTableBase, new()
	{
        if (item is not null)
        {
	        ValidationResult validationResult = WsValidationUtils.GetValidationResult<TItem>(item);
	        if (!result.IsValid) return result.IsValid;
        }
        return result.IsValid;
    }
}