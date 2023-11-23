using Ws.PrinterCore.Enums;

namespace Ws.PrinterCore.Tsc;

public class TscPrintProperties
{
    #region Public and private fields and properties
    
    public EnumPrintChannel Channel { get; set; }

    public string PrintName { get; set; }

    public string PrintIp { get; set; }

    public int PrintPort { get; set; }
    
    #endregion
}