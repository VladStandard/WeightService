// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.TableScaleModels;
using System;

namespace DataCore.DAL.Models
{
    /// <summary>
    /// Table "Access".
    /// </summary>
    public class BaseDummyEntity : BaseEntity<BaseDummyEntity>
    {
        #region Public and private fields and properties

        //

        #endregion

        #region Constructor and destructor

        public BaseDummyEntity() : this(Guid.Empty)
        {
            //
        }

        public BaseDummyEntity(Guid uid) : base(uid)
        {
            //
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString();
        }

        public virtual bool Equals(BaseDummyEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BaseDummyEntity)obj);
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
            return base.EqualsDefault();
        }

        public override object Clone()
        {
            BaseDummyEntity item = (BaseDummyEntity)base.Clone();
            return item;
        }

        #endregion
    }
}
