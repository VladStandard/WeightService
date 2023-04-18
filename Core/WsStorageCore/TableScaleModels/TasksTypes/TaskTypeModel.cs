// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.TasksTypes;

/// <summary>
/// Table "TASKS_TYPES".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(TaskTypeModel)}")]
public class TaskTypeModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    public TaskTypeModel() : base(WsSqlFieldIdentity.Uid)
    {
        //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected TaskTypeModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        //
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((TaskTypeModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault();

    public override object Clone()
    {
        TaskTypeModel item = new();
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
    }

    public override void FillProperties()
    {
        base.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(TaskTypeModel item) =>
        ReferenceEquals(this, item) || base.Equals(item);

    public new virtual TaskTypeModel CloneCast() => (TaskTypeModel)Clone();

    #endregion
}
