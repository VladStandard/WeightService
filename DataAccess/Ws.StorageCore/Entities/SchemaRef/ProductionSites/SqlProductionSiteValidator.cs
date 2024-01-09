namespace Ws.StorageCore.Entities.SchemaRef.ProductionSites;

public sealed class SqlProductionSiteValidator : SqlTableValidator<SqlProductionSiteEntity>
{

    public SqlProductionSiteValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .MaximumLength(150)
            .NotNull();
        RuleFor(item => item.Address)
            .NotEmpty()
            .MaximumLength(512)
            .NotNull();
    }
}
