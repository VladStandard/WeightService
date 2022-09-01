// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MissingXmlDoc

namespace DataCore.Sql.Tables;

/// <summary>
/// Enum column name.
/// </summary>
public enum ColumnName
{
    Default,
    Id,
    Uid,
}

/// <summary>
/// DB table model.
/// </summary>
[Serializable]
public class TableModel : SerializeModel, ICloneable, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual FieldIdentityModel Identity { get; }
    [XmlElement] public virtual DateTime CreateDt { get; set; }
    [XmlElement] public virtual DateTime ChangeDt { get; set; }
    [XmlElement] public virtual bool IsMarked { get; set; }
	[XmlElement] public virtual long IdentityValueId { get => Identity.Id; set => Identity.SetId(value); }
	[XmlElement] public virtual Guid IdentityValueUid { get => Identity.Uid; set => Identity.SetUid(value); }

    /// <summary>
    /// Constructor.
    /// </summary>
    public TableModel()
    {
	    Identity = new(ColumnName.Default);
	    ChangeDt = CreateDt = DateTime.MinValue;
	    IsMarked = false;
    }

	/// <summary>
	/// Constructor.
	/// </summary>
	public TableModel(ColumnName identityName) : this()
    {
	    Identity = new(identityName);
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public TableModel(FieldIdentityModel identity) : this()
	{
		Identity = (FieldIdentityModel)identity.Clone();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    protected TableModel(SerializationInfo info, StreamingContext context)
    {
		Identity = (FieldIdentityModel)info.GetValue(nameof(Identity), typeof(FieldIdentityModel));
        CreateDt = info.GetDateTime(nameof(CreateDt));
        ChangeDt = info.GetDateTime(nameof(ChangeDt));
        IsMarked = info.GetBoolean(nameof(IsMarked));
    }

	#endregion

	#region Public and private methods

	public new virtual string ToString()
    {
        string strCreateDt = CreateDt != DateTime.MinValue ? $"{nameof(CreateDt)}: {CreateDt:yyyy-MM-dd}. " : string.Empty;
        string strChangeDt = ChangeDt != DateTime.MinValue ? $"{nameof(ChangeDt)}: {ChangeDt:yyyy-MM-dd}. " : string.Empty;
        string strIsMarked = IsMarked ? $"{nameof(IsMarked)}: {IsMarked}. " : string.Empty;
        return strCreateDt + strChangeDt + strIsMarked;
    }

    public new virtual int GetHashCode() => Identity.GetHashCode();

    public virtual bool Equals(TableModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        return
            Identity.Equals(item.Identity) &&
            Equals(CreateDt, item.CreateDt) &&
            Equals(ChangeDt, item.ChangeDt) &&
            Equals(IsMarked, item.IsMarked);
    }

    public new virtual bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((TableModel)obj);
    }

	/// <summary>
	/// Get object data for serialization info.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
	    base.GetObjectData(info, context);
		info.AddValue(nameof(Identity), Identity);
        info.AddValue(nameof(ChangeDt), ChangeDt);
        info.AddValue(nameof(CreateDt), CreateDt);
        info.AddValue(nameof(IsMarked), IsMarked);
    }

    public virtual bool EqualsDefault()
    {
        return
	        Identity.EqualsDefault() &&
            Equals(CreateDt, DateTime.MinValue) &&
            Equals(ChangeDt, DateTime.MinValue) &&
            Equals(IsMarked, false);
    }

    public virtual object Clone() => new TableModel(Identity)
    {
        CreateDt = CreateDt,
        ChangeDt = ChangeDt,
        IsMarked = IsMarked,
    };

    public virtual TableModel CloneCast() => (TableModel)Clone();

    public virtual void CloneSetup(TableModel item)
    {
        CreateDt = item.CreateDt;
        ChangeDt = item.ChangeDt;
        IsMarked = item.IsMarked;
    }

    public virtual void SetDt()
    {
	    ChangeDt = CreateDt = DateTime.Now;
    }

    #endregion
}
