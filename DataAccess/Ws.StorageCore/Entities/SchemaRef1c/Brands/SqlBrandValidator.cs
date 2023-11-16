namespace Ws.StorageCore.Entities.SchemaRef1c.Brands;

/// <summary>
/// Table validation "BRANDS".
/// </summary>
public sealed class SqlBrandValidator : SqlTableValidator<SqlBrandEntity>
{
    public SqlBrandValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Name)
            .NotNull()
            .MaximumLength(128);
        RuleFor(item => item.Code)
            .NotEmpty()
            .NotNull()
            .Length(9);
    }
}