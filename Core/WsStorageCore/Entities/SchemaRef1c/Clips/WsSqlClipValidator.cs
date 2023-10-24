namespace WsStorageCore.Entities.SchemaRef1c.Clips;


/// <summary>
/// Table validation "CLIPS".
/// </summary>
public sealed class WsSqlClipValidator : WsSqlTableValidator<WsSqlClipEntity>
{

    public WsSqlClipValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.Weight)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
    }
}