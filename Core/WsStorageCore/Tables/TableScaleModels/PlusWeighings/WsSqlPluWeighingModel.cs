// ReSharper disable VirtualMemberCallInConstructor

namespace WsStorageCore.Tables.TableScaleModels.PlusWeighings;

[DebuggerDisplay("{ToString()}")]
public class WsSqlPluWeighingModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual WsSqlPluScaleModel PluScale { get; set; }
    public virtual WsSqlProductSeriesModel? Series { get; set; }
    public virtual short Kneading { get; set; }
    public virtual string Sscc { get; set; }
    public virtual decimal NettoWeight { get; set; }
    public virtual decimal WeightTare { get; set; }
    public virtual int RegNum { get; set; }
    
    public WsSqlPluWeighingModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        PluScale = new();
        Series = null;
        Kneading = 0;
        Sscc = string.Empty;
        NettoWeight = 0;
        WeightTare = 0;
        RegNum = 0;
    }

    public WsSqlPluWeighingModel(WsSqlPluWeighingModel item) : base(item)
    {
        PluScale = new(item.PluScale);
        Series = item.Series is null ? null : new(item.Series);
        Kneading = item.Kneading;
        Sscc = item.Sscc;
        NettoWeight = item.NettoWeight;
        WeightTare = item.WeightTare;
        RegNum = item.RegNum;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Kneading)}: {Kneading}. " +
        $"{nameof(PluScale)}: {PluScale}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluWeighingModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Kneading, default(short)) &&
        Equals(Sscc, string.Empty) &&
        Equals(NettoWeight, default(decimal)) &&
        Equals(WeightTare, default(decimal)) &&
        Equals(RegNum, default(int)) &&
        PluScale.EqualsDefault() &&
        (Series is null || Series.EqualsDefault());
    
    public override void ClearNullProperties()
    {
        if (Series is not null && Series.Identity.EqualsDefault())
            Series = null;
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Sscc = WsLocaleCore.Sql.SqlItemFieldSscc;
        NettoWeight = 1.1M;
        WeightTare = 0.25M;
        RegNum = 1;
        Kneading = 1;
        PluScale.FillProperties();
        Series?.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluWeighingModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Kneading, item.Kneading) &&
        Equals(PluScale, item.PluScale) &&
        Equals(Sscc, item.Sscc) &&
        Equals(NettoWeight, item.NettoWeight) &&
        Equals(WeightTare, item.WeightTare) &&
        Equals(RegNum, item.RegNum) &&
        PluScale.Equals(item.PluScale) &&
        (Series is null && item.Series is null ||
         Series is not null && item.Series is not null && Series.Equals(item.Series));

    #endregion
}
