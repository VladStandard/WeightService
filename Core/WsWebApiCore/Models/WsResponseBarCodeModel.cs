// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Models;

[Serializable]
public class WsResponseBarCodeModel : SerializeBase, ICloneable
{
    #region Public and private fields, properties, constructor
    [XmlElement(WsWebConstants.Guid)]
    public virtual Guid IdentityValueUid { get; set; }
    [XmlElement] public virtual DateTime CreateDt { get; set; }
    [XmlElement] public virtual DateTime ChangeDt { get; set; }
    [XmlElement] public virtual bool IsMarked { get; set; }
    [XmlElement] public virtual string TypeTop { get; set; }
    [XmlElement] public virtual string ValueTop { get; set; }
    [XmlElement] public virtual string TypeRight { get; set; }
    [XmlElement] public virtual string ValueRight { get; set; }
    [XmlElement] public virtual string TypeBottom { get; set; }
    [XmlElement] public virtual string ValueBottom { get; set; }
    [XmlElement] public virtual Guid PluLabelGuid { get; set; }

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

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsResponseBarCodeModel(SerializationInfo info, StreamingContext context) : base(info, context)
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

    #endregion

    #region Public and private methods

    public override string ToString() =>
        $"{WsWebConstants.Guid}: {IdentityValueUid}. " +
        $"{nameof(ChangeDt)}: {CreateDt}. " +
        $"{nameof(ChangeDt)}: {ChangeDt}. " +
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(TypeTop)}: {TypeTop}. " +
        $"{nameof(ValueTop)}: {ValueTop}. " +
        $"{nameof(TypeRight)}: {TypeRight}. " +
        $"{nameof(ValueRight)}: {ValueRight}. " +
        $"{nameof(TypeBottom)}: {TypeBottom}. " +
        $"{nameof(ValueBottom)}: {ValueBottom}. " +
        $"{nameof(PluLabelGuid)}: {PluLabelGuid}. ";

    public object Clone()
    {
        WsResponseBarCodeModel item = new()
        {
            IdentityValueUid = IdentityValueUid,
            CreateDt = CreateDt,
            ChangeDt = ChangeDt,
            IsMarked = IsMarked,
            TypeTop = TypeTop,
            ValueTop = ValueTop,
            TypeRight = TypeRight,
            ValueRight = ValueRight,
            TypeBottom = TypeBottom,
            ValueBottom = ValueBottom,
            PluLabelGuid = PluLabelGuid
        };
        return item;
    }

    public virtual WsResponseBarCodeModel CloneCast() => (WsResponseBarCodeModel)Clone();

    public virtual WsResponseBarCodeModel CloneCast(BarCodeModel barCode)
    {
        WsResponseBarCodeModel item = new()
        {
            IdentityValueUid = barCode.IdentityValueUid,
            CreateDt = barCode.CreateDt,
            ChangeDt = barCode.ChangeDt,
            IsMarked = barCode.IsMarked,
            TypeTop = barCode.TypeTop,
            ValueTop = barCode.ValueTop,
            TypeRight = barCode.TypeRight,
            ValueRight = barCode.ValueRight,
            TypeBottom = barCode.TypeBottom,
            ValueBottom = barCode.ValueBottom,
            PluLabelGuid = barCode.PluLabel.IdentityValueUid
        };
        return item;
    }

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