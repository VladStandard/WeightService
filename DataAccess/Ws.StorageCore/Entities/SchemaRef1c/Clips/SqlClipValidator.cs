namespace Ws.StorageCore.Entities.SchemaRef1c.Clips;

public sealed class SqlClipValidator : SqlTableValidator<SqlClipEntity>
{

    public SqlClipValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.Weight)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
    }
}