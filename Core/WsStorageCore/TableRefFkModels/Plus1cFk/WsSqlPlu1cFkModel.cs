// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming
// ReSharper disable VirtualMemberCallInConstructor

namespace WsStorageCore.TableRefFkModels.Plus1cFk;

/// <summary>
/// Table "REF.PLUS_1C_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPlu1cFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual PluModel Plu { get; set; }
    [XmlElement] public virtual bool IsEnabled { get; set; }
    [XmlElement] public virtual string RequestDataString { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPlu1cFkModel() : base(WsSqlFieldIdentity.Uid)
    {
        Plu = new();
        IsEnabled = false;
        RequestDataString = string.Empty;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlPlu1cFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Plu = (PluModel)info.GetValue(nameof(Plu), typeof(PluModel));
        IsEnabled = info.GetBoolean(nameof(IsEnabled));
        RequestDataString = info.GetString(nameof(RequestDataString));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{IsMarked} | {IsEnabled} | {Plu}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPlu1cFkModel)obj);
    }

    public override int GetHashCode() => (IsEnabled, RequestDataString).GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Plu.EqualsDefault() && 
        Equals(IsEnabled, default) &&
        Equals(RequestDataString, string.Empty);

    public override object Clone()
    {
        WsSqlPlu1cFkModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Plu = Plu.CloneCast();
        item.IsEnabled = IsEnabled;
        item.RequestDataString = RequestDataString;
        return item;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Plu), Plu);
        info.AddValue(nameof(IsEnabled), IsEnabled);
        info.AddValue(nameof(RequestDataString), RequestDataString);
    }

    public virtual void UpdateProperties(string requestDataString)
    {
        // Get properties from /api/send_nomenclatures/.
        if (string.IsNullOrEmpty(requestDataString)) throw new ArgumentException();
        RequestDataString = requestDataString;
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Plu.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPlu1cFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Plu.Equals(item.Plu) &&
        Equals(IsEnabled, item.IsEnabled) &&
        Equals(RequestDataString, item.RequestDataString);

    public new virtual WsSqlPlu1cFkModel CloneCast() => (WsSqlPlu1cFkModel)Clone();

    #endregion
}