// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataBaseCore.DAL.TableModels
{
    [Serializable]
    public class TaskEntity : BaseEntity<TaskEntity>
    {
        #region Public and private fields and properties

        public Guid Uid { get; set; }
        public TaskTypeEntity TaskType { get; set; }
        public ScaleEntity Scale { get;  set; }
        public bool Enabled { get; set; }

        #endregion

        #region Constructor and destructor

        public TaskEntity()
        {
            //
        }

        public TaskEntity(Guid uid, int scaleId, string scaleDescription, Guid taskTypeUid, string taskType, bool enabled) : this()
        {
            Uid = uid;
            Scale = new ScaleEntity(scaleId, scaleDescription);
            TaskType = new TaskTypeEntity(taskTypeUid, taskType);
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
}