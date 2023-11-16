using System.Text.RegularExpressions;

namespace Ws.StorageCore.WebApiModels.BarCodes;

/// <summary>
/// Barcode down entity.
/// </summary>
[XmlRoot("BarcodeBottom", Namespace = "", IsNullable = false)]
[DebuggerDisplay("{ToString()}")]
public class SqlBarcodeBottomModel : SerializeBase
{
    #region Public and private fields and properties

    [XmlElement(nameof(Const1))]
    public string Const1 { get; set; } = string.Empty;
    
    [XmlElement(nameof(Gtin))]
    public string Gtin { get; set; } = string.Empty;
    
    [XmlElement(nameof(Const2))]
    public string Const2 { get; set; } = string.Empty;
    
    [XmlElement(nameof(Weight))]
    public string Weight { get; set; } = string.Empty;
    
    [XmlElement(nameof(Const3))]
    public string Const3 { get; set; } = string.Empty;
    
    [XmlElement(nameof(Date))]
    public string Date { get; set; } = string.Empty;
    
    [XmlElement(nameof(Const4))]
    public string Const4 { get; set; } = string.Empty;
    
    [XmlElement(nameof(PartNumber))]
    public string PartNumber { get; set; } = string.Empty;
    
    public SqlBarcodeBottomModel(string barcode)
    {
        barcode = Regex.Replace(barcode, @"[\(\)]", "");
        // 0 -2             -16  -20    -26-28    -34-36
        // 01-12600076000000-3103-007440-11-221021-10-2210
        if (barcode.Length is not 40)
            return;
        Const1 = barcode.Substring(0, 2);
        Gtin = barcode.Substring(2, 14);
        Const2 = barcode.Substring(16, 4);
        Weight = barcode.Substring(20, 6);
        Const3 = barcode.Substring(26, 2);
        Date = barcode.Substring(28, 6);
        Const4 = barcode.Substring(34, 2);
        PartNumber = barcode.Substring(36, 4);
    }
    
    protected SqlBarcodeBottomModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Const1 = info.GetString(nameof(Const1)) ?? string.Empty;
        Gtin = info.GetString(nameof(Gtin)) ?? string.Empty;
        Const2 = info.GetString(nameof(Const2)) ?? string.Empty;
        Weight = info.GetString(nameof(Weight)) ?? string.Empty;
        Const3 = info.GetString(nameof(Const3)) ?? string.Empty;
        Date = info.GetString(nameof(Date)) ?? string.Empty;
        Const4 = info.GetString(nameof(Const4)) ?? string.Empty;
        PartNumber = info.GetString(nameof(PartNumber)) ?? string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString() => $"{Const1}{Gtin}{Const2}{Weight}{Const3}{Date}{Const4}{PartNumber}";
    
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Const1), Const1);
        info.AddValue(nameof(Gtin), Gtin);
        info.AddValue(nameof(Const2), Const2);
        info.AddValue(nameof(Weight), Weight);
        info.AddValue(nameof(Const3), Const3);
        info.AddValue(nameof(Date), Date);
        info.AddValue(nameof(Const4), Const4);
        info.AddValue(nameof(PartNumber), PartNumber);
    }

    #endregion
}
