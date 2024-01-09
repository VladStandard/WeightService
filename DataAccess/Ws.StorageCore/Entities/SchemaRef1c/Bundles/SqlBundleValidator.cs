namespace Ws.StorageCore.Entities.SchemaRef1c.Bundles;

public sealed class SqlBundleValidator : SqlTableValidator<SqlBundleEntity>
{

    public SqlBundleValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.Weight)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
    }
}