namespace Ws.Database.EntityFramework.Models;

public partial class Box
{
    public Guid Uid { get; set; }

    public DateTime CreateDt { get; set; }

    public DateTime ChangeDt { get; set; }

    public string Name { get; set; } = null!;

    public decimal Weight { get; set; }

    public Guid Uid1c { get; set; }

    public virtual ICollection<PlusNestingFk> PlusNestingFks { get; set; } = new List<PlusNestingFk>();
}
