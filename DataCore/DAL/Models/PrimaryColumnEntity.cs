// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCore.DAL.Models
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
        public long Id { get; set; }
        public Guid Uid { get; set; }

        #endregion

        #region Public and private methods

        public PrimaryColumnEntity() : this(ColumnName.Default, default, Guid.Empty) { }

        public PrimaryColumnEntity(ColumnName columnName) : this(columnName, default, Guid.Empty) { }

        public PrimaryColumnEntity(ColumnName columnName, int id, Guid uid)
        {
            Name = columnName;
            Id = id;
            Uid = uid;
        }

        #endregion

        #region Public and private methods

        public override string ToString() => Name switch
        {
            ColumnName.Id => $"{nameof(Id)}: {Id}. ",
            ColumnName.Uid => $"{nameof(Uid)}: {Uid}. ",
            _ => $"{nameof(Name)}: {Name}. ",
        };

        public override int GetHashCode()
        {
            return Name switch
            {
                ColumnName.Uid => Uid.GetHashCode(),
                _ => Id.GetHashCode(),
            };
        }

        public virtual bool Equals(PrimaryColumnEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return Equals(Id, entity.Id) &&
                Equals(Uid, entity.Uid);
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
            return Name switch
            {
                ColumnName.Uid => Equals(Uid, default(Guid)),
                _ => Equals(Id, default(int)),
            };
        }

        public virtual object Clone() => new PrimaryColumnEntity(Name) { Id = Id, Uid = Uid };

        #endregion
    }
}
