namespace Ws.StorageCore.Entities.SchemaScale.BarCodes;

[DebuggerDisplay("{ToString()}")]
public class SqlBarCodeEntity : SqlEntityBase
{
    #region Public and private fields, properties, constructor
    public virtual string ValueTop { get; set; }
    public virtual string ValueRight { get; set; }
    public virtual string ValueBottom { get; set; }
    public virtual SqlPluLabelEntity PluLabel { get; set; }
    
    public SqlBarCodeEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        ValueTop = string.Empty;
        ValueRight = string.Empty;
        ValueBottom = string.Empty;
        PluLabel = new();
    }
    
    public SqlBarCodeEntity(SqlPluLabelEntity pluLabel) : this()
    {
        PluLabel = pluLabel;
    }
    
    public SqlBarCodeEntity(SqlBarCodeEntity item) : base(item)
    {
        ValueTop = item.ValueTop;
        ValueRight = item.ValueRight;
        ValueBottom = item.ValueBottom;
        PluLabel = new(item.PluLabel);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(ValueTop)}: {ValueTop}. " +
        $"{nameof(ValueRight)}: {ValueRight}. " +
        $"{nameof(ValueBottom)}: {ValueBottom}. " +
        $"{nameof(PluLabel)}: {PluLabel}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlBarCodeEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(ValueTop, string.Empty) &&
        Equals(ValueRight, string.Empty) &&
        Equals(ValueBottom, string.Empty) &&
        PluLabel.EqualsDefault();

    public override void FillProperties()
    {
        base.FillProperties();
        PluLabel.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(SqlBarCodeEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(ValueTop, item.ValueTop) &&
        Equals(ValueRight, item.ValueRight) &&
        Equals(ValueBottom, item.ValueBottom) &&
        PluLabel.Equals(item.PluLabel);

    #endregion
}