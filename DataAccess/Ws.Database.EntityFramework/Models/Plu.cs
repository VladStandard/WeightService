using Ws.Database.EntityFramework.Models.Ready;

namespace Ws.Database.EntityFramework.Models;

/// <summary>
/// PLUS reference
/// </summary>
public partial class Plu
{
    public Guid Uid { get; set; }

    public DateTime CreateDt { get; set; }

    public DateTime ChangeDt { get; set; }

    public int Number { get; set; }

    public string Name { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public byte ShelfLifeDays { get; set; }

    public string Ean13 { get; set; } = null!;

    public string Itf14 { get; set; } = null!;

    public bool IsCheckWeight { get; set; }

    public Guid Uid1c { get; set; }

    public Guid BundleUid { get; set; }

    public Guid BrandUid { get; set; }

    public Guid StorageMethodUid { get; set; }

    public Guid ClipUid { get; set; }

    public virtual Brand BrandU { get; set; } = null!;

    public virtual Bundle BundleU { get; set; } = null!;

    public virtual Clip ClipU { get; set; } = null!;

    public virtual ICollection<Label> Labels { get; set; } = new List<Label>();

    public virtual ICollection<PlusLine> PlusLines { get; set; } = new List<PlusLine>();

    public virtual ICollection<PlusNestingFk> PlusNestingFks { get; set; } = new List<PlusNestingFk>();

    public virtual StorageMethod StorageMethodU { get; set; } = null!;
}
