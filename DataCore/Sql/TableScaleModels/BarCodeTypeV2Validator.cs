// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "BARCODE_TYPES_V2".
/// </summary>
public class BarCodeTypeV2Validator : AbstractValidator<BarCodeTypeV2Entity>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public BarCodeTypeV2Validator()
	{
		RuleFor(item => item.Name)
			.NotEmpty()
			.NotNull();
	}
}
