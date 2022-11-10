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
[XmlRoot(WebConstants.BarcodeBottom, Namespace = "", IsNullable = false)]
public class BarcodeBottomModel : SerializeBase
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

    /// <summary>
    /// Constructor.
    /// </summary>
    public BarcodeBottomModel()
    {
        //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public BarcodeBottomModel(string const1, string gtin, string const2, string weight, string const3, string date, string const4, string partNumber)
    {
        Const1 = const1;
        Gtin = gtin;
        Const2 = const2;
        Weight = weight;
        Const3 = const3;
        Date = date;
        Const4 = const4;
        PartNumber = partNumber;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public BarcodeBottomModel(string barcode)
    {
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

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private BarcodeBottomModel(SerializationInfo info, StreamingContext context) : base(info, context)
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

    public override string ToString() =>
        @$"{nameof(Const1)}: {Const1}. " + Environment.NewLine +
        @$"{nameof(Gtin)}: {Gtin}. " + Environment.NewLine +
        @$"{nameof(Const2)}: {Const2}. " + Environment.NewLine +
        @$"{nameof(Weight)}: {Weight}. " + Environment.NewLine +
        @$"{nameof(Const3)}: {Const3}. " + Environment.NewLine +
        @$"{nameof(Date)}: {Date}. " + Environment.NewLine +
        @$"{nameof(Const4)}: {Const4}. " + Environment.NewLine +
        @$"{nameof(PartNumber)}: {PartNumber}. ";

    public string GetValue() => @$"{Const1}{Gtin}{Const2}{Weight}{Const3}{Date}{Const4}{PartNumber}";

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
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
