// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Access;

/// <summary>
/// Table "ACCESS".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlAccessModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual DateTime LoginDt { get; set; }
    [XmlElement] public virtual byte Rights { get; set; }

    [XmlIgnore] public virtual WsEnumAccessRights RightsEnum => (WsEnumAccessRights)Rights;

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlAccessModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        LoginDt = DateTime.MinValue;
        Rights = 0x00;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
	/// <param name="context"></param>
	protected WsSqlAccessModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        LoginDt = info.GetDateTime(nameof(LoginDt));
        Rights = info.GetByte(nameof(Rights));
    }

    public WsSqlAccessModel(WsSqlAccessModel item) : base(item)
    {
        LoginDt = item.LoginDt;
        Rights = item.Rights;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {Name} | {RightsEnum}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlAccessModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(LoginDt, DateTime.MinValue) &&
        Equals(Rights, (byte)0x00);

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(LoginDt), LoginDt);
        info.AddValue(nameof(Rights), Rights);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        LoginDt = DateTime.Now;
        Rights = (byte)WsEnumAccessRights.None;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlAccessModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(LoginDt, item.LoginDt) &&
        Equals(Rights, item.Rights);

    #endregion
}
