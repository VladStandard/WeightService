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

	[XmlElement] public virtual ColumnName IdentityName { get; }
	[XmlElement] public virtual long IdentityId { get; set; }
    [XmlElement] public virtual Guid IdentityUid { get; set; }
    [XmlElement] public virtual DateTime CreateDt { get; set; }
    [XmlElement] public virtual DateTime ChangeDt { get; set; }
    [XmlElement] public virtual bool IsMarked { get; set; }
    //[XmlIgnore] public virtual string IdentityUidStr => IdentityUid.ToString();

    /// <summary>
    /// Constructor.
    /// </summary>
    public TableModel() : this(ColumnName.Default) { }

    /// <summary>
    /// Constructor.
    /// </summary>
    public TableModel(ColumnName identityName)
    {
        IdentityName = identityName;
		Init();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="identityName"></param>
    /// <param name="isSetupDates"></param>
    public TableModel(ColumnName identityName, bool isSetupDates) : this(identityName)
    {
	    if (isSetupDates)
        {
            ChangeDt = CreateDt = DateTime.Now;
        }
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="identityName"></param>
    /// <param name="identityId"></param>
    /// <param name="isSetupDates"></param>
    public TableModel(ColumnName identityName, long identityId, bool isSetupDates) : this(identityName, isSetupDates)
    {
		IdentityId = identityId;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="identityName"></param>
    /// <param name="identityUid"></param>
    /// <param name="isSetupDates"></param>
    public TableModel(ColumnName identityName, Guid identityUid, bool isSetupDates) : this(identityName, isSetupDates)
    {
	    IdentityUid = identityUid;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    protected TableModel(SerializationInfo info, StreamingContext context)
    {
		IdentityName = (ColumnName)info.GetValue(nameof(IdentityName), typeof(ColumnName));
        IdentityId = info.GetInt64(nameof(IdentityId));
        IdentityUid = Guid.Parse(info.GetString(nameof(IdentityUid)));
        CreateDt = info.GetDateTime(nameof(CreateDt));
        ChangeDt = info.GetDateTime(nameof(ChangeDt));
        IsMarked = info.GetBoolean(nameof(IsMarked));
    }

    #endregion

    #region Public and private methods

    protected virtual void Init()
    {
	    IdentityId = 0;
        IdentityUid = Guid.Empty;
        ChangeDt = CreateDt = DateTime.MinValue;
        IsMarked = false;
    }

    public override string ToString()
    {
        string strCreateDt = CreateDt != DateTime.MinValue ? $"{nameof(CreateDt)}: {CreateDt:yyyy-MM-dd}. " : string.Empty;
        string strChangeDt = ChangeDt != DateTime.MinValue ? $"{nameof(ChangeDt)}: {ChangeDt:yyyy-MM-dd}. " : string.Empty;
        string strIsMarked = IsMarked ? $"{nameof(IsMarked)}: {IsMarked}. " : string.Empty;
        return strCreateDt + strChangeDt + strIsMarked;
    }

    public override int GetHashCode() => IdentityName switch
    {
        ColumnName.Id => IdentityId.GetHashCode(),
        ColumnName.Uid => IdentityUid.GetHashCode(),
        _ => default,
    };

    public virtual bool Equals(TableModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        return
            Equals(IdentityName, item.IdentityName) &&
            IdentityId.Equals(item.IdentityId) &&
            IdentityUid.Equals(item.IdentityUid) &&
            Equals(CreateDt, item.CreateDt) &&
            Equals(ChangeDt, item.ChangeDt) &&
            Equals(IsMarked, item.IsMarked);
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((TableModel)obj);
    }

    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(IdentityName), IdentityName);
        info.AddValue(nameof(IdentityId), IdentityId);
        info.AddValue(nameof(IdentityUid), IdentityUid);
        info.AddValue(nameof(ChangeDt), ChangeDt);
        info.AddValue(nameof(CreateDt), CreateDt);
        info.AddValue(nameof(IsMarked), IsMarked);
    }

    public virtual bool EqualsDefault()
    {
        return
            Equals(IdentityId, (long)0) &&
            Equals(IdentityUid, Guid.Empty) &&
            Equals(CreateDt, DateTime.MinValue) &&
            Equals(ChangeDt, DateTime.MinValue) &&
            Equals(IsMarked, false);
    }

    public virtual object Clone() => new TableModel(IdentityName)
    {
        IdentityId = IdentityId,
        IdentityUid = IdentityUid,
        CreateDt = CreateDt,
        ChangeDt = ChangeDt,
        IsMarked = IsMarked,
    };

    public virtual TableModel CloneCast() => (TableModel)Clone();

    public virtual void Setup(TableModel baseItem)
    {
        IdentityId = baseItem.IdentityId;
        IdentityUid = baseItem.IdentityUid;
        CreateDt = baseItem.CreateDt;
        ChangeDt = baseItem.ChangeDt;
        IsMarked = baseItem.IsMarked;
    }

    #endregion
}
