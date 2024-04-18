using Ws.Database.EntityFramework.Entities.Ref.Claims;
using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;

namespace Ws.Database.EntityFramework.Entities.Ref.Users;

[Table(SqlTables.Users, Schema = SqlSchemas.Ref)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Users}_NAME", IsUnique = true)]
public sealed class UserEntity : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(32)]
    public string Name { get; set; } = string.Empty;

    [ForeignKey("PRODUCTION_SITE_UID")]
    public ProductionSiteEntity? ProductionSite { get; set; }

    [Column("LOGIN_DT")]
    public DateTime LoginDt { get; set; }

    public List<ClaimEntity> Claims { get; } = [];

    public DateTime CreateDt { get; init; }
}