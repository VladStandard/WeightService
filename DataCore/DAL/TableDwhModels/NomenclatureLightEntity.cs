// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Newtonsoft.Json;

namespace DataCore.DAL.TableDwhModels
{
    public class NomenclatureLightEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string Parents { get; set; }
        public virtual NomenclatureParentEntity ParentConvert => 
            string.IsNullOrEmpty(Parents) ? null : JsonConvert.DeserializeObject<NomenclatureParentEntity>(Parents);
        public virtual string NameFull { get; set; }
        public virtual bool IsService { get; set; }
        public virtual bool IsProduct { get; set; }
        public virtual InformationSystemEntity InformationSystem { get; set; } = new InformationSystemEntity();
        public virtual short? RelevanceStatus { get; set; }
        public virtual short? NormalizationStatus { get; set; }
        public virtual long? MasterId { get; set; }

        #endregion

        #region Constructor and destructor

        public NomenclatureLightEntity() : this(0)
        {
            //
        }

        public NomenclatureLightEntity(long id) : base(id)
        {
            Code = string.Empty;
            Name = string.Empty;
            Parents = string.Empty;
            NameFull = string.Empty;
            IsService = false;
            IsProduct = false;
            InformationSystem = new();
            RelevanceStatus = null;
            NormalizationStatus = null;
            MasterId = null;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            var strInformationSystem = InformationSystem != null ? InformationSystem.IdentityId.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Code)}: {Code}. " +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(Parents)}: {Parents}. " +
                   $"{nameof(NameFull)}: {NameFull}. " +
                   $"{nameof(IsService)}: {IsService}. " +
                   $"{nameof(IsProduct)}: {IsProduct}. " +
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
                   Equals(Name, entity.Name) &&
                   Equals(Parents, entity.Parents) &&
                   Equals(NameFull, entity.NameFull) &&
                   Equals(IsService, entity.IsService) &&
                   Equals(IsProduct, entity.IsProduct) &&
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
                   Equals(Code, string.Empty) &&
                   Equals(Name, string.Empty) &&
                   Equals(Parents, string.Empty) &&
                   Equals(NameFull, string.Empty) &&
                   Equals(IsService, false) &&
                   Equals(IsProduct, false) &&
                   Equals(RelevanceStatus, null) &&
                   Equals(NormalizationStatus, null) &&
                   Equals(MasterId, null);
        }

        public override object Clone()
        {
            NomenclatureLightEntity item = (NomenclatureLightEntity)base.Clone();
            item.Code = Code;
            item.Name = Name;
            item.Parents = Parents;
            item.NameFull = NameFull;
            item.IsService = IsService;
            item.IsProduct = IsProduct;
            item.InformationSystem = (InformationSystemEntity)InformationSystem.Clone();
            item.RelevanceStatus = RelevanceStatus;
            item.NormalizationStatus = NormalizationStatus;
            item.MasterId = MasterId;
            return item;
        }

        #endregion
    }
}
