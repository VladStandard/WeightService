// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Table "Tasks".
    /// </summary>
    public class TaskEntity : BaseEntity<TaskEntity>
    {
        #region Public and private fields and properties

        public virtual TaskTypeEntity TaskType { get; set; }
        public virtual ScaleEntity Scale { get; set; }
        public virtual bool Enabled { get; set; }

        #endregion

        #region Constructor and destructor

        public TaskEntity() : this(Guid.Empty)
        {
            //
        }

        public TaskEntity(Guid uid) : base(uid)
        {
            TaskType = new();
            Scale = new();
            Enabled = false;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string? strTaskType = TaskType != null ? TaskType.IdentityUid.ToString() : "null";
            string? strScale = Scale != null ? Scale.IdentityId.ToString() : "null";
            return base.ToString() +
                $"{nameof(TaskType)}: {strTaskType}. " +
                $"{nameof(Scale)}: {strScale}. " +
                $"{nameof(Enabled)}: {Enabled}. ";
        }

        public virtual bool Equals(TaskEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                TaskType.Equals(entity.TaskType) &&
                Scale.Equals(entity.Scale) &&
                Equals(Enabled, entity.Enabled);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TaskEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new TaskEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(Enabled, false);
        }

        public override object Clone()
        {
            TaskEntity item = (TaskEntity)base.Clone();
            item.TaskType = (TaskTypeEntity)TaskType.Clone();
            item.Scale = (ScaleEntity)Scale.Clone();
            item.Enabled = Enabled;
            return item;
        }

        #endregion
    }
}
