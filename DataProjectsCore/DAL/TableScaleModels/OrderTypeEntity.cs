﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;

namespace DataProjectsCore.DAL.TableScaleModels
{
    /// <summary>
    /// Таблица "Типы заказов".
    /// </summary>
    public class OrderTypeEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Description { get; set; }

        #endregion

        #region Constructor and destructor

        public OrderTypeEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Id);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                $"{nameof(Description)}: {Description}.";
        }

        public virtual bool Equals(OrderTypeEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(Description, entity.Description);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((OrderTypeEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new OrderTypeEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(Description, default(string));
        }

        public override object Clone()
        {
            return new OrderTypeEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Id = Id,
                Description = Description
            };
        }

        #endregion
    }
}
