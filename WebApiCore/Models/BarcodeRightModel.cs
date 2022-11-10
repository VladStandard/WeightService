// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Runtime.Serialization;
using System.Xml.Serialization;
using DataCore.Sql.Models;
using WebApiCore.Utils;

namespace WebApiCore.Models;

/// <summary>
/// Barcode down entity.
/// </summary>
[XmlRoot(WebConstants.BarcodeRight, Namespace = "", IsNullable = false)]
public class BarcodeRightModel : SerializeBase
{
    #region Public and private fields and properties

    [XmlElement(nameof(Const1))]
    public string Const1 { get; set; } = string.Empty;
    [XmlElement(nameof(ArmNumber))]
    public string ArmNumber { get; set; } = string.Empty;
    [XmlElement(nameof(Counter))]
    public string Counter { get; set; } = string.Empty;

    /// <summary>
    /// Constructor.
    /// </summary>
    public BarcodeRightModel()
    {
        //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public BarcodeRightModel(string const1, string armNumber, string counter)
    {
        Const1 = const1;
        ArmNumber = armNumber;
        Counter = counter;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public BarcodeRightModel(string barcode)
    {
        // 0  -3    -8
        // 299-00011-00000019
        if (barcode.Length is not 16)
            return;
        Const1 = barcode.Substring(0, 3);
        ArmNumber = barcode.Substring(3, 5);
        Counter = barcode.Substring(8, 8);
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private BarcodeRightModel(SerializationInfo info, StreamingContext context) : base(info, context)
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
