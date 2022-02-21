// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;
using System.Text;

namespace DataCore.DAL.DataModels
{
    public class LabelQuickEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual DateTime CreateDate { get; set; }
        public virtual int ScaleId { get; set; } = default;
        public virtual string ScaleDescription { get; set; } = string.Empty;
        public virtual int PluId { get; set; } = default;
        public virtual DateTime WeithingDate { get; set; } = default;
        public virtual decimal NetWeight { get; set; } = default;
        public virtual decimal TareWeight { get; set; } = default;
        public virtual DateTime ProductDate { get; set; } = default;
        public virtual int? RegNum { get; set; } = default;
        public virtual int? Kneading { get; set; } = default;
        public virtual byte[] Label { get; set; } = new byte[0];
        public virtual string LabelString
        {
            get => Label == null || Label.Length == 0 ? string.Empty : Encoding.Default.GetString(Label);
            set => Label = Encoding.Default.GetBytes(value);
        }
        public virtual string LabelInfo
        {
            get => GetBytesLength(Label);
            set => _ = value;
        }
        public virtual string Zpl { get; set; } = string.Empty;
        public virtual string ZplInfo
        {
            get => GetStringLength(Zpl);
            set => _ = value;
        }

        #endregion

        #region Constructor and destructor

        public LabelQuickEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Id);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                   $"{nameof(Id)}: {Id}. " +
                   $"{nameof(CreateDate)}: {CreateDate}. " +
                   $"{nameof(ScaleId)}: {ScaleId}. " +
                   $"{nameof(ScaleDescription)}: {ScaleDescription}. " +
                   $"{nameof(PluId)}: {PluId}. " +
                   $"{nameof(WeithingDate)}: {WeithingDate}. " +
                   $"{nameof(NetWeight)}: {NetWeight}. " +
                   $"{nameof(TareWeight)}: {TareWeight}. " +
                   $"{nameof(ProductDate)}: {ProductDate}. " +
                   $"{nameof(RegNum)}: {RegNum}. " +
                   $"{nameof(Kneading)}: {Kneading}. " +
                   $"{nameof(ZplInfo)}: {ZplInfo}. ";
        }

        public virtual bool Equals(LabelQuickEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return
                   Equals(Uid, entity.Uid) &&
                   Equals(CreateDate, entity.CreateDate) &&
                   Equals(ScaleId, entity.ScaleId) &&
                   Equals(ScaleDescription, entity.ScaleDescription) &&
                   Equals(PluId, entity.PluId) &&
                   Equals(WeithingDate, entity.WeithingDate) &&
                   Equals(NetWeight, entity.NetWeight) &&
                   Equals(TareWeight, entity.TareWeight) &&
                   Equals(ProductDate, entity.ProductDate) &&
                   Equals(RegNum, entity.RegNum) &&
                   Equals(Kneading, entity.Kneading) &&
                   Equals(Label, entity.Label) &&
                   Equals(Zpl, entity.Zpl);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((LabelQuickEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new LabelQuickEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(Id, default(int)) &&
                   Equals(CreateDate, default(DateTime)) &&
                   Equals(ScaleId, default(int)) &&
                   Equals(ScaleDescription, default(string)) &&
                   Equals(PluId, default(int)) &&
                   Equals(WeithingDate, default(DateTime)) &&
                   Equals(NetWeight, default(decimal)) &&
                   Equals(TareWeight, default(decimal)) &&
                   Equals(ProductDate, default(DateTime)) &&
                   Equals(RegNum, default(int?)) &&
                   Equals(Label, default(byte[])) &&
                   Equals(Zpl, default(string));
        }

        public override object Clone()
        {
            return new LabelQuickEntity
            {
                Id = Id,
                CreateDate = CreateDate,
                ScaleId = ScaleId,
                ScaleDescription = ScaleDescription,
                PluId = PluId,
                WeithingDate = WeithingDate,
                NetWeight = NetWeight,
                TareWeight = TareWeight,
                ProductDate = ProductDate,
                RegNum = RegNum,
                Label = CloneBytes(Label),
                Zpl = Zpl,
            };
        }

        #endregion
    }
}
