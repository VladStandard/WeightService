// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataShareCore.DAL.Models
{
    public enum ColumnName
    {
        Default,
        Id,
        Uid,
    }

    public class PrimaryColumnEntity
    {
        #region Public and private fields and properties

        public ColumnName Name { get; private set; }
        public object Value { get; set; }
        public int Id { get; set; }
        public int Guid { get; set; }

        #endregion

        #region Public and private methods

        public PrimaryColumnEntity() : this(ColumnName.Default) { }

        public PrimaryColumnEntity(ColumnName name)
        {
            Name = name;
            Value = Name switch
            {
                ColumnName.Uid => default(Guid?),
                ColumnName.Id => default(int?),
                _ => null,
            };
        }

        public PrimaryColumnEntity(ColumnName name, object? value)
        {
            Name = name;
            Value = value;
        }

        #endregion

        #region Public and private methods

        public string GetName() => Name switch { ColumnName.Uid => "UID", ColumnName.Id => "ID" , _ => string.Empty };
        public object? GetValue() => Value == null ? null : Name switch { ColumnName.Uid => (Guid)Value, ColumnName.Id => (int)Value, _ => null };
        public int GetValueAsInt() => Value == null ? 0 : Name switch { ColumnName.Id => (int)Value, _ => 0 };
        public Guid GetValueAsGuid() => Value == null ? Guid.Empty : Name switch { ColumnName.Uid => (Guid)Value, _ => Guid.Empty };

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
                case ColumnName.Id:
                    int? id = (int?)GetValue();
                    return id.GetHashCode();
                default:
                    throw new ArgumentException($"{nameof(Name)} must be set!");
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
                case ColumnName.Id:
                    int? id = (int?)GetValue();
                    return Equals(id, default(int?));
                default:
                    throw new ArgumentException($"{nameof(Name)} must be set!");
            }
        }

        public virtual object Clone() => new PrimaryColumnEntity(Name, Value);

        #endregion
    }
}
