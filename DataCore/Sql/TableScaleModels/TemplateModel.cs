// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "Templates".
/// </summary>
[Serializable]
public class TemplateModel : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual string CategoryId { get; set; }
	[XmlElement] public virtual Guid IdRRef { get; set; }
	[XmlElement] public virtual string Title { get; set; }
    [XmlIgnore] public virtual FieldBinaryModel ImageData { get; set; }
    [XmlElement] public virtual byte[] ImageDataValue { get => ImageData.Value; set => ImageData.Value = value; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public TemplateModel() : base(ColumnName.Id)
	{
		CategoryId = string.Empty;
		IdRRef = Guid.Empty;
		Title = string.Empty;
		ImageData = new();
		ImageDataValue = Array.Empty<byte>();
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
        ImageData = (FieldBinaryModel)info.GetValue(nameof(ImageData), typeof(FieldBinaryModel));
	}

	#endregion

	#region Public and private methods

	public override string ToString() =>
	    $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(CategoryId)}: {CategoryId}. " +
        $"{nameof(IdRRef)}: {IdRRef}. " +
        $"{nameof(Title)}: {Title}. " +
        $"{nameof(ImageData)}: {ImageData}. ";

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

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((TemplateModel)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return 
	        base.EqualsDefault() &&
            Equals(CategoryId, string.Empty) &&
            Equals(IdRRef, Guid.Empty) &&
            Equals(Title, string.Empty) &&
            ImageData.EqualsDefault();
    }

    public new virtual object Clone()
    {
        TemplateModel item = new();
        item.CategoryId = CategoryId;
        item.IdRRef = IdRRef;
        item.Title = Title;
        item.ImageData = ImageData.CloneCast();
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual TemplateModel CloneCast() => (TemplateModel)Clone();

    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(CategoryId), CategoryId);
        info.AddValue(nameof(IdRRef), IdRRef);
        info.AddValue(nameof(Title), Title);
        info.AddValue(nameof(ImageData), ImageData);
	}

    #endregion
}
