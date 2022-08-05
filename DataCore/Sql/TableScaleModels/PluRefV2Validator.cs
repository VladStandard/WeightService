// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "PLU_V2".
/// </summary>
public class PluRefV2Validator : AbstractValidator<PluRefV2Entity>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluRefV2Validator()
    {
		RuleFor(item => item.Plu)
			.NotEmpty()
			.NotNull();
		RuleFor(item => item.Scale)
			.NotEmpty()
			.NotNull();
	}
}
