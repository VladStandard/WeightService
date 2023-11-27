namespace ScalesHybrid.Models;

public class WeightKneadingModel
{
    public int NetWeightG { get; set; } = 0;
    public DateOnly ProductDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public int KneadingCount { get; set; }
}