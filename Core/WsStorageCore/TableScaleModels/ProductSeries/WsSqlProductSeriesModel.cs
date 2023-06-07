// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLocalizationCore.Utils;

namespace WsStorageCore.TableScaleModels.ProductSeries;

/// <summary>
/// Table "ProductSeries".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlProductSeriesModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlScaleModel Scale { get; set; }
    [XmlElement] public virtual bool IsClose { get; set; }
    [XmlElement] public virtual string Sscc { get; set; }
    [XmlElement] public virtual Guid Uid { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlProductSeriesModel() : base(WsSqlFieldIdentity.Id)
    {
        Scale = new();
        IsClose = false;
        Sscc = string.Empty;
        Uid = Guid.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlProductSeriesModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Scale = (WsSqlScaleModel)info.GetValue(nameof(Scale), typeof(WsSqlScaleModel));
        IsClose = info.GetBoolean(nameof(IsClose));
        Sscc = info.GetString(nameof(Sscc));
        Uid = (Guid)info.GetValue(nameof(Uid), typeof(Guid));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Scale)}: {Scale}. " +
        $"{nameof(IsClose)}: {IsClose}. " +
        $"{nameof(Sscc)}: {Sscc}.";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlProductSeriesModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(IsClose, false) &&
        Equals(Sscc, string.Empty) &&
        Equals(Uid, Guid.Empty) &&
        Scale.EqualsDefault();

    public override object Clone()
    {
        WsSqlProductSeriesModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Scale = Scale.CloneCast();
        item.IsClose = IsClose;
        item.Sscc = Sscc;
        item.Uid = Uid;
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
        info.AddValue(nameof(Scale), Scale);
        info.AddValue(nameof(IsClose), IsClose);
        info.AddValue(nameof(Sscc), Sscc);
        info.AddValue(nameof(Uid), Uid);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Sscc = LocaleCore.Sql.SqlItemFieldSscc;
        IsClose = false;
        Scale.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlProductSeriesModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(CreateDt, item.CreateDt) &&
        Equals(IsClose, item.IsClose) &&
        Equals(Sscc, item.Sscc) &&
        Scale.Equals(item.Scale);

    public new virtual WsSqlProductSeriesModel CloneCast() => (WsSqlProductSeriesModel)Clone();

    #endregion
}
