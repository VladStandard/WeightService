// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace WsStorageCore.Common;

/// <summary>
/// Базовый класс SQL-таблицы.
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlTableBase : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlIgnore] public virtual WsSqlFieldIdentityModel Identity { get; set; }
    [XmlElement] public virtual long IdentityValueId { get => Identity.Id; set => Identity.SetId(value); }
    [XmlElement] public virtual Guid IdentityValueUid { get => Identity.Uid; set => Identity.SetUid(value); }
    [XmlElement] public virtual DateTime CreateDt { get; set; } = DateTime.MinValue;
    [XmlElement] public virtual DateTime ChangeDt { get; set; } = DateTime.MinValue;
    [XmlElement] public virtual bool IsMarked { get; set; }
    [XmlElement] public virtual string Name { get; set; } = string.Empty;
    [XmlElement] public virtual string Description { get; set; } = string.Empty;

    [XmlIgnore] public virtual bool IsExists => Identity.IsExists;
    [XmlIgnore] public virtual bool IsNotExists => Identity.IsNotExists;
    [XmlIgnore] public virtual bool IsNew => IsNotExists;
    [XmlIgnore] public virtual bool IsNotNew => IsExists;
    [XmlIgnore] public virtual bool IsIdentityUid => Identity.IsUid;
    [XmlIgnore] public virtual ParseResultModel ParseResult { get; set; } = new();
    [XmlIgnore] public virtual string DisplayName => IsNew ? WsLocaleCore.Table.FieldEmpty : Name;

    public WsSqlTableBase()
    {
        Identity = new(WsSqlEnumFieldIdentity.Empty);
    }

    public WsSqlTableBase(WsSqlEnumFieldIdentity identityName) : this()
    {
        Identity = new(identityName);
    }

    public WsSqlTableBase(WsSqlFieldIdentityModel identity) : this()
    {
        Identity = new(identity);
    }

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

    public WsSqlTableBase(WsSqlTableBase item)
    {
        Identity = new(item.Identity);
        CreateDt = item.CreateDt;
        ChangeDt = item.ChangeDt;
        IsMarked = item.IsMarked;
        Name = item.Name;
        Description = item.Description;
        ParseResult = new(item.ParseResult);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        (CreateDt != DateTime.MinValue ? $"{CreateDt:yyyy-MM-dd} | " : string.Empty) + 
        (ChangeDt != DateTime.MinValue ? $"{ChangeDt:yyyy-MM-dd} | " : string.Empty) + 
        GetIsMarked() + 
        (string.IsNullOrEmpty(Name) ? string.Empty : $"{Name} | ") + 
        (string.IsNullOrEmpty(Description) ? string.Empty : $"{Description}");

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

    public virtual bool EqualsNew() => Equals(new());

    public virtual bool EqualsDefault() =>
        Identity.EqualsDefault() &&
        IsIdentityUid ? Equals(IdentityValueUid, Guid.Empty) : Equals(IdentityValueId, default(long)) &&
        Equals(CreateDt, DateTime.MinValue) &&
        Equals(ChangeDt, DateTime.MinValue) &&
        Equals(IsMarked, false) &&
        Equals(Name, string.Empty) &&
        Equals(Description, string.Empty) &&
        ParseResult.EqualsDefault();

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
    }

    protected virtual void UpdateProperties(WsSqlTableBase item, bool isSkipName)
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
        ParseResult = new(item.ParseResult);
    }

    protected virtual string GetIsMarked() => IsMarked ? "Is marked" : "No marked";
    
    protected virtual string GetIsBool(bool isBool, string positive, string negative) => isBool? positive : negative;

    #endregion

    #region INotifyPropertyChanged

    public virtual event PropertyChangedEventHandler? PropertyChanged;

    public virtual void OnPropertyChanged([CallerMemberName] string memberName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
    }

    #endregion
}