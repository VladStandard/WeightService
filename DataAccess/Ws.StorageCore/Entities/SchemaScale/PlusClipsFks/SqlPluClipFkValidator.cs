using Ws.StorageCore.Entities.SchemaRef1c.Clips;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Models;
namespace Ws.StorageCore.Entities.SchemaScale.PlusClipsFks;

/// <summary>
/// Валидатор таблицы связей клипс и ПЛУ.
/// </summary>
public sealed class SqlPluClipFkValidator : SqlTableValidator<SqlPluClipFkEntity>
{
    public SqlPluClipFkValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlPluValidator(isCheckIdentity));
        RuleFor(item => item.Clip)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlClipValidator(isCheckIdentity));
    }
}