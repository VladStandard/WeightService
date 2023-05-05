// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.PrintersTypes;

/// <summary>
/// Table validation "ZebraPrinterType".
/// </summary>
public sealed class WsSqlPrinterTypeValidator : WsSqlTableValidator<WsSqlPrinterTypeModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPrinterTypeValidator() : base(false, false)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}
