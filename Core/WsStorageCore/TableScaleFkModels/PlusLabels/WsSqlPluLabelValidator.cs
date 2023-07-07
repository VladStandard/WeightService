// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusLabels;

/// <summary>
/// Table validation "PLUS_LABELS".
/// </summary>
public sealed class WsSqlPluLabelValidator : WsSqlTableValidator<WsSqlPluLabelModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="isCheckIdentity"></param>
    public WsSqlPluLabelValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.PluScale)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Zpl)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.ProductDt)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
        RuleFor(item => item.ExpirationDt)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(item => item.ProductDt);
    }

    public bool PreValidate(ValidationContext<WsSqlPluLabelModel> context, ValidationResult result, bool isCheckIdentity)
    {
        switch (context.InstanceToValidate)
        {
            case null:
                result.Errors.Add(new(nameof(context), "Please ensure a model was supplied!"));
                return false;
            default:
                if (!PreValidateSubEntity(context.InstanceToValidate.PluWeighing, ref result, isCheckIdentity))
                    return result.IsValid;
                return result.IsValid;
        }
    }
}