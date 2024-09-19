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
        BarcodeTopTemplate = template.BarcodeTopBody.Select(data => new BarcodeVar(data.Property, data.FormatStr)).ToList();
        BarcodeRightTemplate = template.BarcodeRightBody.Select(data => new BarcodeVar(data.Property, data.FormatStr)).ToList();
        BarcodeBottomTemplate = template.BarcodeBottomBody.Select(data => new BarcodeVar(data.Property, data.FormatStr)).ToList();
    }

    // FOR PROTOBUF
    public TemplateFromCache() { }
}