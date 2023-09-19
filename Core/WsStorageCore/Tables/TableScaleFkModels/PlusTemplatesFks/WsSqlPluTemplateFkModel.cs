namespace WsStorageCore.Tables.TableScaleFkModels.PlusTemplatesFks;

[DebuggerDisplay("{ToString()}")]
public class WsSqlPluTemplateFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual WsSqlPluModel Plu { get; set; }
    public virtual WsSqlTemplateModel Template { get; set; }
    
    public WsSqlPluTemplateFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Plu = new();
        Template = new();
    }

    public WsSqlPluTemplateFkModel(WsSqlPluTemplateFkModel item) : base(item)
    {
        Plu = new(item.Plu);
        Template = new(item.Template);
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

    public override void FillProperties()
    {
        base.FillProperties();
        Plu.FillProperties();
        Template.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluTemplateFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Plu.Equals(item.Plu) &&
        Template.Equals(item.Template);

    #endregion
}