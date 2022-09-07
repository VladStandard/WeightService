// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;
using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "TASKS".
/// </summary>
[Serializable]
public class TaskModel : TableBaseModel, ICloneable, ISqlDbBase, ISerializable
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

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	private TaskModel(SerializationInfo info, StreamingContext context) : base(info, context)
	{
		TaskType = (TaskTypeModel)info.GetValue(nameof(TaskType), typeof(TaskTypeModel));
		Scale = (ScaleModel)info.GetValue(nameof(Scale), typeof(ScaleModel));
		Enabled = info.GetBoolean(nameof(Enabled));
	}

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
		$"{nameof(TaskType)}: {TaskType}. " +
		$"{nameof(Scale)}: {Scale}. " +
		$"{nameof(Enabled)}: {Enabled}. ";

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((TaskModel)obj);
    }

	public override int GetHashCode() => base.GetHashCode();

	public override bool EqualsNew() => Equals(new());

	public override bool EqualsDefault() =>
		base.EqualsDefault() &&
		Equals(Enabled, false);

	public override object Clone()
    {
        TaskModel item = new();
        item.TaskType = TaskType.CloneCast();
        item.Scale = Scale.CloneCast();
        item.Enabled = Enabled;
		item.CloneSetup(base.CloneCast());
		return item;
    }

	/// <summary>
	/// Get object data for serialization info.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
		info.AddValue(nameof(TaskType), TaskType);
		info.AddValue(nameof(Scale), Scale);
		info.AddValue(nameof(Enabled), Enabled);
	}

	#endregion

	#region Public and private methods

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

    public new virtual TaskModel CloneCast() => (TaskModel)Clone();


	#endregion
}
