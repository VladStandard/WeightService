using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Constants;

namespace Ws.Database.EntityFramework.Models.Ready;

[Table(SqlTables.ZplResources)]
[Index(nameof(Name), IsUnique = true, Name = $"UQ_{SqlTables.ZplResources}_NAME")] 
public partial class ZplResource : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(64, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters")]
    public string Name { get; private set; } = string.Empty;

    [Column("ZPL")]
    [StringLength(1000, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 1000 characters")]
    public string Zpl { get; private set; } = string.Empty;
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [ReadOnly(true)]
    [Column(SqlColumns.CreateDt)]
    public DateTime CreateDt { get; private set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [ReadOnly(true)]
    [Column(SqlColumns.ChangeDt)]
    public DateTime ChangeDt { get; private set; }
}
