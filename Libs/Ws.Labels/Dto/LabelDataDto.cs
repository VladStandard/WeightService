namespace Ws.Labels.Dto;

public class LabelDataDto
{
    public decimal Weight { get; set; }
    public decimal WeightTare { get; set; }
    public bool IsCheckWeight { get; set; }

    public string Itf { get; set; } = string.Empty;
    public string Gtin { get; set; } = string.Empty;

    public int LineNumber { get; set; }
    public int LineCounter { get; set; }
    public string LineName { get; set; } = string.Empty;

    public Guid Plu1СGuid { get; set; }
    public short PluNumber { get; set; }
    public string PluFullName { get; set; } = string.Empty;
    public string PluDescription { get; set; } = string.Empty;

    public short BundleCount { get; set; }
    public short Kneading { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Template { get; set; } = string.Empty;
    public DateTime ProductDt { get; set; }
    public DateTime ExpirationDt { get; set; }
}