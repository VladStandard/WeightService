// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "APPS".
/// </summary>
public class AppValidator : AbstractValidator<BaseEntity>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public AppValidator()
	{
		RuleFor(item => ((AppEntity)item).IdentityUid)
			.NotEmpty()
			.NotNull()
			.NotEqual(Guid.Empty);
		RuleFor(item => ((AppEntity)item).Name)
			.NotEmpty()
			.NotNull();
	}
}
