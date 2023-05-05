// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusCharacteristicsFks;

/// <summary>
/// Table "PLUS_CHARACTERISTICS_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluCharacteristicsFkModel)}")]
public class WsSqlPluCharacteristicsFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlPluModel Plu { get; set; }
    [XmlElement] public virtual PluCharacteristicModel Characteristic { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluCharacteristicsFkModel() : base(WsSqlFieldIdentity.Uid)
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
        Characteristic = (PluCharacteristicModel)info.GetValue(nameof(Characteristic),  typeof(PluCharacteristicModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
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

    public override object Clone()
    {
        WsSqlPluCharacteristicsFkModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Plu = Plu.CloneCast();
        item.Characteristic = Characteristic.CloneCast();
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
        info.AddValue(nameof(Characteristic), Characteristic);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Plu.FillProperties();
        Characteristic.FillProperties();
    }

    public override void UpdateProperties(WsSqlTableBase item)
    {
        base.UpdateProperties(item, true);
        // Get properties from /api/send_nomenclatures/.
        if (item is not WsSqlPluCharacteristicsFkModel pluCharacteristicsFk) return;
        Plu = pluCharacteristicsFk.Plu;
        Characteristic = pluCharacteristicsFk.Characteristic;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluCharacteristicsFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Plu.Equals(item.Plu) &&
        Characteristic.Equals(item.Characteristic);

    public new virtual WsSqlPluCharacteristicsFkModel CloneCast() => (WsSqlPluCharacteristicsFkModel)Clone();

    #endregion
}