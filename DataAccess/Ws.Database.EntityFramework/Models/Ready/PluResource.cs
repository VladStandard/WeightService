using System.ComponentModel.DataAnnotations.Schema;
using Ws.Database.EntityFramework.Common;
using Ws.Database.EntityFramework.Constants;

namespace Ws.Database.EntityFramework.Models.Ready;

[Table(SqlTables.PlusResources)]
public sealed class PluResource : EfEntityBase
{
    [ForeignKey("TEMPLATE_UID")]
    public Template Template { get; set; } = new();

    [ForeignKey("STORAGE_METHOD_UID")]
    public StorageMethod StorageMethod { get; set; } = new();

    public void SetPlu(Plu plu)
    {
        if (plu.Id != Guid.Empty) return;
        Id = plu.Id;
    }
}
