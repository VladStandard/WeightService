using System.Runtime.Serialization;
using System.Xml.Serialization;
using Ws.Shared.TypeUtils;

namespace Ws.Labels.Service.Features.PrintLabel.Common;

[Serializable]
public class XmlLabelBaseModel : ISerializable
{
    #region XmlIgnore

    [XmlIgnore] public short Kneading { get; set; }
    [XmlIgnore] public DateTime ExpirationDtValue { get; set; } = DateTime.MinValue;
    [XmlIgnore] public DateTime ProductDtValue { get; set; } = DateTime.MinValue;
    [XmlIgnore] public string ProductDate => $"{ProductDtValue:yyMMdd}";
    [XmlIgnore] public string ProductTime => $"{ProductDtValue:HHmmss}";
    [XmlIgnore] public string ProductDateShort => $"{ProductDtValue:yyMM}";

    #endregion

    #region Line

    [XmlElement] public int LineNumber { get; set; }
    [XmlElement] public int LineCounter { get; set; }
    [XmlElement] public string LineName { get; set; } = string.Empty;
    [XmlElement] public string LineAddress { get; set; } = string.Empty;

    #endregion

    #region Plu

    [XmlElement] public short PluNumber { get; set; }
    [XmlElement] public string PluGtin { get; set; } = string.Empty;
    [XmlElement] public string PluFullName { set; get; } = string.Empty;
    [XmlElement] public string PluDescription { get; set; } = string.Empty;

    #endregion

    #region Other

    [XmlElement] public string ProductDateStr { get => $"{ProductDtValue:dd.MM.yyyy}"; set => _ = value; }
    [XmlElement] public string ExpirationDateStr { get => $"{ExpirationDtValue:dd.MM.yyyy}"; set => _ = value; }
    [XmlElement] public string KneadingStr { get => IntUtils.ToStringToLen(Kneading, 3); set => _ = value; }

    #endregion

    #region Barcodes

    [XmlElement] public virtual string BarCodeTop { get; set; } = string.Empty;
    [XmlElement] public virtual string BarCodeRight { get; set; } = string.Empty;
    [XmlElement] public virtual string BarCodeBottom { get; set; } = string.Empty;

    #endregion

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        throw new NotImplementedException();
    }
}