// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableDirectModels;

[Serializable]
public class TaskDirect : BaseSerializeEntity, ISerializable
{
    #region Public and private fields, properties, constructor

    public Guid Uid { get; set; }
    public TaskTypeDirect TaskType { get; set; }
    public ScaleEntity Scale { get; set; }
    public bool Enabled { get; set; }

    #endregion

    #region Constructor and destructor

    public TaskDirect()
    {
        Uid = Guid.Empty;
        TaskType = new();
        Scale = new();
        Enabled = false;
    }

    public TaskDirect(Guid uid, long scaleId, Guid taskTypeUid, string taskType, bool enabled) : this()
    {
        Uid = uid;
        Scale = new(scaleId);
        TaskType = new(taskTypeUid, taskType);
        Enabled = enabled;
        //TaskTypeUid = GetTaskTypeUid(taskName);
        //ScaleDescription = GetScaleDescription(scaleId);
        //Uid = SaveTask(TaskTypeUid, ScaleId, enabled);
        //GetTask(TaskTypeUid, ScaleId);
    }

    #endregion

    #region Public and private methods

    //

    #endregion
}
