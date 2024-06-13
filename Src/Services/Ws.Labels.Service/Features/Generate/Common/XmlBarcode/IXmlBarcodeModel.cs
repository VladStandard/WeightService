namespace Ws.Labels.Service.Features.Generate.Common.XmlBarcode;

internal interface IXmlBarcodeModel
{
    public int LineNumber { get; set; }
    public int LineCounter { get; set; }
    public short Kneading { get; set; }
    public short PluNumber { get; set; }
    public string PluGtin { get; set; }
    public DateTime ProductDt { get; set; }
}