// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Xml.Serialization;
using DataCore.Sql.Models;
using WebApiCore.Utils;

namespace WebApiCore.Common;

/// <summary>
/// Barcode top entity.
/// </summary>
[XmlRoot(TerraConsts.Info, Namespace = "", IsNullable = false)]
public class BarcodeTopEntity : BaseSerializeDeprecatedEntity<BarcodeTopEntity>
{
    #region Public and private fields and properties

    public string Const1 { get; set; } = string.Empty;
    public string ArmNumber { get; set; } = string.Empty;
    public string Counter { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public string Time { get; set; } = string.Empty;
    public string Plu { get; set; } = string.Empty;
    public string Weight { get; set; } = string.Empty;
    public string Zames { get; set; } = string.Empty;
    public string Crc { get; set; } = string.Empty;

    /// <summary>
    /// Constructor.
    /// </summary>
    public BarcodeTopEntity(string const1, string armNumber, string counter, string date, string time, string plu, string weight, 
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
    public BarcodeTopEntity(string barcode, bool useCrc)
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
    public BarcodeTopEntity()
    {
        //
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

    #endregion
}
