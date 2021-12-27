// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataShareCore.DAL.Models
{
    public enum ColumnName
    {
        Id,
        Uid,
    }

    public class PrimaryColumnEntity
    {
        #region Public and private fields and properties

        public ColumnName Name { get; private set; }
        public object? Value { get; set; }

        #endregion

        #region Public and private methods

        public PrimaryColumnEntity(ColumnName name, object? value)
        {
            Name = name;
            Value = value;
        }

        #endregion

        #region Public and private methods

        private string GetName() => Name switch { ColumnName.Uid => "UID", _ => "ID" };
        private object? GetValue() => Value == null ? null : Name switch { ColumnName.Uid => (Guid)Value, _ => (int)Value };

        public override string ToString()
        {
            return $"{nameof(Name)}: {GetName()}. {nameof(Value)}: {GetValue()}.";
        }

        public override int GetHashCode()
        {
            switch (Name)
            {
                case ColumnName.Uid:
                    Guid? uid = (Guid?)GetValue();
                    return uid.GetHashCode();
                default:
                    int? id = (int?)GetValue();
                    return id.GetHashCode();
            }
        }

        public virtual bool Equals(PrimaryColumnEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return Equals(GetValue(), entity.GetValue());
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((PrimaryColumnEntity)obj);
        }

        public virtual bool EqualsDefault()
        {
            switch (Name)
            {
                case ColumnName.Uid:
                    Guid? uid = (Guid?)GetValue();
                    return Equals(uid, default(Guid?));
                default:
                    int? id = (int?)GetValue();
                    return Equals(id, default(int?));
            }
        }

        public virtual object Clone() => new PrimaryColumnEntity(Name, Value);

        #endregion
    }
}
