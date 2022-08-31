// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "TASKS".
/// </summary>
[Serializable]
public class TaskModel : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual TaskTypeModel TaskType { get; set; }
	[XmlElement] public virtual ScaleModel Scale { get; set; }
	[XmlElement] public virtual bool Enabled { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public TaskModel()
	{
		TaskType = new();
		Scale = new();
		Enabled = false;
	}

	#endregion

	#region Public and private methods

	public override string ToString()
    {
        return
            $"{nameof(IsMarked)}: {IsMarked}. " +
            $"{nameof(TaskType)}: {TaskType}. " +
            $"{nameof(Scale)}: {Scale}. " +
            $"{nameof(Enabled)}: {Enabled}. ";
    }

    public virtual bool Equals(TaskModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        if (!TaskType.Equals(item.TaskType))
            return false;
        if (!Scale.Equals(item.Scale))
            return false;
        return base.Equals(item) &&
            Equals(Enabled, item.Enabled);
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((TaskModel)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(Enabled, false);
    }

    public new virtual object Clone()
    {
        TaskModel item = new();
        item.TaskType = TaskType.CloneCast();
        item.Scale = Scale.CloneCast();
        item.Enabled = Enabled;
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual TaskModel CloneCast() => (TaskModel)Clone();

    #endregion
}
