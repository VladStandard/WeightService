// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Models;

[Serializable]
public sealed class WsResponseBarCodeModel : SerializeBase
{
    #region Public and private fields, properties, constructor
    [XmlElement(WsWebConstants.Guid)]
    public Guid IdentityValueUid { get; set; }
    [XmlElement] 
    public DateTime CreateDt { get; set; }
    [XmlElement] 
    public DateTime ChangeDt { get; set; }
    [XmlElement] 
    public bool IsMarked { get; set; }
    [XmlElement] 
    public string TypeTop { get; set; }
    [XmlElement] 
    public string ValueTop { get; set; }
    [XmlElement] 
    public string TypeRight { get; set; }
    [XmlElement] 
    public string ValueRight { get; set; }
    [XmlElement] 
    public string TypeBottom { get; set; }
    [XmlElement] 
    public string ValueBottom { get; set; }
    [XmlElement] 
    public Guid PluLabelGuid { get; set; }

    public WsResponseBarCodeModel()
    {
        IdentityValueUid = Guid.Empty;
        CreateDt = DateTime.MinValue;
        ChangeDt = DateTime.MinValue;
        IsMarked = false;
        TypeTop = string.Empty;
        TypeRight = string.Empty;
        TypeBottom = string.Empty;
        ValueTop = string.Empty;
        ValueRight = string.Empty;
        ValueBottom = string.Empty;
        PluLabelGuid = Guid.Empty;
    }
    
    private WsResponseBarCodeModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        IdentityValueUid = new(info.GetString(WsWebConstants.Guid) ?? string.Empty);
        CreateDt = info.GetDateTime(nameof(CreateDt));
        ChangeDt = info.GetDateTime(nameof(ChangeDt));
        IsMarked = info.GetBoolean(nameof(IsMarked));
        TypeTop = info.GetString(nameof(TypeTop)) ?? string.Empty;
        ValueTop = info.GetString(nameof(ValueTop)) ?? string.Empty;
        TypeRight = info.GetString(nameof(TypeRight)) ?? string.Empty;
        ValueRight = info.GetString(nameof(ValueRight)) ?? string.Empty;
        TypeBottom = info.GetString(nameof(TypeBottom)) ?? string.Empty;
        ValueBottom = info.GetString(nameof(ValueBottom)) ?? string.Empty;
        PluLabelGuid = new(info.GetString(nameof(PluLabelGuid)) ?? string.Empty);
    }

    public WsResponseBarCodeModel(WsResponseBarCodeModel item) : base(item)
    {
        IdentityValueUid = item.IdentityValueUid;
        CreateDt = item.CreateDt;
        ChangeDt = item.ChangeDt;
        IsMarked = item.IsMarked;
        TypeTop = item.TypeTop;
        ValueTop = item.ValueTop;
        TypeRight = item.TypeRight;
        ValueRight = item.ValueRight;
        TypeBottom = item.TypeBottom;
        ValueBottom = item.ValueBottom;
        PluLabelGuid = item.PluLabelGuid;
    }

    public WsResponseBarCodeModel(WsSqlBarCodeModel barCode)
    {
        WsSqlBarCodeModel barCodeCopy = new(barCode);
        IdentityValueUid = barCodeCopy.IdentityValueUid;
        CreateDt = barCodeCopy.CreateDt;
        ChangeDt = barCodeCopy.ChangeDt;
        IsMarked = barCodeCopy.IsMarked;
        TypeTop = barCodeCopy.TypeTop;
        ValueTop = barCodeCopy.ValueTop;
        TypeRight = barCodeCopy.TypeRight;
        ValueRight = barCodeCopy.ValueRight;
        TypeBottom = barCodeCopy.TypeBottom;
        ValueBottom = barCodeCopy.ValueBottom;
        PluLabelGuid = barCodeCopy.PluLabel.IdentityValueUid;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
        $"{WsWebConstants.Guid}: {IdentityValueUid}. " +
        $"{nameof(ChangeDt)}: {CreateDt}. " +
        $"{nameof(ChangeDt)}: {ChangeDt}. " +
        $"{nameof(TypeTop)}: {TypeTop}. " +
        $"{nameof(ValueTop)}: {ValueTop}. " +
        $"{nameof(TypeRight)}: {TypeRight}. " +
        $"{nameof(ValueRight)}: {ValueRight}. " +
        $"{nameof(TypeBottom)}: {TypeBottom}. " +
        $"{nameof(ValueBottom)}: {ValueBottom}. " +
        $"{nameof(PluLabelGuid)}: {PluLabelGuid}. ";

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(WsWebConstants.Guid, IdentityValueUid);
        info.AddValue(nameof(CreateDt), CreateDt);
        info.AddValue(nameof(ChangeDt), ChangeDt);
        info.AddValue(nameof(IsMarked), IsMarked);
        info.AddValue(nameof(TypeTop), TypeTop);
        info.AddValue(nameof(ValueTop), ValueTop);
        info.AddValue(nameof(TypeRight), TypeRight);
        info.AddValue(nameof(ValueRight), ValueRight);
        info.AddValue(nameof(TypeBottom), TypeBottom);
        info.AddValue(nameof(ValueBottom), ValueBottom);
        info.AddValue(nameof(PluLabelGuid), PluLabelGuid);
    }

    #endregion
}