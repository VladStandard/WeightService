// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    public class AccessEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual DateTime CreateDt { get; set; }
        public virtual DateTime ChangeDt { get; set; }
        public virtual bool IsMarked { get; set; } = false;
        public virtual string User { get; set; } = string.Empty;
        public virtual byte Rights { get; set; } = 0;

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
                   $"{nameof(IsMarked)}: {IsMarked}. " +
                   $"{nameof(User)}: {User}. " +
                   $"{nameof(Rights)}: {Rights}. ";
        }

        public virtual bool Equals(AccessEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDt, entity.CreateDt) &&
                   Equals(ChangeDt, entity.ChangeDt) &&
                   Equals(IsMarked, entity.IsMarked) &&
                   Equals(User, entity.User) &&
                   Equals(Rights, entity.Rights);
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
                   Equals(IsMarked, false) &&
                   Equals(User, string.Empty) &&
                   Equals(Rights, 0);
        }

        public override object Clone()
        {
            return new AccessEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Uid = Uid,
                CreateDt = CreateDt,
                ChangeDt = ChangeDt,
                IsMarked = IsMarked,
                User = User,
                Rights = Rights,
            };
        }

        #endregion
    }
}
