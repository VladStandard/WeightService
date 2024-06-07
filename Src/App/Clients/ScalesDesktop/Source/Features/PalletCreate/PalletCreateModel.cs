using Ws.Domain.Models.Entities.Ref1c.Plu;

namespace ScalesDesktop.Source.Features.PalletCreate;

public class PalletCreateModel
{
    public Plu? Plu { get; set; }
    public PluCharacteristic? Nesting { get; set; }
    public int Count { get; set; } = 1;
    public decimal PalletWeight { get; set; }
    public short Kneading { get; set; } = 1;
    public DateTime? CreateDt { get; set; } = DateTime.Now;
}