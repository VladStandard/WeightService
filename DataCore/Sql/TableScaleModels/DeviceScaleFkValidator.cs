// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "DEVICES_SCALES_FK".
/// </summary>
public class DeviceScaleFkValidator : SqlTableValidator<DeviceScaleFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public DeviceScaleFkValidator()
    {
        RuleFor(item => item.Device)
            .NotEmpty()
            .NotNull()
	        .SetValidator(new DeviceValidator());
		RuleFor(item => item.Scale)
            .NotEmpty()
            .NotNull()
            .SetValidator(new ScaleValidator());
		RuleFor(item => item.Description)
            .NotNull();
    }
}
