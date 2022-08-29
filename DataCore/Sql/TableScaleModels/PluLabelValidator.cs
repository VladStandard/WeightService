// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using FluentValidation.Results;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "PLUS_LABELS".
/// </summary>
public class PluLabelValidator : TableValidator
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluLabelValidator() : base(ColumnName.Uid)
	{
		RuleFor(item => ((PluLabelEntity)item).Zpl)
			.NotEmpty()
			.NotNull();
	}

    protected override bool PreValidate(ValidationContext<TableModel> context, ValidationResult result)
    {
	    if (context.InstanceToValidate is PluLabelEntity scale)
	    {
		    if (!PreValidateSubEntity(scale.PluWeighing, result))
				return result.IsValid;
		}
		return result.IsValid;
	}
}
