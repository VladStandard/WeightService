using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.Scales;
using Ws.StorageCore.Models;
namespace Ws.StorageCore.Entities.SchemaScale.PlusScales;

public sealed class SqlPluScaleValidator : SqlTableValidator<SqlPluScaleEntity>
{
    public SqlPluScaleValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlPluValidator(isCheckIdentity));
        RuleFor(item => item.Line)
            .NotEmpty()
            .NotNull()
            .SetValidator(new SqlScaleValidator(isCheckIdentity));
    }
}
