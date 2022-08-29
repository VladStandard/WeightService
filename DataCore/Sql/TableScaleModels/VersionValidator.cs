// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "___".
/// </summary>
public class VersionValidator : TableValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public VersionValidator() : base(ColumnName.Uid, false, false)
	{
		RuleFor(item => ((VersionEntity)item).ReleaseDt)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		RuleFor(item => ((VersionEntity)item).Version)
			.NotEmpty()
			.NotNull()
			.GreaterThan(default(short));
		RuleFor(item => ((VersionEntity)item).Description)
			.NotEmpty()
			.NotNull();
	}
}
