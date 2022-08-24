// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Models;

/// <summary>
/// Table validation.
/// </summary>
public class BaseIdValidator : BaseValidator
{
    /// <summary>
    /// Constructor.
    /// </summary>
    protected BaseIdValidator()
    {
	    RuleFor(item => ((NomenclatureEntity)item).IdentityId)
		    .NotEmpty()
		    .NotNull()
		    .NotEqual(0);
	}
}
