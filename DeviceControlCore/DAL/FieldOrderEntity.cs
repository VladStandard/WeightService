using DeviceControlCore.DAL.DataModels;

namespace DeviceControlCore.DAL
{
    public class FieldOrderEntity
    {
        #region Public and private fields and properties

        public bool Use { get; set; }
        public EnumField Name { get; set; }
        public EnumOrderDirection Direction { get; set; }

        #endregion

        #region Constructor and destructor

        public FieldOrderEntity(EnumField name, EnumOrderDirection direction)
        {
            Use = true;
            Name = name;
            Direction = direction;
        }

        public FieldOrderEntity()
        {
            Use = false;
        }

        #endregion

        #region Public and private methods

        public virtual bool Equals(FieldOrderEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return Use.Equals(entity.Use) && 
                   Name.Equals(entity.Name) &&
                   Direction.Equals(entity.Direction);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((FieldOrderEntity)obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(Use)}: {Use}. " +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(Direction)}: {Direction}.";
        }

        #endregion
    }
}
