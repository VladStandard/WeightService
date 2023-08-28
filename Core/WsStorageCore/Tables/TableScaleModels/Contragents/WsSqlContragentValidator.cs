namespace WsStorageCore.Tables.TableScaleModels.Contragents;

/// <summary>
/// Table validation "CONTRAGENTS_V2".
/// </summary>
public sealed class WsSqlContragentValidator : WsSqlTableValidator<WsSqlContragentModel>
{

    public WsSqlContragentValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}
