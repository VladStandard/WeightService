// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

using WsStorageCore.Enums;
using WsStorageCore.Tables;

namespace WsStorageCore.TableScaleModels.PlusStorageMethods;

/// <summary>
/// Table "PLUS_STORAGE_METHODS".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PlusStorageMethodModel)} | {IsMarked} | {Name} | {MinTemp} | {MaxTemp}")]
public class PluStorageMethodModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual short MinTemp { get; set; }
    [XmlElement] public virtual short MaxTemp { get; set; }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluStorageMethodModel() : base(WsSqlFieldIdentity.Uid)
    {
        MinTemp = default;
        MaxTemp = default;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluStorageMethodModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        MinTemp = info.GetInt16(nameof(MinTemp));
        MaxTemp = info.GetInt16(nameof(MaxTemp));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(MinTemp)}: {MinTemp}. " +
        $"{nameof(MaxTemp)}: {MaxTemp}.";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluStorageMethodModel)obj);
    }

    public override int GetHashCode() => IdentityValueUid.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(MinTemp, default(short)) &&
        Equals(MaxTemp, default(short));

    public override object Clone()
    {
        PluStorageMethodModel item = new();
        item.CloneSetup(base.CloneCast());
        item.MinTemp = MinTemp;
        item.MaxTemp = MaxTemp;
        return item;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(MinTemp), MinTemp);
        info.AddValue(nameof(MaxTemp), MaxTemp);
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluStorageMethodModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(MinTemp, item.MinTemp) &&
        Equals(MaxTemp, item.MaxTemp);

    public new virtual PluStorageMethodModel CloneCast() => (PluStorageMethodModel)Clone();

    #endregion
}