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

        public virtual DateTime? CreateDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual string Name { get; set; } = string.Empty;
        public virtual Guid? IdRRef { get; set; } = null;
        public virtual bool Marked { get; set; }

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
                $"{nameof(CreateDate)}: {CreateDate}. " +
                $"{nameof(ModifiedDate)}: {ModifiedDate}. " +
                $"{nameof(Name)}: {Name}. " +
                $"{nameof(IdRRef)}: {IdRRef}. " +
                $"{nameof(Marked)}: {Marked}. ";
        }

        public virtual bool Equals(ProductionFacilityEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDate, entity.CreateDate) &&
                   Equals(ModifiedDate, entity.ModifiedDate) &&
                   Equals(Name, entity.Name) &&
                   Equals(IdRRef, entity.IdRRef) &&
                   Equals(Marked, entity.Marked);
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
                   Equals(CreateDate, default(DateTime?)) &&
                   Equals(ModifiedDate, default(DateTime?)) &&
                   Equals(Name, default(string)) &&
                   Equals(IdRRef, default(Guid?)) &&
                   Equals(Marked, default(bool));
        }

        public override object Clone()
        {
            return new ProductionFacilityEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Id = Id,
                CreateDate = CreateDate,
                ModifiedDate = ModifiedDate,
                Name = Name,
                IdRRef = IdRRef,
                Marked = Marked,
            };
        }

        #endregion
    }
}
