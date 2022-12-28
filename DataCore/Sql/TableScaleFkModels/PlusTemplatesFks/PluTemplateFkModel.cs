// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.Templates;

namespace DataCore.Sql.TableScaleFkModels.PlusTemplatesFks;

/// <summary>
/// Table "PLUS_TEMPLATES_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluTemplateFkModel)}")]
public class PluTemplateFkModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual PluModel Plu { get; set; }
    [XmlElement] public virtual TemplateModel Template { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluTemplateFkModel() : base(SqlFieldIdentityEnum.Uid)
    {
        Plu = new();
        Template = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluTemplateFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Plu = (PluModel)info.GetValue(nameof(Plu), typeof(PluModel));
        Template = (TemplateModel)info.GetValue(nameof(Template), typeof(TemplateModel));
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Plu)}: {Plu}. " +
        $"{nameof(Template)}: {Template}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluTemplateFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Plu.EqualsDefault() &&
        Template.EqualsDefault();

    public override object Clone()
    {
        PluTemplateFkModel item = new();
        item.Plu = Plu.CloneCast();
        item.Template = Template.CloneCast();
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
        info.AddValue(nameof(Plu), Plu);
        info.AddValue(nameof(Template), Template);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Plu.FillProperties();
        Template.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluTemplateFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Plu.Equals(item.Plu) &&
        Template.Equals(item.Template);

    public new virtual PluTemplateFkModel CloneCast() => (PluTemplateFkModel)Clone();

    #endregion
}
