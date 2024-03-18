using Ws.Database.EntityFramework.Models.Ready;

namespace Ws.Database.EntityFramework.Models;

public partial class User
{
    public Guid Uid { get; set; }

    public DateTime CreateDt { get; set; }

    public DateTime LoginDt { get; set; }

    public string Name { get; set; } = null!;

    public Guid? ProductionSiteUid { get; set; }

    public virtual ProductionSite? ProductionSiteU { get; set; }

    public virtual ICollection<UsersClaimsFk> UsersClaimsFks { get; set; } = new List<UsersClaimsFk>();
}
