namespace WsStorageCore.Entities.SchemaRef1c.Boxes;

/// <summary>
/// Table validation "BOXES".
/// </summary>
public sealed class WsSqlBoxValidator : WsSqlTableValidator<WsSqlBoxEntity>
{
    public WsSqlBoxValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.Weight)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
    }
}