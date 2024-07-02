using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Print;

public class LabelZpl : EntityBase
{
    public virtual string Zpl { get; set; } = string.Empty;
    public virtual short Width { get; set; }
    public virtual short Height { get; set; }
    public virtual short Rotate { get; set; }
}