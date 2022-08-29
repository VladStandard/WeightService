// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "VERSIONS".
/// </summary>
[Serializable]
public class VersionEntity : TableModel, ISerializable, ITableModel
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Identity name.
    /// </summary>
    public static ColumnName IdentityName => ColumnName.Uid;
    public virtual DateTime ReleaseDt { get; set; }
    public virtual short Version { get; set; }
    public virtual string Description { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public VersionEntity() : base(Guid.Empty, false)
	{
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityUid"></param>
	/// <param name="isSetupDates"></param>
	public VersionEntity(Guid identityUid, bool isSetupDates) : base(identityUid, isSetupDates)
	{
		Init();
	}

	/// <summary>
	/// Constructor for serialization.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
    protected VersionEntity(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        ReleaseDt = info.GetDateTime(nameof(ReleaseDt));
        Version = info.GetInt16(nameof(Version));
        Description = info.GetString(nameof(Description));
    }

	#endregion

	#region Public and private methods

	public new virtual void Init()
	{
		base.Init();
		ReleaseDt = DateTime.MinValue;
		Version = 0;
		Description = string.Empty;
	}

    public override string ToString() =>
	    $"{nameof(IdentityUid)}: {IdentityUid}. " +
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(ReleaseDt)}: {ReleaseDt}. " +
        $"{nameof(Version)}: {Version}. " +
        $"{nameof(Description)}: {Description}. ";

    public virtual bool Equals(VersionEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(ReleaseDt, item.ReleaseDt) &&
               Equals(Version, item.Version) &&
               Equals(Description, item.Description);
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((VersionEntity)obj);
    }

	public override int GetHashCode() => IdentityUid.GetHashCode();

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(ReleaseDt, DateTime.MinValue) &&
               Equals(Version, (short)0) &&
               Equals(Description, string.Empty);
    }

    public new virtual object Clone()
    {
        VersionEntity item = new();
        item.ReleaseDt = ReleaseDt;
        item.Version = Version;
        item.Description = Description;
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual VersionEntity CloneCast() => (VersionEntity)Clone();

    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(ReleaseDt), ReleaseDt);
        info.AddValue(nameof(Version), Version);
        info.AddValue(nameof(Description), Description);
    }

    #endregion
}
