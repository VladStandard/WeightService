// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using DataCore.Utils;

namespace DataCore.Sql.TableDwhModels
{
    public class NomenclatureEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string Parents { get; set; }
        public virtual string? Article { get; set; }
        public virtual bool Weighted { get; set; }
        public virtual string? GuidMercury { get; set; }
        public virtual bool KeepTrackOfCharacteristics { get; set; }
        public virtual string? NameFull { get; set; }
        public virtual string? Comment { get; set; }
        public virtual bool IsService { get; set; }
        public virtual bool IsProduct { get; set; }
        public virtual string? AdditionalDescriptionOfNomenclature { get; set; }
        public virtual byte[] NomenclatureGroupCostBytes { get; set; }
        public virtual NomenclatureGroupEntity NomenclatureGroupCost { get; set; }
        public virtual byte[] NomenclatureGroupBytes { get; set; }
        public virtual NomenclatureGroupEntity NomenclatureGroup { get; set; }
        public virtual byte[] ArticleCost { get; set; }
        public virtual byte[] BrandBytes { get; set; }
        public virtual BrandEntity Brand { get; set; }
        public virtual byte[] NomenclatureTypeBytes { get; set; }
        public virtual NomenclatureTypeEntity NomenclatureType { get; set; }
        public virtual string? VatRate { get; set; }
        public virtual string? Unit { get; set; }
        public virtual decimal Weight { get; set; }
        public virtual byte[] BoxTypeId { get; set; }
        public virtual string? BoxTypeName { get; set; }
        public virtual byte[] PackTypeId { get; set; }
        public virtual string? PackTypeName { get; set; }
        public virtual string? SerializedRepresentationObject { get; set; }
        public virtual StatusEntity Status { get; set; }
        public virtual InformationSystemEntity InformationSystem { get; set; }
        public virtual byte[] CodeInIs { get; set; }
        public virtual short? RelevanceStatus { get; set; }
        public virtual short? NormalizationStatus { get; set; }
        public virtual int? MasterId { get; set; }

        #endregion

        #region Constructor and destructor

        public NomenclatureEntity() : this(0)
        {
            //
        }

