// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableDwhModels;

[Serializable]
public class NomenclatureModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    public virtual string Code { get; set; }
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
    public virtual NomenclatureGroupModel NomenclatureGroupCost { get; set; }
    public virtual byte[] NomenclatureGroupBytes { get; set; }
    public virtual NomenclatureGroupModel NomenclatureGroup { get; set; }
    public virtual byte[] ArticleCost { get; set; }
    public virtual byte[] BrandBytes { get; set; }
    public virtual BrandModel Brand { get; set; }
    public virtual byte[] NomenclatureTypeBytes { get; set; }
    public virtual NomenclatureTypeModel NomenclatureType { get; set; }
    public virtual string? VatRate { get; set; }
    public virtual string? Unit { get; set; }
    public virtual decimal Weight { get; set; }
    public virtual byte[] BoxTypeId { get; set; }
    public virtual string? BoxTypeName { get; set; }
    public virtual byte[] PackTypeId { get; set; }
    public virtual string? PackTypeName { get; set; }
    public virtual string? SerializedRepresentationObject { get; set; }
    public virtual StatusModel Status { get; set; }
    public virtual InformationSystemModel InformationSystem { get; set; }
    public virtual byte[] CodeInIs { get; set; }
    public virtual short? RelevanceStatus { get; set; }
    public virtual short? NormalizationStatus { get; set; }
    public virtual int? MasterId { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public NomenclatureModel() : base(SqlFieldIdentityEnum.Id)
    {
        Code = string.Empty;
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

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(Code)}: {Code}. " +
        $"{nameof(Name)}: {Name}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((NomenclatureModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
	    base.EqualsDefault() &&
	    Equals(Code, string.Empty) &&
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
	    Equals(Weight, default(decimal)) &&
	    Equals(BoxTypeId, new byte[0]) &&
	    Equals(BoxTypeName, string.Empty) &&
	    Equals(PackTypeId, new byte[0]) &&
	    Equals(PackTypeName, string.Empty) &&
	    Equals(SerializedRepresentationObject, string.Empty) &&
	    Equals(CodeInIs, new byte[0]) &&
	    Equals(RelevanceStatus, null) &&
	    Equals(NormalizationStatus, null) &&
	    Equals(MasterId, null) &&
	    Brand.EqualsDefault() &&
	    InformationSystem.EqualsDefault() &&
	    NomenclatureGroupCost.EqualsDefault() &&
	    NomenclatureGroup.EqualsDefault() &&
	    NomenclatureType.EqualsDefault() &&
	    Status.EqualsDefault();

    public override object Clone()
    {
        NomenclatureModel item = new();
        item.Code = Code;
        item.Parents = Parents;
        item.Article = Article;
        item.Weighted = Weighted;
        item.GuidMercury = GuidMercury;
        item.KeepTrackOfCharacteristics = KeepTrackOfCharacteristics;
        item.NameFull = NameFull;
        item.Comment = Comment;
        item.IsService = IsService;
        item.IsProduct = IsProduct;
        item.AdditionalDescriptionOfNomenclature = AdditionalDescriptionOfNomenclature;
        item.NomenclatureGroupCostBytes = DataUtils.ByteClone(NomenclatureGroupCostBytes);
        item.NomenclatureGroupCost = NomenclatureGroupCost.CloneCast();
        item.NomenclatureGroupBytes = DataUtils.ByteClone(NomenclatureGroupBytes);
        item.NomenclatureGroup = NomenclatureGroup.CloneCast();
        item.ArticleCost = DataUtils.ByteClone(ArticleCost);
        item.BrandBytes = DataUtils.ByteClone(BrandBytes);
        item.Brand = Brand.CloneCast();
        item.NomenclatureTypeBytes = DataUtils.ByteClone(NomenclatureTypeBytes);
        item.NomenclatureType = NomenclatureType.CloneCast();
        item.VatRate = VatRate;
        item.Unit = Unit;
        item.Weight = Weight;
        item.BoxTypeId = DataUtils.ByteClone(BoxTypeId);
        item.BoxTypeName = BoxTypeName;
        item.PackTypeId = DataUtils.ByteClone(PackTypeId);
        item.PackTypeName = PackTypeName;
        item.SerializedRepresentationObject = SerializedRepresentationObject;
        item.Status = Status.CloneCast();
        item.InformationSystem = InformationSystem.CloneCast();
        item.CodeInIs = DataUtils.ByteClone(CodeInIs);
        item.RelevanceStatus = RelevanceStatus;
        item.NormalizationStatus = NormalizationStatus;
        item.MasterId = MasterId;
        item.CloneSetup(base.CloneCast());
        return item;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(NomenclatureModel item) =>
	    ReferenceEquals(this, item) || base.Equals(item) && //-V3130
	    Equals(Code, item.Code) &&
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
	    Equals(MasterId, item.MasterId) &&
	    InformationSystem.Equals(item.InformationSystem) &&
	    NomenclatureGroupCost.Equals(item.NomenclatureGroupCost) &&
	    NomenclatureGroup.Equals(item.NomenclatureGroup) &&
	    Brand.Equals(item.Brand) &&
	    NomenclatureType.Equals(item.NomenclatureType) &&
	    Status.Equals(item.Status);

    public new virtual NomenclatureModel CloneCast() => (NomenclatureModel)Clone();

    #endregion
}
