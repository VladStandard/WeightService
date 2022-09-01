// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using Microsoft.AspNetCore.Mvc.Internal;

namespace DataCore.Sql.TableDwhModels;

[Serializable]
public class NomenclatureModel : TableModel, ISerializable, ITableModel
{
    #region Public and private fields, properties, constructor

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
	public NomenclatureModel() : base(ColumnName.Id)
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

    public new virtual string ToString() =>
	    $"{nameof(Code)}: {Code}. " +
	    $"{nameof(Name)}: {Name}. ";

    public virtual bool Equals(NomenclatureModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        if (!NomenclatureGroupCost.Equals(item.NomenclatureGroupCost))
            return false;
        if (!NomenclatureGroup.Equals(item.NomenclatureGroup))
            return false;
        if (!Brand.Equals(item.Brand))
            return false;
        if (!NomenclatureType.Equals(item.NomenclatureType))
            return false;
        if (!Status.Equals(item.Status))
            return false;
        if (!InformationSystem.Equals(item.InformationSystem))
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

	public new virtual bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((NomenclatureModel)obj);
    }

    public new virtual int GetHashCode() => base.GetHashCode();

    public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (!Brand.EqualsDefault())
            return false;
        if (!InformationSystem.EqualsDefault())
            return false;
        if (!NomenclatureGroupCost.EqualsDefault())
            return false;
        if (!NomenclatureGroup.EqualsDefault())
            return false;
        if (!NomenclatureType.EqualsDefault())
            return false;
        if (!Status.EqualsDefault())
            return false;
        return base.EqualsDefault() &&
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
               Equals(Weight, default(decimal)) &&
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
        NomenclatureModel item = new();
        item.Code = Code;
        item.Name = Name;
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

    public new virtual NomenclatureModel CloneCast() => (NomenclatureModel)Clone();

    #endregion
}
