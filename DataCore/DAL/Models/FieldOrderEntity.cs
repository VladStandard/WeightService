// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.DAL.Models
{
    public class FieldOrderEntity
    {
        #region Public and private fields and properties

        public bool Use { get; set; }
        public ShareEnums.DbField Name { get; set; }
        public ShareEnums.DbOrderDirection Direction { get; set; }

        #endregion

        #region Constructor and destructor

        public FieldOrderEntity(ShareEnums.DbField name, ShareEnums.DbOrderDirection direction)
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

        public virtual bool Equals(FieldOrderEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            return Use.Equals(item.Use) &&
                   Name.Equals(item.Name) &&
                   Direction.Equals(item.Direction);
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
