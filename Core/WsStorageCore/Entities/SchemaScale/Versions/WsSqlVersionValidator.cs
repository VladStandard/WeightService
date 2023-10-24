namespace WsStorageCore.Entities.SchemaScale.Versions;

public sealed class WsSqlVersionValidator : WsSqlTableValidator<WsSqlVersionEntity>
{
    public WsSqlVersionValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.ReleaseDt)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
        RuleFor(item => item.Version)
            .NotEmpty()
            .NotNull()
            .GreaterThan(default(short));
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.Description)
            .NotEmpty()
            .NotNull();
    }
}
