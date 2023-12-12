namespace Ws.Labels.Dto;

public class LabelDataDto
{
    public decimal Weight { get; set; }
    public required decimal WeightTare { get; set; }
    public short BundleCount { get; set; }
    public string Itf { get; set; } = string.Empty;
    public string Gtin { get; set; } = string.Empty;
    public required string Template { get; set; }
    public required short Kneading { get; set; }
    public required int LineCounter { get; set; }
    public required int LineNumber { get; set; }
    public required string LineName { get; set; }
    public required bool IsCheckWeight { get; set; }
    public required string Address { get; set; }
    public required string PluFullName { get; set; }
    public required int PluNumber { get; set; }
    public required string PluDescription { get; set; }
    public required DateTime ProductDt { get; set; }
    public required DateTime ExpirationDt { get; set; }
}