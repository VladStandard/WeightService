using Ws.Database.EntityFramework.Models.Ready;

namespace Ws.Database.EntityFramework.Models;

public partial class Printer
{
    public Guid Uid { get; set; }

    public DateTime CreateDt { get; set; }

    public DateTime ChangeDt { get; set; }

    public string Name { get; set; } = null!;

    public string Ip { get; set; } = null!;

    public short Port { get; set; }

    public string Type { get; set; } = null!;

    public Guid ProductionSiteUid { get; set; }

    public virtual ICollection<Line> Lines { get; set; } = new List<Line>();

    public virtual ProductionSite ProductionSiteU { get; set; } = null!;
}
