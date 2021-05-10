using System;

namespace DeviceControl.Core.DAL.TableModels
{
    public class ContragentsEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual DateTime? CreateDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual string Name { get; set; }
        public virtual bool? Marked { get; set; }
        public virtual bool MarkedGui
        {
            get => Marked == true;
            set => Marked = value;
        }
        public virtual string SerializedRepresentationObject { get; set; }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() + 
                $"{nameof(CreateDate)}: {CreateDate}. " +
                $"{nameof(ModifiedDate)}: {ModifiedDate}. " +
                $"{nameof(Name)}: {Name}. " +
                $"{nameof(Marked)}: {Marked}." +
                $"{nameof(SerializedRepresentationObject)}.Length: {SerializedRepresentationObject?.Length ?? 0}. ";
        }

        public virtual bool Equals(ContragentsEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDate, entity.CreateDate) &&
                   Equals(ModifiedDate, entity.ModifiedDate) &&
                   Equals(Name, entity.Name) &&
                   Equals(Marked, entity.Marked) &&
                   Equals(SerializedRepresentationObject, entity.SerializedRepresentationObject);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ContragentsEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new ContragentsEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(CreateDate, default(DateTime?)) && 
                   Equals(ModifiedDate, default(DateTime?)) && 
                   Equals(Name, default(string)) && 
                   Equals(Marked, default(bool?)) &&
                   Equals(SerializedRepresentationObject, default(string));
        }

        public override object Clone()
        {
            return new ContragentsEntity
            {
                Id = Id,
                CreateDate = CreateDate,
                ModifiedDate = ModifiedDate,
                Name = Name,
                Marked = Marked,
                SerializedRepresentationObject = SerializedRepresentationObject,
            };
        }

        #endregion
    }
}
