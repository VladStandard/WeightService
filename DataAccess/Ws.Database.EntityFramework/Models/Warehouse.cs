using Ws.Database.EntityFramework.Models.Ready;

namespace Ws.Database.EntityFramework.Models;

public partial class Warehouse
{
    public Guid Uid { get; set; }

    public DateTime CreateDt { get; set; }

    public DateTime ChangeDt { get; set; }

    public string Name { get; set; } = null!;

    public Guid ProductionSitesUid { get; set; }

    public virtual ICollection<Line> Lines { get; set; } = new List<Line>();

    public virtual ProductionSite ProductionSitesU { get; set; } = null!;
}
