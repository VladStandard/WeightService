using Ws.Database.EntityFramework.Models.Ready;

namespace Ws.Database.EntityFramework.Models;

public partial class UsersClaimsFk
{
    public Guid Uid { get; set; }

    public DateTime CreateDt { get; set; }

    public Guid UserUid { get; set; }

    public Guid ClaimUid { get; set; }

    public virtual Claim ClaimU { get; set; } = null!;

    public virtual User UserU { get; set; } = null!;
}
