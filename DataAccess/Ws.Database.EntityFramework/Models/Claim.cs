namespace Ws.Database.EntityFramework.Models;

public partial class Claim
{
    public Guid Uid { get; set; }

    public DateTime CreateDt { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<UsersClaimsFk> UsersClaimsFks { get; set; } = new List<UsersClaimsFk>();
}
