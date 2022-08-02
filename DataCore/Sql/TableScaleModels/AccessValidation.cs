// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentValidation;
using static DataCore.ShareEnums;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "ACCESS".
/// </summary>
public class AccessValidation : AbstractValidator<AccessEntity>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public AccessValidation()
    {
        RuleFor(access => access.User).NotEmpty();
        RuleFor(access => access.User).NotNull();
        RuleFor(access => access.Rights).NotNull();
        RuleFor(access => access.Rights).LessThanOrEqualTo((byte)AccessRights.Admin);
        RuleFor(access => access.Rights).GreaterThanOrEqualTo((byte)AccessRights.None);
    }
}
