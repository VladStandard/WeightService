// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableConfModels.DeviceSettings;

/// <summary>
/// Валидатор таблицы "DEVICES_SETTINGS".
/// </summary>
public sealed class WsSqlDeviceSettingsValidator : WsSqlTableValidator<WsSqlDeviceSettingsModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="isCheckIdentity"></param>
    public WsSqlDeviceSettingsValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}