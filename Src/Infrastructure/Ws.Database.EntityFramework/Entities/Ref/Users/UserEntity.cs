using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;

namespace Ws.Database.EntityFramework.Entities.Ref.Users;

public sealed class UserEntity : EfEntityBase
{
    public ProductionSiteEntity ProductionSite { get; set; } = new();
}