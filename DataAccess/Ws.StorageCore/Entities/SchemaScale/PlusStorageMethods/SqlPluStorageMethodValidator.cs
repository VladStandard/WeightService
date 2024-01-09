namespace Ws.StorageCore.Entities.SchemaScale.PlusStorageMethods;

public sealed class SqlPluStorageMethodValidator : SqlTableValidator<SqlPluStorageMethodEntity>
{

    public SqlPluStorageMethodValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.MinTemp)
            .NotNull()
            .LessThanOrEqualTo(item => item.MaxTemp);
        RuleFor(item => item.MaxTemp)
            .NotNull()
            .GreaterThanOrEqualTo(item => item.MinTemp);
    }
}