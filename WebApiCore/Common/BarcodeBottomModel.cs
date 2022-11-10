// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Xml.Serialization;
using DataCore.Sql.Models;
using WebApiCore.Utils;

namespace WebApiCore.Common;

/// <summary>
/// Barcode down entity.
/// </summary>
[XmlRoot(TerraConsts.Info, Namespace = "", IsNullable = false)]
public class BarcodeBottomModel : SerializeDeprecatedModel<BarcodeBottomModel>
{
    #region Public and private fields and properties

    public string Const1 { get; set; } = string.Empty;
    public string Gtin { get; set; } = string.Empty;
    public string Const2 { get; set; } = string.Empty;
    public string Weight { get; set; } = string.Empty;
    public string Const3 { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public string Const4 { get; set; } = string.Empty;
    public string PartNumber { get; set; } = string.Empty;
    
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
    public BarcodeBottomModel()
    {
        //
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

    #endregion
}
