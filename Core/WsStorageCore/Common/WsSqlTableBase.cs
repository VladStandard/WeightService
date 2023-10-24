namespace WsStorageCore.Common;

[DebuggerDisplay("{ToString()}")]
public class WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual WsSqlFieldIdentityModel Identity { get; set; }
    public virtual long IdentityValueId { get => Identity.Id; set => Identity.SetId(value); }
    public virtual Guid IdentityValueUid { get => Identity.Uid; set => Identity.SetUid(value); }
    public virtual DateTime CreateDt { get; set; } = DateTime.MinValue;
    public virtual DateTime ChangeDt { get; set; } = DateTime.MinValue;
    public virtual bool IsMarked { get; set; }
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Description { get; set; } = string.Empty;
    public virtual bool IsExists => Identity.IsExists;
    public virtual bool IsNew => Identity.IsNew;
    public virtual string DisplayName => IsNew ? WsLocaleCore.Table.FieldEmpty : Name;

    public WsSqlTableBase()
    {
        Identity = new(WsSqlEnumFieldIdentity.Empty);
    }

    public WsSqlTableBase(WsSqlEnumFieldIdentity identityName) : this()
    {
        Identity = new(identityName);
    }

    protected WsSqlTableBase(SerializationInfo info, StreamingContext context)
    {
        Identity = (WsSqlFieldIdentityModel)info.GetValue(nameof(Identity), typeof(WsSqlFieldIdentityModel));
        CreateDt = info.GetDateTime(nameof(CreateDt));
        ChangeDt = info.GetDateTime(nameof(ChangeDt));
        IsMarked = info.GetBoolean(nameof(IsMarked));
        Name = info.GetString(nameof(Name));
        Description = info.GetString(nameof(Description));
    }

    public WsSqlTableBase(WsSqlTableBase item)
    {
        Identity = new(item.Identity);
        CreateDt = item.CreateDt;
        ChangeDt = item.ChangeDt;
        IsMarked = item.IsMarked;
        Name = item.Name;
        Description = item.Description;
    }

    #endregion

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
        Equals(Description, item.Description);

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlTableBase)obj);
    }

    public override int GetHashCode() => Identity.GetHashCode();

    #region Public and private methods - virtual

    public virtual bool EqualsNew() => Equals(new());

    public virtual bool EqualsDefault() =>
        Identity.EqualsDefault() &&
        Identity.IsUid ? Equals(IdentityValueUid, Guid.Empty) : Equals(IdentityValueId, default(long)) &&
        Equals(CreateDt, DateTime.MinValue) &&
        Equals(ChangeDt, DateTime.MinValue) &&
        Equals(IsMarked, false) &&
        Equals(Name, string.Empty) &&
        Equals(Description, string.Empty);

    public virtual void SetDtNow()
    {
        ChangeDt = CreateDt = DateTime.Now;
    }
    
    public virtual void FillProperties()
    {
        SetDtNow();
    }

    protected virtual string GetIsMarked() => IsMarked ? "Is marked" : "No marked";
    
    protected virtual string GetIsBool(bool isBool, string positive, string negative) => isBool? positive : negative;

    #endregion
}