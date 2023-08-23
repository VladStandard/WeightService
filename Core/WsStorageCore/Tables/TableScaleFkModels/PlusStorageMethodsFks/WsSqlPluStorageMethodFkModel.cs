// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.PlusStorageMethodsFks;

[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluStorageMethodFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlPluModel Plu { get; set; }
    [XmlElement] public virtual WsSqlPluStorageMethodModel Method { get; set; }
    [XmlElement] public virtual WsSqlTemplateResourceModel Resource { get; set; }
    
    public WsSqlPluStorageMethodFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Plu = new();
        Method = new();
        Resource = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlPluStorageMethodFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Plu = (WsSqlPluModel)info.GetValue(nameof(Plu), typeof(WsSqlPluModel));
        Method = (WsSqlPluStorageMethodModel)info.GetValue(nameof(Method),  typeof(WsSqlPluStorageMethodModel));
        Resource = (WsSqlTemplateResourceModel)info.GetValue(nameof(Resource),  typeof(WsSqlTemplateResourceModel));
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

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Plu), Plu);
        info.AddValue(nameof(Method), Method);
        info.AddValue(nameof(Resource), Resource);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Plu.FillProperties();
        Method.FillProperties();
        Resource.FillProperties();
    }

    public virtual void UpdateProperties(WsSqlPluStorageMethodFkModel item)
    {
        // Get properties from /api/send_nomenclatures/.
        base.UpdateProperties(item, true);
        
        Plu = new(item.Plu);
        Method = new(item.Method);
        Resource = new(item.Resource);
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