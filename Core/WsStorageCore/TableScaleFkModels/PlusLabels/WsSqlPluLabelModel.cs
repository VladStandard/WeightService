// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

namespace WsStorageCore.TableScaleFkModels.PlusLabels;

/// <summary>
/// Table "PLUS_LABELS".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluLabelModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement(IsNullable = true)] public virtual WsSqlPluWeighingModel? PluWeighing { get; set; }
    [XmlElement] public virtual WsSqlPluScaleModel PluScale { get; set; }
    [XmlElement] public virtual string Zpl { get; set; }
    [XmlIgnore] public virtual XmlDocument? Xml { get; set; }
    [XmlElement] public virtual DateTime ProductDt { get; set; }
    [XmlElement]
    public virtual DateTime ExpirationDt
    {
        get => PluScale.IsNew ? DateTime.MinValue : ProductDt.AddDays(PluScale.Plu.ShelfLifeDays);
        // This code need for print labels.
        set => _ = value;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluLabelModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        PluWeighing = null;
        PluScale = new();
        Zpl = string.Empty;
        Xml = null;
        ProductDt = DateTime.MinValue;
        ExpirationDt = DateTime.MinValue;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlPluLabelModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        PluWeighing = (WsSqlPluWeighingModel?)info.GetValue(nameof(PluWeighing), typeof(WsSqlPluWeighingModel));
        PluScale = (WsSqlPluScaleModel)info.GetValue(nameof(PluScale), typeof(WsSqlPluScaleModel));
        Zpl = info.GetString(nameof(Zpl));
        Xml = (XmlDocument)info.GetValue(nameof(Xml), typeof(XmlDocument));
        ProductDt = info.GetDateTime(nameof(ProductDt));
        ExpirationDt = info.GetDateTime(nameof(ExpirationDt));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(ProductDt)}: {ProductDt}. " +
        $"{nameof(PluScale.Plu.Number)}: {PluScale.Plu.Number}. " +
        $"{nameof(Zpl)}: {Zpl.Length}. " +
        $"{nameof(Xml)}: {(Xml is null ? 0 : Xml.OuterXml.Length)}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluLabelModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Zpl, string.Empty) &&
        Equals(Xml, null) &&
        Equals(ProductDt, DateTime.MinValue) &&
        (PluWeighing is null || PluWeighing.EqualsDefault()) &&
        PluScale.EqualsDefault();

    public override object Clone()
    {
        WsSqlPluLabelModel item = new();
        item.CloneSetup(base.CloneCast());
        item.IsMarked = IsMarked;
        item.PluWeighing = PluWeighing?.CloneCast();
        item.PluScale = PluScale.CloneCast();
        item.Zpl = Zpl;
        if (Xml is { })
        {
            XmlDocument xml = WsDataFormatUtils.DeserializeFromXml<XmlDocument>(Xml.OuterXml, Encoding.UTF8);
            item.Xml = xml;
        }
        item.ProductDt = ProductDt;
        item.ExpirationDt = ExpirationDt;
        return item;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(PluWeighing), PluWeighing);
        info.AddValue(nameof(PluScale), PluScale);
        info.AddValue(nameof(Zpl), Zpl);
        info.AddValue(nameof(Xml), Xml);
        info.AddValue(nameof(ProductDt), ProductDt);
        info.AddValue(nameof(ExpirationDt), ExpirationDt);
    }

    public override void ClearNullProperties()
    {
        if (PluWeighing is not null && PluWeighing.Identity.EqualsDefault())
            PluWeighing = null;
        //if (PluScale.Identity.EqualsDefault())
        //       PluScale = new();
    }

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

    public virtual bool Equals(WsSqlPluLabelModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Zpl, item.Zpl) &&
        Equals(ProductDt, item.ProductDt) &&
        Equals(ExpirationDt, item.ExpirationDt) &&
        (PluWeighing is null && item.PluWeighing is null || PluWeighing is not null &&
            item.PluWeighing is not null && PluWeighing.Equals(item.PluWeighing)) &&
        (Xml is null && item.Xml is null || Xml is not null && item.Xml is not null && Xml.Equals(item.Xml)) &&
        PluScale.Equals(item.PluScale);

    public new virtual WsSqlPluLabelModel CloneCast() => (WsSqlPluLabelModel)Clone();

    #endregion
}