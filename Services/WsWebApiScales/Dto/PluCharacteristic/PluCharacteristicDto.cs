﻿using System.Xml.Serialization;
namespace WsWebApiScales.Dto.PluCharacteristic;

[Serializable]
public class PluCharacteristicDto
{
    [XmlAttribute("GUID")]
    public Guid Guid { get; set; }

    [XmlAttribute("IsMarked")]
    public bool IsMarked { get; set; }

    [XmlAttribute("NomenclatureGuid")]
    public Guid PluGuid { get; set; }

    [XmlAttribute("Name")]
    public string Name { get; set; } = string.Empty;

    [XmlAttribute("AttachmentsCount")]
    public string AttachmentsCount { get; set; } = string.Empty;

    public int AttachmentsCountAsInt => ParseIntOrMin(AttachmentsCount);
    
    private static int ParseIntOrMin(string value)
    {
        return int.TryParse(value, out int parsed) ? parsed : int.MinValue;
    }
}