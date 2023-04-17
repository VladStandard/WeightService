// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables;
using WsStorageCore.TableScaleModels.Devices;
using WsStorageCore.TableScaleModels.Scales;

namespace WsStorageCore.TableScaleFkModels.DeviceScalesFks;

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