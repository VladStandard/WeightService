// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels.NomenclaturesCharacteristics;

/// <summary>
/// Table "NOMENCLATURES_CHARACTERISTICS".
/// </summary>
[Serializable]
public class NomenclaturesCharacteristicsModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual decimal AttachmentsCount { get; set; }

    public NomenclaturesCharacteristicsModel() : base(SqlFieldIdentityEnum.Uid)
    {
        AttachmentsCount = 0;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private NomenclaturesCharacteristicsModel(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
        AttachmentsCount = info.GetDecimal(nameof(AttachmentsCount));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((NomenclaturesCharacteristicsModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public new virtual bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(AttachmentsCount, (decimal)0);

    public override object Clone()
    {
        NomenclaturesCharacteristicsModel item = new();
        item.AttachmentsCount = AttachmentsCount;
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
        info.AddValue(nameof(AttachmentsCount), AttachmentsCount);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(NomenclaturesCharacteristicsModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(AttachmentsCount, item.AttachmentsCount);
    public new virtual NomenclaturesCharacteristicsModel CloneCast() => (NomenclaturesCharacteristicsModel)Clone();

    #endregion
}

