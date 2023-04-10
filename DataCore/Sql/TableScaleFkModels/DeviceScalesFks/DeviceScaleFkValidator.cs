// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.Scales;

namespace DataCore.Sql.TableScaleFkModels.DeviceScalesFks;

/// <summary>
/// Table validation "DEVICES_SCALES_FK".
/// </summary>
public sealed class DeviceScaleFkValidator : WsSqlTableValidator<DeviceScaleFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public DeviceScaleFkValidator() : base(true, true)
    {
        RuleFor(item => item.Device)
            .NotEmpty()
            .NotNull()
            .SetValidator(new DeviceValidator());
        RuleFor(item => item.Scale)
            .NotEmpty()
            .NotNull()
            .SetValidator(new ScaleValidator());
    }
}