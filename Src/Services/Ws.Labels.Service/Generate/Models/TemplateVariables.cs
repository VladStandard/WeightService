using Ws.Shared.Extensions;

namespace Ws.Labels.Service.Generate.Models;

/// <summary>
/// DON'T TOUCH (THIS IS VARIABLES FOR TEMPLATES) BE CAREFUL
/// </summary>
public class TemplateVariables(
    string pluName, ushort pluNumber, string pluDescription,
    int lineNumber, string lineName, string lineAddress,
    string barcodeTop, string barcodeRight, string barcodeBottom,
    ushort bundleCount, ushort kneading, decimal weight, string storageMethod,
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

    public readonly string BarcodeTop = barcodeTop;
    public readonly string BarcodeRight = barcodeRight;
    public readonly string BarcodeBottom = barcodeBottom;

    #endregion

    #region Other

    public readonly string StorageMethod = storageMethod;
    public readonly ushort BundleCount = bundleCount;
    public readonly string Kneading = $"{kneading:D3}";
    public readonly string Weight =  weight.ToSepStr(",");
    public readonly string ProductDate = $"{productDt:dd.MM.yyyy}";
    public readonly string ExpirationDate = $"{expirationDt:dd.MM.yyyy}";

    #endregion
};