using Ws.StorageCore.Enums;

namespace Ws.StorageCore.Entities.SchemaRef.Printers;

[DebuggerDisplay("{ToString()}")]
public class SqlPrinterEntity : SqlEntityBase
{
    #region Public and private fields, properties, constructor
    public virtual string Ip { get; set; }
    public virtual short Port { get; set; }
    public virtual PrinterTypeEnum Type { get; set; }
    public virtual string Link => string.IsNullOrEmpty(Ip) ? string.Empty : $"http://{Ip}";
    public override string DisplayName => IsNew ?  LocaleCore.Table.FieldEmpty : $"{Name} | {Ip}";

    public SqlPrinterEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Ip = string.Empty;
        Port = 0;
        Type = PrinterTypeEnum.Tsc;
    }

    public SqlPrinterEntity(SqlPrinterEntity item) : base(item)
    {
        Ip = item.Ip;
        Port = item.Port;
        Type = item.Type;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{nameof(Type)}: {Type}.";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlPrinterEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override void FillProperties()
    {
        base.FillProperties();
        Port = 9100;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(SqlPrinterEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Ip, item.Ip) &&
        Equals(Port, item.Port) &&
        Equals(Type, PrinterTypeEnum.Tsc);

    #endregion
}