using Ws.Database.EntityFramework.Entities.Ref.StorageMethods;
using Ws.Database.EntityFramework.Entities.Ref.Templates;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;

namespace Ws.Database.EntityFramework.Entities.Ref.PluResources;

[Table(SqlTables.PlusResources)]
public sealed class PluResourceEntity : EfEntityBase
{
    [ForeignKey("TEMPLATE_UID")]
    public TemplateEntity Template { get; set; } = new();

    [ForeignKey("STORAGE_METHOD_UID")]
    public StorageMethodEntity StorageMethod { get; set; } = new();

    public void SetPlu(PluEntity plu)
    {
        if (plu.Id != Guid.Empty) return;
        Id = plu.Id;
    }
}