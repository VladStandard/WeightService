using Ws.Domain.Models.Entities.Ref;

namespace Ws.StorageCore.Entities.Ref.StorageMethods;

public sealed class SqlStorageMethodValidator : SqlTableValidator<StorageMethodEntity>
{

    public SqlStorageMethodValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Zpl)
            .NotNull();
    }
}