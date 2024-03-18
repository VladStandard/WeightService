namespace Ws.Database.EntityFramework.Models;

public partial class Template
{
    public Guid Uid { get; set; }

    public DateTime CreateDt { get; set; }

    public DateTime ChangeDt { get; set; }

    public string Name { get; set; } = null!;

    public string Body { get; set; } = null!;

    public virtual ICollection<PlusTemplatesFk> PlusTemplatesFks { get; set; } = new List<PlusTemplatesFk>();
}
