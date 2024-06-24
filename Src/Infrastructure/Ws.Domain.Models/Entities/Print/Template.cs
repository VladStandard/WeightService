// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.ValueTypes;

namespace Ws.Domain.Models.Entities.Print;

[DebuggerDisplay("{ToString()}")]
public class Template : EntityBase
{
    public virtual short Width { get; set; }
    public virtual short Height { get; set; }
    public virtual bool IsWeight { get; set; }
    public virtual string Body { get; set; } = string.Empty;
    public virtual string Name { get; set; } = string.Empty;
    public virtual IList<BarcodeItem> BarcodeTopTemplate { get; set; } = [];
    public virtual IList<BarcodeItem> BarcodeRightTemplate { get; set; } = [];
    public virtual IList<BarcodeItem> BarcodeBottomTemplate { get; set; } = [];

    public virtual string SizeView => $"{Width}x{Height}";

    protected override bool CastEquals(EntityBase obj)
    {
        Template item = (Template)obj;
        return Equals(Name, item.Name) &&
               Equals(Body, item.Body) &&
               Equals(IsWeight, item.IsWeight) &&
               Equals(Width, item.Width) &&
               Equals(Height, item.Height) &&
               Equals(BarcodeTopTemplate, item.BarcodeTopTemplate) &&
               Equals(BarcodeRightTemplate, item.BarcodeRightTemplate) &&
               Equals(BarcodeBottomTemplate, item.BarcodeBottomTemplate);
    }
}