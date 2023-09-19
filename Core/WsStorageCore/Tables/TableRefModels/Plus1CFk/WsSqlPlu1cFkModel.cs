// ReSharper disable VirtualMemberCallInConstructor

namespace WsStorageCore.Tables.TableRefModels.Plus1CFk;

/// <summary>
/// Доменная модель таблицы REF.PLUS_1C_FK.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlPlu1CFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual WsSqlPluModel Plu { get; set; }
    public virtual bool IsEnabled { get; set; }
    public virtual string RequestDataString { get; set; }

    public WsSqlPlu1CFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Plu = new();
        IsEnabled = false;
        RequestDataString = string.Empty;
    }

    public WsSqlPlu1CFkModel(WsSqlPlu1CFkModel item) : base(item)
    {
        Plu = new(item.Plu);
        IsEnabled = item.IsEnabled;
        RequestDataString = item.RequestDataString;
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

    #endregion
}