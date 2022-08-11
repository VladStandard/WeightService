// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableDwhModels;

[Serializable]
public class StatusEntity : BaseEntity, ISerializable, IBaseEntity
{
    #region Public and private fields, properties, constructor

    public virtual string Name { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public StatusEntity() : base(0, false)
    {
	    Init();
    }

	/// <summary>
	/// Constructor.
	/// </summary>
	public StatusEntity(long identityId, bool isSetupDates) : base(identityId, isSetupDates)
    {
	    Init();
    }

    #endregion

    public new virtual void Init()
    {
	    base.Init();
        Name = string.Empty;
    }

    #region Public and private methods

    public override string ToString() =>
        base.ToString() +
        $"{nameof(Name)}: {Name}. ";

    public virtual bool Equals(StatusEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(Name, item.Name);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((StatusEntity)obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(Name, string.Empty);
    }

    public new virtual object Clone()
    {
        StatusEntity item = new();
        item.Name = Name;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual StatusEntity CloneCast() => (StatusEntity)Clone();

    #endregion
}
