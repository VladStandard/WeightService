// ReSharper disable VirtualMemberCallInConstructor

namespace WsStorageCore.Tables.TableDiagModels.LogsMemories;

[DebuggerDisplay("{ToString()}")]
public class WsSqlLogMemoryModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual short SizeAppMb { get; set; }
    [XmlElement] public virtual short SizeFreeMb { get; set; }
    [XmlElement] public virtual WsSqlAppModel App { get; set; }
    [XmlElement] public virtual WsSqlDeviceModel Device { get; set; }

    public WsSqlLogMemoryModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        SizeAppMb = default;
        SizeFreeMb = default;
        App = new();
        Device = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlLogMemoryModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        SizeAppMb = info.GetInt16(nameof(SizeAppMb));
        SizeFreeMb = info.GetInt16(nameof(SizeAppMb));
        App = (WsSqlAppModel)info.GetValue(nameof(App), typeof(WsSqlAppModel));
        Device = (WsSqlDeviceModel)info.GetValue(nameof(Device), typeof(WsSqlDeviceModel));
    }

    public WsSqlLogMemoryModel(WsSqlLogMemoryModel item) : base(item)
    {
        SizeAppMb = item.SizeAppMb;
        SizeFreeMb = item.SizeFreeMb;
        App = new(item.App);
        Device = new(item.Device);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => 
        $"{GetIsMarked()} | {App.Name} | {Device.Name} | {SizeAppMb} / {SizeFreeMb}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlLogMemoryModel)obj);
    }

    public override int GetHashCode() => (App, Device, SizeAppMb, SizeFreeMb).GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(SizeAppMb, Array.Empty<byte>()) &&
        App.EqualsDefault() && 
        Device.EqualsDefault() && 
        Equals(SizeAppMb, default) &&
        Equals(SizeFreeMb, default);

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(SizeAppMb), SizeAppMb);
        info.AddValue(nameof(SizeFreeMb), SizeFreeMb);
        info.AddValue(nameof(App), App);
        info.AddValue(nameof(Device), Device);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        App.FillProperties();
        Device.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlLogMemoryModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(SizeAppMb, item.SizeAppMb) &&
        Equals(SizeFreeMb, item.SizeFreeMb) &&
        Device.Equals(item.Device) &&
        Equals(App, item.App) &&
        Equals(Device, item.Device);

    #endregion
}