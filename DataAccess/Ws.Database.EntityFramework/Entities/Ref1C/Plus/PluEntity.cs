using Ws.Database.EntityFramework.Entities.Ref.PluResources;
using Ws.Database.EntityFramework.Entities.Ref1C.Brands;
using Ws.Database.EntityFramework.Entities.Ref1C.Bundles;
using Ws.Database.EntityFramework.Entities.Ref1C.Clips;
using Ws.Database.EntityFramework.Entities.Ref1C.Nestings;

namespace Ws.Database.EntityFramework.Entities.Ref1C.Plus;

[Table(SqlTables.Plus)]
[Index(nameof(Number), Name = $"UQ_{SqlTables.Lines}_NUMBER", IsUnique = true)]
[Index(nameof(Uid1C), Name = $"UQ_{SqlTables.Plus}_UID_1C", IsUnique = true)]
public sealed class PluEntity : EfEntityBase
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
    public BundleEntity Bundle { get; set; } = new();

    [ForeignKey("BRAND_UID")]
    public BrandEntity Brand { get; set; } = new();

    [ForeignKey("CLIP_UID")]
    public ClipEntity Clip { get; set; } = new();

    public PluResourceEntity Resource { get; set; } = new();
    
    public ICollection<PluNestingEntity> Nestings { get; set; } = [];
    
    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
    
    // public virtual ICollection<Label> Labels { get; set; } = new List<Label>();
    //
    // public virtual ICollection<PlusLine> PlusLines { get; set; } = new List<PlusLine>();
}
