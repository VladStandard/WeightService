// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Models;

public class BaseDummyEntity : BaseEntity, ISerializable, IBaseEntity
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Constructor.
	/// </summary>
	public BaseDummyEntity() : base(Guid.Empty, false)
    {
	    Init();
    }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityUid"></param>
	/// <param name="isSetupDates"></param>
	public BaseDummyEntity(Guid identityUid, bool isSetupDates) : base(identityUid, isSetupDates)
    {
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityId"></param>
	/// <param name="isSetupDates"></param>
	public BaseDummyEntity(long identityId, bool isSetupDates) : base(identityId, isSetupDates)
    {
		Init();
	}

    #endregion

    #region Public and private methods

    public new virtual void Init()
    {
	    base.Init();
        //
    }

    public override string ToString() => base.ToString();

    public virtual bool Equals(BaseDummyEntity item)
    {
        //if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item);
    }

    public override bool Equals(object obj)
    {
        //if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BaseDummyEntity)obj);
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
        return base.EqualsDefault();
    }

    public new virtual object Clone()
    {
        BaseDummyEntity item = new();
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual BaseDummyEntity CloneCast() => (BaseDummyEntity)Clone();

    #endregion
}
