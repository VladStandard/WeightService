// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "PLUS_SCALES".
/// </summary>
[Serializable]
public class PluScaleModel : TableModel, ISerializable, ITableModel
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual bool IsActive { get; set; }
    [XmlElement] public virtual PluModel Plu { get; set; }
    [XmlElement] public virtual ScaleModel Scale { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluScaleModel()
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
        Plu = (PluModel)info.GetValue(nameof(Plu), typeof(PluModel));
        Scale = (ScaleModel)info.GetValue(nameof(Scale), typeof(ScaleModel));
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        return
            $"{nameof(IsMarked)}: {IsMarked}. " +
            $"{nameof(IsActive)}: {IsActive}. " +
            $"{nameof(Plu)}: {Plu.Name}. " +
            $"{nameof(Scale)}: {Scale.Description}. ";
    }

    public virtual bool Equals(PluScaleModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        if (!Plu.Equals(item.Plu))
            return false;
        if (!Scale.Equals(item.Scale))
            return false;
        return
            base.Equals(item) &&
            Equals(IsActive, item.IsActive) &&
            Equals(Plu, item.Plu) &&
            Equals(Scale, item.Scale);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluScaleModel)obj);
    }

    public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (!Plu.EqualsDefault())
            return false;
        if (!Scale.EqualsDefault())
            return false;
        return
            base.EqualsDefault();
    }

    public new virtual object Clone()
    {
        PluScaleModel item = new();
        item.IsActive = IsActive;
        item.Plu = Plu.CloneCast();
        item.Scale = Scale.CloneCast();
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual PluScaleModel CloneCast() => (PluScaleModel)Clone();

    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(IsActive), IsActive);
        info.AddValue(nameof(Plu), Plu);
        info.AddValue(nameof(Scale), Scale);
    }

    #endregion
}
