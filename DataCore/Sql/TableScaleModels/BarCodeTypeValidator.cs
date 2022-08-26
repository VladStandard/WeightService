// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "BARCODE_TYPES_V2".
/// </summary>
public class BarCodeTypeValidator : BaseValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public BarCodeTypeValidator() : base(ColumnName.Uid)
	{
		RuleFor(item => ((BarCodeTypeEntity)item).Name)
			.NotEmpty()
			.NotNull();
	}
}
