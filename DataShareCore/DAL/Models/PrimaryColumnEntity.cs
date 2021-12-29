// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataShareCore.DAL.Models
{
    public enum ColumnName
    {
        Id = 0,
        Uid = 1,
    }

    public class PrimaryColumnEntity
    {
        #region Public and private fields and properties

        public ColumnName Name { get; set; }

        public int Id { get; set; }
        public Guid Uid { get; set; }

        #endregion

        #region Public and private methods

        public PrimaryColumnEntity(int id)
        {
            Name = ColumnName.Id;
            Id = id;
            Uid = default;
        }

        public PrimaryColumnEntity(Guid uid)
        {
            Name = ColumnName.Uid;
            Id = default;
            Uid = uid;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return Id != default 
                ? $"{nameof(Id)}: {Id}. "
                : $"{nameof(Uid)}: {Uid}. ";
        }

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

        public virtual object Clone() => new PrimaryColumnEntity(Id) { Name = Name, Uid = Uid };

        #endregion
    }
}
