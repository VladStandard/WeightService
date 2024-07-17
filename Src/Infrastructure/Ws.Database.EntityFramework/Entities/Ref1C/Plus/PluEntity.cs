using Ws.Database.EntityFramework.Entities.Ref1C.Brands;
using Ws.Database.EntityFramework.Entities.Ref1C.Bundles;
using Ws.Database.EntityFramework.Entities.Ref1C.Clips;

namespace Ws.Database.EntityFramework.Entities.Ref1C.Plus;

public sealed class PluEntity : EfEntityBase
{
    public string Name { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public short Number { get; set; }
    public short ShelfLifeDays { get; set; }
    public string Ean13 { get; set; } = string.Empty;
    public string Itf14 { get; set; } = string.Empty;
    public bool IsWeight { get; set; }
    public decimal Weight { get; set; }
    public string StorageMethod { get; set; } = string.Empty;

    //

    public Guid BundleId { get; set; }
    public BundleEntity Bundle { get; set; } = new();

    //

    public Guid BrandId { get; set; }
    public BrandEntity Brand { get; set; } = new();

    //

    public Guid ClipId { get; set; }
    public ClipEntity Clip { get; set; } = new();

    //

    [ForeignKey("TEMPLATE_UID"), Column("TEMPLATE_UID")]
    public Guid? TemplateId { get; set; }

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion

    [NotMapped] public override bool IsNew => CreateDt.Equals(DateTime.MinValue);
}