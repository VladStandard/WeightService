// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using System;

namespace DataProjectsCore.DAL.TableSystemModels
{
    public class AccessEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual DateTime CreateDt { get; set; }
        public virtual DateTime ChangeDt { get; set; }
        public virtual string? User { get; set; }
        public virtual bool? Level { get; set; }

        #endregion

        #region Constructor and destructor

        public AccessEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Uid);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                   $"{nameof(Uid)}: {Uid}. " +
                   $"{nameof(CreateDt)}: {CreateDt}. " +
                   $"{nameof(ChangeDt)}: {ChangeDt}. " +
                   $"{nameof(User)}: {User}. " +
                   $"{nameof(Level)}: {Level}. ";
        }

        public virtual bool Equals(AccessEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDt, entity.CreateDt) &&
                   Equals(ChangeDt, entity.ChangeDt) &&
                   Equals(User, entity.User) &&
                   Equals(Level, entity.Level);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((AccessEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new AccessEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(CreateDt, default(DateTime)) &&
                   Equals(ChangeDt, default(DateTime)) &&
                   Equals(User, default(string)) &&
                   Equals(Level, default(bool?));
        }

        public override object Clone()
        {
            return new AccessEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Uid = Uid,
                CreateDt = CreateDt,
                ChangeDt = ChangeDt,
                User = User,
                Level = Level,
            };
        }

        #endregion
    }
}
