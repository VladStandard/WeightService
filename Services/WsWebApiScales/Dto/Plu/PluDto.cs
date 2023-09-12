using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WsWebApiScales.Dto.Plu;

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
    
    public string Name { get; set; }
    public string FullName { get; set; } 
    public string BoxTypeName { get; set; }
    public string ClipTypeName { get; set; }
    public string PackageTypeName { get; set; }
    
    #endregion

    #region Weight
    
    public decimal BoxTypeWeight { get; set; }
    public decimal PackageTypeWeight { get; set; }
    public decimal ClipTypeWeight { get; set; }

    #endregion

    #region Codes
    
    public string Code { get; set; }
    public string Ean13 { get; set; }
    public string Itf14 { get; set; }
    
    #endregion
    
    #region Other

    public bool IsCheckWeight => MeasurementType.ToLower() == "кг";
    public bool IsGroup { get; set; }
    public int PluNumber { get; set; }
    public short AttachmentsCount { get; set; }
    public string Description { get; set; }
    public string MeasurementType { get; set; }
    public int ShelfLife { get; set; }

    #endregion
    
    
    public PluDto() {}

    public XmlSchema GetSchema() { return null; }
    
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

    public void WriteXml(XmlWriter writer) {}
    
    private static Guid ParseGuidOrDefault(XmlReader reader, string attributeName) => 
        Guid.TryParse(reader.GetAttribute(attributeName), out Guid parsed) ? parsed : Guid.Empty;
    
    private static bool ParseBoolOrDefault(XmlReader reader, string attributeName) =>
        (bool.TryParse(reader.GetAttribute(attributeName), out bool parsed) ? parsed : default)
        ||  ParseStringOrDefault(reader, attributeName) == "1";

    private static decimal ParseDecimalOrDefault(XmlReader reader, string attributeName)
    {
        string? attributeValue = reader.GetAttribute(attributeName)?.Replace(',', '.');
        return decimal.TryParse(attributeValue, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal parsed) ? parsed : default;
    }
    
    private static string ParseStringOrDefault(XmlReader reader, string attributeName) =>
        reader.GetAttribute(attributeName) ?? string.Empty;
    
    private static int ParseIntOrDefault(XmlReader reader, string attributeName) =>
        int.TryParse(reader.GetAttribute(attributeName), out int parsed) ? parsed : default;

}