// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables;

namespace WsStorageCore.TableScaleModels.PrintersTypes;

/// <summary>
/// Table validation "ZebraPrinterType".
/// </summary>
public sealed class PrinterTypeValidator : WsSqlTableValidator<PrinterTypeModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PrinterTypeValidator() : base(false, false)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}
