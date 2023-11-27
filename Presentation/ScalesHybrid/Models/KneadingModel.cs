namespace ScalesHybrid.Models;

public class KneadingModel
{
    public DateOnly ProductDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public int Kneading { get; set; } = 1;
    public int PaletteSize { get; set; } = 1;
}