namespace WsStorageCore.Tables.TableScaleModels.Clips;


/// <summary>
/// Table validation "CLIPS".
/// </summary>
public sealed class WsSqlClipValidator : WsSqlTableValidator<WsSqlClipModel>
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