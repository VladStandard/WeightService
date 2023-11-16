namespace WsStorageCore.WebApiModels.BarCodes;

/// <summary>
/// Barcode down entity.
/// </summary>
[XmlRoot("BarcodeTop", Namespace = "", IsNullable = false)]
[DebuggerDisplay("{ToString()}")]
public class SqlBarcodeTopModel : ISerializable
{
    #region Public and private fields and properties

    [XmlElement(nameof(Const1))]
    public string Const1 { get; set; } = string.Empty;
    
    [XmlElement(nameof(ArmNumber))]
    public string ArmNumber { get; set; } = string.Empty;
    
    [XmlElement(nameof(Counter))]
    public string Counter { get; set; } = string.Empty;
    
    [XmlElement(nameof(Date))]
    public string Date { get; set; } = string.Empty;
    
    [XmlElement(nameof(Time))]
    public string Time { get; set; } = string.Empty;
    
    [XmlElement(nameof(Plu))]
    public string Plu { get; set; } = string.Empty;
    
    [XmlElement(nameof(Weight))]
    public string Weight { get; set; } = string.Empty;
    
    [XmlElement(nameof(Zames))]
    public string Zames { get; set; } = string.Empty;
    
    
    public SqlBarcodeTopModel(string barcode, bool useCrc)
    {
        // 0  -3    -8       -16    -22    -28 -31   -36 
        // 233-12312-15001108-231018-103650-844-00000-001
        if (barcode.Length != 39)
            return;
        Const1 = barcode.Substring(0, 3);
        ArmNumber = barcode.Substring(3, 5);
        Counter = barcode.Substring(8, 8);
        Date = barcode.Substring(16, 6);
        Time = barcode.Substring(22, 6);
        Plu = barcode.Substring(28, 3);
        Weight = barcode.Substring(31, 5); 
        Zames = barcode.Substring(36, 3);
    }

    #endregion

    #region Public and private methods

    public override string ToString() => $"{Const1}{ArmNumber}{Counter}{Date}{Time}{Plu}{Weight}{Zames}";
    
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Const1), Const1);
        info.AddValue(nameof(ArmNumber), ArmNumber);
        info.AddValue(nameof(Counter), Counter);
        info.AddValue(nameof(Date), Date);
        info.AddValue(nameof(Time), Time);
        info.AddValue(nameof(Plu), Plu);
        info.AddValue(nameof(Weight), Weight);
        info.AddValue(nameof(Zames), Zames);
    }

    #endregion
}
