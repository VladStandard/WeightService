using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Common;
using Ws.Database.EntityFramework.Constants;

namespace Ws.Database.EntityFramework.Models.Ready;

[Table(SqlTables.Users)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Users}_NAME", IsUnique = true)]
public sealed class User : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 32 characters")]
    public string Name { get; set; } = string.Empty;
    
    [ForeignKey("PRODUCTION_SITE_UID")]
    public ProductionSite? ProductionSite { get; set; }
    
    [Column("LOGIN_DT")]
    public DateTime LoginDt { get; set; }
    
    public List<Claim> Claims { get; } = [];
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [ReadOnly(true)]
    [Column(SqlColumns.CreateDt)]
    public DateTime CreateDt { get; set; }
}
