// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;

namespace DataCore.Sql.TableScaleFkModels.DeviceTypesFks;

/// <summary>
/// Table validation "DEVICES_TYPES_FK".
/// </summary>
public class DeviceTypeFkValidator : SqlTableValidator<DeviceTypeFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public DeviceTypeFkValidator() : base(true, true)
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