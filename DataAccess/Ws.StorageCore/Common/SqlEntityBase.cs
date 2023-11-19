namespace Ws.StorageCore.Common;

[DebuggerDisplay("{ToString()}")]
public class SqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual SqlFieldIdentityModel Identity { get; set; }
    public virtual long IdentityValueId { get => Identity.Id; set => Identity.SetId(value); }
    public virtual Guid IdentityValueUid { get => Identity.Uid; set => Identity.SetUid(value); }
    public virtual DateTime CreateDt { get; set; } = DateTime.MinValue;
    public virtual DateTime ChangeDt { get; set; } = DateTime.MinValue;
    public virtual bool IsMarked { get; set; }
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Description { get; set; } = string.Empty;
    public virtual bool IsExists => Identity.IsExists;
    public virtual bool IsNew => Identity.IsNew;
    public virtual string DisplayName => IsNew ? LocaleCore.Table.FieldEmpty : Name;

    public SqlEntityBase()
    {
        Identity = new(SqlEnumFieldIdentity.Empty);
    }

    public SqlEntityBase(SqlEnumFieldIdentity identityName) : this()
    {
        Identity = new(identityName);
    }

    public SqlEntityBase(SqlEntityBase item)
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
        (string.IsNullOrEmpty(Name) ? string.Empty : $"{Name} | ") +
        (string.IsNullOrEmpty(Description) ? string.Empty : $"{Description}");

    public virtual bool Equals(SqlEntityBase item) =>
        ReferenceEquals(this, item) || Identity.Equals(item.Identity) &&//-V3130
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
        return Equals((SqlEntityBase)obj);
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
    
    public virtual void FillProperties()
    {
        ChangeDt = CreateDt = DateTime.Now;
    }
    
    #endregion
}