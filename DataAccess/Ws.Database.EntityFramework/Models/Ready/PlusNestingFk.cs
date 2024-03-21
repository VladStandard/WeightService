using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Common;
using Ws.Database.EntityFramework.Constants;

namespace Ws.Database.EntityFramework.Models.Ready;

[Table(SqlTables.PluNesting)]
[Index(nameof(BundleCount), nameof(BoxId), Name = $"UQ_{SqlTables.PluNesting}_BUNDLE_BOX", IsUnique = true)]
public sealed class PluNesting : EfEntityBase
{
    [Column("UID_1C")]
    public Guid Uid1C { get; set; }
   
    [Column("BUNDLE_COUNT")]
    [Range(1, 100, ErrorMessage = "BundleCount must be between 1 and 100")]
    public short BundleCount { get; set; }

    #region Box

    [ForeignKey("BOX_UID"), Column("BOX_UID")]
    public Guid BoxId { get; set;}
    public Box Box { get; set; } = new();

    #endregion

    [ForeignKey("PLU_UID"), Column("PLU_UID")]
    public Guid PluId { get; set; }

    [Column("IS_DEFAULT")]
    public bool IsDefault { get; set; }
    
    #region Date

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [ReadOnly(true)]
    [Column(SqlColumns.CreateDt)]
    public DateTime CreateDt { get; private set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [ReadOnly(true)]
    [Column(SqlColumns.ChangeDt)]
    public DateTime ChangeDt { get; private set; }

    #endregion
}
