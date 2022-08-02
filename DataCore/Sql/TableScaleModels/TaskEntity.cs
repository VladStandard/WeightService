// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "TASKS".
/// </summary>
public class TaskEntity : BaseEntity
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Identity name.
    /// </summary>
    public static ColumnName IdentityName => ColumnName.Uid;
    public virtual TaskTypeEntity TaskType { get; set; }
    public virtual ScaleEntity Scale { get; set; }
    public virtual bool Enabled { get; set; }

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
        string strTaskType = TaskType != null ? TaskType.IdentityUid.ToString() : "null";
        string strScale = Scale != null ? Scale.IdentityId.ToString() : "null";
        return base.ToString() +
            $"{nameof(TaskType)}: {strTaskType}. " +
            $"{nameof(Scale)}: {strScale}. " +
            $"{nameof(Enabled)}: {Enabled}. ";
    }

    public virtual bool Equals(TaskEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        if (TaskType != null && item.TaskType != null && !TaskType.Equals(item.TaskType))
            return false;
        if (Scale != null && item.Scale != null && !Scale.Equals(item.Scale))
            return false;
        return base.Equals(item) &&
            Equals(Enabled, item.Enabled);
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
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(Enabled, false);
    }

    public new virtual object Clone()
    {
        TaskEntity item = new();
        item.TaskType = TaskType.CloneCast();
        item.Scale = Scale.CloneCast();
        item.Enabled = Enabled;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual TaskEntity CloneCast() => (TaskEntity)Clone();

    #endregion
}
