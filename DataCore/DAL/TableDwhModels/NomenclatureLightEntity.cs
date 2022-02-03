// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Newtonsoft.Json;
using System;

namespace DataCore.DAL.TableDwhModels
{
    public class NomenclatureLightEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Code { get; set; }
        public virtual bool? Marked { get; set; }
        public virtual string Name { get; set; }
        public virtual string Parents { get; set; }

        public virtual NomenclatureParentEntity ParentConvert => string.IsNullOrEmpty(Parents)
            ? null
            : JsonConvert.DeserializeObject<NomenclatureParentEntity>(Parents);
        public virtual string NameFull { get; set; }
        public virtual bool? IsService { get; set; }
        public virtual bool? IsProduct { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime Dlm { get; set; }
        public virtual InformationSystemEntity InformationSystem { get; set; } = new InformationSystemEntity();
        public virtual short? RelevanceStatus { get; set; }
        public virtual short? NormalizationStatus { get; set; }
        public virtual int? MasterId { get; set; }

        #endregion

        #region Constructor and destructor

        public NomenclatureLightEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Id);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            var strInformationSystem = InformationSystem != null ? InformationSystem.Id.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Code)}: {Code}. " +
                   $"{nameof(Marked)}: {Marked}. " +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(Parents)}: {Parents}. " +
                   $"{nameof(NameFull)}: {NameFull}. " +
                   $"{nameof(IsService)}: {IsService}. " +
                   $"{nameof(IsProduct)}: {IsProduct}. " +
                   $"{nameof(CreateDate)}: {CreateDate}. " +
                   $"{nameof(Dlm)}: {Dlm}. " +
                   $"{nameof(InformationSystem)}: {strInformationSystem}. " +
                   $"{nameof(RelevanceStatus)}: {RelevanceStatus}. " +
                   $"{nameof(NormalizationStatus)}: {NormalizationStatus}. " +
                   $"{nameof(MasterId)}: {MasterId}. ";
        }

        public virtual bool Equals(NomenclatureLightEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(Code, entity.Code) &&
                   Equals(Marked, entity.Marked) &&
                   Equals(Name, entity.Name) &&
                   Equals(Parents, entity.Parents) &&
                   Equals(NameFull, entity.NameFull) &&
                   Equals(IsService, entity.IsService) &&
                   Equals(IsProduct, entity.IsProduct) &&
                   Equals(CreateDate, entity.CreateDate) &&
                   Equals(Dlm, entity.Dlm) &&
                   Equals(InformationSystem, entity.InformationSystem) &&
                   Equals(RelevanceStatus, entity.RelevanceStatus) &&
                   Equals(NormalizationStatus, entity.NormalizationStatus) &&
                   Equals(MasterId, entity.MasterId);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((NomenclatureLightEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new NomenclatureLightEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (InformationSystem != null && !InformationSystem.EqualsDefault())
                return false;
            return base.EqualsDefault() &&
                   Equals(Code, default(string)) &&
                   Equals(Marked, default(bool?)) &&
                   Equals(Name, default(string)) &&
                   Equals(Parents, default(string)) &&
                   Equals(NameFull, default(string)) &&
                   Equals(IsService, default(bool?)) &&
                   Equals(IsProduct, default(bool?)) &&
                   Equals(CreateDate, default(DateTime)) &&
                   Equals(Dlm, default(DateTime)) &&
                   Equals(RelevanceStatus, default(short?)) &&
                   Equals(NormalizationStatus, default(short?)) &&
                   Equals(MasterId, default(int?));
        }

        public override object Clone()
        {
            return new NomenclatureLightEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Id = Id,
                Code = Code,
                Marked = Marked,
                Name = Name,
                Parents = Parents,
                NameFull = NameFull,
                IsService = IsService,
                IsProduct = IsProduct,
                CreateDate = CreateDate,
                Dlm = Dlm,
                InformationSystem = (InformationSystemEntity)InformationSystem.Clone(),
                RelevanceStatus = RelevanceStatus,
                NormalizationStatus = NormalizationStatus,
                MasterId = MasterId,
            };
        }

        #endregion
    }
}
