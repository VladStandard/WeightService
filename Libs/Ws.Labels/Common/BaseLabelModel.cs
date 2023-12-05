namespace Ws.Labels.Common;

public class BaseLabelModel
{
    #region DateTime
    
    public DateTime ExpirationDtValue { get; set; } = DateTime.MinValue;
    public DateTime ProductDtValue { get; set; } = DateTime.MinValue;
    
    public string ProductDate => $"{ProductDtValue:yyMMdd}";
    public string ProductTime => $"{ProductDtValue:HHmmss}";
    public string ProductDateShort => $"{ProductDtValue:yyMM}";
    
    #endregion

    #region Line

    public int LineNumber { get; set; }
    public int LineCounter { get; set; }
    public string LineName { get; set; } = string.Empty;
    public string LineAddress { get; set; } = string.Empty;
    
    #endregion
    
    #region Plu

    public int PluNumber { get; set; }
    public string PluGtin { get; set; } = string.Empty;
    public string PluFullName { set; get; } = string.Empty;
    public string PluDescription { get; set; } = string.Empty;

    #endregion
    
    #region Other
    
    public int Kneading { get; set; }
    
    #endregion

    protected string IntToStr(int value, int charLen) =>  value.ToString().PadLeft(charLen, '0');
}