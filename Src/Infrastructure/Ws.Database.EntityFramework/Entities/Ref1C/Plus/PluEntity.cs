using Ws.Database.EntityFramework.Entities.Ref1C.Brands;
using Ws.Database.EntityFramework.Entities.Ref1C.Bundles;
using Ws.Database.EntityFramework.Entities.Ref1C.Clips;

namespace Ws.Database.EntityFramework.Entities.Ref1C.Plus;

[Table(SqlTables.Plus, Schema = SqlSchemas.Ref1C)]
[Index(nameof(Number), Name = $"UQ_{SqlTables.Plus}_NUMBER", IsUnique = true)]
public sealed class PluEntity : EfEntityBase
{
    [Column(SqlColumns.Name), StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Column("FULL_NAME"), StringLength(200)]
    public string FullName { get; set; } = string.Empty;

    [Column("DESCRIPTION"), StringLength(200)]
    public string Description { get; set; } = string.Empty;

    [Column("NUMBER")]
    public short Number { get; set; }

    [Column("SHELF_LIFE_DAYS")]
    public short ShelfLifeDays { get; set; }

    [Column("EAN_13"), StringLength(13)]
    public string Ean13 { get; set; } = string.Empty;

    [Column("ITF_14"), StringLength(14)]
    public string Itf14 { get; set; } = string.Empty;

    [Column("IS_WEIGHT")]
    public bool IsWeight { get; set; }

    [Column(SqlColumns.Weight, TypeName = "decimal(4,3)")]
    public decimal Weight { get; set; }

    [Column("STORAGE_METHOD"), StringLength(32)]
    public string StorageMethod { get; set; } = string.Empty;

    [ForeignKey("BUNDLE_UID"), Column("BUNDLE_UID")]
    public Guid BundleEntityId { get; set; }
    public BundleEntity Bundle { get; set; } = new();

    [ForeignKey("BRAND_UID"), Column("BRAND_UID")]
    public Guid BrandEntityId { get; set; }
    public BrandEntity Brand { get; set; } = new();

    [ForeignKey("CLIP_UID"), Column("CLIP_UID")]
    public Guid ClipEntityId { get; set; }
    public ClipEntity Clip { get; set; } = new();

    [ForeignKey("TEMPLATE_UID"), Column("TEMPLATE_UID")]
    public Guid? TemplateEntityId { get; set; }

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
}