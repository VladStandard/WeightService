namespace WsStorageCore.Tables.TableScaleModels.Brands;

/// <summary>
/// Table validation "BRANDS".
/// </summary>
public sealed class WsSqlBrandValidator : WsSqlTableValidator<WsSqlBrandModel>
{

    public WsSqlBrandValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(128);
        RuleFor(item => item.Code)
            .NotEmpty()
            .NotNull()
            .Length(9);
    }
}
