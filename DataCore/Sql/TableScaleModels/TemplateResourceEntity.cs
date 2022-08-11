// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "TemplateResources".
/// </summary>
[Serializable]
public class TemplateResourceEntity : BaseEntity, ISerializable, IBaseEntity
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Id;
	[XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual string Description { get; set; }
	[XmlElement] public virtual string Type { get; set; }
	[XmlElement] public virtual ImageDataEntity ImageData { get; set; }
	[XmlElement] public virtual byte[] ImageDataValue { get => ImageData.Value; set => ImageData.Value = value; }
	[XmlElement] public virtual Guid IdRRef { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public TemplateResourceEntity() : this(0, false)
	{
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityId"></param>
	/// <param name="isSetupDates"></param>
	public TemplateResourceEntity(long identityId, bool isSetupDates) : base(identityId, isSetupDates)
    {
	    Init();
    }

	#endregion

	#region Public and private methods

	public new virtual void Init()
	{
		base.Init();
		Name = string.Empty;
		Description = string.Empty;
		Type = string.Empty;
		ImageData = new();
		IdRRef = Guid.Empty;
	}

    public override string ToString() =>
	    $"{nameof(IdentityId)}: {IdentityId}. " +
	    $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Description)}: {Description}. " +
        $"{nameof(Type)}: {Type}. " +
        $"{nameof(ImageData)}: {ImageData}. " +
        $"{nameof(IdRRef)}: {IdRRef}. ";

    public virtual bool Equals(TemplateResourceEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        if (ImageData != null && item.ImageData != null && !ImageData.Equals(item.ImageData))
            return false;
        return base.Equals(item) &&
               Equals(Name, item.Name) &&
               Equals(Description, item.Description) &&
               Equals(Type, item.Type) &&
               Equals(IdRRef, item.IdRRef);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((TemplateResourceEntity)obj);
    }

	public override int GetHashCode() => IdentityId.GetHashCode();

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(Name, string.Empty) &&
               Equals(Description, string.Empty) &&
               Equals(Type, string.Empty) &&
               ImageData != null && ImageData.Equals(new())  &&
               Equals(IdRRef, Guid.Empty);
    }

    public new virtual object Clone()
    {
        TemplateResourceEntity item = new();
        item.Name = Name;
        item.Description = Description;
        item.Type = Type;
        item.IdRRef = IdRRef;
        item.ImageData = ImageData.CloneCast();
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual TemplateResourceEntity CloneCast() => (TemplateResourceEntity)Clone();

    #endregion
}
