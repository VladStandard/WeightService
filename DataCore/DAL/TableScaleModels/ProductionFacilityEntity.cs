// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Таблица "Производственные площадки".
    /// </summary>
    public class ProductionFacilityEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; } = string.Empty;
        public virtual Guid? IdRRef { get; set; } = null;

        #endregion

        #region Constructor and destructor

        public ProductionFacilityEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Id);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                $"{nameof(Name)}: {Name}. " +
                $"{nameof(IdRRef)}: {IdRRef}. ";
        }

        public virtual bool Equals(ProductionFacilityEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(Name, entity.Name) &&
                   Equals(IdRRef, entity.IdRRef);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ProductionFacilityEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new ProductionFacilityEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(Name, default(string)) &&
                   Equals(IdRRef, default(Guid?));
        }

        public override object Clone()
        {
            return new ProductionFacilityEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                CreateDt = CreateDt,
                ChangeDt = ChangeDt,
                IsMarked = IsMarked,
                Name = Name,
                IdRRef = IdRRef,
            };
        }

        #endregion
    }
}
