namespace WsStorageCore.Tables.TableDiagModels.LogsWebs;

public sealed class WsSqlLogWebValidator : WsSqlTableValidator<WsSqlLogWebModel>
{
    public WsSqlLogWebValidator(bool isCheckIdentity) : base(isCheckIdentity, true, false)
    {
        RuleFor(item => item.StampDt)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
        RuleFor(item => item.Version)
            .NotNull();
        RuleFor(item => item.Url)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.DataRequest)
            .NotNull();
        RuleFor(item => item.DataResponse)
            .NotNull();
        RuleFor(item => item.CountAll)
            .NotNull()
            .GreaterThanOrEqualTo(0);
        RuleFor(item => item.CountSuccess)
            .NotNull()
            .GreaterThanOrEqualTo(0);
        RuleFor(item => item.CountErrors)
            .NotNull()
            .GreaterThanOrEqualTo(0);
    }
}