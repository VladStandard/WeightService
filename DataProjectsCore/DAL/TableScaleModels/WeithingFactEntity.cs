// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using System;

namespace DataProjectsCore.DAL.TableScaleModels
{
    public class WeithingFactEntity : BaseIdEntity
    {
        #region Public and private fields and properties

        public virtual PluEntity Plu { get; set; } = new PluEntity();
        public virtual ScaleEntity Scales { get; set; } = new ScaleEntity();
        public virtual ProductSeriesEntity Series { get; set; } = new ProductSeriesEntity();
        public virtual OrderEntity Orders { get; set; } = new OrderEntity();
        public virtual string Sscc { get; set; }
        public virtual DateTime? WeithingDate { get; set; }
        public virtual decimal NetWeight { get; set; }
        public virtual decimal TareWeight { get; set; }
        public virtual Guid Uid { get; set; }
        public virtual DateTime? ProductDate { get; set; }
        public virtual int? RegNum { get; set; }
        public virtual int? Kneading { get; set; }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            var strPlu = Plu != null ? Plu.Id.ToString() : "null";
            var strScale = Scales != null ? Scales.Id.ToString() : "null";
            var strSeries = Series != null ? Series.Id.ToString() : "null";
            var strOrder = Orders != null ? Orders.Id.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Plu)}: {strPlu}. " +
                   $"{nameof(Scales)}: {strScale}. " +
                   $"{nameof(Series)}: {strSeries}. " +
                   $"{nameof(Orders)}: {strOrder}. " +
                   $"{nameof(Sscc)}: {Sscc}. " +
                   $"{nameof(WeithingDate)}: {WeithingDate}. " +
                   $"{nameof(NetWeight)}: {NetWeight}. " +
                   $"{nameof(TareWeight)}: {TareWeight}." +
                   $"{nameof(Uid)}: {Uid}." +
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
                   Equals(Uid, entity.Uid) &&
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
                   Equals(Sscc, default(string)) &&
                   Equals(WeithingDate, default(DateTime?)) &&
                   Equals(NetWeight, default(decimal)) &&
                   Equals(TareWeight, default(decimal)) &&
                   Equals(Uid, default(Guid?)) &&
                   Equals(ProductDate, default(DateTime?)) &&
                   Equals(RegNum, default(int?)) &&
                   Equals(Kneading, default(int?));
        }

        public override object Clone()
        {
            return new WeithingFactEntity
            {
                Id = Id,
                Plu = (PluEntity)Plu?.Clone(),
                Scales = (ScaleEntity)Scales?.Clone(),
                Series = (ProductSeriesEntity)Series?.Clone(),
                Orders = (OrderEntity)Orders?.Clone(),
                Sscc = Sscc,
                WeithingDate = WeithingDate,
                NetWeight = NetWeight,
                TareWeight = TareWeight,
                Uid = Uid,
                ProductDate = ProductDate,
                RegNum = RegNum,
                Kneading = Kneading,
            };
        }

        #endregion
    }
}
