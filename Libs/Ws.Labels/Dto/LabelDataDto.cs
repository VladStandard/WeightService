namespace Ws.Labels.Dto;

public class LabelDataDto
{
    public short Kneading { get; set; } = 0;
    public decimal Weight { get; set; } = 0;
    public int LineCounter { get; set; } = 0;
    public short BundleCount { get; set; } = 0;

    public bool IsCheckWeight { get; set; } = false;
    
    public string Itf { get; set; } = string.Empty;
    public string Gtin { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PluName { get; set; } = string.Empty;
    public string PluFullName { get; set; } = string.Empty;
    public string PluDescription { get; set; } = string.Empty;

    public DateTime ProductDt { get; set; } = DateTime.MinValue;
    public DateTime ExpirationDt { get; set; } = DateTime.MinValue;
}