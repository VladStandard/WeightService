// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Scales;

/// <summary>
/// Валидатор таблицы SCALES.
/// </summary>
public sealed class WsSqlScaleValidator : WsSqlTableValidator<WsSqlScaleModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="isCheckIdentity"></param>
    public WsSqlScaleValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Description)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Number)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(10000)
            .LessThanOrEqualTo(99999);


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

    public bool PreValidate(ValidationContext<WsSqlScaleModel> context, ValidationResult result, bool isCheckIdentity)
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
                if (!PreValidateSubEntity(context.InstanceToValidate.WorkShop, ref result, isCheckIdentity))
                    return result.IsValid;
                if (!PreValidateSubEntity(context.InstanceToValidate.PrinterMain, ref result, isCheckIdentity))
                    return result.IsValid;
                if (!PreValidateSubEntity(context.InstanceToValidate.PrinterShipping, ref result, isCheckIdentity))
                    return result.IsValid;
                return result.IsValid;
        }
    }
}