// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "DEVICES_TYPES_FK".
/// </summary>
public class DeviceTypeFkValidator : SqlTableValidator<DeviceTypeFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public DeviceTypeFkValidator()
    {
        RuleFor(item => item.Device)
            .NotEmpty()
            .NotNull()
	        .SetValidator(new DeviceValidator());
		RuleFor(item => item.DeviceType)
            .NotEmpty()
            .NotNull()
            .SetValidator(new DeviceTypeValidator());
		RuleFor(item => item.Description)
            .NotNull();
    }
}
