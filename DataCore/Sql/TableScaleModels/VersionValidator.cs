// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "___".
/// </summary>
public class VersionValidator : BaseUidValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public VersionValidator()
	{
		RuleFor(item => ((VersionEntity)item).Version)
			.NotEmpty()
			.NotNull();
	}
}
