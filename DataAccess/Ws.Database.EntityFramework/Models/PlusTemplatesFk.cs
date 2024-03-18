using Ws.Database.EntityFramework.Models.Ready;

namespace Ws.Database.EntityFramework.Models;

/// <summary>
/// PLUS_TEMPLATES_FK reference
/// </summary>
public partial class PlusTemplatesFk
{
    public Guid Uid { get; set; }

    public DateTime CreateDt { get; set; }

    public DateTime ChangeDt { get; set; }

    public Guid PluUid { get; set; }

    public Guid TemplateUid { get; set; }

    public virtual Template TemplateU { get; set; } = null!;
}
