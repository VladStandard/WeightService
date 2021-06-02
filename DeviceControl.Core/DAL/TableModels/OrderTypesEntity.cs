namespace DeviceControl.Core.DAL.TableModels
{
    public class OrderTypesEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Description { get; set; }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                $"{nameof(Description)}: {Description}.";
        }

        public virtual bool Equals(OrderTypesEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(Description, entity.Description);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((OrderTypesEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new OrderTypesEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(Description, default(string));
        }

        public override object Clone()
        {
            return new OrderTypesEntity
            {
                Id = Id,
                Description = Description
            };
        }

        #endregion
    }
}
