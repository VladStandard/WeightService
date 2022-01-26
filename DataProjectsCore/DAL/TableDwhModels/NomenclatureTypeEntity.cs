// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using System;

namespace DataProjectsCore.DAL.TableDwhModels
{
    public class NomenclatureTypeEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime Dlm { get; set; }
        public virtual string Name { get; set; }
        public virtual bool? GoodsForSale { get; set; }
        public virtual int StatusId { get; set; }
        public virtual InformationSystemEntity InformationSystem { get; set; } = new InformationSystemEntity();
        public virtual byte[] CodeInIs { get; set; }

        #endregion

        #region Constructor and destructor

        public NomenclatureTypeEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Id);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            var strInformationSystem = InformationSystem != null ? InformationSystem.Id.ToString() : "null";
            return base.ToString() +
                   $"{nameof(CreateDate)}: {CreateDate}. " +
                   $"{nameof(Dlm)}: {Dlm}. " +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(GoodsForSale)}: {GoodsForSale}. " +
                   $"{nameof(StatusId)}: {StatusId}. " +
                   $"{nameof(InformationSystem)}: {strInformationSystem}. " +
                   $"{nameof(CodeInIs)}.Length: {CodeInIs?.Length ?? 0}. ";
        }

        public virtual bool Equals(NomenclatureTypeEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDate, entity.CreateDate) &&
                   Equals(Dlm, entity.Dlm) &&
                   Equals(Name, entity.Name) &&
                   Equals(GoodsForSale, entity.GoodsForSale) &&
                   Equals(StatusId, entity.StatusId) &&
                   Equals(InformationSystem, entity.InformationSystem) &&
                   Equals(CodeInIs, entity.CodeInIs);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((NomenclatureTypeEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new NomenclatureTypeEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (InformationSystem != null && !InformationSystem.EqualsDefault())
                return false;
            return base.EqualsDefault() &&
                   Equals(CreateDate, default(DateTime)) &&
                   Equals(Dlm, default(DateTime)) &&
                   Equals(Name, default(string)) &&
                   Equals(GoodsForSale, default(bool?)) &&
                   Equals(StatusId, default(int)) &&
                   Equals(CodeInIs, default(byte[]));
        }

        public override object Clone()
        {
            return new NomenclatureTypeEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                CreateDate = CreateDate,
                Dlm = Dlm,
                Id = Id,
                Name = Name,
                GoodsForSale = GoodsForSale,
                StatusId = StatusId,
                InformationSystem = (InformationSystemEntity)InformationSystem.Clone(),
                CodeInIs = CloneBytes(CodeInIs),
            };
        }

        #endregion
    }
}
