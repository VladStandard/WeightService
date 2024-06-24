using ProtoBuf;
using Ws.Domain.Models.ValueTypes;

namespace Ws.Labels.Service.Generate.Models.Cache;

[ProtoContract]
public class BarcodeItemTemplateFromCache
{

    public BarcodeItemTemplateFromCache(BarcodeItem barcodeItem)
    {
        Property = barcodeItem.Property;
        FormatStr = barcodeItem.FormatStr;
        Length = barcodeItem.Length;
        IsConst = barcodeItem.IsConst;
    }

    // FOR PROTOBUF
    public BarcodeItemTemplateFromCache() { }
    [ProtoMember(1)] public string Property { get; set; } = string.Empty;
    [ProtoMember(2)] public string FormatStr { get; set; } = string.Empty;
    [ProtoMember(3)] public ushort Length { get; set; }
    [ProtoMember(4)] public bool IsConst { get; set; }
}