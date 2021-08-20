// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace BlazorCore.DAL
{
    public class BaseUidEntity : BaseEntity, ICloneable, IBaseUidEntity
    {
        #region Public and private fields and properties

        public virtual Guid Uid { get; set; }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return $"{nameof(Uid)}: {Uid}. ";
        }

        public override int GetHashCode()
        {
            return Uid.GetHashCode();
        }

        public virtual bool Equals(BaseUidEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return Uid.Equals(entity.Uid);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BaseUidEntity)obj);
        }

        public virtual bool EqualsDefault()
        {
            return Equals(Uid, default(Guid));
        }

        public virtual object Clone()
        {
            return new BaseUidEntity
            {
                Uid = Uid,
            };
        }

        #endregion
    }
}
