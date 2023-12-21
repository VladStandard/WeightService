using System.Xml;
using System.Xml.Schema;
using Ws.Shared.TypeUtils;

namespace Ws.WebApiScales.Dto.Plu;

[Serializable]
public class PluDto : IXmlSerializable 
{
    #region Default
    
    public Guid Uid { get; set; }
    public bool IsMarked { get; set; }

    #endregion
    
    #region UID
    
    public Guid ParentGroupGuid { get; set; }
    public Guid CategoryGuid { get; set; }
    public Guid BrandGuid { get; set; }
    public Guid GroupGuid { get; set; }
    public Guid BoxTypeGuid { get; set; }
    public Guid ClipTypeGuid { get; set; }
    public Guid PackageTypeGuid { get; set; }
    
    #endregion
    
    #region Name

    public string Name { get; set; } = string.Empty;
    public string FullName { get; set; }  = string.Empty;
    public string BoxTypeName { get; set; } = string.Empty;
    public string ClipTypeName { get; set; } = string.Empty;
    public string PackageTypeName { get; set; } = string.Empty;
    
    #endregion

    #region Weight
    
    public decimal BoxTypeWeight { get; set; }
    public decimal PackageTypeWeight { get; set; }
    public decimal ClipTypeWeight { get; set; }

    #endregion

    #region Codes
    
    public string Code { get; set; } = string.Empty;
    public string Ean13 { get; set; } = string.Empty;
    public string Itf14 { get; set; } = string.Empty;
    
    #endregion
    
    #region Other

    public bool IsCheckWeight => MeasurementType.ToLower() == "кг";
    public bool IsGroup { get; set; }
    public int PluNumber { get; set; }
    public short AttachmentsCount { get; set; }
    public string Description { get; set; } = string.Empty;
    public string MeasurementType { get; set; } = string.Empty;
    public int ShelfLife { get; set; }

    #endregion

    #region IXmlSerializable

    public XmlSchema? GetSchema() => null;
    public void ReadXml(XmlReader reader)
    {

    #region Default binding

    Uid = ParseGuidOrDefault(reader, "GUID");
    IsMarked = ParseBoolOrDefault(reader, nameof(IsMarked));

    #endregion

    #region Uid binding

    ParentGroupGuid = ParseGuidOrDefault(reader, nameof(ParentGroupGuid));
    CategoryGuid = ParseGuidOrDefault(reader, nameof(CategoryGuid));
    BrandGuid = ParseGuidOrDefault(reader, nameof(BrandGuid));
    GroupGuid = ParseGuidOrDefault(reader, nameof(GroupGuid));
    BoxTypeGuid = ParseGuidOrDefault(reader, nameof(BoxTypeGuid));
    ClipTypeGuid = ParseGuidOrDefault(reader, nameof(ClipTypeGuid));
    PackageTypeGuid = ParseGuidOrDefault(reader, nameof(PackageTypeGuid));

    #endregion

    #region Name binding

    Name = ParseStringOrDefault(reader, nameof(Name));
    FullName = ParseStringOrDefault(reader, nameof(FullName));
    BoxTypeName = ParseStringOrDefault(reader, nameof(BoxTypeName));
    ClipTypeName = ParseStringOrDefault(reader, nameof(ClipTypeName));
    PackageTypeName = ParseStringOrDefault(reader, nameof(PackageTypeName));

    #endregion

    #region Weight binding

    BoxTypeWeight = ParseDecimalOrDefault(reader, nameof(BoxTypeWeight));
    PackageTypeWeight = ParseDecimalOrDefault(reader, nameof(PackageTypeWeight));
    ClipTypeWeight = ParseDecimalOrDefault(reader, nameof(ClipTypeWeight));

    #endregion

    #region Codes binding

    Code = ParseStringOrDefault(reader, nameof(Code));
    Ean13 = ParseStringOrDefault(reader, nameof(Ean13));
    Itf14 = ParseStringOrDefault(reader, nameof(Itf14));

    #endregion

    #region Other binging

    IsGroup = ParseBoolOrDefault(reader, nameof(IsGroup));
    MeasurementType = ParseStringOrDefault(reader, nameof(MeasurementType));
    Description = ParseStringOrDefault(reader, nameof(Description));
    AttachmentsCount = (short)ParseIntOrDefault(reader, nameof(AttachmentsCount));
    PluNumber = ParseIntOrDefault(reader, nameof(PluNumber));
    ShelfLife = ParseIntOrDefault(reader, nameof(ShelfLife));

    #endregion

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
        ||  ParseStringOrDefault(reader, attributeName) == "1";
    
    private static string ParseStringOrDefault(XmlReader reader, string attributeName) =>
        reader.GetAttribute(attributeName) ?? string.Empty;

    private static decimal ParseDecimalOrDefault(XmlReader reader, string attributeName) =>
        DecimalUtils.ConvertStrToDecimal(reader.GetAttribute(attributeName));
    
    private static int ParseIntOrDefault(XmlReader reader, string attributeName) =>
        IntUtils.ConvertStrToIntOrMin(reader.GetAttribute(attributeName));

    #endregion
}