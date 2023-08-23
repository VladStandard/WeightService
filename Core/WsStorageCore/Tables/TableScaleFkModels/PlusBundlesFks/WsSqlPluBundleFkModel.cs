// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.PlusBundlesFks;

[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluBundleFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlBundleModel Bundle { get; set; }
    [XmlElement] public virtual WsSqlPluModel Plu { get; set; }
    
    public WsSqlPluBundleFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Bundle = new();
        Plu = new();

    }
    
    protected WsSqlPluBundleFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Plu = (WsSqlPluModel)info.GetValue(nameof(Plu), typeof(WsSqlPluModel));
        Bundle = (WsSqlBundleModel)info.GetValue(nameof(Bundle), typeof(WsSqlBundleModel));
    }

    public WsSqlPluBundleFkModel(WsSqlPluBundleFkModel item) : base(item)
    {
        Bundle = new(item.Bundle);
        Plu = new(item.Plu);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{Bundle} | {Plu}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluBundleFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Bundle.EqualsDefault() &&
        Plu.EqualsDefault();

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Bundle), Bundle);
        info.AddValue(nameof(Plu), Plu);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Bundle.FillProperties();
        Plu.FillProperties();
    }

    public virtual void UpdateProperties(WsSqlPluBundleFkModel item)
    {
        // Get properties from /api/send_nomenclatures/.
        base.UpdateProperties(item, true);
        
        Plu = new(item.Plu);
        Bundle = new(item.Bundle);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluBundleFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Bundle.Equals(item.Bundle) &&
        Plu.Equals(item.Plu);

    #endregion
}