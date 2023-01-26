// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels.Access;

/// <summary>
/// Table validation "ACCESS".
/// </summary>
public class AccessValidator : SqlTableValidator<AccessModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public AccessValidator() : base(true, true)
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
            .LessThanOrEqualTo((byte)AccessRightsEnum.Admin)
            .GreaterThanOrEqualTo((byte)AccessRightsEnum.None);
    }
}
