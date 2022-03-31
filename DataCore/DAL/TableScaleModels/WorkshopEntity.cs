// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Таблица "Цеха".
    /// </summary>
    public class WorkshopEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual DateTime CreateDate { get; set; } = default;
        public virtual DateTime ModifiedDate { get; set; } = default;
        public virtual ProductionFacilityEntity ProductionFacility { get; set; } = new ProductionFacilityEntity();
        public virtual string Name { get; set; } = string.Empty;
        public virtual Guid? IdRRef { get; set; } = null;
        public virtual bool IsMarked { get; set; } = false;

        #endregion

        #region Constructor and destructor

        public WorkshopEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Id);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string? strProductionFacility = ProductionFacility != null ? ProductionFacility.Id.ToString() : "null";
            return base.ToString() +
                   $"{nameof(ProductionFacility)}: {strProductionFacility}. " +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(CreateDate)}: {CreateDate}. " +
                   $"{nameof(ModifiedDate)}: {ModifiedDate}. " +
                   $"{nameof(IdRRef)}: {IdRRef}. " +
                   $"{nameof(IsMarked)}: {IsMarked}. ";
        }

        public virtual bool Equals(WorkshopEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDate, entity.CreateDate) &&
                   Equals(ModifiedDate, entity.ModifiedDate) &&
                   ProductionFacility.Equals(entity.ProductionFacility) &&
                   Equals(Name, entity.Name) &&
                   Equals(IdRRef, entity.IdRRef) &&
                   Equals(IsMarked, entity.IsMarked);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((WorkshopEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new WorkshopEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (ProductionFacility != null && !ProductionFacility.EqualsDefault())
                return false;
            return base.EqualsDefault() &&
                   Equals(CreateDate, default(DateTime)) &&
                   Equals(ModifiedDate, default(DateTime)) &&
                   Equals(Name, default(string)) &&
                   Equals(IdRRef, default(Guid?)) &&
                   Equals(IsMarked, false);
        }

        public override object Clone()
        {
            return new WorkshopEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Id = Id,
                CreateDate = CreateDate,
                ModifiedDate = ModifiedDate,
                ProductionFacility = (ProductionFacilityEntity)ProductionFacility?.Clone(),
                Name = Name,
                IdRRef = IdRRef,
                IsMarked = IsMarked,
            };
        }

        #endregion
    }
}
