// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.Fields;

/// <summary>
/// DB field Identity model.
/// </summary>
[Serializable]
public class FieldIdentityModel : SerializeModel, ICloneable, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual ColumnName Name { get; private set; }
	[XmlElement] public virtual long Id { get; private set; }
	[XmlElement] public virtual Guid Uid { get; private set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public FieldIdentityModel(ColumnName identityName)
	{
		Name = identityName;
		Id = 0;
		Uid = Guid.Empty;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	protected FieldIdentityModel(SerializationInfo info, StreamingContext context)
	{
		Name = (ColumnName)info.GetValue(nameof(Name), typeof(ColumnName));
		Id = info.GetInt64(nameof(Id));
		Uid = Guid.Parse(info.GetString(nameof(Uid)));
	}

	private FieldIdentityModel(ColumnName identityName, long identityId, Guid identityUid) : this(identityName)
	{
		Id = identityId;
		Uid = identityUid;
	}

	#endregion

	#region Public and private methods

	public override string ToString()
	{
		string strIdentityValue = string.Empty;
		switch (Name)
		{
			case ColumnName.Id:
				strIdentityValue = $"{nameof(Id)}: {Id}. ";
				break;
			case ColumnName.Uid:
				strIdentityValue = $"{nameof(Uid)}: {Uid}. ";
				break;
		}
		return $"{nameof(Name)}: {Name}. " + strIdentityValue;
	}

	public virtual bool Equals(FieldIdentityModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		return
			Equals(Name, item.Name) &&
			Id.Equals(item.Id) &&
			Uid.Equals(item.Uid);
	}

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
		return Equals((FieldIdentityModel)obj);
	}

	public override int GetHashCode() => Name switch
	{
		ColumnName.Id => Id.GetHashCode(),
		ColumnName.Uid => Uid.GetHashCode(),
		_ => default,
	};

	public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue(nameof(Name), Name);
		info.AddValue(nameof(Id), Id);
		info.AddValue(nameof(Uid), Uid);
	}

	public virtual bool EqualsDefault()
	{
		return
			Equals(Id, (long)0) &&
			Equals(Uid, Guid.Empty);
	}

	public virtual object Clone() => new FieldIdentityModel(Name, Id, Uid);

	public virtual FieldIdentityModel CloneCast() => (FieldIdentityModel)Clone();

	public void SetId(long value)
	{
		Id = value;
	}

	public void SetUid(Guid value)
	{
		Uid = value;
	}

	#endregion
}
