// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "Organization".
/// </summary>
public class OrganizationValidator : BaseValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public OrganizationValidator() : base(ColumnName.Id)
	{
		RuleFor(item => ((OrganizationEntity)item).Name)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((OrganizationEntity)item).Gln)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((OrganizationEntity)item).Xml)
			.NotEmpty()
			.NotNull();
	}
}
