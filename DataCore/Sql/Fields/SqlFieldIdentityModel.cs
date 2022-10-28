// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Fields;

/// <summary>
/// DB field Identity model.
/// </summary>
[Serializable]
public class SqlFieldIdentityModel : SqlFieldBase, ICloneable, ISqlDbBase, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual SqlFieldIdentityEnum Name { get; private set; }
	[XmlElement] public virtual long Id { get; private set; }
	[XmlElement] public virtual Guid Uid { get; private set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public SqlFieldIdentityModel()
	{
		FieldName = nameof(SqlFieldIdentityModel);
		Name = SqlFieldIdentityEnum.Empty;
		Id = 0;
		Uid = Guid.Empty;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityName"></param>
	public SqlFieldIdentityModel(SqlFieldIdentityEnum identityName) : this()
	{
		Name = identityName;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	private SqlFieldIdentityModel(SqlFieldIdentityEnum identityName, long identityId, Guid identityUid) : this(identityName)
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
		Name = (SqlFieldIdentityEnum)info.GetValue(nameof(Name), typeof(SqlFieldIdentityEnum));
		Id = info.GetInt64(nameof(Id));
		Uid = Guid.Parse(info.GetString(nameof(Uid)));
	}

	#endregion

	#region Public and private methods - override

	public override string ToString()
	{
		string strIdentityValue = string.Empty;
		switch (Name)
		{
			case SqlFieldIdentityEnum.Id:
				strIdentityValue = $"{nameof(Id)}: {Id}. ";
				break;
			case SqlFieldIdentityEnum.Uid:
				strIdentityValue = $"{nameof(Uid)}: {Uid}. ";
				break;
		}
		return strIdentityValue;
	}

	public virtual string GetValueAsString()
	{
		return Name switch
		{
			SqlFieldIdentityEnum.Id => Id.ToString(),
			SqlFieldIdentityEnum.Uid => Uid.ToString(),
			_ => string.Empty
		};
	}

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
		return Equals((SqlFieldIdentityModel)obj);
	}

	public override int GetHashCode() => Name switch
	{
		SqlFieldIdentityEnum.Id => Id.GetHashCode(),
		SqlFieldIdentityEnum.Uid => Uid.GetHashCode(),
		_ => default,
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
		ReferenceEquals(this, item) || Equals(Name, item.Name) &&
		Id.Equals(item.Id) &&
		Uid.Equals(item.Uid);

	public new virtual SqlFieldIdentityModel CloneCast() => (SqlFieldIdentityModel)Clone();

	public virtual void SetId(long value) => Id = value;

	public virtual void SetUid(Guid value) => Uid = value;

	public virtual bool IsNew() => Name switch
	{
		SqlFieldIdentityEnum.Id => Equals(Id, default(long)),
		SqlFieldIdentityEnum.Uid => Equals(Uid, Guid.Empty),
		_ => default,
	};

	public virtual bool IsNotNew() => Name switch
	{
		SqlFieldIdentityEnum.Id => !Equals(Id, default(long)),
		SqlFieldIdentityEnum.Uid => !Equals(Uid, Guid.Empty),
		_ => default,
	};

	#endregion
}
