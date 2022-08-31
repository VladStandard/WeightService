// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "PLUS_LABELS".
/// </summary>
[Serializable]
public class PluLabelModel : TableModel, ISerializable, ITableModel
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual PluWeighingModel? PluWeighing { get; set; }
    [XmlElement] public virtual string Zpl { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluLabelModel()
    {
	    PluWeighing = null;
	    Zpl = string.Empty;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluLabelModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        PluWeighing = (PluWeighingModel)info.GetValue(nameof(PluWeighing), typeof(PluWeighingModel));
        Zpl = info.GetString(nameof(Zpl));
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
	    return
            $"{nameof(PluWeighing)}: {PluWeighing?.ToString() ?? string.Empty}. ";
    }

    public virtual bool Equals(PluLabelModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        if (PluWeighing != null && item.PluWeighing != null && !PluWeighing.Equals(item.PluWeighing))
            return false;
        return
            base.Equals(item) &&
            Equals(PluWeighing, item.PluWeighing) &&
            Equals(Zpl, item.Zpl);
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((PluLabelModel)obj);
    }

    public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (PluWeighing != null && !PluWeighing.EqualsDefault())
            return false;
        return
            base.EqualsDefault() &&
            Equals(Zpl, string.Empty);
    }

    public new virtual object Clone()
    {
        PluLabelModel item = new();
        item.IsMarked = IsMarked;
        item.PluWeighing = PluWeighing?.CloneCast();
        item.Zpl = Zpl;
		item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual PluLabelModel CloneCast() => (PluLabelModel)Clone();

    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(PluWeighing), PluWeighing);
        info.AddValue(nameof(Zpl), Zpl);
    }

    #endregion
}
