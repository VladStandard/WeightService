﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
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
        public virtual ScaleEntity Scale { get; set; }
        public virtual ProductSeriesEntity Serie { get; set; }
        public virtual OrderEntity Order { get; set; }
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
            Scale = new();
            Serie = new();
            Order = new();
            Sscc = string.Empty;
            WeithingDate = DateTime.MinValue;
            NetWeight = 0;
            TareWeight = 0;
            ProductDate = DateTime.MinValue;
            RegNum = null;
            Kneading = null;
        }

        private WeithingFactEntity(BaseEntity baseItem) : this()
        {
            base.Setup(baseItem);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string strPlu = Plu != null ? Plu.IdentityId.ToString() : "null";
            string strScale = Scale != null ? Scale.IdentityId.ToString() : "null";
            string strSeries = Serie != null ? Serie.IdentityId.ToString() : "null";
            string strOrder = Order != null ? Order.IdentityId.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Plu)}: {strPlu}. " +
                   $"{nameof(Scale)}: {strScale}. " +
                   $"{nameof(Serie)}: {strSeries}. " +
                   $"{nameof(Order)}: {strOrder}. " +
                   $"{nameof(Sscc)}: {Sscc}. " +
                   $"{nameof(WeithingDate)}: {WeithingDate}. " +
                   $"{nameof(NetWeight)}: {NetWeight}. " +
                   $"{nameof(TareWeight)}: {TareWeight}." +
                   $"{nameof(ProductDate)}: {ProductDate}." +
                   $"{nameof(RegNum)}: {RegNum}." +
                   $"{nameof(Kneading)}: {Kneading}.";
        }

        public virtual bool Equals(WeithingFactEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            return base.Equals(item) &&
                   Plu != null && item.Plu != null && Plu.Equals(item.Plu) &&
                   Scale != null && item.Scale != null && Scale.Equals(item.Scale) &&
                   Serie != null && item.Serie != null && Serie.Equals(item.Serie) &&
                   Order != null && item.Order != null && Order.Equals(item.Order) &&
                   Equals(Sscc, item.Sscc) &&
                   Equals(WeithingDate, item.WeithingDate) &&
                   Equals(NetWeight, item.NetWeight) &&
                   Equals(TareWeight, item.TareWeight) &&
                   Equals(ProductDate, item.ProductDate) &&
                   Equals(RegNum, item.RegNum) &&
                   Equals(Kneading, item.Kneading);
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
            if (Scale != null && !Scale.EqualsDefault())
                return false;
            if (Serie != null && !Serie.EqualsDefault())
                return false;
            if (Order != null && !Order.EqualsDefault())
                return false;
            return base.EqualsDefault(IdentityName) &&
                   Equals(Sscc, string.Empty) &&
                   Equals(WeithingDate, DateTime.MinValue) &&
                   Equals(NetWeight, (decimal)0) &&
                   Equals(TareWeight, (decimal)0) &&
                   Equals(ProductDate, DateTime.MinValue) &&
                   Equals(RegNum, null) &&
                   Equals(Kneading, null);
        }

        public override object Clone()
        {
            WeithingFactEntity item = new((BaseEntity)base.Clone())
            {
                //
            };

            item.Plu = (PluEntity)Plu.Clone();
            item.Scale = (ScaleEntity)Scale.Clone();
            item.Serie = (ProductSeriesEntity)Serie.Clone();
            item.Order = (OrderEntity)Order.Clone();
            item.Sscc = Sscc;
            item.WeithingDate = WeithingDate;
            item.NetWeight = NetWeight;
            item.TareWeight = TareWeight;
            item.ProductDate = ProductDate;
            item.RegNum = RegNum;
            item.Kneading = Kneading;
            return item;
        }

        public virtual WeithingFactEntity CloneCast() => (WeithingFactEntity)Clone();
        
        #endregion
    }
}
