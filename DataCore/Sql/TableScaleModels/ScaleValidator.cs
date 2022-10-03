// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using FluentValidation.Results;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "SCALES".
/// </summary>
public class ScaleValidator : SqlTableValidator<ScaleModel>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public ScaleValidator()
	{
		RuleFor(item => item.Description)
			.NotEmpty()
			.NotNull();
		//RuleFor(item => item.TemplateDefault)
		//	.Custom((template, _) =>
		//	{
		//		if (template is not null)
		//		{
		//			TemplateValidator validator = new();
		//			validator.Validate(template);
		//		}
		//	})
		//	.SetValidator(new TemplateValidator());
		//RuleFor(item => (item).TemplateSeries).Custom((x, context) =>
		//	{
		//		if (context.RootContextData.ContainsKey("MyCustomData"))
		//		{
		//			context.AddFailure("My error message");
		//		}
		//	});
	}

	protected override bool PreValidate(ValidationContext<ScaleModel> context, ValidationResult result)
	{
		//if (context.InstanceToValidate is ScaleModel scale1)
		//{
		//	if (!PreValidateSubEntity(scale1.TemplateSeries, ref result))
		//	{
		//		//result.Errors.Add(new(nameof(scale1.TemplateSeries), result.Errors.));
		//		context.RootContextData["MyCustomData"] = "Test";
		//		return result.IsValid;
		//	}
		//}
		switch (context.InstanceToValidate)
		{
			case null:
				result.Errors.Add(new(nameof(context), "Please ensure a model was supplied!"));
				return false;
			default:
				if (!PreValidateSubEntity(context.InstanceToValidate.TemplateDefault, ref result))
					return result.IsValid;
				if (!PreValidateSubEntity(context.InstanceToValidate.TemplateSeries, ref result))
					return result.IsValid;
				if (!PreValidateSubEntity(context.InstanceToValidate.WorkShop, ref result))
					return result.IsValid;
				if (!PreValidateSubEntity(context.InstanceToValidate.PrinterMain, ref result))
					return result.IsValid;
				if (!PreValidateSubEntity(context.InstanceToValidate.PrinterShipping, ref result))
					return result.IsValid;
				if (!PreValidateSubEntity(context.InstanceToValidate.Host, ref result))
					return result.IsValid;
				return result.IsValid;
		}
	}
}
