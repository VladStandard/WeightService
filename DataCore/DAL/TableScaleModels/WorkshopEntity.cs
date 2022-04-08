// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Table "WorkShops".
    /// </summary>
    public class WorkShopEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual ProductionFacilityEntity ProductionFacility { get; set; }
        public virtual string Name { get; set; }
        public virtual Guid IdRRef { get; set; }

        #endregion

        #region Constructor and destructor

        public WorkShopEntity() : this(0)
        {
            //
        }

        public WorkShopEntity(long id) : base(id)
        {
            ProductionFacility = new ProductionFacilityEntity();
            Name = string.Empty;
            IdRRef = Guid.Empty;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string? strProductionFacility = ProductionFacility != null ? ProductionFacility.IdentityId.ToString() : "null";
            return base.ToString() +
                   $"{nameof(ProductionFacility)}: {strProductionFacility}. " +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(IdRRef)}: {IdRRef}. ";
        }

        public virtual bool Equals(WorkShopEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   ProductionFacility.Equals(entity.ProductionFacility) &&
                   Equals(Name, entity.Name) &&
                   Equals(IdRRef, entity.IdRRef);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((WorkShopEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new WorkShopEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (ProductionFacility != null && !ProductionFacility.EqualsDefault())
                return false;
            return base.EqualsDefault() &&
                   Equals(Name, string.Empty) &&
                   Equals(IdRRef, Guid.Empty);
        }

        public override object Clone()
        {
            WorkShopEntity item = (WorkShopEntity)base.Clone();
            item.ProductionFacility = (ProductionFacilityEntity)ProductionFacility.Clone();
            item.Name = Name;
            item.IdRRef = IdRRef;
            return item;
        }

        #endregion
    }
}
