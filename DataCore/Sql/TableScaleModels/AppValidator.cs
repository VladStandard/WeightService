// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "APPS".
/// </summary>
public class AppValidator : BaseValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public AppValidator() : base(ColumnName.Uid)
	{
		RuleFor(item => ((AppEntity)item).Name)
			.NotEmpty()
			.NotNull();
	}
}
