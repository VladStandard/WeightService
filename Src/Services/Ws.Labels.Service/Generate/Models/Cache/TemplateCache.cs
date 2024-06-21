using ProtoBuf;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Labels.Service.Generate.Models.Cache;


[ProtoContract]
public class TemplateCache
{
    [ProtoMember(1)]
    public string Body { get; set; } = string.Empty;

    [ProtoMember(2)]
    public List<BarcodeItemCache> BarcodeTopBody { get; set; } = [];

    [ProtoMember(3)]
    public List<BarcodeItemCache> BarcodeRightBody { get; set; } = [];

    [ProtoMember(4)]
    public List<BarcodeItemCache> BarcodeBottomBody { get; set; } = [];

    public TemplateCache(Template template)
    {
        Body = template.Body;
        BarcodeTopBody.AddRange(template.BarcodeTopBody.Select(data => new BarcodeItemCache(data)));
        BarcodeRightBody.AddRange(template.BarcodeRightBody.Select(data => new BarcodeItemCache(data)));
        BarcodeBottomBody.AddRange(template.BarcodeBottomBody.Select(data => new BarcodeItemCache(data)));
    }

    // FOR PROTOBUF
    public TemplateCache() { }
}
