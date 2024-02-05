namespace ScalesDesktop.Models;

public class WeightKneadingModel
{
    public int NetWeightG { get; set; }
    public DateTime ProductDate { get; set; } = DateTime.Now;
    public int KneadingCount { get; set; } = 1;
}