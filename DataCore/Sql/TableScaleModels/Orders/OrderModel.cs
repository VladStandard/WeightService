// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels.Orders;

/// <summary>
/// Table "ORDERS".
/// </summary>
[Serializable]
public class OrderModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual DateTime BeginDt { get; set; }
    [XmlElement] public virtual DateTime EndDt { get; set; }
    [XmlElement] public virtual DateTime ProdDt { get; set; }
    [XmlElement] public virtual int BoxCount { get; set; }
    [XmlElement] public virtual int PalletCount { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public OrderModel() : base(SqlFieldIdentityEnum.Uid)
    {
        BeginDt = DateTime.MinValue;
        ProdDt = DateTime.MinValue;
        EndDt = DateTime.MinValue;
        BoxCount = default;
        PalletCount = default;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected OrderModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        BeginDt = info.GetDateTime(nameof(BeginDt));
        EndDt = info.GetDateTime(nameof(EndDt));
        ProdDt = info.GetDateTime(nameof(ProdDt));
        BoxCount = info.GetInt32(nameof(BoxCount));
        PalletCount = info.GetInt32(nameof(PalletCount));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
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
        return Equals((OrderModel)obj);
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

    public override object Clone()
    {
        OrderModel item = new();
        item.BeginDt = BeginDt;
        item.EndDt = EndDt;
        item.ProdDt = ProdDt;
        item.BoxCount = BoxCount;
        item.PalletCount = PalletCount;
        item.CloneSetup(base.CloneCast());
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
        info.AddValue(nameof(BeginDt), BeginDt);
        info.AddValue(nameof(EndDt), EndDt);
        info.AddValue(nameof(ProdDt), ProdDt);
        info.AddValue(nameof(BoxCount), BoxCount);
        info.AddValue(nameof(PalletCount), PalletCount);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        BoxCount = 1;
        PalletCount = 1;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(OrderModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(BeginDt, item.BeginDt) &&
        Equals(EndDt, item.EndDt) &&
        Equals(ProdDt, item.ProdDt) &&
        Equals(BoxCount, item.BoxCount) &&
        Equals(PalletCount, item.PalletCount);

    public new virtual OrderModel CloneCast() => (OrderModel)Clone();

    #endregion
}
