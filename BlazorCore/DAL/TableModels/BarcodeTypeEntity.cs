// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorCore.DAL.TableModels
{
    public class BarcodeTypeEntity : BaseIdEntity
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() + 
                $"{nameof(Name)}: {Name}.";
        }

        public virtual bool Equals(BarcodeTypeEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(Name, entity.Name);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BarcodeTypeEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new BarcodeTypeEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() && 
            Equals(Name, default(string));
        }

        public override object Clone()
        {
            return new BarcodeTypeEntity
            {
                Id = Id,
                Name = Name
            };
        }

        #endregion
    }
}
