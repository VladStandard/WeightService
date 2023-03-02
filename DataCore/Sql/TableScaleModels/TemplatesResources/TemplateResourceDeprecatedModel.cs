// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;

namespace DataCore.Sql.TableScaleModels.TemplatesResources;

/// <summary>
/// Table "TemplateResources".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(TemplateResourceModel)}")]
public class TemplateResourceDeprecatedModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual string Type { get; set; }
    [XmlElement] public virtual SqlFieldBinaryModel ImageData { get; set; }
    [XmlIgnore] public virtual byte[] ImageDataValue { get => ImageData.Value; set => ImageData.Value = value; }
    [XmlElement] public virtual Guid IdRRef { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public TemplateResourceDeprecatedModel() : base(SqlFieldIdentity.Id)
    {
        Type = string.Empty;
        ImageData = new();
        IdRRef = Guid.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected TemplateResourceDeprecatedModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
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
        return Equals((TemplateResourceDeprecatedModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Type, string.Empty) &&
        ImageData.Equals(new()) &&
        Equals(IdRRef, Guid.Empty);

    public override object Clone()
    {
        TemplateResourceDeprecatedModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Type = Type;
        item.IdRRef = IdRRef;
        item.ImageData = ImageData.CloneCast();
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
        info.AddValue(nameof(Type), Type);
        info.AddValue(nameof(ImageData), ImageData);
        info.AddValue(nameof(IdRRef), IdRRef);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Description = LocaleCore.Sql.SqlItemFieldDescription;
        ImageData.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(TemplateResourceDeprecatedModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Type, item.Type) &&
        Equals(IdRRef, item.IdRRef) &&
        ImageData.Equals(item.ImageData);

    public new virtual TemplateResourceDeprecatedModel CloneCast() => (TemplateResourceDeprecatedModel)Clone();

    #endregion
}