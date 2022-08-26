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
    protected BaseValidator(ColumnName columnName)
    {
	    RuleFor(item => item.CreateDt)
		    .NotEmpty()
		    .NotNull()
		    .GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
	    RuleFor(item => item.ChangeDt)
		    .NotEmpty()
		    .NotNull()
		    .GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
	    switch (columnName)
	    {
		    case ColumnName.Id:
			    RuleFor(item => item.IdentityId)
				    .NotEmpty()
				    .NotNull()
				    .NotEqual(0);
			    break;
		    case ColumnName.Uid:
				RuleFor(item => item.IdentityUid)
					.NotEmpty()
					.NotNull()
					.NotEqual(Guid.Empty);
				break;
		    default:
			    throw new ArgumentOutOfRangeException(nameof(columnName), columnName, null);
	    }
    }
}
