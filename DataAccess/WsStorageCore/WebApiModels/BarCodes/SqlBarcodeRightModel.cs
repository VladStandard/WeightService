namespace WsStorageCore.WebApiModels.BarCodes;

/// <summary>
/// Barcode down entity.
/// </summary>
[XmlRoot("BarcodeRight", Namespace = "", IsNullable = false)]
[DebuggerDisplay("{ToString()}")]
public class SqlBarcodeRightModel : SerializeBase
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

    public SqlBarcodeRightModel(string barcode)
    {
        // 0  -3    -8
        // 299-00011-00000019
        if (barcode.Length < 16)
            return;
        Const1 = barcode.Substring(0, 3);
        ArmNumber = barcode.Substring(3, 5);

        switch (barcode.Length)
        {
            case 16:
                Counter = barcode.Substring(8, 8);
                Date = string.Empty;
                break;
            case 20:
                Counter = barcode.Substring(8, 6);
                Date = barcode.Substring(14, 6);;
                break;
        }
    }
    
    private SqlBarcodeRightModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Const1 = info.GetString(nameof(Const1)) ?? string.Empty;
        ArmNumber = info.GetString(nameof(ArmNumber)) ?? string.Empty;
        Counter = info.GetString(nameof(Counter)) ?? string.Empty;
        Date = info.GetString(nameof(Date)) ?? string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString() => $"{Const1}{ArmNumber}{Counter}{Date}";

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Const1), Const1);
        info.AddValue(nameof(ArmNumber), ArmNumber);
        info.AddValue(nameof(Counter), Counter);
        info.AddValue(nameof(Date), Date);
    }

    #endregion
}
