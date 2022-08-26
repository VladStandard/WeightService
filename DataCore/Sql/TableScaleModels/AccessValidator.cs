// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using static DataCore.ShareEnums;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "ACCESS".
/// </summary>
public class AccessValidator : BaseValidator
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public AccessValidator() : base(ColumnName.Uid)
    {
        RuleFor(item => ((AccessEntity)item).User)
	        .NotEmpty()
	        .NotNull();
        RuleFor(item => ((AccessEntity)item).Rights)
	        .NotNull()
	        .LessThanOrEqualTo((byte)AccessRights.Admin)
	        .GreaterThanOrEqualTo((byte)AccessRights.None);
    }
}
