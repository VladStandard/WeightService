// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Table "Organizations".
    /// </summary>
    public class OrganizationEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; }
        public virtual int Gln { get; set; }
        public virtual string SerializedRepresentationObject { get; set; }

        #endregion

        #region Constructor and destructor

        public OrganizationEntity() : this(0)
        {
            //
        }

        public OrganizationEntity(long id) : base(id)
        {
            Name = string.Empty;
            Gln = default;
            SerializedRepresentationObject = string.Empty;
        }

        #endregion

        #region Public and private methods

        public override string ToString() =>
            base.ToString() +
            $"{nameof(Name)}: {Name}. " +
            $"{nameof(Gln)}: {Gln}. " +
            $"{nameof(SerializedRepresentationObject)}: {SerializedRepresentationObject.Length}. ";

        public virtual bool Equals(OrganizationEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            return base.Equals(item) &&
                   Equals(Name, item.Name) &&
                   Equals(Gln, item.Gln) &&
                   Equals(SerializedRepresentationObject, item.SerializedRepresentationObject);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((OrganizationEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new OrganizationEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault(IdentityName) &&
                   Equals(Name, string.Empty) &&
                   Equals(Gln, 0) &&
                   Equals(SerializedRepresentationObject, string.Empty);
        }

        public override object Clone()
        {
            OrganizationEntity item = (OrganizationEntity)base.Clone();
            item.Name = Name;
            item.Gln = Gln;
            item.SerializedRepresentationObject = SerializedRepresentationObject;
            return item;
        }

        #endregion
    }
}
