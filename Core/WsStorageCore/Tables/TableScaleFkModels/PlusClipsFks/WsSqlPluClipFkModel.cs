namespace WsStorageCore.Tables.TableScaleFkModels.PlusClipsFks;

[DebuggerDisplay("{ToString()}")]
public class WsSqlPluClipFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public virtual WsSqlClipModel Clip { get; set; }
    public virtual WsSqlPluModel Plu { get; set; }
    
    public WsSqlPluClipFkModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Clip = new();
        Plu = new();

    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    protected WsSqlPluClipFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Plu = (WsSqlPluModel)info.GetValue(nameof(Plu), typeof(WsSqlPluModel));
        Clip = (WsSqlClipModel)info.GetValue(nameof(Clip), typeof(WsSqlClipModel));
    }

    public WsSqlPluClipFkModel(WsSqlPluClipFkModel item) : base(item)
    {
        Clip = new(item.Clip);
        Plu = new(item.Plu);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Plu)}: {Plu.Name}. " +
        $"{nameof(Clip)}: {Clip.Name}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluClipFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Clip.EqualsDefault() &&
        Plu.EqualsDefault();

    public override void FillProperties()
    {
        base.FillProperties();
        Clip.FillProperties();
        Plu.FillProperties();
    }
    
    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluClipFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Clip.Equals(item.Clip) &&
        Plu.Equals(item.Plu);

    #endregion
}