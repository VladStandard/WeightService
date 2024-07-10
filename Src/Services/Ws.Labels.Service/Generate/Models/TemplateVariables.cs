using Ws.Labels.Service.Generate.Common;
using Ws.Shared.Extensions;

namespace Ws.Labels.Service.Generate.Models;

/// <summary>
/// DON'T TOUCH (THIS IS VARIABLES FOR TEMPLATES) BE CAREFUL
/// </summary>
public class TemplateVariables(
    string pluName, ushort pluNumber, string pluDescription,
    int lineNumber, string lineName, string lineAddress,
    BarcodeReadyModel barcodeTop, BarcodeReadyModel barcodeRight, BarcodeReadyModel barcodeBottom,
    ushort bundleCount, ushort kneading, decimal weight,
    decimal weightGross, ushort palletOrder, string palletNumber,
    DateTime productDt, DateTime expirationDt)
{
    #region Plu

    public readonly string PluName = pluName;
    public readonly ushort PluNumber = pluNumber;
    public readonly string PluDescription = pluDescription;

    #endregion

    #region Line

    public readonly int LineNumber = lineNumber;
    public readonly string LineName = lineName;
    public readonly string LineAddress  = lineAddress;

    #endregion

    #region Barcodes

    public readonly BarcodeReadyModel BarcodeTop = barcodeTop;
    public readonly BarcodeReadyModel BarcodeRight = barcodeRight;
    public readonly BarcodeReadyModel BarcodeBottom = barcodeBottom;

    #endregion

    #region Pallet

    public string PalletNumber = palletNumber;
    public readonly ushort PalletOrder = palletOrder;

    #endregion

    #region Weight

    public readonly string Weight =  weight.ToSepStr(",");
    public readonly string WeightGross =  weightGross.ToSepStr(",");

    #endregion

    #region Other

    public readonly ushort BundleCount = bundleCount;
    public readonly string Kneading = $"{kneading:D3}";

    public readonly string ProductDate = $"{productDt:dd.MM.yyyy}";
    public readonly string ExpirationDate = $"{expirationDt:dd.MM.yyyy}";

    #endregion
};