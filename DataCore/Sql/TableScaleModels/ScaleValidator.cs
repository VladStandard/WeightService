// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using FluentValidation.Results;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "___".
/// </summary>
public class ScaleValidator : TableValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public ScaleValidator() : base(ColumnName.Id)
	{
		RuleFor(item => ((ScaleEntity)item).Description)
			.NotEmpty()
			.NotNull();
	}

	protected override bool PreValidate(ValidationContext<TableModel> context, ValidationResult result)
	{
		if (context.InstanceToValidate is ScaleEntity scale)
		{
			if (!PreValidateSubEntity(scale.TemplateDefault, result))
				return result.IsValid;
			if (!PreValidateSubEntity(scale.TemplateSeries, result))
				return result.IsValid;
			if (!PreValidateSubEntity(scale.WorkShop, result))
				return result.IsValid;
			if (!PreValidateSubEntity(scale.PrinterMain, result))
				return result.IsValid;
			if (!PreValidateSubEntity(scale.PrinterShipping, result))
				return result.IsValid;
			if (!PreValidateSubEntity(scale.Host, result))
				return result.IsValid;
		}
		return result.IsValid;
	}
}
