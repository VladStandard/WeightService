namespace Ws.StorageCore.Entities.SchemaRef1c.Boxes;

/// <summary>
/// Table validation "BOXES".
/// </summary>
public sealed class SqlBoxValidator : SqlTableValidator<SqlBoxEntity>
{
    public SqlBoxValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.Weight)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
    }
}