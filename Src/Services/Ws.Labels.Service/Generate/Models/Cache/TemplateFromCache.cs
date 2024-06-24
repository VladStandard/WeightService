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

    public TemplateFromCache(Template template)
    {
        Template = template.Body;
        BarcodeTopTemplate.AddRange(template.BarcodeTopTemplate.Select(data => new BarcodeItemTemplateFromCache(data)));
        BarcodeRightTemplate.AddRange(template.BarcodeRightTemplate.Select(data => new BarcodeItemTemplateFromCache(data)));
        BarcodeBottomTemplate.AddRange(template.BarcodeBottomTemplate.Select(data => new BarcodeItemTemplateFromCache(data)));
    }

    // FOR PROTOBUF
    public TemplateFromCache() { }
}
