// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "ZebraPrinter".
/// </summary>
[Serializable]
public class PrinterModel : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual string Ip { get; set; }
	[XmlElement] public virtual string Link => string.IsNullOrEmpty(Ip) ? string.Empty : $"http://{Ip}";
	[XmlElement] public virtual short Port { get; set; }
	[XmlElement] public virtual string Password { get; set; }
	[XmlElement] public virtual PrinterTypeModel PrinterType { get; set; }
	[XmlElement] public virtual FieldMacAddressModel MacAddress { get; set; }
	[XmlElement] public virtual string MacAddressValue { get => MacAddress.Value; set => MacAddress.Value = value; }
	[XmlElement] public virtual bool PeelOffSet { get; set; }
	[XmlElement] public virtual short DarknessLevel { get; set; }
    [XmlIgnore] public virtual HttpStatusCode HttpStatusCode { get; set; }
    [XmlIgnore] public virtual IPStatus PingStatus { get; set; }
    [XmlIgnore] public virtual bool IsPing => PingStatus == IPStatus.Success;
    [XmlIgnore] public virtual Exception? HttpStatusException { get; set; }
    [XmlIgnore] public virtual bool IsConnect => HttpStatusCode == HttpStatusCode.OK;

	/// <summary>
	/// Constructor.
	/// </summary>
    public PrinterModel() : base(ColumnName.Id)
    {
	    Name = string.Empty;
	    Ip = string.Empty;
	    Port = 0;
	    Password = string.Empty;
	    PrinterType = new();
	    MacAddress = new();
	    PeelOffSet = false;
	    DarknessLevel = 0;
	    HttpStatusCode = HttpStatusCode.BadRequest;
	    HttpStatusException = null;
    }

	/// <summary>
	/// Constructor for serialization.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	public PrinterModel(SerializationInfo info, StreamingContext context) : base(info, context)
	{
		Name = info.GetString(nameof(Name));
		Ip = info.GetString(nameof(Ip));
		Port = info.GetInt16(nameof(Port));
		Password = info.GetString(nameof(Password));
		PrinterType = (PrinterTypeModel)info.GetValue(nameof(PrinterType), typeof(PrinterTypeModel));
		MacAddress = (FieldMacAddressModel)info.GetValue(nameof(MacAddress), typeof(FieldMacAddressModel));
		PeelOffSet = info.GetBoolean(nameof(PeelOffSet));
		DarknessLevel = info.GetInt16(nameof(DarknessLevel));
		HttpStatusCode = (HttpStatusCode)info.GetValue(nameof(HttpStatusCode), typeof(HttpStatusCode));
		HttpStatusException = (Exception)info.GetValue(nameof(HttpStatusException), typeof(Exception));
	}

	#endregion

	#region Public and private methods

	public override string ToString() =>
	    $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Ip)}: {Ip}. " +
        $"{nameof(Port)}: {Port}. " +
        $"{nameof(Password)}: {Password}. " +
        $"{nameof(PrinterType)}: {PrinterType}. " +
        $"{nameof(MacAddress)}: {MacAddress}. " +
        $"{nameof(PeelOffSet)}: {PeelOffSet}. " +
        $"{nameof(DarknessLevel)}: {DarknessLevel}. " +
        $"{nameof(HttpStatusCode)}: {HttpStatusCode}. " +
        $"{nameof(HttpStatusException)}: {HttpStatusException}. ";

    public virtual bool Equals(PrinterModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        if (!PrinterType.Equals(item.PrinterType))
            return false;
        if (!MacAddress.Equals(item.MacAddress))
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
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((PrinterModel)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (!PrinterType.EqualsDefault())
            return false;
        if (!MacAddress.EqualsDefault())
            return false;
        return base.EqualsDefault() &&
               Equals(Name, string.Empty) &&
               Equals(Ip, string.Empty) &&
               Equals(Port, (short)0) &&
               Equals(Password, string.Empty) &&
               Equals(PeelOffSet, false) &&
               Equals(DarknessLevel, (short)0) &&
               Equals(HttpStatusCode, HttpStatusCode.BadRequest) &&
               Equals(HttpStatusException, null);
    }

    public new virtual object Clone()
    {
        PrinterModel item = new();
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
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual PrinterModel CloneCast() => (PrinterModel)Clone();

    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
	    base.GetObjectData(info, context);
	    info.AddValue(nameof(Name), Name);
	    info.AddValue(nameof(Ip), Ip);
	    info.AddValue(nameof(Port), Port);
	    info.AddValue(nameof(Password), Password);
	    info.AddValue(nameof(PrinterType), PrinterType);
	    info.AddValue(nameof(MacAddress), MacAddress);
	    info.AddValue(nameof(PeelOffSet), PeelOffSet);
	    info.AddValue(nameof(DarknessLevel), DarknessLevel);
	    info.AddValue(nameof(HttpStatusCode), HttpStatusCode);
	    info.AddValue(nameof(HttpStatusException), HttpStatusException);
	}

	#endregion
}
