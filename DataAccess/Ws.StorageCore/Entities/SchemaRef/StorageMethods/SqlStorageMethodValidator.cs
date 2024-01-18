namespace Ws.StorageCore.Entities.SchemaRef.StorageMethods;

public sealed class SqlStorageMethodValidator : SqlTableValidator<SqlStorageMethodEntity>
{

    public SqlStorageMethodValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Zpl)
            .NotNull();
    }
}