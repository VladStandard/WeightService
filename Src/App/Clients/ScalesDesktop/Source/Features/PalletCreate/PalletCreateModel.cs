using Ws.Domain.Models.Entities.Ref1c;

namespace ScalesDesktop.Source.Features.PalletCreate;

public class PalletCreateModel
{
    public PluEntity? Plu { get; set; }
    public CharacteristicEntity? Nesting { get; set; }
    public int Count { get; set; } = 1;
    public decimal PalletWeight { get; set; }
    public short Kneading { get; set; } = 1;
    public DateTime? CreateDt { get; set; } = DateTime.Now;
}