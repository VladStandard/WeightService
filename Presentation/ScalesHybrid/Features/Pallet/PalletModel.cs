using Ws.Domain.Models.Entities.Print;

namespace ScalesHybrid.Features.Pallet;

public class PalletModel
{
    public int Number { get; set; } = int.MinValue;
    public Guid Uid { get; set; } = Guid.Empty;
    public DateTime CreateDt { get; set; } = DateTime.Now;
    public IEnumerable<LabelEntity> Labels { get; set; } = [];
}