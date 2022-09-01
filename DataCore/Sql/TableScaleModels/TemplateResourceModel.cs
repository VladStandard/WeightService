// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "TemplateResources".
/// </summary>
[Serializable]
public class TemplateResourceModel : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual string Description { get; set; }
	[XmlElement] public virtual string Type { get; set; }
	[XmlElement] public virtual FieldBinaryModel ImageData { get; set; }
	[XmlElement] public virtual byte[] ImageDataValue { get => ImageData.Value; set => ImageData.Value = value; }
	[XmlElement] public virtual Guid IdRRef { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public TemplateResourceModel() : base(ColumnName.Id)
	{
		Name = string.Empty;
		Description = string.Empty;
		Type = string.Empty;
		ImageData = new();
		IdRRef = Guid.Empty;
	}

	#endregion

	#region Public and private methods

	public new virtual string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Description)}: {Description}. " +
        $"{nameof(Type)}: {Type}. " +
        $"{nameof(ImageData)}: {ImageData}. " +
        $"{nameof(IdRRef)}: {IdRRef}. ";

    public virtual bool Equals(TemplateResourceModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        if (!ImageData.Equals(item.ImageData))
            return false;
        return 
	        base.Equals(item) &&
            Equals(Name, item.Name) &&
            Equals(Description, item.Description) &&
            Equals(Type, item.Type) &&
            Equals(IdRRef, item.IdRRef);
    }

	public new virtual bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((TemplateResourceModel)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return 
	        base.EqualsDefault() &&
	        Equals(Name, string.Empty) &&
	        Equals(Description, string.Empty) &&
	        Equals(Type, string.Empty) && 
	        ImageData.Equals(new())  &&
            Equals(IdRRef, Guid.Empty);
    }

    public new virtual int GetHashCode() => base.GetHashCode();

	public new virtual object Clone()
    {
        TemplateResourceModel item = new();
        item.Name = Name;
        item.Description = Description;
        item.Type = Type;
        item.IdRRef = IdRRef;
        item.ImageData = ImageData.CloneCast();
		item.CloneSetup(base.CloneCast());
		return item;
    }

    public new virtual TemplateResourceModel CloneCast() => (TemplateResourceModel)Clone();

    #endregion
}
