using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework.Common;
using Ws.Database.EntityFramework.Constants;

namespace Ws.Database.EntityFramework.Models.Ready;

[Table(SqlTables.Plus)]
[Index(nameof(Number), Name = $"UQ_{SqlTables.Lines}_NUMBER", IsUnique = true)]
[Index(nameof(Uid1C), Name = $"UQ_{SqlTables.Plus}_UID_1C", IsUnique = true)]
public sealed class Plu : EfEntityBase
{
    [Column("UID_1C")]
    public Guid Uid1C { get; set; }
    
    [Column(SqlColumns.Name)]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Column("FULL_NAME")]
    [StringLength(150, MinimumLength = 1, ErrorMessage = "Full name must be between 1 and 150 characters")]
    public string FullName { get; set; } = string.Empty;

    [Column("DESCRIPTION")]
    [StringLength(150, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 150 characters")]
    public string Description { get; set; } = string.Empty;
    
    [Column("NUMBER")]
    [Range(100, 999, ErrorMessage = "Number must be between 100 and 999")]
    public int Number { get; set; }

    [Column("SHELF_LIFE_DAYS")]
    public byte ShelfLifeDays { get; set; }
    
    [Column("EAN_13", TypeName = "varchar")]
    [StringLength(13, MinimumLength = 13, ErrorMessage = "Ean13 must be 13 len")]
    public string Ean13 { get; set; } = string.Empty;

    [Column("ITF_14", TypeName = "varchar")]
    [StringLength(14, MinimumLength = 14, ErrorMessage = "Itf14 must be 14 len")]
    public string Itf14 { get; set; } = string.Empty;

    [Column("IS_WEIGHT")]
    public bool IsWeight { get; set; }

    [ForeignKey("BUNDLE_UID")]
    public Bundle Bundle { get; set; } = new();

    [ForeignKey("BRAND_UID")]
    public Brand Brand { get; set; } = new();

    [ForeignKey("CLIP_UID")]
    public Clip Clip { get; set; } = new();

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
    
    // public virtual ICollection<Label> Labels { get; set; } = new List<Label>();
    //
    // public virtual ICollection<PlusLine> PlusLines { get; set; } = new List<PlusLine>();
    //
    // public virtual ICollection<PlusNestingFk> PlusNestingFks { get; set; } = new List<PlusNestingFk>();
}
