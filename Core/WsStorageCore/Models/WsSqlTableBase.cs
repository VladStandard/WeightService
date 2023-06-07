// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLocalizationCore.Utils;

namespace WsStorageCore.Models;

/// <summary>
/// SQL table model.
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlTableBase : SerializeBase, ICloneable
{
    #region Public and private fields, properties, constructor

    [XmlIgnore] public virtual WsSqlFieldIdentityModel Identity { get; set; }
    [XmlElement] public virtual long IdentityValueId { get => Identity.Id; set => Identity.SetId(value); }
    [XmlElement] public virtual Guid IdentityValueUid { get => Identity.Uid; set => Identity.SetUid(value); }
    [XmlIgnore] public virtual bool IsExists => Identity.IsExists;
    [XmlIgnore] public virtual bool IsNotExists => Identity.IsNotExists;
    [XmlIgnore] public virtual bool IsNew => IsNotExists;
    [XmlIgnore] public virtual bool IsNotNew => IsExists;
    [XmlIgnore] public virtual bool IsIdentityId => Identity.IsId;
    [XmlIgnore] public virtual bool IsIdentityUid => Identity.IsUid;
    [XmlElement] public virtual DateTime CreateDt { get; set; } = DateTime.MinValue;
    [XmlElement] public virtual DateTime ChangeDt { get; set; } = DateTime.MinValue;
    [XmlElement] public virtual bool IsMarked { get; set; } = false;
    [XmlElement] public virtual string Name { get; set; } = string.Empty;
    [XmlElement] public virtual string Description { get; set; } = string.Empty;
    [XmlIgnore] public virtual ParseResultModel ParseResult { get; set; } = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlTableBase()
    {
        Identity = new(WsSqlFieldIdentity.Empty);
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlTableBase(WsSqlFieldIdentity identityName) : this()
    {
        Identity = new(identityName);
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlTableBase(WsSqlFieldIdentityModel identity) : this()
    {
        Identity = (WsSqlFieldIdentityModel)identity.Clone();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    protected WsSqlTableBase(SerializationInfo info, StreamingContext context)
    {
        Identity = (WsSqlFieldIdentityModel)info.GetValue(nameof(Identity), typeof(WsSqlFieldIdentityModel));
        CreateDt = info.GetDateTime(nameof(CreateDt));
        ChangeDt = info.GetDateTime(nameof(ChangeDt));
        IsMarked = info.GetBoolean(nameof(IsMarked));
        Name = info.GetString(nameof(Name));
        Description = info.GetString(nameof(Description));
        ParseResult = (ParseResultModel)info.GetValue(nameof(ParseResult), typeof(ParseResultModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString()
    {
        string strCreateDt = CreateDt != DateTime.MinValue ? $"{CreateDt:yyyy-MM-dd} | " : string.Empty;
        string strChangeDt = ChangeDt != DateTime.MinValue ? $"{ChangeDt:yyyy-MM-dd} | " : string.Empty;
        string strIsMarked = $"{GetIsMarked()}";
        string strName = string.IsNullOrEmpty(Name) ? string.Empty : $"{Name} | ";
        string strDescription = string.IsNullOrEmpty(Description) ? string.Empty : $"{Description} | ";
        return strCreateDt + strChangeDt + strIsMarked + strName + strDescription;
    }

    public virtual bool Equals(WsSqlTableBase item) =>
        ReferenceEquals(this, item) || Identity.Equals(item.Identity) && //-V3130
        Equals(CreateDt, item.CreateDt) &&
        Equals(ChangeDt, item.ChangeDt) &&
        Equals(IsMarked, item.IsMarked) &&
        Equals(Name, item.Name) &&
        Equals(Description, item.Description) &&
        Equals(ParseResult, item.ParseResult);

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlTableBase)obj);
    }

    public override int GetHashCode() => Identity.GetHashCode();

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Identity), Identity);
        info.AddValue(nameof(ChangeDt), ChangeDt);
        info.AddValue(nameof(CreateDt), CreateDt);
        info.AddValue(nameof(IsMarked), IsMarked);
        info.AddValue(nameof(Name), Name);
        info.AddValue(nameof(Description), Description);
        info.AddValue(nameof(ParseResult), ParseResult);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool EqualsNew() => Equals(new WsSqlTableBase());

    public virtual bool EqualsDefault() =>
        Identity.EqualsDefault() &&
        IsIdentityUid ? Equals(IdentityValueUid, Guid.Empty) : Equals(IdentityValueId, default(long)) &&
        Equals(CreateDt, DateTime.MinValue) &&
        Equals(ChangeDt, DateTime.MinValue) &&
        Equals(IsMarked, false) &&
        Equals(Name, string.Empty) &&
        Equals(Description, string.Empty) &&
        ParseResult.EqualsDefault();

    public virtual object Clone() => new WsSqlTableBase(Identity)
    {
        CreateDt = CreateDt,
        ChangeDt = ChangeDt,
        IsMarked = IsMarked,
        Name = Name,
        Description = Description,
        ParseResult = ParseResult.CloneCast()
    };

    public virtual WsSqlTableBase CloneCast() => (WsSqlTableBase)Clone();

    public virtual void CloneSetup(WsSqlTableBase item)
    {
        CreateDt = item.CreateDt;
        ChangeDt = item.ChangeDt;
        IsMarked = item.IsMarked;
        Name = item.Name;
        Description = item.Description;
    }

    public virtual void SetDtNow()
    {
        ChangeDt = CreateDt = DateTime.Now;
    }

    public virtual void ClearNullProperties()
    {
        //throw new NotImplementedException();
    }

    public virtual void FillProperties()
    {
        SetDtNow();
        Name = LocaleCore.Sql.SqlItemFieldName;
        Description = LocaleCore.Sql.SqlItemFieldDescription;
    }

    public virtual void UpdateProperties(WsSqlTableBase item) => UpdateProperties(item, false);

    public virtual void UpdateProperties(WsSqlTableBase item, bool isSkipName)
    {
        if (!item.CreateDt.Equals(DateTime.MinValue))
            CreateDt = item.CreateDt;
        if (!item.ChangeDt.Equals(DateTime.MinValue))
            ChangeDt = item.ChangeDt;
        IsMarked = item.IsMarked;
        if (!isSkipName && string.IsNullOrEmpty(item.Name)) throw new ArgumentException(nameof(Name));
        Name = item.Name;
        if (!string.IsNullOrEmpty(item.Description))
            Description = item.Description;
        ParseResult = item.ParseResult.CloneCast();
    }

    public virtual string GetIsMarked() => IsMarked ? "Is hide" : "Is actual";

    #endregion
}