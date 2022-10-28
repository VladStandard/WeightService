// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Core;

/// <summary>
/// SQL table name.
/// </summary>
[Serializable]
public class SqlTableIdentityModel : SerializeBase, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Field name.
    /// </summary>
    [XmlElement] public SqlFieldIdentityEnum Name { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public SqlTableIdentityModel()
    {
        Name = SqlFieldIdentityEnum.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name"></param>
    /// <exception cref="ArgumentException"></exception>
    public SqlTableIdentityModel(SqlFieldIdentityEnum name)
    {
        Name = name;
        //if (typeof(T) == typeof(string))
        //{
        //    if (Value == null)
        //        Value = (T)Convert.ChangeType(string.Empty, typeof(T));
        //    if (DefaultValue == null)
        //        DefaultValue = (T)Convert.ChangeType(string.Empty, typeof(T));
        //}
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private SqlTableIdentityModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Name = (SqlFieldIdentityEnum)info.GetValue(nameof(Name), typeof(SqlFieldIdentityEnum));
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() => $"{nameof(Name)}: {Name}. ";

    public override bool Equals(object obj)
    {
	    if (ReferenceEquals(null, obj)) return false;
	    if (ReferenceEquals(this, obj)) return true;
	    if (obj.GetType() != GetType()) return false;
	    return Equals((SqlTableIdentityModel)obj);
    }

    public override int GetHashCode() => Name.GetHashCode();

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
	    base.GetObjectData(info, context);
	    info.AddValue(nameof(Name), Name);
    }

	#endregion

	#region Public and private methods - virtual

	public virtual bool EqualsNew() => Equals(new());

	public virtual bool Equals(SqlTableIdentityModel item) => 
		ReferenceEquals(this, item) || Equals(Name, item.Name);

	public virtual bool EqualsDefault() => Equals(Name, SqlFieldIdentityEnum.Empty);

	public virtual object Clone() => new SqlTableIdentityModel(Name);

	public virtual SqlTableIdentityModel CloneCast() => (SqlTableIdentityModel)Clone();

	public virtual void ClearNullProperties()
	{
		throw new NotImplementedException();
	}

	public virtual void FillProperties()
	{
		throw new NotImplementedException();
	}

	#endregion
}
