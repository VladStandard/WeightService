// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Enums;
using WsStorageCore.Tables;

namespace WsStorageCore.TableScaleModels.Access;

/// <summary>
/// Table "ACCESS".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(AccessModel)}")]
public class AccessModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual DateTime LoginDt { get; set; }
    [XmlElement] public virtual byte Rights { get; set; }
    [XmlIgnore] public virtual AccessRightsEnum RightsEnum => (AccessRightsEnum)Rights;

    /// <summary>
    /// Constructor.
    /// </summary>
    public AccessModel() : base(WsSqlFieldIdentity.Uid)
    {
        LoginDt = DateTime.MinValue;
        Rights = 0x00;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
	/// <param name="context"></param>
	protected AccessModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        LoginDt = info.GetDateTime(nameof(LoginDt));
        Rights = info.GetByte(nameof(Rights));
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Rights)}: {RightsEnum}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((AccessModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(LoginDt, DateTime.MinValue) &&
        Equals(Rights, (byte)0x00);

    public override object Clone()
    {
        AccessModel item = new();
        item.CloneSetup(base.CloneCast());
        item.LoginDt = LoginDt;
        item.Rights = Rights;
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
        info.AddValue(nameof(LoginDt), LoginDt);
        info.AddValue(nameof(Rights), Rights);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        LoginDt = DateTime.Now;
        Rights = (byte)AccessRightsEnum.None;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(AccessModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(LoginDt, item.LoginDt) &&
        Equals(Rights, item.Rights);

    public new virtual AccessModel CloneCast() => (AccessModel)Clone();

    #endregion
}
