namespace ScalesDesktop.Source.Shared.Models;

public class WeightKneadingModel
{
    public int NetWeightG { get; set; }
    public DateTime ProductDate { get; set; } = DateTime.Now;
    public ushort KneadingCount { get; set; } = 1;
}