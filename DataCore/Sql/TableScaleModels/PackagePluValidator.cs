// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "PACKAGES_PLUS".
/// </summary>
public class PackagePluValidator : SqlTableValidator<PackagePluModel>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public PackagePluValidator()
	{
		RuleFor(item => item.Name)
			.NotEmpty()
			.NotNull();
		RuleFor(item => item.Package)
			.NotEmpty()
			.NotNull()
			.SetValidator(new PackageValidator());
		RuleFor(item => item.Plu)
			.NotEmpty()
			.NotNull()
			.SetValidator(new PluValidator());
	}
}
