namespace ScalesTablet.Source.Shared.Models;

public record Pallet {
    public Guid Id { get; } = Guid.NewGuid();
    public List<Batch> Batches { get; set; } = [];
    public string DefaultPlu { get; set; } = string.Empty;
    public bool Mono { get; set; } = true;
}
