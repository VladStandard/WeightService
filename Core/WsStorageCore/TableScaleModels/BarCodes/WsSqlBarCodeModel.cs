// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.BarCodes;

/// <summary>
/// Table "BARCODES".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlBarCodeModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual string TypeTop { get; set; }
    [XmlElement] public virtual string ValueTop { get; set; }
    [XmlElement] public virtual string TypeRight { get; set; }
    [XmlElement] public virtual string ValueRight { get; set; }
    [XmlElement] public virtual string TypeBottom { get; set; }
    [XmlElement] public virtual string ValueBottom { get; set; }
    [XmlIgnore] public virtual WsSqlPluLabelModel PluLabel { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlBarCodeModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        TypeTop = string.Empty;
        ValueTop = string.Empty;
        TypeRight = string.Empty;
        ValueRight = string.Empty;
        TypeBottom = string.Empty;
        ValueBottom = string.Empty;
        PluLabel = new();
    }

    /// <summary>
    /// Constructor with parameters.
    /// </summary>
    /// <param name="pluLabel"></param>
    public WsSqlBarCodeModel(WsSqlPluLabelModel pluLabel) : this()
    {
        PluLabel = pluLabel;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlBarCodeModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        TypeTop = info.GetString(nameof(TypeTop));
        ValueTop = info.GetString(nameof(ValueTop));
        TypeRight = info.GetString(nameof(TypeRight));
        ValueRight = info.GetString(nameof(ValueRight));
        TypeBottom = info.GetString(nameof(TypeBottom));
        ValueBottom = info.GetString(nameof(ValueBottom));
        PluLabel = (WsSqlPluLabelModel)info.GetValue(nameof(PluLabel), typeof(WsSqlPluLabelModel));
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(TypeTop)}: {TypeTop}. " +
        $"{nameof(ValueTop)}: {ValueTop}. " +
        $"{nameof(TypeRight)}: {TypeRight}. " +
        $"{nameof(ValueRight)}: {ValueRight}. " +
        $"{nameof(TypeBottom)}: {TypeBottom}. " +
        $"{nameof(ValueBottom)}: {ValueBottom}. " +
        $"{nameof(PluLabel)}: {PluLabel}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlBarCodeModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(TypeTop, string.Empty) &&
        Equals(ValueTop, string.Empty) &&
        Equals(TypeRight, string.Empty) &&
        Equals(ValueRight, string.Empty) &&
        Equals(TypeBottom, string.Empty) &&
        Equals(ValueBottom, string.Empty) &&
        PluLabel.EqualsDefault();

    public override object Clone()
    {
        WsSqlBarCodeModel item = new();
        item.CloneSetup(base.CloneCast());
        item.TypeTop = TypeTop;
        item.ValueTop = ValueTop;
        item.TypeRight = TypeRight;
        item.ValueRight = ValueRight;
        item.TypeBottom = TypeBottom;
        item.ValueBottom = ValueBottom;
        item.PluLabel = PluLabel.CloneCast();
        return item;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(TypeTop), TypeTop);
        info.AddValue(nameof(ValueTop), ValueTop);
        info.AddValue(nameof(TypeRight), TypeRight);
        info.AddValue(nameof(ValueRight), ValueRight);
        info.AddValue(nameof(TypeBottom), TypeBottom);
        info.AddValue(nameof(ValueBottom), ValueBottom);
        info.AddValue(nameof(PluLabel), PluLabel);
    }

    public override void ClearNullProperties()
    {
        //if (PluLabel is not null && PluLabel.Identity.EqualsDefault())
        // PluLabel = null;
    }

    public override void FillProperties()
    {
        base.FillProperties();
        TypeTop = WsSqlBarcodeType.Default.ToString();
        ValueTop = WsLocaleCore.Sql.SqlItemFieldValue;
        TypeRight = WsSqlBarcodeType.Default.ToString();
        ValueRight = WsLocaleCore.Sql.SqlItemFieldValue;
        TypeBottom = WsSqlBarcodeType.Default.ToString();
        ValueBottom = WsLocaleCore.Sql.SqlItemFieldValue;
        PluLabel.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlBarCodeModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(TypeTop, item.TypeTop) &&
        Equals(ValueTop, item.ValueTop) &&
        Equals(TypeRight, item.TypeRight) &&
        Equals(ValueRight, item.ValueRight) &&
        Equals(TypeBottom, item.TypeBottom) &&
        Equals(ValueBottom, item.ValueBottom) &&
        PluLabel.Equals(item.PluLabel);

    public new virtual WsSqlBarCodeModel CloneCast() => (WsSqlBarCodeModel)Clone();

    #endregion
}