namespace Ws.StorageCore.Entities.SchemaRef1c.Brands;

public sealed class SqlBrandValidator : SqlTableValidator<SqlBrandEntity>
{
    public SqlBrandValidator(bool isCheckIdentity) : base(isCheckIdentity)
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