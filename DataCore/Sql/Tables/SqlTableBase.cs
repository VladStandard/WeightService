// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Sql.Core.Enums;
using DataCore.Sql.Core.Interfaces;

namespace DataCore.Sql.Tables;

/// <summary>
/// DB table model.
/// </summary>
[Serializable]
public class SqlTableBase : SerializeBase, ICloneable, ISqlTable
{
	#region Public and private fields, properties, constructor

	[XmlIgnore] public virtual SqlFieldIdentityModel Identity { get; set; }
	[XmlElement] public virtual long IdentityValueId { get => Identity.Id; set => Identity.SetId(value); }
	[XmlElement] public virtual Guid IdentityValueUid { get => Identity.Uid; set => Identity.SetUid(value); }
	[XmlIgnore] public virtual bool IsNew => Identity.IsNew();
	[XmlIgnore] public virtual bool IsNotNew => Identity.IsNotNew();
	[XmlIgnore] public virtual bool IsIdentityId => Equals(Identity.Name, SqlFieldIdentity.Id);
	[XmlIgnore] public virtual bool IsIdentityUid => Equals(Identity.Name, SqlFieldIdentity.Uid);
	[XmlElement] public virtual DateTime CreateDt { get; set; } = DateTime.MinValue;
    [XmlElement] public virtual DateTime ChangeDt { get; set; } = DateTime.MinValue;
    [XmlElement] public virtual bool IsMarked { get; set; } = false;
    [XmlElement] public virtual string Name { get; set; } = string.Empty;
    [XmlElement] public virtual string Description { get; set; } = string.Empty;
    [XmlIgnore] public virtual ParseResultModel ParseResult { get; set; } = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    public SqlTableBase()
    {
	    Identity = new(SqlFieldIdentity.Empty);
    }

	/// <summary>
	/// Constructor.
	/// </summary>
	public SqlTableBase(SqlFieldIdentity identityName) : this()
    {
	    Identity = new(identityName);
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public SqlTableBase(SqlFieldIdentityModel identity) : this()
	{
		Identity = (SqlFieldIdentityModel)identity.Clone();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    protected SqlTableBase(SerializationInfo info, StreamingContext context)
    {
		Identity = (SqlFieldIdentityModel)info.GetValue(nameof(Identity), typeof(SqlFieldIdentityModel));
        CreateDt = info.GetDateTime(nameof(CreateDt));
        ChangeDt = info.GetDateTime(nameof(ChangeDt));
        IsMarked = info.GetBoolean(nameof(IsMarked));
		Name = info.GetString(nameof(Name));
        Description = info.GetString(nameof(Description));
        ParseResult = (ParseResultModel)info.GetValue(nameof(ParseResult), typeof(ParseResultModel));
    }

	#endregion

	#region Public and private methods - override

	public override string ToString()
    {
        string strCreateDt = CreateDt != DateTime.MinValue ? $"{nameof(CreateDt)}: {CreateDt:yyyy-MM-dd}. " : string.Empty;
        string strChangeDt = ChangeDt != DateTime.MinValue ? $"{nameof(ChangeDt)}: {ChangeDt:yyyy-MM-dd}. " : string.Empty;
        string strIsMarked = $"{nameof(IsMarked)}: {IsMarked}. ";
        string strName = string.IsNullOrEmpty(Name) ? string.Empty : $"{nameof(Name)}: {Name}. ";
        string strDescription = string.IsNullOrEmpty(Description) ? string.Empty : $"{nameof(Description)}: {Description}. ";
		return strCreateDt + strChangeDt + strIsMarked + strName + strDescription;
    }

    public virtual bool Equals(ISqlTable item) =>
	    ReferenceEquals(this, item) || Identity.Equals(item.Identity) && //-V3130
	    Equals(CreateDt, item.CreateDt) &&
	    Equals(ChangeDt, item.ChangeDt) &&
	    Equals(IsMarked, item.IsMarked) &&
	    Equals(Name, item.Name) &&
	    Equals(Description, item.Description) &&
        Equals(ParseResult, item.ParseResult);

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((SqlTableBase)obj);
    }
    
    public override int GetHashCode() => Identity.GetHashCode();

	/// <summary>
	/// Get object data for serialization info.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
	    base.GetObjectData(info, context);
		info.AddValue(nameof(Identity), Identity);
        info.AddValue(nameof(ChangeDt), ChangeDt);
        info.AddValue(nameof(CreateDt), CreateDt);
        info.AddValue(nameof(IsMarked), IsMarked);
        info.AddValue(nameof(Name), Name);
        info.AddValue(nameof(Description), Description);
        info.AddValue(nameof(ParseResult), ParseResult);
    }

	#endregion

	#region Public and private methods - virtual

	public virtual bool EqualsNew() => Equals(new SqlTableBase());

	public virtual bool EqualsDefault() =>
		Identity.EqualsDefault() &&
		Equals(CreateDt, DateTime.MinValue) &&
		Equals(ChangeDt, DateTime.MinValue) &&
		Equals(IsMarked, false) &&
		Equals(Name, string.Empty) &&
		Equals(Description, string.Empty) &&
        ParseResult.EqualsDefault();

	public virtual object Clone() => new SqlTableBase(Identity)
	{
		CreateDt = CreateDt,
		ChangeDt = ChangeDt,
		IsMarked = IsMarked,
		Name = Name,
		Description = Description,
        ParseResult = ParseResult.CloneCast(),
	};

	public virtual SqlTableBase CloneCast() => (SqlTableBase)Clone();

	public virtual void CloneSetup(SqlTableBase item)
	{
		CreateDt = item.CreateDt;
		ChangeDt = item.ChangeDt;
		IsMarked = item.IsMarked;
		Name = item.Name;
		Description = item.Description;
	}

	public virtual void SetDtNow()
	{
		ChangeDt = CreateDt = DateTime.Now;
	}

	public virtual void ClearNullProperties()
	{
		//throw new NotImplementedException();
	}

	public virtual void FillProperties()
	{
		SetDtNow();
		Name = LocaleCore.Sql.SqlItemFieldName;
		Description = LocaleCore.Sql.SqlItemFieldDescription;
	}

    public virtual void UpdateProperties(ISqlTable item)
    {
		if (!item.CreateDt.Equals(DateTime.MinValue))
			CreateDt = item.CreateDt;
        if (!item.ChangeDt.Equals(DateTime.MinValue))
            ChangeDt = item.ChangeDt;
		IsMarked = item.IsMarked;
        Name = item.Name;
		Description = item.Description;
        ParseResult = item.ParseResult.CloneCast();
    }

    #endregion
}
