// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Nomenclatures;
using DataCore.Sql.TableScaleModels.NomenclaturesCharacteristics;

namespace DataCore.Sql.TableScaleFkModels.NomenclaturesCharacteristicsFks;

/// <summary>
/// Table "NOMENCLATURES_CHARACTERISTICS_FK".
/// </summary>
[Serializable]
public class NomenclaturesCharacteristicsFkModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual NomenclatureV2Model Nomenclature { get; set; }
    [XmlElement] public virtual NomenclaturesCharacteristicsModel NomenclaturesCharacteristics { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public NomenclaturesCharacteristicsFkModel() : base(SqlFieldIdentityEnum.Uid)
    {
        Nomenclature = new();
        NomenclaturesCharacteristics = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected NomenclaturesCharacteristicsFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Nomenclature = (NomenclatureV2Model)info.GetValue(nameof(Nomenclature), typeof(NomenclatureV2Model));
        NomenclaturesCharacteristics = (NomenclaturesCharacteristicsModel)info.GetValue(nameof(NomenclaturesCharacteristics),  typeof(NomenclaturesCharacteristicsModel));
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(NomenclaturesCharacteristics)}: {NomenclaturesCharacteristics}. " +
        $"{nameof(Nomenclature)}: {Nomenclature}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((NomenclaturesCharacteristicsFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() && 
        Nomenclature.EqualsDefault() &&
        NomenclaturesCharacteristics.EqualsDefault();

    public override object Clone()
    {
        NomenclaturesCharacteristicsFkModel item = new();
        item.Nomenclature = Nomenclature.CloneCast();
        item.NomenclaturesCharacteristics = NomenclaturesCharacteristics.CloneCast();
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
        info.AddValue(nameof(Nomenclature), Nomenclature);
        info.AddValue(nameof(NomenclaturesCharacteristics), NomenclaturesCharacteristics);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Nomenclature.FillProperties();
        NomenclaturesCharacteristics.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(NomenclaturesCharacteristicsFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Nomenclature.Equals(item.Nomenclature) &&
        NomenclaturesCharacteristics.Equals(item.NomenclaturesCharacteristics);

    public new virtual NomenclaturesCharacteristicsFkModel CloneCast() => (NomenclaturesCharacteristicsFkModel)Clone();

    #endregion
}
