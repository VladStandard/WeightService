// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using Zebra.Sdk.Device;

namespace DataCore.Sql.TableDwhModels;

[Serializable]
public class BrandModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    public virtual string Code { get; set; }
    public virtual int StatusId { get; set; }
    public virtual InformationSystemModel InformationSystem { get; set; }
    public virtual byte[] CodeInIs { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public BrandModel() : base(SqlFieldIdentityEnum.Id)
	{
		Code = string.Empty;
		StatusId = 0;
		InformationSystem = new();
		CodeInIs = new byte[0];
	}

	#endregion

	#region Public and private methods - virtual

	public override string ToString() =>
		$"{nameof(Code)}: {Code}. " +
		$"{nameof(Name)}: {Name}. " +
		$"{nameof(StatusId)}: {StatusId}. ";

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((BrandModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
	    base.EqualsDefault() &&
	    Equals(Code, string.Empty) &&
	    Equals(StatusId, 0) &&
	    Equals(CodeInIs, new byte[0]) &&
	    InformationSystem.EqualsDefault();

    public override object Clone()
    {
        BrandModel item = new();
        item.Code = Code;
        item.StatusId = StatusId;
        item.InformationSystem = InformationSystem.CloneCast();
        item.CodeInIs = DataUtils.ByteClone(CodeInIs);
		item.CloneSetup(base.CloneCast());
		return item;
    }

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(BrandModel item) =>
		ReferenceEquals(this, item) || base.Equals(item) && //-V3130
		Equals(Code, item.Code) &&
		Equals(StatusId, item.StatusId) &&
		Equals(CodeInIs, item.CodeInIs) &&
		InformationSystem.Equals(item.InformationSystem);

	public new virtual BrandModel CloneCast() => (BrandModel)Clone();

    #endregion
}
