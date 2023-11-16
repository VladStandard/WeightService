namespace Ws.StorageCore.Entities.SchemaScale.PlusTemplatesFks;

[DebuggerDisplay("{ToString()}")]
public class SqlPluTemplateFkEntity : SqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual SqlPluEntity Plu { get; set; }
    public virtual SqlTemplateEntity Template { get; set; }
    
    public SqlPluTemplateFkEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Plu = new();
        Template = new();
    }

    public SqlPluTemplateFkEntity(SqlPluTemplateFkEntity item) : base(item)
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
        return Equals((SqlPluTemplateFkEntity)obj);
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

    public virtual bool Equals(SqlPluTemplateFkEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Plu.Equals(item.Plu) &&
        Template.Equals(item.Template);

    #endregion
}