        public NomenclatureEntity(long id) : base(id)
        {
            Code = string.Empty;
            Name = string.Empty;
            Parents = string.Empty;
            Article = string.Empty;
            Weighted = false;
            GuidMercury = string.Empty;
            KeepTrackOfCharacteristics = false;
            NameFull = string.Empty;
            Comment = string.Empty;
            IsService = false;
            IsProduct = false;
            AdditionalDescriptionOfNomenclature = string.Empty;
            NomenclatureGroupCostBytes = new byte[0];
            NomenclatureGroupCost = new();
            NomenclatureGroupBytes = new byte[0];
            NomenclatureGroup = new();
            ArticleCost = new byte[0];
            BrandBytes = new byte[0];
            Brand = new();
            NomenclatureTypeBytes = new byte[0];
            NomenclatureType = new();
            VatRate = string.Empty;
            Unit = string.Empty;
            Weight = 0;
            BoxTypeId = new byte[0];
            BoxTypeName = string.Empty;
            PackTypeId = new byte[0];
            PackTypeName = string.Empty;
            SerializedRepresentationObject = string.Empty;
            Status = new();
            InformationSystem = new();
            CodeInIs = new byte[0];
            RelevanceStatus = null;
            NormalizationStatus = null;
            MasterId = null;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string strBrand = Brand != null ? Brand.IdentityId.ToString() : "null";
            string strInformationSystem = InformationSystem != null ? InformationSystem.IdentityId.ToString() : "null";
            string strNomenclatureGroup = NomenclatureGroup != null ? NomenclatureGroup.IdentityId.ToString() : "null";
            string strNomenclatureGroupCost = NomenclatureGroupCost != null ? NomenclatureGroupCost.IdentityId.ToString() : "null";
            string strNomenclatureType = NomenclatureType != null ? NomenclatureType.IdentityId.ToString() : "null";
            string strStatus = Status != null ? Status.IdentityId.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Code)}: {Code}. " +
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
                   $"{nameof(Status)}: {strStatus}. " +
                   $"{nameof(InformationSystem)}: {strInformationSystem}. " +
                   $"{nameof(CodeInIs)}.Length: {CodeInIs?.Length ?? 0}. " +
                   $"{nameof(RelevanceStatus)}: {RelevanceStatus}. " +
                   $"{nameof(NormalizationStatus)}: {NormalizationStatus}. " +
                   $"{nameof(MasterId)}: {MasterId}. ";
        }

        public virtual bool Equals(NomenclatureEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            if (NomenclatureGroupCost != null && item.NomenclatureGroupCost != null && !NomenclatureGroupCost.Equals(item.NomenclatureGroupCost))
                return false;
            if (NomenclatureGroup != null && item.NomenclatureGroup != null && !NomenclatureGroup.Equals(item.NomenclatureGroup))
                return false;
            if (Brand != null && item.Brand != null && !Brand.Equals(item.Brand))
                return false;
            if (NomenclatureType != null && item.NomenclatureType != null && !NomenclatureType.Equals(item.NomenclatureType))
                return false;
            if (Status != null && item.Status != null && !Status.Equals(item.Status))
                return false;
            if (InformationSystem != null && item.InformationSystem != null && !InformationSystem.Equals(item.InformationSystem))
                return false;
            return base.Equals(item) &&
                   Equals(Code, item.Code) &&
                   Equals(Name, item.Name) &&
                   Equals(Parents, item.Parents) &&
                   Equals(Article, item.Article) &&
                   Equals(Weighted, item.Weighted) &&
                   Equals(GuidMercury, item.GuidMercury) &&
                   Equals(KeepTrackOfCharacteristics, item.KeepTrackOfCharacteristics) &&
                   Equals(NameFull, item.NameFull) &&
                   Equals(Comment, item.Comment) &&
                   Equals(IsService, item.IsService) &&
                   Equals(IsProduct, item.IsProduct) &&
                   Equals(AdditionalDescriptionOfNomenclature, item.AdditionalDescriptionOfNomenclature) &&
                   Equals(NomenclatureGroupCostBytes, item.NomenclatureGroupCostBytes) &&
                   Equals(NomenclatureGroupBytes, item.NomenclatureGroupBytes) &&
                   Equals(ArticleCost, item.ArticleCost) &&
                   Equals(BrandBytes, item.BrandBytes) &&
                   Equals(NomenclatureTypeBytes, item.NomenclatureTypeBytes) &&
                   Equals(VatRate, item.VatRate) &&
                   Equals(Unit, item.Unit) &&
                   Equals(Weight, item.Weight) &&
                   Equals(BoxTypeId, item.BoxTypeId) &&
                   Equals(BoxTypeName, item.BoxTypeName) &&
                   Equals(PackTypeId, item.PackTypeId) &&
                   Equals(PackTypeName, item.PackTypeName) &&
                   Equals(SerializedRepresentationObject, item.SerializedRepresentationObject) &&
                   Equals(CodeInIs, item.CodeInIs) &&
                   Equals(RelevanceStatus, item.RelevanceStatus) &&
                   Equals(NormalizationStatus, item.NormalizationStatus) &&
                   Equals(MasterId, item.MasterId);
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
            return base.EqualsDefault(IdentityName) &&
                   Equals(Code, string.Empty) &&
                   Equals(Name, string.Empty) &&
                   Equals(Parents, string.Empty) &&
                   Equals(Article, string.Empty) &&
                   Equals(Weighted, false) &&
                   Equals(GuidMercury, string.Empty) &&
                   Equals(KeepTrackOfCharacteristics, false) &&
                   Equals(NameFull, string.Empty) &&
                   Equals(Comment, string.Empty) &&
                   Equals(IsService, false) &&
                   Equals(IsProduct, false) &&
                   Equals(AdditionalDescriptionOfNomenclature, string.Empty) &&
                   Equals(NomenclatureGroupCostBytes, new byte[0]) &&
                   Equals(NomenclatureGroupBytes, new byte[0]) &&
                   Equals(ArticleCost, new byte[0]) &&
                   Equals(BrandBytes, new byte[0]) &&
                   Equals(NomenclatureTypeBytes, new byte[0]) &&
                   Equals(VatRate, string.Empty) &&
                   Equals(Unit, string.Empty) &&
                   Equals(Weight, 0) &&
                   Equals(BoxTypeId, new byte[0]) &&
                   Equals(BoxTypeName, string.Empty) &&
                   Equals(PackTypeId, new byte[0]) &&
                   Equals(PackTypeName, string.Empty) &&
                   Equals(SerializedRepresentationObject, string.Empty) &&
                   Equals(CodeInIs, new byte[0]) &&
                   Equals(RelevanceStatus, null) &&
                   Equals(NormalizationStatus, null) &&
                   Equals(MasterId, null);
        }

        public new virtual object Clone()
        {
            NomenclatureEntity item = new()
            {
                Code = Code,
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
                NomenclatureGroupCostBytes = DataUtils.ByteClone(NomenclatureGroupCostBytes),
                NomenclatureGroupCost = NomenclatureGroupCost.CloneCast,
                NomenclatureGroupBytes = DataUtils.ByteClone(NomenclatureGroupBytes),
                NomenclatureGroup = NomenclatureGroup.CloneCast,
                ArticleCost = DataUtils.ByteClone(ArticleCost),
                BrandBytes = DataUtils.ByteClone(BrandBytes),
                Brand = Brand.CloneCast,
                NomenclatureTypeBytes = DataUtils.ByteClone(NomenclatureTypeBytes),
                NomenclatureType = NomenclatureType.CloneCast,
                VatRate = VatRate,
                Unit = Unit,
                Weight = Weight,
                BoxTypeId = DataUtils.ByteClone(BoxTypeId),
                BoxTypeName = BoxTypeName,
                PackTypeId = DataUtils.ByteClone(PackTypeId),
                PackTypeName = PackTypeName,
                SerializedRepresentationObject = SerializedRepresentationObject,
                Status = Status.CloneCast,
                InformationSystem = InformationSystem.CloneCast,
                CodeInIs = DataUtils.ByteClone(CodeInIs),
                RelevanceStatus = RelevanceStatus,
                NormalizationStatus = NormalizationStatus,
                MasterId = MasterId,
            };
            item.Setup(((BaseEntity)this).CloneCast);
            return item;
        }

        public new virtual NomenclatureEntity CloneCast => (NomenclatureEntity)Clone();

        #endregion
    }
}
