// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "DEVICES_TYPES".
/// </summary>
public class DeviceTypeValidator : SqlTableValidator<DeviceTypeModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public DeviceTypeValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.PrettyName)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Description)
            .NotEmpty()
            .NotNull();
    }
}
