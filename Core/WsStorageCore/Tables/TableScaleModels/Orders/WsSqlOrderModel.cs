// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Orders;

[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlOrderModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual DateTime BeginDt { get; set; }
    [XmlElement] public virtual DateTime EndDt { get; set; }
    [XmlElement] public virtual DateTime ProdDt { get; set; }
    [XmlElement] public virtual int BoxCount { get; set; }
    [XmlElement] public virtual int PalletCount { get; set; }
    
    public WsSqlOrderModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        BeginDt = DateTime.MinValue;
        ProdDt = DateTime.MinValue;
        EndDt = DateTime.MinValue;
        BoxCount = default;
        PalletCount = default;
    }
    
    protected WsSqlOrderModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        BeginDt = info.GetDateTime(nameof(BeginDt));
        EndDt = info.GetDateTime(nameof(EndDt));
        ProdDt = info.GetDateTime(nameof(ProdDt));
        BoxCount = info.GetInt32(nameof(BoxCount));
        PalletCount = info.GetInt32(nameof(PalletCount));
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

    public virtual bool Equals(WsSqlOrderModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(BeginDt, item.BeginDt) &&
        Equals(EndDt, item.EndDt) &&
        Equals(ProdDt, item.ProdDt) &&
        Equals(BoxCount, item.BoxCount) &&
        Equals(PalletCount, item.PalletCount);

    #endregion
}