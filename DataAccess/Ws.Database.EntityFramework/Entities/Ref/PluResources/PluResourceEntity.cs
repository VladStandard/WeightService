using Ws.Database.EntityFramework.Entities.Ref.StorageMethods;
using Ws.Database.EntityFramework.Entities.Ref.Templates;

namespace Ws.Database.EntityFramework.Entities.Ref.PluResources;

[Table(SqlTables.PlusResources)]
public sealed class PluResourceEntity : EfEntityBase
{
    [ForeignKey("TEMPLATE_UID")]
    public TemplateEntity Template { get; set; } = new();

    [ForeignKey("STORAGE_METHOD_UID")]
    public StorageMethodEntity StorageMethod { get; set; } = new();
}