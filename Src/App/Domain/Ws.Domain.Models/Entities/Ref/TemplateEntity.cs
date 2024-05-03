// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.ValueTypes;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class TemplateEntity : EntityBase
{
    public virtual short Width { get; set; }
    public virtual short Height { get; set; }
    public virtual bool IsWeight { get; set; }
    public virtual string Body { get; set; } = string.Empty;
    public virtual string Name { get; set; } = string.Empty;
    public virtual IList<BarcodeItem> BarcodeTopBody { get; set; } = [];
    public virtual IList<BarcodeItem> BarcodeRightBody { get; set; } = [];
    public virtual IList<BarcodeItem> BarcodeBottomBody { get; set; } = [];

    public virtual string SizeView => $"{Width}x{Height}";

    protected override bool CastEquals(EntityBase obj)
    {
        TemplateEntity item = (TemplateEntity)obj;
        return Equals(Name, item.Name) &&
               Equals(Body, item.Body) &&
               Equals(IsWeight, item.IsWeight) &&
               Equals(Width, item.Width) &&
               Equals(Height, item.Height) &&
               Equals(BarcodeTopBody, item.BarcodeTopBody) &&
               Equals(BarcodeRightBody, item.BarcodeRightBody) &&
               Equals(BarcodeBottomBody, item.BarcodeBottomBody);
    }
}