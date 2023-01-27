// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.Scales;

namespace DataCore.Sql.TableScaleModels.PlusScales;

/// <summary>
/// Table "PLUS_SCALES".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluScaleModel)} | Scale = {Scale.Description} | Plu.Number = {Plu.Number} | Plu = {Plu.Name}")]
public class PluScaleModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual bool IsActive { get; set; }
    [XmlElement] public virtual PluModel Plu { get; set; }
    [XmlElement] public virtual ScaleModel Scale { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluScaleModel() : base(SqlFieldIdentity.Uid)
    {
        IsActive = false;
        Plu = new();
        Scale = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluScaleModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        IsActive = info.GetBoolean(nameof(IsActive));
        Plu = (PluModel)info.GetValue(nameof(Plu), typeof(PluModel));
        Scale = (ScaleModel)info.GetValue(nameof(Scale), typeof(ScaleModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(IsActive)}: {IsActive}. " +
        $"{nameof(Plu)}: {Plu}. " +
        $"{nameof(Scale)}: {Scale.Description}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluScaleModel)obj);
    }

    public override int GetHashCode() => (IsActive, Plu, Scale).GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(IsActive, false) &&
        Plu.EqualsDefault() &&
        Scale.EqualsDefault();

    public override object Clone()
    {
        PluScaleModel item = new();
        item.IsActive = IsActive;
        item.Plu = Plu.CloneCast();
        item.Scale = Scale.CloneCast();
        item.CloneSetup(base.CloneCast());
        return item;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(IsActive), IsActive);
        info.AddValue(nameof(Plu), Plu);
        info.AddValue(nameof(Scale), Scale);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        IsActive = true;
        Plu.FillProperties();
        Scale.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluScaleModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(IsActive, item.IsActive) &&
        Equals(Plu, item.Plu) &&
        Equals(Scale, item.Scale) &&
        Plu.Equals(item.Plu) &&
        Scale.Equals(item.Scale);

    public new virtual PluScaleModel CloneCast() => (PluScaleModel)Clone();

    #endregion
}
