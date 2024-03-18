using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Common;
using Ws.Database.EntityFramework.Constants;

namespace Ws.Database.EntityFramework.Models.Ready;

[Table(SqlTables.Claims)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Claims}_NAME")]
public sealed class Claim : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(16, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 16 characters")]
    public string Name { get; set; } = string.Empty;
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [ReadOnly(true)]
    [Column(SqlColumns.CreateDt)]
    public DateTime CreateDt { get; private set; }

    // public virtual ICollection<UsersClaimsFk> UsersClaimsFks { get; set; } = new List<UsersClaimsFk>();
}