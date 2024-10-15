namespace ScalesTablet.Source.Shared.Models;

public class Pallet {
    public Guid Id { get; } = Guid.NewGuid();
    public List<Batch> Batches { get; set; } = [];
    public string DefaultPlu { get; set; } = string.Empty;
    public bool Mono { get; set; } = true;
    public string DocumentNumber { get; set; } = string.Empty;
}
