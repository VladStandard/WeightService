// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;
using DataCore.Sql.Tables;

namespace DataCore.Sql.TableDwhModels;

[Serializable]
public class NomenclatureLightModel : TableBaseModel, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    public virtual string Code { get; set; }
    public virtual string Name { get; set; }
    public virtual string Parents { get; set; }
    public virtual NomenclatureParentModel ParentConvert => 
        string.IsNullOrEmpty(Parents) ? null : JsonConvert.DeserializeObject<NomenclatureParentModel>(Parents);
    public virtual string NameFull { get; set; }
    public virtual bool IsService { get; set; }
    public virtual bool IsProduct { get; set; }
    public virtual InformationSystemModel InformationSystem { get; set; } = new();
    public virtual short? RelevanceStatus { get; set; }
    public virtual short? NormalizationStatus { get; set; }
    public virtual long? MasterId { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public NomenclatureLightModel() : base(ColumnName.Id)
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

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(Code)}: {Code}. " +
	    $"{nameof(Name)}: {Name}. " +
	    $"{nameof(Parents)}: {Parents}. " +
	    $"{nameof(NameFull)}: {NameFull}. " +
	    $"{nameof(IsService)}: {IsService}. " +
	    $"{nameof(IsProduct)}: {IsProduct}. " +
	    $"{nameof(InformationSystem)}: {InformationSystem}. " +
	    $"{nameof(RelevanceStatus)}: {RelevanceStatus}. " +
	    $"{nameof(NormalizationStatus)}: {NormalizationStatus}. " +
	    $"{nameof(MasterId)}: {MasterId}. ";

    public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((NomenclatureLightModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault()
	{
        if (!InformationSystem.EqualsDefault())
            return false;
        return 
	        base.EqualsDefault() &&
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
        NomenclatureLightModel item = new();
        item.Code = Code;
        item.Name = Name;
        item.Parents = Parents;
        item.NameFull = NameFull;
        item.IsService = IsService;
        item.IsProduct = IsProduct;
        item.InformationSystem = InformationSystem.CloneCast();
        item.RelevanceStatus = RelevanceStatus;
        item.NormalizationStatus = NormalizationStatus;
        item.MasterId = MasterId;
		item.CloneSetup(base.CloneCast());
		return item;
    }

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(NomenclatureLightModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		if (!InformationSystem.Equals(item.InformationSystem))
			return false;
		return
			base.Equals(item) &&
			Equals(Code, item.Code) &&
			Equals(Name, item.Name) &&
			Equals(Parents, item.Parents) &&
			Equals(NameFull, item.NameFull) &&
			Equals(IsService, item.IsService) &&
			Equals(IsProduct, item.IsProduct) &&
			Equals(RelevanceStatus, item.RelevanceStatus) &&
			Equals(NormalizationStatus, item.NormalizationStatus) &&
			Equals(MasterId, item.MasterId);
	}

	public new virtual NomenclatureLightModel CloneCast() => (NomenclatureLightModel)Clone();

    #endregion
}
