// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "Templates".
/// </summary>
[Serializable]
public class TemplateModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual string CategoryId { get; set; }
	[XmlElement] public virtual Guid IdRRef { get; set; }
	[XmlElement] public virtual string Title { get; set; }
    [XmlIgnore] public virtual SqlFieldBinaryModel ImageData { get; set; }
    [XmlIgnore] public virtual byte[] ImageDataValue { get => ImageData.Value ?? Array.Empty<byte>(); set => ImageData.Value = value; }
    [XmlElement] public virtual string ImageDataValueUnicode
		{ get => Encoding.Unicode.GetString(ImageDataValue); set => ImageDataValue = Encoding.Unicode.GetBytes(value); }

	/// <summary>
	/// Constructor.
	/// </summary>
	public TemplateModel() : base(SqlFieldIdentityEnum.Id)
	{
		CategoryId = string.Empty;
		IdRRef = Guid.Empty;
		Title = string.Empty;
		ImageData = new();
		//ImageDataValue = Array.Empty<byte>();
		ImageDataValueUnicode = string.Empty;
	}

	/// <summary>
	/// Constructor for serialization.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
    protected TemplateModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        CategoryId = info.GetString(nameof(CategoryId));
        IdRRef = Guid.Parse(info.GetString(nameof(IdRRef)));
        Title = info.GetString(nameof(Title));
        ImageData = (SqlFieldBinaryModel)info.GetValue(nameof(ImageData), typeof(SqlFieldBinaryModel));
	}

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(CategoryId)}: {CategoryId}. " +
		$"{nameof(Title)}: {Title}. " +
        $"{nameof(ImageData)}: {ImageData}. ";

    public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((TemplateModel)obj);
    }

	public override int GetHashCode() => base.GetHashCode();

	public override bool EqualsNew() => Equals(new());

	public override bool EqualsDefault() =>
		base.EqualsDefault() &&
		Equals(CategoryId, string.Empty) &&
		Equals(IdRRef, Guid.Empty) &&
		Equals(Title, string.Empty) &&
		ImageData.EqualsDefault();

	public override object Clone()
    {
        TemplateModel item = new();
        item.CategoryId = CategoryId;
        item.IdRRef = IdRRef;
        item.Title = Title;
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
        info.AddValue(nameof(CategoryId), CategoryId);
        info.AddValue(nameof(IdRRef), IdRRef);
        info.AddValue(nameof(Title), Title);
        info.AddValue(nameof(ImageData), ImageData);
	}

	public override void FillProperties()
	{
		base.FillProperties();
		Title = LocaleCore.Sql.SqlItemFieldTitle;
		ImageData.FillProperties();
	}

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(TemplateModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		if (!ImageData.Equals(item.ImageData))
			return false;
		return
			base.Equals(item) &&
			Equals(CategoryId, item.CategoryId) &&
			Equals(IdRRef, item.IdRRef) &&
			Equals(Title, item.Title);
	}

	public new virtual TemplateModel CloneCast() => (TemplateModel)Clone();

	#endregion
}
