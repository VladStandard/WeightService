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

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("PluDto");

        #region Default

        WriteXmlElement(writer, "GUID", Uid.ToString());
        WriteXmlElement(writer, nameof(IsMarked), IsMarked ? "1" : "0");

        #endregion

        #region Uid

        WriteXmlElement(writer, nameof(ParentGroupGuid), ParentGroupGuid.ToString());
        WriteXmlElement(writer, nameof(CategoryGuid), CategoryGuid.ToString());
        WriteXmlElement(writer, nameof(BrandGuid), BrandGuid.ToString());
        WriteXmlElement(writer, nameof(GroupGuid), GroupGuid.ToString());
        WriteXmlElement(writer, nameof(BoxTypeGuid), BoxTypeGuid.ToString());
        WriteXmlElement(writer, nameof(ClipTypeGuid), ClipTypeGuid.ToString());
        WriteXmlElement(writer, nameof(PackageTypeGuid), PackageTypeGuid.ToString());

        #endregion

        #region Name

        WriteXmlElement(writer, nameof(Name), Name);
        WriteXmlElement(writer, nameof(FullName), FullName);
        WriteXmlElement(writer, nameof(BoxTypeName), BoxTypeName);
        WriteXmlElement(writer, nameof(ClipTypeName), ClipTypeName);
        WriteXmlElement(writer, nameof(PackageTypeName), PackageTypeName);

        #endregion

        #region Weight

        WriteXmlElement(writer, nameof(BoxTypeWeight), BoxTypeWeight.ToString(CultureInfo.InvariantCulture));
        WriteXmlElement(writer, nameof(PackageTypeWeight), PackageTypeWeight.ToString(CultureInfo.InvariantCulture));
        WriteXmlElement(writer, nameof(ClipTypeWeight), ClipTypeWeight.ToString(CultureInfo.InvariantCulture));

        #endregion

        #region Codes

        WriteXmlElement(writer, nameof(Code), Code);
        WriteXmlElement(writer, nameof(Ean13), Ean13);
        WriteXmlElement(writer, nameof(Itf14), Itf14);

        #endregion

        #region Other

        WriteXmlElement(writer, nameof(IsGroup), IsGroup ? "1" : "0");
        WriteXmlElement(writer, nameof(MeasurementType), MeasurementType);
        WriteXmlElement(writer, nameof(Description), Description);
        WriteXmlElement(writer, nameof(AttachmentsCount), AttachmentsCount.ToString());
        WriteXmlElement(writer, nameof(PluNumber), PluNumber.ToString());
        WriteXmlElement(writer, nameof(ShelfLife), ShelfLife.ToString());

        #endregion

        writer.WriteEndElement();
    }

    private static void WriteXmlElement(XmlWriter writer, string elementName, string elementValue)
    {
        writer.WriteStartAttribute(elementName);
        writer.WriteString(elementValue);
        writer.WriteEndAttribute();
    }
        
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