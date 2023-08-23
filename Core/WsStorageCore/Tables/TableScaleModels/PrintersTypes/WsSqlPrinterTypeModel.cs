// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.PrintersTypes;

[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPrinterTypeModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor
    
    public WsSqlPrinterTypeModel() : base(WsSqlEnumFieldIdentity.Id)
    {
        //
    }
    
    protected WsSqlPrinterTypeModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        //
    }

    public WsSqlPrinterTypeModel(WsSqlPrinterTypeModel item) : base(item) { }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Name)}: {Name}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPrinterTypeModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() => base.EqualsDefault();

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPrinterTypeModel item) =>
        ReferenceEquals(this, item) || base.Equals(item);

    #endregion
}
