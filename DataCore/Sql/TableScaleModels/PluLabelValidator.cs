// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "PLUS_LABELS".
/// </summary>
public class PluLabelValidator : BaseValidator
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluLabelValidator() : base(ColumnName.Uid)
	{
		RuleFor(item => ((PluLabelEntity)item).Zpl)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((PluLabelEntity)item).PluWeighing)
			.NotEmpty()
			.NotNull();
	}
}
