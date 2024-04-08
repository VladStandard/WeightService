using Ws.Database.EntityFramework.Entities.Ref.Users;

namespace Ws.Database.EntityFramework.Entities.Ref.Claims;

[Table(SqlTables.Claims)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Claims}_NAME", IsUnique = true)]
public sealed class ClaimEntity : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(16)]
    public string Name { get; set; } = string.Empty;

    public List<UserEntity> Users { get; } = [];

    public DateTime CreateDt { get; init; }

    // public virtual ICollection<UsersClaimsFk> UsersClaimsFks { get; set; } = new List<UsersClaimsFk>();
}