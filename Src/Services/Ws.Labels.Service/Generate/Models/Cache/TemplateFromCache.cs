using ProtoBuf;
using Ws.Barcodes.Models;
using Ws.Database.EntityFramework.Entities.Zpl.Templates;

namespace Ws.Labels.Service.Generate.Models.Cache;


[ProtoContract]
public class TemplateFromCache
{
    [ProtoMember(1)]
    public string Template { get; set; } = string.Empty;

    [ProtoMember(2)]
    public List<BarcodeVar> BarcodeTopTemplate { get; set; } = [];

    [ProtoMember(3)]
    public List<BarcodeVar> BarcodeRightTemplate { get; set; } = [];

    [ProtoMember(4)]
    public List<BarcodeVar> BarcodeBottomTemplate { get; set; } = [];

    [ProtoMember(5)]
    public short Width { get; set; }

    [ProtoMember(6)]
    public short Height { get; set; }

    [ProtoMember(7)]
    public short Rotate { get; set; }

    public TemplateFromCache(TemplateEntity template)
    {
        Template = template.Body;
        Width = template.Width;
        Height = template.Height;
        Rotate = template.Rotate;
        BarcodeTopTemplate = template.BarcodeTopBody.ConvertAll(data => new BarcodeVar(data.Property, data.FormatStr));
        BarcodeRightTemplate = template.BarcodeRightBody.ConvertAll(data => new BarcodeVar(data.Property, data.FormatStr));
        BarcodeBottomTemplate = template.BarcodeBottomBody.ConvertAll(data => new BarcodeVar(data.Property, data.FormatStr));
    }

    // FOR PROTOBUF
    public TemplateFromCache() { }
}