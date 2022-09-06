// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MissingXmlDoc

using DataCore.Sql.Core;

namespace DataCore.Sql.Fields;

/// <summary>
/// DB table model.
/// </summary>
[Serializable]
public class FieldBaseModel : SerializeModel, ICloneable, IDbBaseModel, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual string FieldName { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public FieldBaseModel()
    {
	    FieldName = nameof(FieldBaseModel);
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    protected FieldBaseModel(SerializationInfo info, StreamingContext context)// : base(info, context)
    {
	    FieldName = info.GetString(nameof(FieldName));
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() => $"{nameof(FieldName)}: {FieldName}. ";

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((FieldBaseModel)obj);
    }
    
    public override int GetHashCode() => FieldName.GetHashCode();

	/// <summary>
	/// Get object data for serialization info.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
	    //base.GetObjectData(info, context);
		info.AddValue(nameof(FieldName), FieldName);
    }

	#endregion

	#region Public and private methods - virtual

	public virtual bool EqualsNew() => Equals(new());

	public virtual bool Equals(FieldBaseModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		return
			Equals(FieldName, item.FieldName);
	}

	public virtual bool EqualsDefault() => Equals(FieldName, string.Empty);

	public virtual object Clone() => new FieldBaseModel()
	{
		FieldName = FieldName,
	};

	public virtual FieldBaseModel CloneCast() => (FieldBaseModel)Clone();

	public virtual void CloneSetup(FieldBaseModel item)
	{
		FieldName = item.FieldName;
	}

	#endregion
}
