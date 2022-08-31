// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "BARCODES_V2".
/// </summary>
public class BarCodeValidator : TableValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public BarCodeValidator()
	{
		RuleFor(item => ((BarCodeModel)item).Value)
			.NotEmpty()
			.NotNull();
	}
}
