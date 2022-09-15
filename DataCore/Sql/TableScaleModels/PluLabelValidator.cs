// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using FluentValidation.Results;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "PLUS_LABELS".
/// </summary>
public class PluLabelValidator : SqlTableValidator<PluLabelModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluLabelValidator()
    {
		RuleFor(item => item.Zpl)
			.NotEmpty()
			.NotNull();
	}

    protected override bool PreValidate(ValidationContext<PluLabelModel> context, ValidationResult result)
    {
	    switch (context.InstanceToValidate)
	    {
		    case null:
			    result.Errors.Add(new(nameof(context), "Please ensure a model was supplied!"));
			    return false;
		    default:
			    if (!PreValidateSubEntity(context.InstanceToValidate.PluWeighing, ref result))
				    return result.IsValid;
				return result.IsValid;
	    }
	}
}
