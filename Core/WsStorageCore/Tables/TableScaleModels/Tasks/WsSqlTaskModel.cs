namespace WsStorageCore.Tables.TableScaleModels.Tasks;

[DebuggerDisplay("{ToString()}")]
public class WsSqlTaskModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual WsSqlTaskTypeModel TaskType { get; set; }
    public virtual WsSqlScaleModel Scale { get; set; }
    public virtual bool Enabled { get; set; }
    
    public WsSqlTaskModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        TaskType = new();
        Scale = new();
        Enabled = false;
    }

    public WsSqlTaskModel(WsSqlTaskModel item) : base(item)
    {
        TaskType = new(item.TaskType);
        Scale = new(item.Scale);
        Enabled = item.Enabled;
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


    #endregion
}