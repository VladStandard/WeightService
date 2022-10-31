// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableDwhModels;

[Serializable]
public class NomenclatureGroupModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    public virtual int StatusId { get; set; }
    public virtual InformationSystemModel InformationSystem { get; set; }
    public virtual byte[] CodeInIs { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public NomenclatureGroupModel() : base(SqlFieldIdentityEnum.Id)
    {
	    StatusId = 0;
	    InformationSystem = new();
	    CodeInIs = new byte[0];
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(Name)}: {Name}. " +
	    $"{nameof(StatusId)}: {StatusId}. ";

    public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((NomenclatureGroupModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

	public override bool EqualsDefault() =>
		base.EqualsDefault() &&
		Equals(StatusId, 0) &&
		Equals(CodeInIs, new byte[0]) &&
		InformationSystem.EqualsDefault();

	public override object Clone()
    {
        NomenclatureGroupModel item = new();
        item.StatusId = StatusId;
        item.InformationSystem = InformationSystem.CloneCast();
        item.CodeInIs = DataUtils.ByteClone(CodeInIs);
		item.CloneSetup(base.CloneCast());
		return item;
    }

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(NomenclatureGroupModel item) =>
		ReferenceEquals(this, item) || base.Equals(item) &&
		Equals(StatusId, item.StatusId) &&
		Equals(CodeInIs, item.CodeInIs) &&
		InformationSystem.Equals(item.InformationSystem);

	public new virtual NomenclatureGroupModel CloneCast() => (NomenclatureGroupModel)Clone();

    #endregion
}
