// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

namespace WsStorageCore.TableScaleModels.PlusScales;

/// <summary>
/// Table "PLUS_SCALES".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluScaleModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual bool IsActive { get; set; }
    [XmlElement] public virtual WsSqlPluModel Plu { get; set; }
    [XmlElement] public virtual WsSqlScaleModel Line { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluScaleModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        IsActive = false;
        Plu = new();
        Line = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlPluScaleModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        IsActive = info.GetBoolean(nameof(IsActive));
        Plu = (WsSqlPluModel)info.GetValue(nameof(Plu), typeof(WsSqlPluModel));
        Line = (WsSqlScaleModel)info.GetValue(nameof(Line), typeof(WsSqlScaleModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {IsActive} | {Plu} | {Line}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluScaleModel)obj);
    }

    public override int GetHashCode() => (IsActive, Plu, Scale: Line).GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(IsActive, false) &&
        Plu.EqualsDefault() &&
        Line.EqualsDefault();

    public object Clone()
    {
        WsSqlPluScaleModel item = new();
        item.IsActive = IsActive;
        item.Plu = Plu.CloneCast();
        item.Line = Line.CloneCast();
        return item;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(IsActive), IsActive);
        info.AddValue(nameof(Plu), Plu);
        info.AddValue(nameof(Line), Line);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        IsActive = true;
        Plu.FillProperties();
        Line.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluScaleModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(IsActive, item.IsActive) &&
        Equals(Plu, item.Plu) &&
        Equals(Line, item.Line) &&
        Plu.Equals(item.Plu) &&
        Line.Equals(item.Line);

    public new virtual WsSqlPluScaleModel CloneCast() => (WsSqlPluScaleModel)Clone();

    #endregion
}
