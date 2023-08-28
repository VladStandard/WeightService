namespace WsStorageCore.WebApiModels.BarCodes;

/// <summary>
/// Barcode down entity.
/// </summary>
[XmlRoot(WsWebConstants.BarcodeRight, Namespace = "", IsNullable = false)]
[DebuggerDisplay("{ToString()}")]
public class WsSqlBarcodeRightModel : SerializeBase
{
    #region Public and private fields and properties

    [XmlElement(nameof(Const1))]
    public string Const1 { get; set; } = string.Empty;
    [XmlElement(nameof(ArmNumber))]
    public string ArmNumber { get; set; } = string.Empty;
    [XmlElement(nameof(Counter))]
    public string Counter { get; set; } = string.Empty;
    
    public WsSqlBarcodeRightModel()
    {
        //
    }
    
    public WsSqlBarcodeRightModel(string const1, string armNumber, string counter)
    {
        Const1 = const1;
        ArmNumber = armNumber;
        Counter = counter;
    }
    
    public WsSqlBarcodeRightModel(string barcode)
    {
        // 0  -3    -8
        // 299-00011-00000019
        if (barcode.Length is not 16)
            return;
        Const1 = barcode.Substring(0, 3);
        ArmNumber = barcode.Substring(3, 5);
        Counter = barcode.Substring(8, 8);
    }
    
    private WsSqlBarcodeRightModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Const1 = info.GetString(nameof(Const1)) ?? string.Empty;
        ArmNumber = info.GetString(nameof(ArmNumber)) ?? string.Empty;
        Counter = info.GetString(nameof(Counter)) ?? string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
        @$"{nameof(Const1)}: {Const1}. " + Environment.NewLine +
        @$"{nameof(ArmNumber)}: {ArmNumber}. " + Environment.NewLine +
        @$"{nameof(Counter)}: {Counter}. ";

    public string GetValue() => @$"{Const1}{ArmNumber}{Counter}";

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Const1), Const1);
        info.AddValue(nameof(ArmNumber), ArmNumber);
        info.AddValue(nameof(Counter), Counter);
    }

    #endregion
}
