using Ws.StorageCore.Models;
namespace Ws.StorageCore.Entities.SchemaScale.PlusStorageMethods;

/// <summary>
/// Table validation "PLUS_STORAGE_METHODS".
/// </summary>
public sealed class SqlPluStorageMethodValidator : SqlTableValidator<SqlPluStorageMethodEntity>
{

    public SqlPluStorageMethodValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.MinTemp)
            .NotNull()
            .LessThanOrEqualTo(item => item.MaxTemp);
        RuleFor(item => item.MaxTemp)
            .NotNull()
            .GreaterThanOrEqualTo(item => item.MinTemp);
    }
}