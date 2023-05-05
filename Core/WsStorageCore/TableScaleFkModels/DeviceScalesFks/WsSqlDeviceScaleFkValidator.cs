// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.DeviceScalesFks;

/// <summary>
/// Table validation "DEVICES_SCALES_FK".
/// </summary>
public sealed class WsSqlDeviceScaleFkValidator : WsSqlTableValidator<WsSqlDeviceScaleFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlDeviceScaleFkValidator() : base(true, true)
    {
        RuleFor(item => item.Device)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlDeviceValidator());
        RuleFor(item => item.Scale)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlScaleValidator());
    }
}