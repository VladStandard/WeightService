using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusStorageMethods;
using Ws.StorageCore.Entities.SchemaScale.TemplatesResources;
using Ws.StorageCore.Models;
namespace Ws.StorageCore.Entities.SchemaScale.PlusStorageMethodsFks;

/// <summary>
/// Table validation "PLUS_STORAGE_METHODS_FK".
/// </summary>
public sealed class SqlPluStorageMethodFkValidator : SqlTableValidator<SqlPluStorageMethodFkEntity>
{

    public SqlPluStorageMethodFkValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlPluValidator(isCheckIdentity));
        RuleFor(item => item.Method)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlPluStorageMethodValidator(isCheckIdentity));
        RuleFor(item => item.Resource)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlTemplateResourceValidator(isCheckIdentity));
    }
}