using System;
using Ws.Shared.Enums;

namespace Ws.StorageCore.Entities.SchemaScale.Access;

public sealed class SqlAccessValidator : SqlTableValidator<SqlAccessEntity>
{

    public SqlAccessValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.LoginDt)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(new DateTime(2000, 01, 01));
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Rights)
            .NotNull()
            .LessThanOrEqualTo((byte)EnumAccessRights.Admin)
            .GreaterThanOrEqualTo((byte)EnumAccessRights.None);
    }
}
