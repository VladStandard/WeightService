// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.DeviceTypes;

/// <summary>
/// Table validation "DEVICES_TYPES".
/// </summary>
public sealed class DeviceTypeValidator : WsSqlTableValidator<DeviceTypeModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public DeviceTypeValidator() : base(true, true)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.PrettyName)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Description)
            .NotNull();
    }
}
