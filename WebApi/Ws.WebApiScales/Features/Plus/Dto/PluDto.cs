using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Ws.Shared.TypeUtils;

namespace Ws.WebApiScales.Features.Plus.Dto;

[Serializable]
public sealed class PluDto : IXmlSerializable
{
    public Guid Uid { get; set; }
    public bool IsDelete { get; set; }
    public string Name { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Number { get; set; }
    public short BundleCount { get; set; }
    public int ShelfLifeDays { get; set; }
    public Guid BrandUid { get; set; }
    public Guid BoxUid { get; set; }
    public Guid ClipUid { get; set; }
    public Guid BundleUid { get; set; }
    public string Ean13 { get; set; } = string.Empty;
    public string Itf14 { get; set; } = string.Empty;
    public bool IsCheckWeight { get; set; }
    public string StorageMethod { get; set; } = string.Empty;

    #region IXmlSerializable

    public XmlSchema? GetSchema() => null;

    public void ReadXml(XmlReader reader)
    {
        Uid = ParseGuidOrDefault(reader, nameof(Uid));
        IsDelete = ParseBoolOrDefault(reader, nameof(IsDelete));
        IsCheckWeight = ParseBoolOrDefault(reader, nameof(IsCheckWeight));

        BrandUid = ParseGuidOrDefault(reader, nameof(BrandUid));
        BoxUid = ParseGuidOrDefault(reader, nameof(BoxUid));
        ClipUid = ParseGuidOrDefault(reader, nameof(ClipUid));
        BundleUid = ParseGuidOrDefault(reader, nameof(BundleUid));

        Name = ParseStringOrDefault(reader, nameof(Name));
        FullName = ParseStringOrDefault(reader, nameof(FullName));
        Number = ParseIntOrDefault(reader, nameof(Number));
        Description = ParseStringOrDefault(reader, nameof(Description));

        Ean13 = ParseStringOrDefault(reader, nameof(Ean13));
        Itf14 = ParseStringOrDefault(reader, nameof(Itf14));
        StorageMethod = ParseStringOrDefault(reader, nameof(StorageMethod));

        BundleCount = (short)ParseIntOrDefault(reader, nameof(BundleCount));
        ShelfLifeDays = ParseIntOrDefault(reader, nameof(ShelfLifeDays));

        reader.Read();
    }

    public void WriteXml(XmlWriter writer)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Utils

    private static Guid ParseGuidOrDefault(XmlReader reader, string attributeName) =>
        Guid.TryParse(reader.GetAttribute(attributeName), out Guid parsed) ? parsed : Guid.Empty;

    private static bool ParseBoolOrDefault(XmlReader reader, string attributeName) =>
        (bool.TryParse(reader.GetAttribute(attributeName), out bool parsed) ? parsed : default)
        || ParseStringOrDefault(reader, attributeName) == "1";

    private static string ParseStringOrDefault(XmlReader reader, string attributeName) =>
        reader.GetAttribute(attributeName) ?? string.Empty;

    private static decimal ParseDecimalOrDefault(XmlReader reader, string attributeName) =>
        DecimalUtils.ConvertStrToDecimal(reader.GetAttribute(attributeName));

    private static int ParseIntOrDefault(XmlReader reader, string attributeName) =>
        IntUtils.ConvertStrToIntOrMin(reader.GetAttribute(attributeName));

    #endregion
}