// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

namespace WsStorageCore.TableDiagModels.LogsMemories;

/// <summary>
/// Table "diag.LOGS_MEMORIES".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(LogMomoryModel)} | {Device.Description} | {SizeAppMb} | SizeFreeMb")]
public class LogMemoryModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual short SizeAppMb { get; set; }
    [XmlElement] public virtual short SizeFreeMb { get; set; }
    [XmlElement] public virtual WsSqlAppModel App { get; set; }
    [XmlElement] public virtual DeviceModel Device { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public LogMemoryModel() : base(WsSqlFieldIdentity.Uid)
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
    protected LogMemoryModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        SizeAppMb = info.GetInt16(nameof(SizeAppMb));
        SizeFreeMb = info.GetInt16(nameof(SizeAppMb));
        App = (WsSqlAppModel)info.GetValue(nameof(App), typeof(WsSqlAppModel));
        Device = (DeviceModel)info.GetValue(nameof(Device), typeof(DeviceModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(App)}: {App.Name}. " +
        $"{nameof(Device)}: {Device.Name}. " +
        $"{nameof(SizeAppMb)}: {SizeAppMb}. " + 
        $"{nameof(SizeFreeMb)}: {SizeFreeMb}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((LogMemoryModel)obj);
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

    public override object Clone()
    {
        LogMemoryModel item = new();
        item.CloneSetup(base.CloneCast());
        item.SizeAppMb = SizeAppMb;
        item.SizeFreeMb = SizeFreeMb;
        item.App = App.CloneCast();
        item.Device = Device.CloneCast();
        return item;
    }

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

    public virtual bool Equals(LogMemoryModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(SizeAppMb, item.SizeAppMb) &&
        Equals(SizeFreeMb, item.SizeFreeMb) &&
        Device.Equals(item.Device) &&
        Equals(App, item.App) &&
        Equals(Device, item.Device);

    public new virtual LogMemoryModel CloneCast() => (LogMemoryModel)Clone();

    #endregion
}