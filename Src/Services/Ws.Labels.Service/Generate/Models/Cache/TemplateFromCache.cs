using ProtoBuf;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Labels.Service.Generate.Models.Cache;


[ProtoContract]
public class TemplateFromCache
{
    [ProtoMember(1)]
    public string Template { get; set; } = string.Empty;

    [ProtoMember(2)]
    public List<BarcodeItemTemplateFromCache> BarcodeTopTemplate { get; set; } = [];

    [ProtoMember(3)]
    public List<BarcodeItemTemplateFromCache> BarcodeRightTemplate { get; set; } = [];

    [ProtoMember(4)]
    public List<BarcodeItemTemplateFromCache> BarcodeBottomTemplate { get; set; } = [];

    [ProtoMember(5)]
    public short Width { get; set; }

    [ProtoMember(6)]
    public short Height { get; set; }

    [ProtoMember(7)]
    public short Rotate { get; set; }

    public TemplateFromCache(Template template)
    {
        Template = template.Body;
        Width = template.Width;
        Height = template.Height;
        Rotate = template.Rotate;
        BarcodeTopTemplate.AddRange(template.BarcodeTopTemplate.Select(data => new BarcodeItemTemplateFromCache(data)));
        BarcodeRightTemplate.AddRange(template.BarcodeRightTemplate.Select(data => new BarcodeItemTemplateFromCache(data)));
        BarcodeBottomTemplate.AddRange(template.BarcodeBottomTemplate.Select(data => new BarcodeItemTemplateFromCache(data)));
    }

    // FOR PROTOBUF
    public TemplateFromCache() { }
}
