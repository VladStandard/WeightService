// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Core.Interfaces;
using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusCharacteristics;

namespace DataCore.Sql.TableScaleFkModels.PlusCharacteristicsFks;

/// <summary>
/// Table "PLUS_CHARACTERISTICS_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluCharacteristicsFkModel)}")]
public class PluCharacteristicsFkModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual PluModel Plu { get; set; }
    [XmlElement] public virtual PluCharacteristicModel PluCharacteristic { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluCharacteristicsFkModel() : base(SqlFieldIdentity.Uid)
    {
        Plu = new();
        PluCharacteristic = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluCharacteristicsFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Plu = (PluModel)info.GetValue(nameof(Plu), typeof(PluModel));
        PluCharacteristic = (PluCharacteristicModel)info.GetValue(nameof(PluCharacteristic),  typeof(PluCharacteristicModel));
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(PluCharacteristic)}: {PluCharacteristic}. " +
        $"{nameof(Plu)}: {Plu}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluCharacteristicsFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() && 
        Plu.EqualsDefault() &&
        PluCharacteristic.EqualsDefault();

    public override object Clone()
    {
        PluCharacteristicsFkModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Plu = Plu.CloneCast();
        item.PluCharacteristic = PluCharacteristic.CloneCast();
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
        info.AddValue(nameof(PluCharacteristic), PluCharacteristic);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Plu.FillProperties();
        PluCharacteristic.FillProperties();
    }

    public override void UpdateProperties(ISqlTable item)
    {
        base.UpdateProperties(item);
        // Get properties from /api/send_nomenclatures/.
        if (item is not PluCharacteristicsFkModel pluCharacteristicsFk) return;
        Plu = pluCharacteristicsFk.Plu;
        PluCharacteristic = pluCharacteristicsFk.PluCharacteristic;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluCharacteristicsFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Plu.Equals(item.Plu) &&
        PluCharacteristic.Equals(item.PluCharacteristic);

    public new virtual PluCharacteristicsFkModel CloneCast() => (PluCharacteristicsFkModel)Clone();

    #endregion
}