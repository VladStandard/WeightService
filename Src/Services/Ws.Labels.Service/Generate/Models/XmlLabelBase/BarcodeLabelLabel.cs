using System.Runtime.Serialization;
using Ws.Labels.Service.Generate.Common.XmlBarcode;

namespace Ws.Labels.Service.Generate.Models.XmlLabelBase;

public abstract partial class BarcodeLabelLabel : IBarcodeLabel, ISerializable
{
    #region Line

    public required int LineNumber { get; set; }
    public required int LineCounter { get; set; }

    #endregion

    #region Plu

    public required short PluNumber { get; set; }
    public required string PluGtin { get; set; }

    #endregion

    #region Barcodes

    public string BarCodeTop => _barCodeTop ??= GenerateBarcode(BarcodeTopTemplate);
    public string BarCodeRight => _barCodeRight ??= GenerateBarcode(BarcodeRightTemplate);
    public string BarCodeBottom => _barCodeBottom ??= GenerateBarcode(BarcodeBottomTemplate);

    #endregion
}