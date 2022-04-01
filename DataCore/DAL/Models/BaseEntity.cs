// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCore.DAL.Models
{
    public class BaseEntity : ICloneable
    {
        #region Public and private fields and properties

        public virtual PrimaryColumnEntity PrimaryColumn { get; set; } = new PrimaryColumnEntity(ColumnName.Default);
        public virtual long Id { get => PrimaryColumn.Id; set { PrimaryColumn.Id = value; } }
        public virtual Guid Uid { get => PrimaryColumn.Uid; set { PrimaryColumn.Uid = value; } }
        public virtual DateTime CreateDt { get; set; } = default;
        public virtual DateTime ChangeDt { get; set; } = default;
        public virtual bool IsMarked { get; set; } = false;
        public virtual bool IsMarkedGui
        {
            get => IsMarked == true;
            set => IsMarked = value;
        }

        #endregion

        #region Public and private methods

        public virtual bool EqualsEmpty() => PrimaryColumn == null;

        #endregion

        #region Public and private methods - override

        public override string ToString()
        {
            string strCreateDt = CreateDt != null ? CreateDt.ToString() : "null";
            string strChangeDt = ChangeDt != null ? ChangeDt.ToString() : "null";
            return
                $"{nameof(PrimaryColumn)}: {(PrimaryColumn == null ? "null" : PrimaryColumn.ToString())}. " +
                $"{nameof(CreateDt)}: {strCreateDt}. " +
                $"{nameof(ChangeDt)}: {strChangeDt}. " +
                $"{nameof(IsMarked)}: {IsMarked}. ";
        }

        public override int GetHashCode() => PrimaryColumn == null ? -1 : PrimaryColumn.GetHashCode();

        public virtual bool Equals(BaseEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return 
                PrimaryColumn != null && 
                PrimaryColumn.Equals(entity.PrimaryColumn) &&
                Equals(CreateDt, entity.CreateDt) &&
                Equals(ChangeDt, entity.ChangeDt) &&
                Equals(IsMarked, entity.IsMarked);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BaseEntity)obj);
        }

        public virtual bool EqualsDefault()
        {
            return
                (PrimaryColumn == null || PrimaryColumn.EqualsDefault()) &&
                Equals(CreateDt, default) &&
                Equals(ChangeDt, default) &&
                Equals(IsMarked, false);
        }

        public virtual object Clone() => PrimaryColumn == null ? new object() : PrimaryColumn.Clone();

        #endregion
    }
}
