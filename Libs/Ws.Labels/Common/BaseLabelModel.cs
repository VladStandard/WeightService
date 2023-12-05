using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Ws.Labels.Common;

[Serializable]
public class BaseLabelModel : ISerializable
{
    #region DateTime
    
    [XmlIgnore] public DateTime ExpirationDtValue { get; set; } = DateTime.MinValue;
    [XmlIgnore] public DateTime ProductDtValue { get; set; } = DateTime.MinValue;
    
    [XmlIgnore] public string ProductDate => $"{ProductDtValue:yyMMdd}";
    [XmlIgnore] public string ProductTime => $"{ProductDtValue:HHmmss}";
    [XmlIgnore]  public string ProductDateShort => $"{ProductDtValue:yyMM}";
    
    [XmlElement] public string ProductDateValue { get => $"{ProductDtValue:dd.MM.yy}"; set => _ = value; }
    [XmlElement] public string ProductExpirationValue { get => $"{ExpirationDtValue:dd.MM.yy}"; set => _ = value; }
    
    #endregion

    #region Line

    [XmlElement] public int LineNumber { get; set; }
    [XmlElement] public int LineCounter { get; set; }
    [XmlElement] public string LineName { get; set; } = string.Empty;
    [XmlElement] public string LineAddress { get; set; } = string.Empty;
    
    #endregion
    
    #region Plu

    [XmlElement] public int PluNumber { get; set; }
    [XmlElement] public string PluGtin { get; set; } = string.Empty;
    [XmlElement] public string PluFullName { set; get; } = string.Empty;
    [XmlElement] public string PluDescription { get; set; } = string.Empty;

    #endregion
    
    #region Other
    
    [XmlElement] public short Kneading { get; set; }
    
    #endregion

    protected string IntToStr(int value, int charLen) =>  value.ToString().PadLeft(charLen, '0');
    
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        throw new NotImplementedException();
    }
}