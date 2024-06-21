namespace Ws.Labels.Service.Generate.Common.XmlBarcode;

internal interface IBarcodeLabel
{
    public int LineNumber { get; set; }
    public int LineCounter { get; set; }
    public short Kneading { get; set; }
    public short PluNumber { get; set; }
    public string PluGtin { get; set; }
    public DateTime ProductDt { get; set; }
}