namespace Ws.Database.EntityFramework.Models;

public partial class StorageMethod
{
    public Guid Uid { get; set; }

    public DateTime CreateDt { get; set; }

    public string Name { get; set; } = null!;

    public string Zpl { get; set; } = null!;

    public virtual ICollection<Plu> Plus { get; set; } = new List<Plu>();
}
