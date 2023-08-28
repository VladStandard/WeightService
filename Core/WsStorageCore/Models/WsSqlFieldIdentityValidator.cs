namespace WsStorageCore.Models;

public class WsSqlFieldIdentityValidator : AbstractValidator<WsSqlFieldIdentityModel>
{
    public WsSqlFieldIdentityValidator()
    {
        RuleFor(item => item.Id)
            .NotEmpty()
            .NotNull()
            .NotEqual(0)
            .When(item => item.Name == WsSqlEnumFieldIdentity.Id);
        RuleFor(item => item.Uid)
            .NotEmpty()
            .NotNull()
            .NotEqual(Guid.Empty)
            .When(item => item.Name == WsSqlEnumFieldIdentity.Uid);
    }
}