// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Models;

/// <summary>
/// Table validation.
/// </summary>
public class BaseValidator : AbstractValidator<BaseEntity>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    protected BaseValidator()
    {
	    RuleFor(item => item.CreateDt)
		    .NotEmpty()
		    .NotNull()
		    .GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
	    RuleFor(item => item.ChangeDt)
		    .NotEmpty()
		    .NotNull()
		    .GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
	}
}
