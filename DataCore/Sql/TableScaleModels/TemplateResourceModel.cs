// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "TemplateResources".
/// </summary>
[Serializable]
public class TemplateResourceModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual string Type { get; set; }
	[XmlElement] public virtual SqlFieldBinaryModel ImageData { get; set; }
	[XmlIgnore] public virtual byte[] ImageDataValue { get => ImageData.Value; set => ImageData.Value = value; }
	[XmlElement] public virtual Guid IdRRef { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public TemplateResourceModel() : base(SqlFieldIdentityEnum.Id)
	{
		Name = string.Empty;
		Type = string.Empty;
		ImageData = new();
		IdRRef = Guid.Empty;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	private TemplateResourceModel(SerializationInfo info, StreamingContext context) : base(info, context)
	{
		Name = info.GetString(nameof(Name));
		Type = info.GetString(nameof(Type));
		ImageData = (SqlFieldBinaryModel)info.GetValue(nameof(ImageData), typeof(SqlFieldBinaryModel));
		IdRRef = (Guid)info.GetValue(nameof(IdRRef), typeof(Guid));
	}

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Type)}: {Type}. " +
        $"{nameof(ImageData)}: {ImageData}. " +
        $"{nameof(IdRRef)}: {IdRRef}. ";

    public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((TemplateResourceModel)obj);
    }

	public override int GetHashCode() => base.GetHashCode();

	public override bool EqualsNew() => Equals(new());

	public override bool EqualsDefault() =>
		base.EqualsDefault() &&
		Equals(Name, string.Empty) &&
		Equals(Type, string.Empty) && 
		ImageData.Equals(new())  &&
		Equals(IdRRef, Guid.Empty);

	public override object Clone()
    {
        TemplateResourceModel item = new();
        item.Name = Name;
        item.Type = Type;
        item.IdRRef = IdRRef;
        item.ImageData = ImageData.CloneCast();
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
		info.AddValue(nameof(Name), Name);
		info.AddValue(nameof(Type), Type);
		info.AddValue(nameof(ImageData), ImageData);
		info.AddValue(nameof(IdRRef), IdRRef);
	}

	public override void FillProperties()
	{
		base.FillProperties();
		Name = LocaleCore.Sql.SqlItemFieldName;
		Description = LocaleCore.Sql.SqlItemFieldDescription;
		ImageData.FillProperties();
	}

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(TemplateResourceModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		if (!ImageData.Equals(item.ImageData))
			return false;
		return
			base.Equals(item) &&
			Equals(Name, item.Name) &&
			Equals(Type, item.Type) &&
			Equals(IdRRef, item.IdRRef);
	}

	public new virtual TemplateResourceModel CloneCast() => (TemplateResourceModel)Clone();

	#endregion
}
