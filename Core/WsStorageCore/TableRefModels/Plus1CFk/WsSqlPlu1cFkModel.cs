// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

namespace WsStorageCore.TableRefModels.Plus1CFk;

/// <summary>
/// Доменная модель таблицы REF.PLUS_1C_FK.
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPlu1CFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlPluModel Plu { get; set; }
    [XmlElement] public virtual bool IsEnabled { get; set; }
    [XmlElement] public virtual string RequestDataString { get; set; }

    public WsSqlPlu1CFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Plu = new();
        IsEnabled = false;
        RequestDataString = string.Empty;
    }

    public WsSqlPlu1CFkModel(WsSqlPluModel plu) : this()
    {
        Plu = plu;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlPlu1CFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Plu = (WsSqlPluModel)info.GetValue(nameof(Plu), typeof(WsSqlPluModel));
        IsEnabled = info.GetBoolean(nameof(IsEnabled));
        RequestDataString = info.GetString(nameof(RequestDataString));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {IsEnabled} | {Plu}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPlu1CFkModel)obj);
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
        WsSqlPlu1CFkModel item = new();
        item.CloneSetup(this);
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
        RequestDataString = requestDataString;
    }

    public virtual void UpdateProperties(WsSqlPlu1CFkModel plu1CFk)
    {
        // Get properties from /api/send_nomenclatures/.
        base.UpdateProperties(plu1CFk, true);

        Plu = plu1CFk.Plu;
        IsEnabled = plu1CFk.IsEnabled;
        RequestDataString = plu1CFk.RequestDataString;
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Plu.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPlu1CFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Plu.Equals(item.Plu) &&
        Equals(IsEnabled, item.IsEnabled) &&
        Equals(RequestDataString, item.RequestDataString);

    public new virtual WsSqlPlu1CFkModel CloneCast() => (WsSqlPlu1CFkModel)Clone();

    #endregion
}