namespace WsStorageCore.Tables.TableScaleModels.Orders;

[DebuggerDisplay("{ToString()}")]
public class WsSqlOrderModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual DateTime BeginDt { get; set; }
    public virtual DateTime EndDt { get; set; }
    public virtual DateTime ProdDt { get; set; }
    public virtual int BoxCount { get; set; }
    public virtual int PalletCount { get; set; }
    
    public WsSqlOrderModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        BeginDt = DateTime.MinValue;
        ProdDt = DateTime.MinValue;
        EndDt = DateTime.MinValue;
        BoxCount = default;
        PalletCount = default;
    }
    
    public WsSqlOrderModel(WsSqlOrderModel item) : base(item)
    {
        BeginDt = item.BeginDt;
        EndDt = item.EndDt;
        ProdDt = item.ProdDt;
        BoxCount = item.BoxCount;
        PalletCount = item.PalletCount;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(BeginDt)}: {BeginDt}. " +
        $"{nameof(EndDt)}: {EndDt}. " +
        $"{nameof(ProdDt)}: {ProdDt}. " +
        $"{nameof(BoxCount)}: {BoxCount}. " +
        $"{nameof(PalletCount)}: {PalletCount}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlOrderModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(BeginDt, DateTime.MinValue) &&
        Equals(EndDt, DateTime.MinValue) &&
        Equals(ProdDt, DateTime.MinValue) &&
        Equals(BoxCount, 0) &&
        Equals(PalletCount, 0);

    public override void FillProperties()
    {
        base.FillProperties();
        BoxCount = 1;
        PalletCount = 1;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlOrderModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(BeginDt, item.BeginDt) &&
        Equals(EndDt, item.EndDt) &&
        Equals(ProdDt, item.ProdDt) &&
        Equals(BoxCount, item.BoxCount) &&
        Equals(PalletCount, item.PalletCount);

    #endregion
}