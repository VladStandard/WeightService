// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Models;

/// <summary>
/// Table validation.
/// </summary>
public class BaseUidValidator : BaseValidator
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public BaseUidValidator()
    {
	    RuleFor(item => item.IdentityUid)
		    .NotEmpty()
		    .NotNull()
		    .NotEqual(Guid.Empty);
	}
}
