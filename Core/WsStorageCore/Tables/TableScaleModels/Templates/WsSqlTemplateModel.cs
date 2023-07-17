// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Templates;

/// <summary>
/// Table "Templates".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlTemplateModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual string CategoryId { get; set; }
    [XmlElement] public virtual string Title { get; set; }
    [XmlElement] public virtual string Data { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlTemplateModel() : base(WsSqlEnumFieldIdentity.Id)
    {
        CategoryId = string.Empty;
        Title = string.Empty;
        Data = string.Empty;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlTemplateModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        CategoryId = info.GetString(nameof(CategoryId));
        Title = info.GetString(nameof(Title));
        Data = info.GetString(nameof(Data));
    }

    public WsSqlTemplateModel(WsSqlTemplateModel item) : base(item)
    {
        CategoryId = item.CategoryId;
        Title = item.Title;
        Data = item.Data;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {CategoryId} | {Title}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlTemplateModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(CategoryId, string.Empty) &&
        Equals(Title, string.Empty) &&
        Equals(Data, string.Empty);

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(CategoryId), CategoryId);
        info.AddValue(nameof(Title), Title);
        info.AddValue(nameof(Data), Data);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Data = WsLocaleCore.Sql.SqlItemFieldTemplateData;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlTemplateModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(CategoryId, item.CategoryId) &&
        Equals(Title, item.Title) &&
        Equals(Data, item.Data);

    #endregion
}