// ReSharper disable VirtualMemberCallInConstructor

namespace WsStorageCore.Entities.SchemaScale.PlusLabels;

/// <summary>
/// Table "PLUS_LABELS".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluLabelEntity : WsSqlTableBase
{
    #region Public and private fields, properties, constructor
    public virtual WsSqlPluWeighingEntity? PluWeighing { get; set; }
    public virtual WsSqlPluScaleEntity PluScale { get; set; }
    public virtual string Zpl { get; set; }
    public virtual DateTime ProductDt { get; set; }
    public virtual DateTime ExpirationDt
    {
        get => PluScale.IsNew ? DateTime.MinValue : ProductDt.AddDays(PluScale.Plu.ShelfLifeDays);
        set => _ = value;
    }

    public WsSqlPluLabelEntity() : base(WsSqlEnumFieldIdentity.Uid)
    {
        PluWeighing = null;
        PluScale = new();
        Zpl = string.Empty;
        ProductDt = DateTime.MinValue;
        ExpirationDt = DateTime.MinValue;
    }

    public WsSqlPluLabelEntity(WsSqlPluLabelEntity item) : base(item)
    {
        IsMarked = item.IsMarked;
        PluWeighing = item.PluWeighing is null ? null : new(item.PluWeighing);
        PluScale = new(item.PluScale);
        Zpl = item.Zpl;
        ProductDt = item.ProductDt;
        ExpirationDt = item.ExpirationDt;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(ProductDt)}: {ProductDt}. " +
        $"{nameof(PluScale.Plu.Number)}: {PluScale.Plu.Number}. " +
        $"{nameof(Zpl)}: {Zpl.Length}.";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluLabelEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Zpl, string.Empty) &&
        Equals(ProductDt, DateTime.MinValue) &&
        (PluWeighing is null || PluWeighing.EqualsDefault()) &&
        PluScale.EqualsDefault();

    public override void FillProperties()
    {
        base.FillProperties();
        Zpl = WsLocaleCore.Sql.SqlItemFieldZpl;
        ProductDt = DateTime.Now;
        PluWeighing?.FillProperties();
        PluScale.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluLabelEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Zpl, item.Zpl) &&
        Equals(ProductDt, item.ProductDt) &&
        Equals(ExpirationDt, item.ExpirationDt) &&
        (PluWeighing is null && item.PluWeighing is null || PluWeighing is not null &&
            item.PluWeighing is not null && PluWeighing.Equals(item.PluWeighing)) &&
        PluScale.Equals(item.PluScale);

    #endregion
}