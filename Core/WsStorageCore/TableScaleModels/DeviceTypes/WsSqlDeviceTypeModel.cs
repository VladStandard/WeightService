// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.DeviceTypes;

/// <summary>
/// Table "DEVICES_TYPES".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlDeviceTypeModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual string PrettyName { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlDeviceTypeModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        PrettyName = string.Empty;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
	/// <param name="context"></param>
	protected WsSqlDeviceTypeModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        PrettyName = info.GetString(nameof(PrettyName));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Name)}: {Name}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlDeviceTypeModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(PrettyName, string.Empty);

    public object Clone()
    {
        WsSqlDeviceTypeModel item = new();
        item.PrettyName = PrettyName;
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
        info.AddValue(nameof(PrettyName), PrettyName);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        PrettyName = WsLocaleCore.Sql.SqlItemFieldPrettyName;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlDeviceTypeModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(PrettyName, item.PrettyName);

    public new virtual WsSqlDeviceTypeModel CloneCast() => (WsSqlDeviceTypeModel)Clone();

    #endregion
}