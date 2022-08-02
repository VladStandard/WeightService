// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "TemplateResources".
/// </summary>
public class TemplateResourceEntity : BaseEntity
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Identity name.
    /// </summary>
    public static ColumnName IdentityName => ColumnName.Id;
    public virtual string Name { get; set; }
    public virtual string Description { get; set; }
    public virtual string Type { get; set; }
    public virtual ImageDataEntity ImageData { get; set; }
    public virtual byte[] ImageDataValue { get => ImageData.Value; set => ImageData.Value = value; }
    public virtual Guid IdRRef { get; set; }

    #endregion

    #region Constructor and destructor

    public TemplateResourceEntity() : this(0)
    {
        //
    }

    public TemplateResourceEntity(long id) : base(id)
    {
        Name = string.Empty;
        Description = string.Empty;
        Type = string.Empty;
        ImageData = new();
        IdRRef = Guid.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
        base.ToString() +
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

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

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
