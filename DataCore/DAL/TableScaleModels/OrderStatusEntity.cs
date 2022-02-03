// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Таблица "Статусы заказов".
    /// </summary>
    public class OrderStatusEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string OrderId { get; set; } = string.Empty;
        public virtual DateTime? CurrentDate { get; set; }
        public virtual byte? CurrentStatus { get; set; }

        #endregion

        #region Constructor and destructor

        public OrderStatusEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Id);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                $"{nameof(OrderId)}: {OrderId}. " +
                $"{nameof(CurrentDate)}: {CurrentDate}. " +
                $"{nameof(CurrentStatus)}: {CurrentStatus}.";
        }

        public virtual bool Equals(OrderStatusEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(OrderId, entity.OrderId) &&
                   Equals(CurrentDate, entity.CurrentDate) &&
                   Equals(CurrentStatus, entity.CurrentStatus);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((OrderStatusEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new OrderStatusEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(OrderId, default(string)) &&
                   Equals(CurrentDate, default(DateTime?)) &&
                   Equals(CurrentStatus, default(byte?));
        }

        public override object Clone()
        {
            return new OrderStatusEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Id = Id,
                OrderId = OrderId,
                CurrentDate = CurrentDate,
                CurrentStatus = CurrentStatus
            };
        }

        #endregion
    }
}
