using Ws.Database.EntityFramework.Entities.Zpl.StorageMethods;
using Ws.Database.EntityFramework.Entities.Zpl.Templates;

namespace Ws.Database.EntityFramework.Entities.Zpl.PluResources;

[Table(SqlTables.PlusResources,  Schema = SqlSchemas.Zpl)]
public sealed class PluResourceEntity : EfEntityBase
{
    [ForeignKey("TEMPLATE_UID")]
    public TemplateEntity Template { get; set; } = new();

    [ForeignKey("STORAGE_METHOD_UID")]
    public StorageMethodEntity StorageMethod { get; set; } = new();
}