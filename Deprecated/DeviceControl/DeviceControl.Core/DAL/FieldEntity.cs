using DeviceControl.Core.DAL.DataModels;

namespace DeviceControl.Core.DAL
{
    public class FieldEntity
    {
        #region Public and private fields and properties

        public EnumField Name { get; set; }
        public object Value { get; set; }

        #endregion

        #region Constructor and destructor

        public FieldEntity(EnumField name, object value)
        {
            Name = name;
            Value = value;
        }

        #endregion

        #region Public and private methods

        public virtual bool Equals(FieldEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return Name.Equals(entity.Name) &&
                   Value.Equals(entity.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((FieldEntity)obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}. " +
                   $"{nameof(Value)}: {Value}.";
        }

        #endregion
    }
}
