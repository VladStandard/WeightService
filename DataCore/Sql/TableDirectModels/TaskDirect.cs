// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels;
using DataCore.Sql.Models;
using System;

namespace DataCore.Sql.TableDirectModels
{
    public class TaskDirect : BaseSerializeEntity
    {
        #region Public and private fields and properties

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
            Scale = new ScaleEntity(scaleId);
            TaskType = new TaskTypeDirect(taskTypeUid, taskType);
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