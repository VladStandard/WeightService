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
    public string Region { get; set; } = string.Empty;
    public string Weight { get; set; } = string.Empty;
    public string Zames { get; set; } = string.Empty;

    /// <summary>
    /// Constructor.
    /// </summary>
    public BarcodeTopEntity(string const1, string armNumber, string counter, string date, string time, string region, string weight, string zames)
    {
        Const1 = const1;
        ArmNumber = armNumber;
        Counter = counter;
        Date = date;
        Time = time;
        Region = region;
        Weight = weight;
        Zames = zames;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public BarcodeTopEntity(string barcode)
    {
        // 0  -3    -8       -16    -22  -26-28   -33
        // 298-00011-00000019-220714-1701-33-00400-001
        if (barcode.Length != 36)
            return;
        Const1 = barcode.Substring(0, 3);
        ArmNumber = barcode.Substring(3, 5);
        Counter = barcode.Substring(8, 8);
        Date = barcode.Substring(16, 6);
        Time = barcode.Substring(22, 4);
        Region = barcode.Substring(26, 2);
        Weight = barcode.Substring(28, 5);
        Zames = barcode.Substring(33, 3);
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
        return 
            @$"{nameof(Const1)}: {Const1}. " + Environment.NewLine +
            @$"{nameof(ArmNumber)}: {ArmNumber}. " + Environment.NewLine +
            @$"{nameof(Counter)}: {Counter}. " + Environment.NewLine +
            @$"{nameof(Date)}: {Date}. " + Environment.NewLine +
            @$"{nameof(Time)}: {Time}. " + Environment.NewLine +
            @$"{nameof(Region)}: {Region}. " + Environment.NewLine +
            @$"{nameof(Weight)}: {Weight}. " + Environment.NewLine +
            @$"{nameof(Zames)}: {Zames}. ";
    }

    #endregion
}
