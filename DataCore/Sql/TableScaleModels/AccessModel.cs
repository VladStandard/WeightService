// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "ACCESS".
/// </summary>
[Serializable]
public class AccessModel : TableBase, ICloneable, ISqlDbBase, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual string User { get; set; }
	[XmlElement] public virtual byte Rights { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public AccessModel()
	{
		User = string.Empty;
		Rights = 0x00;
	}

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
	/// <param name="context"></param>
	private AccessModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        User = info.GetString(nameof(User));
        Rights = info.GetByte(nameof(Rights));
    }

	#endregion

	#region Public and private methods - override

	/// <summary>
	/// To string.
	/// </summary>
	/// <returns></returns>
	public override string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(User)}: {User}. " +
        $"{nameof(Rights)}: {Rights}. ";

    public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((AccessModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
	    base.EqualsDefault() &&
	    Equals(User, string.Empty) &&
	    Equals(Rights, (byte)0x00);
    
    public override object Clone()
    {
        AccessModel item = new();
        item.User = User;
        item.Rights = Rights;
		item.CloneSetup(base.CloneCast());
		return item;
    }

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
		info.AddValue(nameof(User), User);
		info.AddValue(nameof(Rights), Rights);
	}

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(AccessModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		return base.Equals(item) &&
		       Equals(User, item.User) &&
		       Equals(Rights, item.Rights);
	}

	public new virtual AccessModel CloneCast() => (AccessModel)Clone();

	#endregion
}
