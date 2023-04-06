// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;

namespace DataCore.Sql.TableScaleModels.TemplatesResources;

/// <summary>
/// Table "TEMPLATES_RESOURCES".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(TemplateResourceModel)} | {Type} | {Name}")]
public class TemplateResourceModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual string Type { get; set; }
    [XmlElement] public virtual SqlFieldBinaryModel Data { get; set; }
    [XmlIgnore] public virtual byte[] DataValue { get => Data.Value; set => Data.Value = value; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public TemplateResourceModel() : base(WsSqlFieldIdentity.Uid)
    {
        Type = string.Empty;
        Data = new();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected TemplateResourceModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Type = info.GetString(nameof(Type));
        Data = (SqlFieldBinaryModel)info.GetValue(nameof(Data), typeof(SqlFieldBinaryModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Type)}: {Type}. ";

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
        Equals(Type, string.Empty) &&
        Data.Equals(new());

    public override object Clone()
    {
        TemplateResourceModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Type = Type;
        item.Data = Data.CloneCast();
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
        info.AddValue(nameof(Data), Data);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Description = LocaleCore.Sql.SqlItemFieldDescription;
        Data.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(TemplateResourceModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Type, item.Type) &&
        Data.Equals(item.Data);

    public new virtual TemplateResourceModel CloneCast() => (TemplateResourceModel)Clone();

    #endregion
}