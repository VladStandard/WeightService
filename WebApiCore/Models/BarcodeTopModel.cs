// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Runtime.Serialization;
using System.Xml.Serialization;
using DataCore.Models;
using DataCore.Sql.Models;
using DataCore.Utils;

namespace WebApiCore.Models;

/// <summary>
/// Barcode down entity.
/// </summary>
[XmlRoot("BarcodeTop", Namespace = "", IsNullable = false)]
public class BarcodeTopModel : SerializeBase
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
    [XmlElement(nameof(Crc))]
    public string Crc { get; set; } = string.Empty;

    /// <summary>
    /// Constructor.
    /// </summary>
    public BarcodeTopModel()
    {
        //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public BarcodeTopModel(string const1, string armNumber, string counter, string date, string time, string plu, string weight, 
        string zames, string crc)
    {
        Const1 = const1;
        ArmNumber = armNumber;
        Counter = counter;
        Date = date;
        Time = time;
        Plu = plu;
        Weight = weight;
        Zames = zames;
        Crc = crc;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public BarcodeTopModel(string barcode, bool useCrc)
    {
        // 0  -3    -8       -16    -22    -28 -31   -36 -39
        // 298-00428-00000018-220722-164810-106-01475-001-0
        if ((!useCrc && barcode.Length == 39) || (useCrc && barcode.Length == 40))
        {
            Const1 = barcode.Substring(0, 3);
            ArmNumber = barcode.Substring(3, 5);
            Counter = barcode.Substring(8, 8);
            Date = barcode.Substring(16, 6);
            Time = barcode.Substring(22, 6);
            Plu = barcode.Substring(28, 3);
            Weight = barcode.Substring(31, 5);
            Zames = barcode.Substring(36, 3);
            if (useCrc)
                Crc = barcode.Substring(39, 1);
        }
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private BarcodeTopModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Const1 = info.GetString(nameof(Const1)) ?? string.Empty;
        ArmNumber = info.GetString(nameof(ArmNumber)) ?? string.Empty;
        Counter = info.GetString(nameof(Counter)) ?? string.Empty;
        Date = info.GetString(nameof(Date)) ?? string.Empty;
        Time = info.GetString(nameof(Time)) ?? string.Empty;
        Plu = info.GetString(nameof(Plu)) ?? string.Empty;
        Weight = info.GetString(nameof(Weight)) ?? string.Empty;
        Zames = info.GetString(nameof(Zames)) ?? string.Empty;
        Crc = info.GetString(nameof(Crc)) ?? string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        string result = 
            @$"{nameof(Const1)}: {Const1}. " + Environment.NewLine +
            @$"{nameof(ArmNumber)}: {ArmNumber}. " + Environment.NewLine +
            @$"{nameof(Counter)}: {Counter}. " + Environment.NewLine +
            @$"{nameof(Date)}: {Date}. " + Environment.NewLine +
            @$"{nameof(Time)}: {Time}. " + Environment.NewLine +
            @$"{nameof(Plu)}: {Plu}. " + Environment.NewLine +
            @$"{nameof(Weight)}: {Weight}. " + Environment.NewLine +
            @$"{nameof(Zames)}: {Zames}. ";
        if (!string.IsNullOrEmpty(Crc))
            result += Environment.NewLine + @$"{nameof(Crc)}: {Crc}. ";
        return result;
    }

    public string GetValue() => string.IsNullOrEmpty(Crc) 
        ? @$"{Const1}{ArmNumber}{Counter}{Date}{Time}{Plu}{Weight}{Zames}"
        : @$"{Const1}{ArmNumber}{Counter}{Date}{Time}{Plu}{Weight}{Zames}{Crc}";

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
        info.AddValue(nameof(Date), Date);
        info.AddValue(nameof(Time), Time);
        info.AddValue(nameof(Plu), Plu);
        info.AddValue(nameof(Weight), Weight);
        info.AddValue(nameof(Zames), Zames);
        info.AddValue(nameof(Crc), Crc);
    }

    #endregion
}
