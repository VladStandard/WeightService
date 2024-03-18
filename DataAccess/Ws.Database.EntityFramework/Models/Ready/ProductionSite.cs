using System.ComponentModel.DataAnnotations.Schema;
using Ws.Database.EntityFramework.Constants;

namespace Ws.Database.EntityFramework.Models.Ready;

[Table(SqlTables.ProductionSites)]
public partial class ProductionSite : EfEntityBase
{
    public DateTime CreateDt { get; set; }

    public DateTime ChangeDt { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    // public virtual ICollection<Printer> Printers { get; set; } = new List<Printer>();
    //
    // public virtual ICollection<User> Users { get; set; } = new List<User>();
    //
    // public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
