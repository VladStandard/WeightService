// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;

namespace DataCore.Sql.TableScaleModels
{
    /// <summary>
    /// Table "ProductSeries".
    /// </summary>
    public class ProductSeriesEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual ScaleEntity Scale { get; set; }
        public virtual bool IsClose { get; set; }
        public virtual string Sscc { get; set; }

        #endregion

        #region Constructor and destructor

        public ProductSeriesEntity() : this(0)
        {
            //
        }

        public ProductSeriesEntity(long id) : base(id)
        {
            Scale = new();
            IsClose = false;
            Sscc = string.Empty;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string strScale = Scale != null ? Scale.IdentityId.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Scale)}: {strScale}. " +
                   $"{nameof(IsClose)}: {IsClose}. " +
                   $"{nameof(Sscc)}: {Sscc}.";
        }

        public virtual bool Equals(ProductSeriesEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            if (Scale != null && item.Scale != null && !Scale.Equals(item.Scale))
                return false;
            return base.Equals(item) &&
                   Equals(CreateDt, item.CreateDt) &&
                   Equals(IsClose, item.IsClose) &&
                   Equals(Sscc, item.Sscc);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ProductSeriesEntity)obj);
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
            if (Scale != null && !Scale.EqualsDefault())
                return false;
            return base.EqualsDefault(IdentityName) &&
                   Equals(IsClose, false) &&
                   Equals(Sscc, string.Empty);
        }

        public new virtual object Clone()
        {
            ProductSeriesEntity item = new();
            item.Scale = Scale.CloneCast();
            item.IsClose = IsClose;
            item.Sscc = Sscc;
            item.Setup(((BaseEntity)this).CloneCast());
            return item;
        }

        public new virtual ProductSeriesEntity CloneCast() => (ProductSeriesEntity)Clone();

        #endregion
    }
}
