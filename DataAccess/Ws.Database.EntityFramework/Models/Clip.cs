namespace Ws.Database.EntityFramework.Models;

public partial class Clip
{
    public Guid Uid { get; set; }

    public DateTime CreateDt { get; set; }

    public DateTime ChangeDt { get; set; }

    public string Name { get; set; } = null!;

    public decimal Weight { get; set; }

    public Guid Uid1c { get; set; }

    public virtual ICollection<Plu> Plus { get; set; } = new List<Plu>();
}
