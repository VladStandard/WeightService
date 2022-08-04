// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "ZebraPrinter".
/// </summary>
public class PrinterEntity : BaseEntity
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Identity name.
    /// </summary>
    public static ColumnName IdentityName => ColumnName.Id;
    public virtual string Name { get; set; }
    public virtual string Ip { get; set; }
    public virtual string Link => string.IsNullOrEmpty(Ip) ? string.Empty : $"http://{Ip}";
    public virtual short Port { get; set; }
    public virtual string Password { get; set; }
    public virtual PrinterTypeEntity PrinterType { get; set; }
    public virtual MacAddressEntity MacAddress { get; set; }
    public virtual string MacAddressValue { get => MacAddress.Value; set => MacAddress.Value = value; }
    public virtual bool PeelOffSet { get; set; }
    public virtual short DarknessLevel { get; set; }
    [XmlIgnore] public virtual System.Net.HttpStatusCode HttpStatusCode { get; set; }
    [XmlIgnore] public virtual IPStatus PingStatus { get; set; }
    [XmlIgnore] public virtual bool IsPing => PingStatus == IPStatus.Success;
    [XmlIgnore] public virtual Exception? HttpStatusException { get; set; }
    [XmlIgnore] public virtual bool IsConnect => HttpStatusCode == System.Net.HttpStatusCode.OK;

    public PrinterEntity() : this(0)
    {
        //
    }

    public PrinterEntity(long id) : base(id)
    {
        Name = string.Empty;
        Ip = string.Empty;
        Port = 0;
        Password = string.Empty;
        PrinterType = new();
        MacAddress = new();
        PeelOffSet = false;
        DarknessLevel = 0;
        HttpStatusCode = System.Net.HttpStatusCode.BadRequest;
        HttpStatusException = null;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
	    $"{nameof(IdentityId)}: {IdentityId}. " +
		base.ToString() +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Ip)}: {Ip}. " +
        $"{nameof(Port)}: {Port}. " +
        $"{nameof(Password)}: {Password}. " +
        $"{nameof(PrinterType)}: {(PrinterType != null ? PrinterType.IdentityId.ToString() : "null")}. " +
        $"{nameof(MacAddress)}: {MacAddress}. " +
        $"{nameof(PeelOffSet)}: {PeelOffSet}. " +
        $"{nameof(DarknessLevel)}: {DarknessLevel}. " +
        $"{nameof(HttpStatusCode)}: {HttpStatusCode}. " +
        $"{nameof(HttpStatusException)}: {HttpStatusException}. ";

    public virtual bool Equals(PrinterEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        if (PrinterType != null && item.PrinterType != null && !PrinterType.Equals(item.PrinterType))
            return false;
        if (MacAddress != null && item.MacAddress != null && !MacAddress.Equals(item.MacAddress))
            return false;
        return base.Equals(item) &&
               Equals(Name, item.Name) &&
               Equals(Ip, item.Ip) &&
               Equals(Port, item.Port) &&
               Equals(Password, item.Password) &&
               Equals(PeelOffSet, item.PeelOffSet) &&
               Equals(DarknessLevel, item.DarknessLevel) &&
               Equals(HttpStatusCode, item.HttpStatusCode) &&
               Equals(HttpStatusException, item.HttpStatusException);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PrinterEntity)obj);
    }

	public override int GetHashCode() => IdentityId.GetHashCode();

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (PrinterType != null && !PrinterType.EqualsDefault())
            return false;
        if (MacAddress != null && !MacAddress.EqualsDefault())
            return false;
        return base.EqualsDefault() &&
               Equals(Name, string.Empty) &&
               Equals(Ip, string.Empty) &&
               Equals(Port, (short)0) &&
               Equals(Password, string.Empty) &&
               Equals(PeelOffSet, false) &&
               Equals(DarknessLevel, (short)0) &&
               Equals(HttpStatusCode, System.Net.HttpStatusCode.BadRequest) &&
               Equals(HttpStatusException, null);
    }

    public new virtual object Clone()
    {
        PrinterEntity item = new();
        item.Name = Name;
        item.Ip = Ip;
        item.Port = Port;
        item.Password = Password;
        item.PrinterType = PrinterType.CloneCast();
        item.MacAddress = MacAddress.CloneCast();
        item.PeelOffSet = PeelOffSet;
        item.DarknessLevel = DarknessLevel;
        item.HttpStatusCode = HttpStatusCode;
        item.HttpStatusException = HttpStatusException;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual PrinterEntity CloneCast() => (PrinterEntity)Clone();

    #endregion
}
