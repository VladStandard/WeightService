// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Table "WeithingFacts".
    /// </summary>
    public class WeithingFactEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual PluEntity Plu { get; set; }
        public virtual ScaleEntity Scales { get; set; }
        public virtual ProductSeriesEntity Series { get; set; }
        public virtual OrderEntity Orders { get; set; }
        public virtual string Sscc { get; set; }
        public virtual DateTime WeithingDate { get; set; }
        public virtual decimal NetWeight { get; set; }
        public virtual decimal TareWeight { get; set; }
        public virtual DateTime ProductDate { get; set; }
        public virtual int? RegNum { get; set; }
        public virtual int? Kneading { get; set; }

        #endregion

        #region Constructor and destructor

        public WeithingFactEntity() : this(0)
        {
            //
        }

        public WeithingFactEntity(long id) : base(id)
        {
            Plu = new();
            Scales = new();
            Series = new();
            Orders = new();
            Sscc = string.Empty;
            WeithingDate = DateTime.MinValue;
            NetWeight = 0;
            TareWeight = 0;
            ProductDate = DateTime.MinValue;
            RegNum = null;
            Kneading = null;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string? strPlu = Plu != null ? Plu.IdentityId.ToString() : "null";
            string? strScale = Scales != null ? Scales.IdentityId.ToString() : "null";
            string? strSeries = Series != null ? Series.IdentityId.ToString() : "null";
            string? strOrder = Orders != null ? Orders.IdentityId.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Plu)}: {strPlu}. " +
                   $"{nameof(Scales)}: {strScale}. " +
                   $"{nameof(Series)}: {strSeries}. " +
                   $"{nameof(Orders)}: {strOrder}. " +
                   $"{nameof(Sscc)}: {Sscc}. " +
                   $"{nameof(WeithingDate)}: {WeithingDate}. " +
                   $"{nameof(NetWeight)}: {NetWeight}. " +
                   $"{nameof(TareWeight)}: {TareWeight}." +
                   $"{nameof(ProductDate)}: {ProductDate}." +
                   $"{nameof(RegNum)}: {RegNum}." +
                   $"{nameof(Kneading)}: {Kneading}.";
        }

        public virtual bool Equals(WeithingFactEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Plu.Equals(entity.Plu) &&
                   Scales.Equals(entity.Scales) &&
                   Series.Equals(entity.Series) &&
                   Orders.Equals(entity.Orders) &&
                   Equals(Sscc, entity.Sscc) &&
                   Equals(WeithingDate, entity.WeithingDate) &&
                   Equals(NetWeight, entity.NetWeight) &&
                   Equals(TareWeight, entity.TareWeight) &&
                   Equals(ProductDate, entity.ProductDate) &&
                   Equals(RegNum, entity.RegNum) &&
                   Equals(Kneading, entity.Kneading);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((WeithingFactEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new WeithingFactEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (Plu != null && !Plu.EqualsDefault())
                return false;
            if (Scales != null && !Scales.EqualsDefault())
                return false;
            if (Series != null && !Series.EqualsDefault())
                return false;
            if (Orders != null && !Orders.EqualsDefault())
                return false;
            return base.EqualsDefault() &&
                   Equals(Sscc, string.Empty) &&
                   Equals(WeithingDate, DateTime.MinValue) &&
                   Equals(NetWeight, 0) &&
                   Equals(TareWeight, 0) &&
                   Equals(ProductDate, DateTime.MinValue) &&
                   Equals(RegNum, null) &&
                   Equals(Kneading, null);
        }

        public override object Clone()
        {
            WeithingFactEntity item = (WeithingFactEntity)base.Clone();
            item.Plu = (PluEntity)Plu.Clone();
            item.Scales = (ScaleEntity)Scales.Clone();
            item.Series = (ProductSeriesEntity)Series.Clone();
            item.Orders = (OrderEntity)Orders.Clone();
            item.Sscc = Sscc;
            item.WeithingDate = WeithingDate;
            item.NetWeight = NetWeight;
            item.TareWeight = TareWeight;
            item.ProductDate = ProductDate;
            item.RegNum = RegNum;
            item.Kneading = Kneading;
            return item;
        }

        #endregion
    }
}
