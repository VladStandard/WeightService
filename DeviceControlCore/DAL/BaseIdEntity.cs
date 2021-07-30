using System;

namespace DeviceControlCore.DAL
{
    public class BaseIdEntity : BaseEntity, ICloneable
    {
        #region Public and private fields and properties

        public virtual int Id { get; set; }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}. ";
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public virtual bool Equals(BaseIdEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return Id.Equals(entity.Id);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BaseIdEntity)obj);
        }

        public virtual bool EqualsDefault()
        {
            return Equals(Id, default(int));
        }

        public virtual object Clone()
        {
            return new BaseIdEntity
            {
                Id = Id,
            };
        }

        #endregion
    }
}
