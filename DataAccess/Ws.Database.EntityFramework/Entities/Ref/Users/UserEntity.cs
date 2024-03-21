using Ws.Database.EntityFramework.Entities.Ref.Claims;
using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;

namespace Ws.Database.EntityFramework.Entities.Ref.Users;

[Table(SqlTables.Users)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Users}_NAME", IsUnique = true)]
public sealed class UserEntity : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 32 characters")]
    public string Name { get; set; } = string.Empty;
    
    [ForeignKey("PRODUCTION_SITE_UID")]
    public ProductionSiteEntity? ProductionSite { get; set; }
    
    [Column("LOGIN_DT")]
    public DateTime LoginDt { get; set; }
    
    public List<ClaimEntity> Claims { get; } = [];
    
    public DateTime CreateDt { get; init; }
}
