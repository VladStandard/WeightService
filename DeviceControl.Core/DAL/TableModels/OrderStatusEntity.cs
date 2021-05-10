using System;

namespace DeviceControl.Core.DAL.TableModels
{
    public class OrderStatusEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string OrderId { get; set; }
        public virtual DateTime? CurrentDate { get; set; }
        public virtual byte? CurrentStatus { get; set; }

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
                Id = Id,
                OrderId = OrderId,
                CurrentDate = CurrentDate,
                CurrentStatus = CurrentStatus
            };
        }

        #endregion
    }
}
