// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableConfModels.DeviceSettingsFks;

public sealed class WsSqlDeviceSettingsFkValidator : WsSqlTableValidator<WsSqlDeviceSettingsFkModel>
{
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