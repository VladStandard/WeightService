// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.DeviceTypesFks;

/// <summary>
/// Table validation "DEVICES_TYPES_FK".
/// </summary>
public sealed class WsSqlDeviceTypeFkValidator : WsSqlTableValidator<WsSqlDeviceTypeFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlDeviceTypeFkValidator() : base(true, true)
    {
        RuleFor(item => item.Device)
            .NotEmpty()
            .NotNull()
            .SetValidator(new DeviceValidator());
        RuleFor(item => item.Type)
            .NotEmpty()
            .NotNull()
            .SetValidator(new DeviceTypeValidator());
        RuleFor(item => item.Description)
            .NotNull();
    }
}