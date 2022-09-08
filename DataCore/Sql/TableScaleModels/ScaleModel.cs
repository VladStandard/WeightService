// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "Scales".
/// </summary>
[Serializable]
public class ScaleModel : TableBase, ICloneable, ISqlDbBase, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement(IsNullable = true)] public virtual TemplateModel? TemplateDefault { get; set; }
	[XmlElement(IsNullable = true)] public virtual TemplateModel? TemplateSeries { get; set; }
	[XmlElement(IsNullable = true)] public virtual WorkShopModel? WorkShop { get; set; }
	[XmlElement(IsNullable = true)] public virtual PrinterModel? PrinterMain { get; set; }
	[XmlElement(IsNullable = true)] public virtual PrinterModel? PrinterShipping { get; set; }
	[XmlElement] public virtual byte ShippingLength { get; set; }
	[XmlElement(IsNullable = true)] public virtual HostModel? Host { get; set; }
	[XmlElement] public virtual string Description { get; set; }
	[XmlElement] public virtual string DeviceIp { get; set; }
	[XmlElement] public virtual short DevicePort { get; set; }
	[XmlElement] public virtual string DeviceMac { get; set; }
	[XmlElement(IsNullable = true)] public virtual short? DeviceSendTimeout { get; set; }
	[XmlElement(IsNullable = true)] public virtual short? DeviceReceiveTimeout { get; set; }
	[XmlElement] public virtual string DeviceComPort { get; set; }
	[XmlElement] public virtual string ZebraIp { get; set; }
    [XmlIgnore] public virtual string ZebraLink => string.IsNullOrEmpty(ZebraIp) ? string.Empty : $"http://{ZebraIp}";
    [XmlElement(IsNullable = true)] public virtual short? ZebraPort { get; set; }
    [XmlElement] public virtual int Number { get; set; }
    [XmlElement] public virtual int Counter { get; set; }
    [XmlElement(IsNullable = true)] public virtual int? ScaleFactor { get; set; }
    [XmlElement] public virtual bool IsShipping { get; set; }
    [XmlElement] public virtual bool IsOrder { get; set; }
    [XmlElement] public virtual bool IsKneading { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public ScaleModel() : base(SqlFieldIdentityEnum.Id)
    {
	    TemplateDefault = null;
	    TemplateSeries = null;
	    WorkShop = null;
	    Host = null;
	    PrinterMain = null;
	    PrinterShipping = null;
	    ShippingLength = 0;
	    Description = string.Empty;
	    DeviceIp = string.Empty;
	    DevicePort = 0;
	    DeviceMac = string.Empty;
	    DeviceSendTimeout = default;
	    DeviceReceiveTimeout = default;
	    DeviceComPort = string.Empty;
	    ZebraIp = string.Empty;
	    ZebraPort = default;
	    Number = 0;
	    Counter = 0;
	    ScaleFactor = default;
	    IsShipping = false;
	    IsOrder = false;
	    IsKneading = false;
    }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	public ScaleModel(SerializationInfo info, StreamingContext context) : this()
    {
        TemplateDefault = (TemplateModel?)info.GetValue(nameof(TemplateDefault), typeof(TemplateModel));
		TemplateSeries = (TemplateModel?)info.GetValue(nameof(TemplateSeries), typeof(TemplateModel));
		WorkShop = (WorkShopModel?)info.GetValue(nameof(WorkShop), typeof(WorkShopModel));
		Host = (HostModel?)info.GetValue(nameof(Host), typeof(HostModel));
		PrinterMain = (PrinterModel?)info.GetValue(nameof(PrinterMain), typeof(PrinterModel));
		PrinterShipping = (PrinterModel?)info.GetValue(nameof(PrinterShipping), typeof(PrinterModel));
		ShippingLength = info.GetByte(nameof(ShippingLength));
		Description = info.GetString(nameof(Description));
		DeviceIp = info.GetString(nameof(DeviceIp));
		DevicePort = info.GetInt16(nameof(DevicePort));
		DeviceMac = info.GetString(nameof(DeviceMac));
		DeviceSendTimeout = (short?)info.GetValue(nameof(DeviceSendTimeout), typeof(short));
		DeviceReceiveTimeout = (short?)info.GetValue(nameof(DeviceReceiveTimeout), typeof(short));
		DeviceComPort = info.GetString(nameof(DeviceComPort));
		ZebraIp = info.GetString(nameof(ZebraIp));
		ZebraPort = (short?)info.GetValue(nameof(ZebraPort), typeof(short));
		Number = info.GetInt32(nameof(Number));
		Counter = info.GetInt32(nameof(Counter));
		ScaleFactor = (int?)info.GetValue(nameof(ScaleFactor), typeof(int));
		IsShipping = info.GetBoolean(nameof(IsShipping));
		IsOrder = info.GetBoolean(nameof(IsOrder));
		IsKneading = info.GetBoolean(nameof(IsKneading));
	}

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
	    $"{nameof(Description)}: {Description}. " +
	    $"{nameof(DeviceIp)}: {DeviceIp}. ";

    public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((ScaleModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

	public override bool EqualsNew() => Equals(new());

	public override bool EqualsDefault()
    {
        if (TemplateDefault != null && !TemplateDefault.EqualsDefault())
            return false;
        if (TemplateSeries != null && !TemplateSeries.EqualsDefault())
            return false;
        if (WorkShop != null && !WorkShop.EqualsDefault())
            return false;
        if (PrinterMain != null && !PrinterMain.EqualsDefault())
            return false;
        if (PrinterShipping != null && !PrinterShipping.EqualsDefault())
	        return false;
		if (Host != null && !Host.EqualsDefault())
			return false;
        return base.EqualsDefault() &&
               Equals(Description, string.Empty) &&
               Equals(DeviceIp, string.Empty) &&
               Equals(DevicePort, (short)0) &&
               Equals(DeviceMac, string.Empty) &&
               Equals(DeviceSendTimeout, null) &&
               Equals(DeviceReceiveTimeout, null) &&
               Equals(DeviceComPort, string.Empty) &&
               Equals(ZebraIp, string.Empty) &&
               Equals(ZebraPort, null) &&
               Equals(IsOrder, false) &&
               Equals(Number, 0) &&
               Equals(Counter, 0) &&
               Equals(ScaleFactor, null) &&
               Equals(IsShipping, false) &&
               Equals(IsKneading, false) &&
               Equals(ShippingLength, (byte)0);
    }

	public override object Clone()
    {
        ScaleModel item = new();
        item.TemplateDefault = TemplateDefault?.CloneCast();
        item.TemplateSeries = TemplateSeries?.CloneCast();
        item.WorkShop = WorkShop?.CloneCast();
        item.PrinterMain = PrinterMain?.CloneCast();
        item.PrinterShipping = PrinterShipping?.CloneCast();
        item.IsShipping = IsShipping;
        item.IsKneading = IsKneading;
        item.ShippingLength = ShippingLength;
        item.Host = Host?.CloneCast();
        item.Description = Description;
        item.DeviceIp = DeviceIp;
        item.DevicePort = DevicePort;
        item.DeviceMac = DeviceMac;
        item.DeviceSendTimeout = DeviceSendTimeout;
        item.DeviceReceiveTimeout = DeviceReceiveTimeout;
        item.DeviceComPort = DeviceComPort;
        item.ZebraIp = ZebraIp;
        item.ZebraPort = ZebraPort;
        item.IsOrder = IsOrder;
        item.Number = Number;
        item.Counter = Counter;
        item.ScaleFactor = ScaleFactor;
		item.CloneSetup(base.CloneCast());
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
        info.AddValue(nameof(TemplateDefault), TemplateDefault);
        info.AddValue(nameof(TemplateSeries), TemplateSeries);
        info.AddValue(nameof(WorkShop), WorkShop);
        info.AddValue(nameof(Host), Host);
        info.AddValue(nameof(PrinterMain), PrinterMain);
        info.AddValue(nameof(PrinterShipping), PrinterShipping);
        info.AddValue(nameof(ShippingLength), ShippingLength);
        info.AddValue(nameof(Description), Description);
        info.AddValue(nameof(DeviceIp), DeviceIp);
        info.AddValue(nameof(DevicePort), DevicePort);
        info.AddValue(nameof(DeviceMac), DeviceMac);
        info.AddValue(nameof(DeviceSendTimeout), DeviceSendTimeout);
        info.AddValue(nameof(DeviceReceiveTimeout), DeviceReceiveTimeout);
        info.AddValue(nameof(DeviceComPort), DeviceComPort);
        info.AddValue(nameof(ZebraIp), ZebraIp);
        info.AddValue(nameof(ZebraPort), ZebraPort);
        info.AddValue(nameof(Number), Number);
        info.AddValue(nameof(Counter), Counter);
        info.AddValue(nameof(ScaleFactor), ScaleFactor);
        info.AddValue(nameof(IsShipping), IsShipping);
        info.AddValue(nameof(IsOrder), IsOrder);
        info.AddValue(nameof(IsKneading), IsKneading);
	}

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(ScaleModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		if (TemplateDefault != null && item.TemplateDefault != null && !TemplateDefault.Equals(item.TemplateDefault))
			return false;
		if (TemplateSeries != null && item.TemplateSeries != null && !TemplateSeries.Equals(item.TemplateSeries))
			return false;
		if (WorkShop != null && item.WorkShop != null && !WorkShop.Equals(item.WorkShop))
			return false;
		if (PrinterMain != null && item.PrinterMain != null && !PrinterMain.Equals(item.PrinterMain))
			return false;
		if (PrinterShipping != null && item.PrinterShipping != null && !PrinterShipping.Equals(item.PrinterShipping))
			return false;
		if (Host != null && item.Host != null && !Host.Equals(item.Host))
			return false;
		return base.Equals(item) &&
			   Equals(Description, item.Description) &&
			   Equals(DeviceIp, item.DeviceIp) &&
			   Equals(DevicePort, item.DevicePort) &&
			   Equals(DeviceMac, item.DeviceMac) &&
			   Equals(DeviceSendTimeout, item.DeviceSendTimeout) &&
			   Equals(DeviceReceiveTimeout, item.DeviceReceiveTimeout) &&
			   Equals(DeviceComPort, item.DeviceComPort) &&
			   Equals(ZebraIp, item.ZebraIp) &&
			   Equals(ZebraPort, item.ZebraPort) &&
			   Equals(IsOrder, item.IsOrder) &&
			   Equals(Number, item.Number) &&
			   Equals(Counter, item.Counter) &&
			   Equals(ScaleFactor, item.ScaleFactor) &&
			   Equals(IsShipping, item.IsShipping) &&
			   Equals(IsKneading, item.IsKneading) &&
			   ShippingLength.Equals(item.ShippingLength);
	}

	public new virtual ScaleModel CloneCast() => (ScaleModel)Clone();


	#endregion
}
