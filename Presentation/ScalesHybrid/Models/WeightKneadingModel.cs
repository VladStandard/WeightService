namespace ScalesHybrid.Models;

public class WeightKneadingModel
{
    public string PluName { get; set; } = string.Empty;
    public string PluNesting { get; set; } = string.Empty;
    public decimal NetWeight { get; set; } = decimal.MinValue;
    public decimal TareWeight { get; set; } = decimal.MinValue;
    public DateOnly ProductDate { get; set; }
    public int KneadingCount { get; set; } = int.MinValue;
}