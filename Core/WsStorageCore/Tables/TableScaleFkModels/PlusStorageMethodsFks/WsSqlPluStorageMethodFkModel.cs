namespace WsStorageCore.Tables.TableScaleFkModels.PlusStorageMethodsFks;

[DebuggerDisplay("{ToString()}")]
public class WsSqlPluStorageMethodFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual WsSqlPluModel Plu { get; set; }
    public virtual WsSqlPluStorageMethodModel Method { get; set; }
    public virtual WsSqlTemplateResourceModel Resource { get; set; }
    
    public WsSqlPluStorageMethodFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Plu = new();
        Method = new();
        Resource = new();
    }

    public WsSqlPluStorageMethodFkModel(WsSqlPluStorageMethodFkModel item) : base(item)
    {
        Plu = new(item.Plu);
        Method = new(item.Method);
        Resource = new(item.Resource);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(Plu)}: {Plu}. " + 
        $"{nameof(Method)}: {Method}. " +
        $"{nameof(Resource)}: {Resource}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluStorageMethodFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() && 
        Plu.EqualsDefault() &&
        Method.EqualsDefault() &&
        Resource.EqualsDefault();

    public override void FillProperties()
    {
        base.FillProperties();
        Plu.FillProperties();
        Method.FillProperties();
        Resource.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluStorageMethodFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Plu.Equals(item.Plu) &&
        Method.Equals(item.Method) &&
        Resource.Equals(item.Resource);

    #endregion
}