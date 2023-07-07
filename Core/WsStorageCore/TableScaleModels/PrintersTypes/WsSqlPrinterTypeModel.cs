// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.PrintersTypes;

/// <summary>
/// Table "ZebraPrinterType".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPrinterTypeModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPrinterTypeModel() : base(WsSqlEnumFieldIdentity.Id)
    {
        //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlPrinterTypeModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        //
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
        return Equals((WsSqlPrinterTypeModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault();

    public object Clone()
    {
        WsSqlPrinterTypeModel item = new();
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
    }

    public override void FillProperties()
    {
        base.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPrinterTypeModel item) =>
        ReferenceEquals(this, item) || base.Equals(item);

    public new virtual WsSqlPrinterTypeModel CloneCast() => (WsSqlPrinterTypeModel)Clone();

    #endregion
}
