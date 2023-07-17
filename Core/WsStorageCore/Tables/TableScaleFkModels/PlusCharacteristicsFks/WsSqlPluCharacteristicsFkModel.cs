// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.PlusCharacteristicsFks;

/// <summary>
/// Table "PLUS_CHARACTERISTICS_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluCharacteristicsFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlPluModel Plu { get; set; }
    [XmlElement] public virtual WsSqlPluCharacteristicModel Characteristic { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluCharacteristicsFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Plu = new();
        Characteristic = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlPluCharacteristicsFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Plu = (WsSqlPluModel)info.GetValue(nameof(Plu), typeof(WsSqlPluModel));
        Characteristic = (WsSqlPluCharacteristicModel)info.GetValue(nameof(Characteristic),  typeof(WsSqlPluCharacteristicModel));
    }

    public WsSqlPluCharacteristicsFkModel(WsSqlPluCharacteristicsFkModel item) : base(item)
    {
        Plu = new(item.Plu);
        Characteristic = new(item.Characteristic);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Characteristic)}: {Characteristic}. " +
        $"{nameof(Plu)}: {Plu}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluCharacteristicsFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() && 
        Plu.EqualsDefault() &&
        Characteristic.EqualsDefault();

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Plu), Plu);
        info.AddValue(nameof(Characteristic), Characteristic);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Plu.FillProperties();
        Characteristic.FillProperties();
    }

    public virtual void UpdateProperties(WsSqlPluCharacteristicsFkModel item)
    {
        // Get properties from /api/send_nomenclatures/.
        base.UpdateProperties(item, true);
        
        Plu = new(item.Plu);
        Characteristic = new(item.Characteristic);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluCharacteristicsFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Plu.Equals(item.Plu) &&
        Characteristic.Equals(item.Characteristic);

    #endregion
}