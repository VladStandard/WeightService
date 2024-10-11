namespace ScalesTablet.Source.Shared.Models;

public record Batch {
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Plu { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public decimal Weight { get; set; } = decimal.Zero;
}