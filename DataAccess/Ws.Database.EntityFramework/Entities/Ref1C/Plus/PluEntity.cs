using Ws.Database.EntityFramework.Entities.Zpl.PluResources;
using Ws.Database.EntityFramework.Entities.Ref1C.Brands;
using Ws.Database.EntityFramework.Entities.Ref1C.Bundles;
using Ws.Database.EntityFramework.Entities.Ref1C.Clips;

namespace Ws.Database.EntityFramework.Entities.Ref1C.Plus;

[Table(SqlTables.Plus, Schema = SqlSchemas.Ref1C)]
[Index(nameof(Number), Name = $"UQ_{SqlTables.Plus}_NUMBER", IsUnique = true)]
public sealed class PluEntity : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Column("FULL_NAME")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Full name must be between 1 and 200 characters")]
    public string FullName { get; set; } = string.Empty;

    [Column("DESCRIPTION")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 200 characters")]
    public string Description { get; set; } = string.Empty;

    [Column("NUMBER")]
    [Range(100, 999, ErrorMessage = "Number must be between 100 and 999")]
    public short Number { get; set; }

    [Column("SHELF_LIFE_DAYS")]
    public short ShelfLifeDays { get; set; }

    [Column("EAN_13", TypeName = "varchar")]
    [StringLength(13, MinimumLength = 13, ErrorMessage = "Ean13 must be 13 len")]
    public string Ean13 { get; set; } = string.Empty;

    [Column("ITF_14", TypeName = "varchar")]
    [StringLength(14, MinimumLength = 14, ErrorMessage = "Itf14 must be 14 len")]
    public string Itf14 { get; set; } = string.Empty;

    [Column("IS_WEIGHT")]
    public bool IsWeight { get; set; }

    [Column(SqlColumns.Weight, TypeName = "decimal(4,3)")]
    public decimal Weight { get; set; }

    [ForeignKey("BUNDLE_UID"), Column("BUNDLE_UID")]
    public Guid BundleEntityId { get; set; }
    public BundleEntity Bundle { get; set; } = new();

    [ForeignKey("BRAND_UID"), Column("BRAND_UID")]
    public Guid BrandEntityId { get; set; }
    public BrandEntity Brand { get; set; } = new();

    [ForeignKey("CLIP_UID"), Column("CLIP_UID")]
    public Guid ClipEntityId { get; set; }
    public ClipEntity Clip { get; set; } = new();

    public PluResourceEntity Resource { get; set; } = new();

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion

    [NotMapped] public override bool IsNew => CreateDt.Equals(DateTime.MinValue);

    public PluEntity() { }

    public PluEntity(Guid uid, DateTime updateDt)
    {
        Id = uid;
        ChangeDt = updateDt;
    }

    // public ICollection<PluNestingEntity> Nestings { get; set; } = [];
    // public virtual ICollection<Label> Labels { get; set; } = new List<Label>();
    //
    // public virtual ICollection<PlusLine> PlusLines { get; set; } = new List<PlusLine>();
}