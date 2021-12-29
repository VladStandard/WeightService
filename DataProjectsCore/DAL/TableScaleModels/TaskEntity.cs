// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;

namespace DataProjectsCore.DAL.TableScaleModels
{
    public class TaskEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual TaskTypeEntity TaskType { get; set; } = new TaskTypeEntity();
        public virtual ScaleEntity Scale { get; set; } = new ScaleEntity();
        public virtual bool Enabled { get; set; } = default;

        #endregion
        
        #region Constructor and destructor

        public TaskEntity()
        {
            PrimaryColumn.Name = ColumnName.Uid;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string? strTaskType = TaskType != null ? TaskType.Uid.ToString() : "null";
            string? strScale = Scale != null ? Scale.Id.ToString() : "null";
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
                   Equals(Enabled, default(bool));
        }

        public override object Clone()
        {
            return new TaskEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Uid = Uid,
                TaskType = (TaskTypeEntity)TaskType.Clone(),
                Scale = (ScaleEntity)Scale.Clone(),
                Enabled = Enabled,
            };
        }

        #endregion
    }
}
