// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MissingXmlDoc

namespace DataCore.Sql.Tables;

/// <summary>
/// DB table model.
/// </summary>
[Serializable]
public class TableBase : SerializeBase, ICloneable, ISqlDbBase, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual SqlFieldIdentityModel Identity { get; }
	[XmlElement] public virtual long IdentityValueId { get => Identity.Id; set => Identity.SetId(value); }
	[XmlElement] public virtual Guid IdentityValueUid { get => Identity.Uid; set => Identity.SetUid(value); }
    [XmlElement] public virtual DateTime CreateDt { get; set; }
    [XmlElement] public virtual DateTime ChangeDt { get; set; }
    [XmlElement] public virtual bool IsMarked { get; set; }
    [XmlElement] public virtual string Description { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public TableBase()
    {
	    Identity = new(SqlFieldIdentityEnum.Empty);
	    ChangeDt = CreateDt = DateTime.MinValue;
	    IsMarked = false;
	    Description = string.Empty;
    }

	/// <summary>
	/// Constructor.
	/// </summary>
	public TableBase(SqlFieldIdentityEnum identityName) : this()
    {
	    Identity = new(identityName);
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public TableBase(SqlFieldIdentityModel identity) : this()
	{
		Identity = (SqlFieldIdentityModel)identity.Clone();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    protected TableBase(SerializationInfo info, StreamingContext context)
    {
		Identity = (SqlFieldIdentityModel)info.GetValue(nameof(Identity), typeof(SqlFieldIdentityModel));
        CreateDt = info.GetDateTime(nameof(CreateDt));
        ChangeDt = info.GetDateTime(nameof(ChangeDt));
        IsMarked = info.GetBoolean(nameof(IsMarked));
        Description = info.GetString(nameof(Description));
    }

	#endregion

	#region Public and private methods - override

	public override string ToString()
    {
        string strCreateDt = CreateDt != DateTime.MinValue ? $"{nameof(CreateDt)}: {CreateDt:yyyy-MM-dd}. " : string.Empty;
        string strChangeDt = ChangeDt != DateTime.MinValue ? $"{nameof(ChangeDt)}: {ChangeDt:yyyy-MM-dd}. " : string.Empty;
        string strIsMarked = $"{nameof(IsMarked)}: {IsMarked}. ";
        string strDescription = string.IsNullOrEmpty(Description) ? string.Empty : $"{nameof(Description)}: {Description}. ";
		return strCreateDt + strChangeDt + strIsMarked + strDescription;
    }

    public virtual bool Equals(TableBase item)
    {
        if (ReferenceEquals(this, item)) return true;
        return
            Identity.Equals(item.Identity) &&
            Equals(CreateDt, item.CreateDt) &&
            Equals(ChangeDt, item.ChangeDt) &&
            Equals(IsMarked, item.IsMarked) && 
            Equals(Description, item.Description);
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((TableBase)obj);
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
        info.AddValue(nameof(Description), Description);
    }

	#endregion

	#region Public and private methods - virtual

	public virtual bool EqualsNew() => Equals(new());

	public virtual bool EqualsDefault() =>
		Identity.EqualsDefault() &&
		Equals(CreateDt, DateTime.MinValue) &&
		Equals(ChangeDt, DateTime.MinValue) &&
		Equals(IsMarked, false) &&
		Equals(Description, string.Empty);

	public virtual object Clone() => new TableBase(Identity)
	{
		CreateDt = CreateDt,
		ChangeDt = ChangeDt,
		IsMarked = IsMarked,
		Description = Description,
	};

	public virtual TableBase CloneCast() => (TableBase)Clone();

	public virtual void CloneSetup(TableBase item)
	{
		CreateDt = item.CreateDt;
		ChangeDt = item.ChangeDt;
		IsMarked = item.IsMarked;
		Description = item.Description;
	}

	public virtual void SetDtNow()
	{
		ChangeDt = CreateDt = DateTime.Now;
	}

	#endregion
}
