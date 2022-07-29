// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System;

namespace DataCore.Sql.TableScaleModels
{
    /// <summary>
    /// Table "Orders".
    /// </summary>
    public class OrderEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual OrderTypeEntity OrderTypes { get; set; }
        public virtual DateTime ProductDate { get; set; }
        public virtual int? PlaneBoxCount { get; set; }
        public virtual int? PlanePalletCount { get; set; }
        public virtual DateTime PlanePackingOperationBeginDate { get; set; }
        public virtual DateTime PlanePackingOperationEndDate { get; set; }
        public virtual ScaleEntity Scales { get; set; }
        public virtual PluEntity Plu { get; set; }
        public virtual Guid IdRRef { get; set; }
        public virtual TemplateEntity Templates { get; set; }

        #endregion

        #region Constructor and destructor

        public OrderEntity() : this(0)
        {
            //
        }

        public OrderEntity(long id) : base(id)
        {
            OrderTypes = new();
            ProductDate = DateTime.MinValue;
            PlaneBoxCount = default;
            PlanePalletCount = default;
            PlanePackingOperationBeginDate = DateTime.MinValue;
            PlanePackingOperationEndDate = DateTime.MinValue;
            Scales = new();
            Plu = new();
            IdRRef = Guid.Empty;
            Templates = new();
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string strOrderTypes = OrderTypes != null ? OrderTypes.IdentityId.ToString() : "null";
            string strScales = Scales != null ? Scales.IdentityId.ToString() : "null";
            string strPlu = Plu != null ? Plu.IdentityId.ToString() : "null";
            string strTemplates = Templates != null ? Templates.IdentityId.ToString() : "null";
            return base.ToString() +
                   $"{nameof(OrderTypes)}: {strOrderTypes}. " +
                   $"{nameof(ProductDate)}: {ProductDate}. " +
                   $"{nameof(PlaneBoxCount)}: {PlaneBoxCount}. " +
                   $"{nameof(PlanePalletCount)}: {PlanePalletCount}. " +
                   $"{nameof(PlanePackingOperationBeginDate)}: {PlanePackingOperationBeginDate}. " +
                   $"{nameof(PlanePackingOperationEndDate)}: {PlanePackingOperationEndDate}. " +
                   $"{nameof(Scales)}: {strScales}. " +
                   $"{nameof(Plu)}: {strPlu}." +
                   $"{nameof(IdRRef)}: {IdRRef}." +
                   $"{nameof(Templates)}: {strTemplates}.";
        }

        public virtual bool Equals(OrderEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            if (OrderTypes != null && item.OrderTypes != null && !OrderTypes.Equals(item.OrderTypes))
                return false;
            if (Scales != null && item.Scales != null && !Scales.Equals(item.Scales))
                return false;
            if (Plu != null && item.Plu != null && !Plu.Equals(item.Plu))
                return false;
            if (Templates != null && item.Templates != null && !Templates.Equals(item.Templates))
                return false;
            return base.Equals(item) &&
                   Equals(ProductDate, item.ProductDate) &&
                   Equals(PlaneBoxCount, item.PlaneBoxCount) &&
                   Equals(PlanePalletCount, item.PlanePalletCount) &&
                   Equals(PlanePackingOperationBeginDate, item.PlanePackingOperationBeginDate) &&
                   Equals(PlanePackingOperationEndDate, item.PlanePackingOperationEndDate) &&
                   Equals(IdRRef, item.IdRRef);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((OrderEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new());
        }

        public new virtual bool EqualsDefault()
        {
            if (OrderTypes != null && !OrderTypes.EqualsDefault())
                return false;
            if (Plu != null && !Plu.EqualsDefault())
                return false;
            if (Scales != null && !Scales.EqualsDefault())
                return false;
            if (Templates != null && !Templates.EqualsDefault())
                return false;
            return base.EqualsDefault(IdentityName) &&
                   Equals(ProductDate, DateTime.MinValue) &&
                   Equals(PlaneBoxCount, null) &&
                   Equals(PlanePalletCount, null) &&
                   Equals(PlanePackingOperationBeginDate, DateTime.MinValue) &&
                   Equals(PlanePackingOperationEndDate, DateTime.MinValue) &&
                   Equals(IdRRef, Guid.Empty);
        }

        public new virtual object Clone()
        {
            OrderEntity item = new();
            item.OrderTypes = OrderTypes.CloneCast();
            item.ProductDate = ProductDate;
            item.PlaneBoxCount = PlaneBoxCount;
            item.PlanePalletCount = PlanePalletCount;
            item.PlanePackingOperationBeginDate = PlanePackingOperationBeginDate;
            item.PlanePackingOperationEndDate = PlanePackingOperationEndDate;
            item.Scales = Scales.CloneCast();
            item.Plu = Plu.CloneCast();
            item.IdRRef = IdRRef;
            item.Templates = Templates.CloneCast();
            item.Setup(((BaseEntity)this).CloneCast());
            return item;
        }

        public new virtual OrderEntity CloneCast() => (OrderEntity)Clone();

        #endregion
    }
}
