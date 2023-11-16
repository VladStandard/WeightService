namespace WsStorageCore.Entities.SchemaScale.Apps;

/// <summary>
/// Table validation "APPS".
/// </summary>
public sealed class SqlAppValidator : SqlTableValidator<SqlAppEntity>
{

    public SqlAppValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}