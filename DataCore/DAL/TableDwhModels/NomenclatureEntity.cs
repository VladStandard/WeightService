// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using Newtonsoft.Json;
using System;

namespace DataCore.DAL.TableDwhModels
{
    public class NomenclatureEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Code { get; set; }
        public virtual bool? Marked { get; set; }
        public virtual string Name { get; set; }
        public virtual string Parents { get; set; }

        public virtual NomenclatureParentEntity ParentConvert => string.IsNullOrEmpty(Parents)
            ? null
            : JsonConvert.DeserializeObject<NomenclatureParentEntity>(Parents);
        public virtual string Article { get; set; }
        public virtual bool? Weighted { get; set; }
        public virtual string GuidMercury { get; set; }
        public virtual bool? KeepTrackOfCharacteristics { get; set; }
        public virtual string NameFull { get; set; }
        public virtual string Comment { get; set; }
        public virtual bool? IsService { get; set; }
        public virtual bool? IsProduct { get; set; }
        public virtual string AdditionalDescriptionOfNomenclature { get; set; }
        public virtual byte[] NomenclatureGroupCostBytes { get; set; }
        public virtual NomenclatureGroupEntity NomenclatureGroupCost { get; set; } = new NomenclatureGroupEntity();
        public virtual byte[] NomenclatureGroupBytes { get; set; }
        public virtual NomenclatureGroupEntity NomenclatureGroup { get; set; } = new NomenclatureGroupEntity();
        public virtual byte[] ArticleCost { get; set; }
        public virtual byte[] BrandBytes { get; set; }
        public virtual BrandEntity Brand { get; set; } = new BrandEntity();
        public virtual byte[] NomenclatureTypeBytes { get; set; }
        public virtual NomenclatureTypeEntity NomenclatureType { get; set; } = new NomenclatureTypeEntity();
        public virtual string VatRate { get; set; }
        public virtual string Unit { get; set; }
        public virtual decimal Weight { get; set; }
        public virtual byte[] BoxTypeId { get; set; }
        public virtual string BoxTypeName { get; set; }
        public virtual byte[] PackTypeId { get; set; }
        public virtual string PackTypeName { get; set; }
        public virtual string SerializedRepresentationObject { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime Dlm { get; set; }
        public virtual StatusEntity Status { get; set; } = new StatusEntity();
        public virtual InformationSystemEntity InformationSystem { get; set; } = new InformationSystemEntity();
        public virtual byte[] CodeInIs { get; set; }
        public virtual short? RelevanceStatus { get; set; }
        public virtual short? NormalizationStatus { get; set; }
        public virtual int? MasterId { get; set; }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            var strBrand = Brand != null ? Brand.Id.ToString() : "null";
            var strInformationSystem = InformationSystem != null ? InformationSystem.Id.ToString() : "null";
            var strNomenclatureGroup = NomenclatureGroup != null ? NomenclatureGroup.Id.ToString() : "null";
            var strNomenclatureGroupCost = NomenclatureGroupCost != null ? NomenclatureGroupCost.Id.ToString() : "null";
            var strNomenclatureType = NomenclatureType != null ? NomenclatureType.Id.ToString() : "null";
            var strStatus = Status != null ? Status.Id.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Code)}: {Code}. " +
                   $"{nameof(Marked)}: {Marked}. " +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(Parents)}: {Parents}. " +
                   $"{nameof(Article)}: {Article}. " +
                   $"{nameof(Weighted)}: {Weighted}. " +
                   $"{nameof(GuidMercury)}: {GuidMercury}. " +
                   $"{nameof(KeepTrackOfCharacteristics)}: {KeepTrackOfCharacteristics}. " +
                   $"{nameof(NameFull)}: {NameFull}. " +
                   $"{nameof(Comment)}: {Comment}. " +
                   $"{nameof(IsService)}: {IsService}. " +
                   $"{nameof(IsProduct)}: {IsProduct}. " +
                   $"{nameof(AdditionalDescriptionOfNomenclature)}: {AdditionalDescriptionOfNomenclature}. " +
                   $"{nameof(NomenclatureGroupCostBytes)}.Length: {NomenclatureGroupCostBytes?.Length ?? 0}. " +
                   $"{nameof(NomenclatureGroupCost)}.Length: {strNomenclatureGroupCost}. " +
                   $"{nameof(NomenclatureGroupBytes)}.Length: {NomenclatureGroupBytes?.Length ?? 0}. " +
                   $"{nameof(NomenclatureGroup)}.Length: {strNomenclatureGroup}. " +
                   $"{nameof(ArticleCost)}.Length: {ArticleCost?.Length ?? 0}. " +
                   $"{nameof(BrandBytes)}.Length: {BrandBytes?.Length ?? 0}. " +
                   $"{nameof(Brand)}: {strBrand}. " +
                   $"{nameof(NomenclatureTypeBytes)}.Length: {NomenclatureTypeBytes?.Length ?? 0}. " +
                   $"{nameof(NomenclatureType)}.Length: {strNomenclatureType}. " +
                   $"{nameof(VatRate)}: {VatRate}. " +
                   $"{nameof(Unit)}: {Unit}. " +
                   $"{nameof(Weight)}: {Weight}. " +
                   $"{nameof(BoxTypeId)}.Length: {BoxTypeId?.Length ?? 0}. " +
                   $"{nameof(BoxTypeName)}: {BoxTypeName}. " +
                   $"{nameof(PackTypeId)}.Length: {PackTypeId?.Length ?? 0}. " +
                   $"{nameof(PackTypeName)}: {PackTypeName}. " +
                   $"{nameof(SerializedRepresentationObject)}.Length: {SerializedRepresentationObject?.Length ?? 0}. " +
                   $"{nameof(CreateDate)}: {CreateDate}. " +
                   $"{nameof(Dlm)}: {Dlm}. " +
                   $"{nameof(Status)}: {strStatus}. " +
                   $"{nameof(InformationSystem)}: {strInformationSystem}. " +
                   $"{nameof(CodeInIs)}.Length: {CodeInIs?.Length ?? 0}. " +
                   $"{nameof(RelevanceStatus)}: {RelevanceStatus}. " +
                   $"{nameof(NormalizationStatus)}: {NormalizationStatus}. " +
                   $"{nameof(MasterId)}: {MasterId}. ";
        }

        public virtual bool Equals(NomenclatureEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(Code, entity.Code) &&
                   Equals(Marked, entity.Marked) &&
                   Equals(Name, entity.Name) &&
                   Equals(Parents, entity.Parents) &&
                   Equals(Article, entity.Article) &&
                   Equals(Weighted, entity.Weighted) &&
                   Equals(GuidMercury, entity.GuidMercury) &&
                   Equals(KeepTrackOfCharacteristics, entity.KeepTrackOfCharacteristics) &&
                   Equals(NameFull, entity.NameFull) &&
                   Equals(Comment, entity.Comment) &&
                   Equals(IsService, entity.IsService) &&
                   Equals(IsProduct, entity.IsProduct) &&
                   Equals(AdditionalDescriptionOfNomenclature, entity.AdditionalDescriptionOfNomenclature) &&
                   Equals(NomenclatureGroupCostBytes, entity.NomenclatureGroupCostBytes) &&
                   Equals(NomenclatureGroupCost, entity.NomenclatureGroupCost) &&
                   Equals(NomenclatureGroupBytes, entity.NomenclatureGroupBytes) &&
                   Equals(NomenclatureGroup, entity.NomenclatureGroup) &&
                   Equals(ArticleCost, entity.ArticleCost) &&
                   Equals(BrandBytes, entity.BrandBytes) &&
                   Equals(Brand, entity.Brand) &&
                   Equals(NomenclatureTypeBytes, entity.NomenclatureTypeBytes) &&
                   Equals(NomenclatureType, entity.NomenclatureType) &&
                   Equals(VatRate, entity.VatRate) &&
                   Equals(Unit, entity.Unit) &&
                   Equals(Weight, entity.Weight) &&
                   Equals(BoxTypeId, entity.BoxTypeId) &&
                   Equals(BoxTypeName, entity.BoxTypeName) &&
                   Equals(PackTypeId, entity.PackTypeId) &&
                   Equals(PackTypeName, entity.PackTypeName) &&
                   Equals(SerializedRepresentationObject, entity.SerializedRepresentationObject) &&
                   Equals(CreateDate, entity.CreateDate) &&
                   Equals(Dlm, entity.Dlm) &&
                   Equals(Status, entity.Status) &&
                   Equals(InformationSystem, entity.InformationSystem) &&
                   Equals(CodeInIs, entity.CodeInIs) &&
                   Equals(RelevanceStatus, entity.RelevanceStatus) &&
                   Equals(NormalizationStatus, entity.NormalizationStatus) &&
                   Equals(MasterId, entity.MasterId);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((NomenclatureEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new NomenclatureEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (Brand != null && !Brand.EqualsDefault())
                return false;
            if (InformationSystem != null && !InformationSystem.EqualsDefault())
                return false;
            if (NomenclatureGroupCost != null && !NomenclatureGroupCost.EqualsDefault())
                return false;
            if (NomenclatureGroup != null && !NomenclatureGroup.EqualsDefault())
                return false;
            if (NomenclatureType != null && !NomenclatureType.EqualsDefault())
                return false;
            if (Status != null && !Status.EqualsDefault())
                return false;
            return base.EqualsDefault() &&
                   Equals(Code, default(string)) &&
                   Equals(Marked, default(bool?)) &&
                   Equals(Name, default(string)) &&
                   Equals(Parents, default(string)) &&
                   Equals(Article, default(string)) &&
                   Equals(Weighted, default(bool?)) &&
                   Equals(GuidMercury, default(string)) &&
                   Equals(KeepTrackOfCharacteristics, default(bool?)) &&
                   Equals(NameFull, default(string)) &&
                   Equals(Comment, default(string)) &&
                   Equals(IsService, default(bool?)) &&
                   Equals(IsProduct, default(bool?)) &&
                   Equals(AdditionalDescriptionOfNomenclature, default(string)) &&
                   Equals(NomenclatureGroupCostBytes, default(byte[])) &&
                   Equals(NomenclatureGroupBytes, default(byte[])) &&
                   Equals(ArticleCost, default(byte[])) &&
                   Equals(BrandBytes, default(byte[])) &&
                   Equals(NomenclatureTypeBytes, default(byte[])) &&
                   Equals(VatRate, default(string)) &&
                   Equals(Unit, default(string)) &&
                   Equals(Weight, default(decimal)) &&
                   Equals(BoxTypeId, default(byte[])) &&
                   Equals(BoxTypeName, default(string)) &&
                   Equals(PackTypeId, default(byte[])) &&
                   Equals(PackTypeName, default(string)) &&
                   Equals(SerializedRepresentationObject, default(string)) &&
                   Equals(CreateDate, default(DateTime)) &&
                   Equals(Dlm, default(DateTime)) &&
                   Equals(CodeInIs, default(byte[])) &&
                   Equals(RelevanceStatus, default(short?)) &&
                   Equals(NormalizationStatus, default(short?)) &&
                   Equals(MasterId, default(long?));
        }

        public override object Clone()
        {
            return new NomenclatureEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Id = Id,
                Code = Code,
                Marked = Marked,
                Name = Name,
                Parents = Parents,
                Article = Article,
                Weighted = Weighted,
                GuidMercury = GuidMercury,
                KeepTrackOfCharacteristics = KeepTrackOfCharacteristics,
                NameFull = NameFull,
                Comment = Comment,
                IsService = IsService,
                IsProduct = IsProduct,
                AdditionalDescriptionOfNomenclature = AdditionalDescriptionOfNomenclature,
                NomenclatureGroupCostBytes = CloneBytes(NomenclatureGroupCostBytes),
                NomenclatureGroupCost = (NomenclatureGroupEntity)NomenclatureGroupCost.Clone(),
                NomenclatureGroupBytes = CloneBytes(NomenclatureGroupBytes),
                NomenclatureGroup = (NomenclatureGroupEntity)NomenclatureGroup.Clone(),
                ArticleCost = CloneBytes(ArticleCost),
                BrandBytes = CloneBytes(BrandBytes),
                Brand = (BrandEntity)Brand.Clone(),
                NomenclatureTypeBytes = CloneBytes(NomenclatureTypeBytes),
                NomenclatureType = (NomenclatureTypeEntity)NomenclatureType.Clone(),
                VatRate = VatRate,
                Unit = Unit,
                Weight = Weight,
                BoxTypeId = CloneBytes(BoxTypeId),
                BoxTypeName = BoxTypeName,
                PackTypeId = CloneBytes(PackTypeId),
                PackTypeName = PackTypeName,
                SerializedRepresentationObject = SerializedRepresentationObject,
                CreateDate = CreateDate,
                Dlm = Dlm,
                Status = (StatusEntity)Status.Clone(),
                InformationSystem = (InformationSystemEntity)InformationSystem.Clone(),
                CodeInIs = CloneBytes(CodeInIs),
                RelevanceStatus = RelevanceStatus,
                NormalizationStatus = NormalizationStatus,
                MasterId = MasterId,
            };
        }

        #endregion
    }
}
