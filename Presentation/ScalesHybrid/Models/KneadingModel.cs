namespace ScalesHybrid.Models;

public class KneadingModel
{
    public DateOnly ProductDate { get; set; }
    public int Kneading { get; set; }
    public int PaletteSize { get; set; }

    public KneadingModel()
    {
        ProductDate =  DateOnly.FromDateTime(DateTime.Now);
        PaletteSize = 1;
        Kneading = 1;
    }
}