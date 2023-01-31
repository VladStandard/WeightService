// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;

namespace DataCore.Sql.Fields;

/// <summary>
/// DB field Identity model.
/// </summary>
[Serializable]
public class SqlFieldIdentityModel : SqlFieldBase
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual SqlFieldIdentity Name { get; private set; }
	[XmlElement] public virtual long Id { get; private set; }
	[XmlElement] public virtual Guid Uid { get; private set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public SqlFieldIdentityModel()
	{
		FieldName = nameof(SqlFieldIdentityModel);
		Name = SqlFieldIdentity.Empty;
		Id = 0;
		Uid = Guid.Empty;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityName"></param>
	public SqlFieldIdentityModel(SqlFieldIdentity identityName) : this()
	{
		Name = identityName;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	private SqlFieldIdentityModel(SqlFieldIdentity identityName, long identityId, Guid identityUid) : this(identityName)
	{
		Id = identityId;
		Uid = identityUid;
	}
	
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	protected SqlFieldIdentityModel(SerializationInfo info, StreamingContext context) : base(info, context)
	{
		Name = (SqlFieldIdentity)info.GetValue(nameof(Name), typeof(SqlFieldIdentity));
		Id = info.GetInt64(nameof(Id));
		Uid = Guid.Parse(info.GetString(nameof(Uid).ToUpper()));
	}

	#endregion

	#region Public and private methods - override

	public override string ToString()
	{
		string strIdentityValue = string.Empty;
		switch (Name)
		{
			case SqlFieldIdentity.Id:
				strIdentityValue = $"{nameof(Id)}: {Id}. ";
				break;
			case SqlFieldIdentity.Uid:
				strIdentityValue = $"{nameof(Uid)}: {Uid}. ";
				break;
		}
		return strIdentityValue;
	}

    public virtual string GetValueAsString() => Name switch
    {
        SqlFieldIdentity.Id => Id.ToString(),
        SqlFieldIdentity.Uid => Uid.ToString(),
        _ => string.Empty
    };

    public virtual object? GetValueAsObjectNullable() => Name switch
    {
        SqlFieldIdentity.Id => Id,
        SqlFieldIdentity.Uid => Uid,
        _ => null
    };

    public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
		return Equals((SqlFieldIdentityModel)obj);
	}

	public override int GetHashCode() => Name switch
	{
		SqlFieldIdentity.Id => Id.GetHashCode(),
		SqlFieldIdentity.Uid => Uid.GetHashCode(),
		_ => default
    };

	public override bool EqualsNew() => Equals(new());

	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
		info.AddValue(nameof(Name), Name);
		info.AddValue(nameof(Id), Id);
		info.AddValue(nameof(Uid), Uid);
	}

	public override bool EqualsDefault() =>
		Equals(Id, (long)0) &&
		Equals(Uid, Guid.Empty);

	public override object Clone() => new SqlFieldIdentityModel(Name, Id, Uid);

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(SqlFieldIdentityModel item) =>
		ReferenceEquals(this, item) || Equals(Name, item.Name) && //-V3130
		Id.Equals(item.Id) &&
		Uid.Equals(item.Uid);

	public new virtual SqlFieldIdentityModel CloneCast() => (SqlFieldIdentityModel)Clone();

	public virtual void SetId(long value) => Id = value;

	public virtual void SetUid(Guid value) => Uid = value;

	public virtual bool IsNotExists => Name switch
	{
		SqlFieldIdentity.Id => Equals(Id, default(long)),
		SqlFieldIdentity.Uid => Equals(Uid, Guid.Empty),
		_ => default
    };

	public virtual bool IsExists => Name switch
	{
		SqlFieldIdentity.Id => !Equals(Id, default(long)),
		SqlFieldIdentity.Uid => !Equals(Uid, Guid.Empty),
		_ => default
    };

	[Obsolete(@"Use IsNotExists")]
	public virtual bool IsNew => IsNotExists;

    [Obsolete(@"Use IsExists")]
    public virtual bool IsNotNew => IsExists;

	#endregion
}