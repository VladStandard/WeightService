namespace Ws.Database.EntityFramework.Models;

public partial class Host
{
    public Guid Uid { get; set; }

    public DateTime CreateDt { get; set; }

    public DateTime ChangeDt { get; set; }

    public DateTime LoginDt { get; set; }

    public string Name { get; set; } = null!;

    public string Ip { get; set; } = null!;
}
