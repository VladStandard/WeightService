// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "TASKS_TYPES".
/// </summary>
[Serializable]
public class TaskTypeEntity : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Uid;
	[XmlElement] public virtual string Name { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public TaskTypeEntity() : base(Guid.Empty, false)
	{
		Init();
	}

    public TaskTypeEntity(Guid identityUid, bool isSetupDates) : base(identityUid, isSetupDates)
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
	    $"{nameof(IdentityUid)}: {IdentityUid}. " +
	    $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. ";

    public virtual bool Equals(TaskTypeEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
            Equals(Name, item.Name);
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((TaskTypeEntity)obj);
    }

	public override int GetHashCode() => IdentityUid.GetHashCode();

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
        TaskTypeEntity item = new();
        item.Name = Name;
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual TaskTypeEntity CloneCast() => (TaskTypeEntity)Clone();

    #endregion
}
