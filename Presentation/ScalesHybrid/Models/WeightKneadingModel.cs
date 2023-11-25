namespace ScalesHybrid.Models;

public class WeightKneadingModel
{
    public string PluName { get; set; } = string.Empty;
    public string PluNesting { get; set; } = string.Empty;
    public decimal NetWeight { get; set; }
    public decimal TareWeight { get; set; }
    public DateOnly ProductDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public int KneadingCount { get; set; }
}