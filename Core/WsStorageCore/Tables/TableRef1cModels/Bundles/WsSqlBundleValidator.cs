namespace WsStorageCore.Tables.TableRef1cModels.Bundles;

/// <summary>
/// Table validation "BUNDLES".
/// </summary>
public sealed class WsSqlBundleValidator : WsSqlTableValidator<WsSqlBundleModel>
{

    public WsSqlBundleValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.Weight)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
    }
}