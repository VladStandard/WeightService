namespace WsStorageCore.Tables.TableScaleFkModels.DeviceScalesFks;

/// <summary>
/// Table validation "DEVICES_SCALES_FK".
/// </summary>
public sealed class WsSqlDeviceScaleFkValidator : WsSqlTableValidator<WsSqlDeviceScaleFkModel>
{

    public WsSqlDeviceScaleFkValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Device)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlDeviceValidator(isCheckIdentity));
        RuleFor(item => item.Scale)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlScaleValidator(isCheckIdentity));
    }
}