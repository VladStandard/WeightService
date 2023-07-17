// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableConfModels.DeviceSettingsFks;

/// <summary>
/// Валидатор таблицы "DEVICES_SETTINGS_FK".
/// </summary>
public sealed class WsSqlDeviceSettingsFkValidator : WsSqlTableValidator<WsSqlDeviceSettingsFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="isCheckIdentity"></param>
    public WsSqlDeviceSettingsFkValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Device)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlDeviceValidator(isCheckIdentity));
        RuleFor(item => item.Setting)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlDeviceSettingsValidator(isCheckIdentity));
    }
}