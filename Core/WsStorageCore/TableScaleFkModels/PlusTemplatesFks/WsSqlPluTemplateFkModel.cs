// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusTemplatesFks;

/// <summary>
/// Table "PLUS_TEMPLATES_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluTemplateFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlPluModel Plu { get; set; }
    [XmlElement] public virtual WsSqlTemplateModel Template { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluTemplateFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Plu = new();
        Template = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlPluTemplateFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Plu = (WsSqlPluModel)info.GetValue(nameof(Plu), typeof(WsSqlPluModel));
        Template = (WsSqlTemplateModel)info.GetValue(nameof(Template), typeof(WsSqlTemplateModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Plu)}: {Plu}. " +
        $"{nameof(Template)}: {Template}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluTemplateFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Plu.EqualsDefault() &&
        Template.EqualsDefault();

    public object Clone()
    {
        WsSqlPluTemplateFkModel item = new();
        item.Plu = Plu.CloneCast();
        item.Template = Template.CloneCast();
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

    public virtual void UpdateProperties(WsSqlPluTemplateFkModel pluTemplateFk)
    {
        // Get properties from /api/send_nomenclatures/.
        base.UpdateProperties(pluTemplateFk, true);
        
        Plu = pluTemplateFk.Plu;
        Template = pluTemplateFk.Template;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluTemplateFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Plu.Equals(item.Plu) &&
        Template.Equals(item.Template);

    public new virtual WsSqlPluTemplateFkModel CloneCast() => (WsSqlPluTemplateFkModel)Clone();

    #endregion
}