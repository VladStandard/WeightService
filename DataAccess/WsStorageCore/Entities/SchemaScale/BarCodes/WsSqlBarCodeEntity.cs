namespace WsStorageCore.Entities.SchemaScale.BarCodes;

[DebuggerDisplay("{ToString()}")]
public class WsSqlBarCodeEntity : WsSqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual string TypeTop { get; set; }
    public virtual string ValueTop { get; set; }
    public virtual string TypeRight { get; set; }
    public virtual string ValueRight { get; set; }
    public virtual string TypeBottom { get; set; }
    public virtual string ValueBottom { get; set; }
    public virtual WsSqlPluLabelEntity PluLabel { get; set; }
    
    public WsSqlBarCodeEntity() : base(WsSqlEnumFieldIdentity.Uid)
    {
        TypeTop = string.Empty;
        ValueTop = string.Empty;
        TypeRight = string.Empty;
        ValueRight = string.Empty;
        TypeBottom = string.Empty;
        ValueBottom = string.Empty;
        PluLabel = new();
    }
    
    public WsSqlBarCodeEntity(WsSqlPluLabelEntity pluLabel) : this()
    {
        PluLabel = pluLabel;
    }
    
    public WsSqlBarCodeEntity(WsSqlBarCodeEntity item) : base(item)
    {
        TypeTop = item.TypeTop;
        ValueTop = item.ValueTop;
        TypeRight = item.TypeRight;
        ValueRight = item.ValueRight;
        TypeBottom = item.TypeBottom;
        ValueBottom = item.ValueBottom;
        PluLabel = new(item.PluLabel);
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
        return Equals((WsSqlBarCodeEntity)obj);
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

    public override void FillProperties()
    {
        base.FillProperties();
        TypeTop = WsSqlBarcodeType.Default.ToString();
        TypeRight = WsSqlBarcodeType.Default.ToString();
        TypeBottom = WsSqlBarcodeType.Default.ToString();
        PluLabel.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlBarCodeEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(TypeTop, item.TypeTop) &&
        Equals(ValueTop, item.ValueTop) &&
        Equals(TypeRight, item.TypeRight) &&
        Equals(ValueRight, item.ValueRight) &&
        Equals(TypeBottom, item.TypeBottom) &&
        Equals(ValueBottom, item.ValueBottom) &&
        PluLabel.Equals(item.PluLabel);

    #endregion
}