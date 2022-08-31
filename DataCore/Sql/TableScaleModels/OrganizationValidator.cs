// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "Organization".
/// </summary>
public class OrganizationValidator : TableValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public OrganizationValidator()
	{
		RuleFor(item => ((OrganizationModel)item).Name)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((OrganizationModel)item).Gln)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((OrganizationModel)item).Xml)
			.NotEmpty()
			.NotNull();
	}
}
