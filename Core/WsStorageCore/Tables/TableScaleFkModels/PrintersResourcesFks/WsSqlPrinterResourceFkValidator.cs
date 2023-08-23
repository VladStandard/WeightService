// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.PrintersResourcesFks;

/// <summary>
/// Table validation "ZebraPrinterResourceRef".
/// </summary>
public sealed class WsSqlPrinterResourceFkValidator : WsSqlTableValidator<WsSqlPrinterResourceFkModel>
{

    public WsSqlPrinterResourceFkValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Printer)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPrinterValidator(isCheckIdentity));
        RuleFor(item => item.TemplateResource)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlTemplateResourceValidator(isCheckIdentity));
    }
}