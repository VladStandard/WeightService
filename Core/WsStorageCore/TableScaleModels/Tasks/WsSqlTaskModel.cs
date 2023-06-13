// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Tasks;

/// <summary>
/// Table "TASKS".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlTaskModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlTaskTypeModel TaskType { get; set; }
    [XmlElement] public virtual WsSqlScaleModel Scale { get; set; }
    [XmlElement] public virtual bool Enabled { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlTaskModel() : base(WsSqlFieldIdentity.Uid)
    {
        TaskType = new();
        Scale = new();
        Enabled = false;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlTaskModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        TaskType = (WsSqlTaskTypeModel)info.GetValue(nameof(TaskType), typeof(WsSqlTaskTypeModel));
        Scale = (WsSqlScaleModel)info.GetValue(nameof(Scale), typeof(WsSqlScaleModel));
        Enabled = info.GetBoolean(nameof(Enabled));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(TaskType)}: {TaskType}. " +
        $"{nameof(Scale)}: {Scale}. " +
        $"{nameof(Enabled)}: {Enabled}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlTaskModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Enabled, false);

    public override object Clone()
    {
        WsSqlTaskModel item = new();
        item.CloneSetup(base.CloneCast());
        item.TaskType = TaskType.CloneCast();
        item.Scale = Scale.CloneCast();
        item.Enabled = Enabled;
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
        info.AddValue(nameof(TaskType), TaskType);
        info.AddValue(nameof(Scale), Scale);
        info.AddValue(nameof(Enabled), Enabled);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        TaskType.FillProperties();
        Scale.FillProperties();
    }

    #endregion

    #region Public and private methods

    public virtual bool Equals(WsSqlTaskModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Enabled, item.Enabled) &&
        TaskType.Equals(item.TaskType) &&
        Scale.Equals(item.Scale);

    public new virtual WsSqlTaskModel CloneCast() => (WsSqlTaskModel)Clone();


    #endregion
}
