// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com


using DataCore.Sql.Core.Enums;
using DataCore.Sql.Core.Interfaces;
using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleFkModels.PlusCharacteristicsFks;
using DataCore.Sql.TableScaleModels.Clips;
using DataCore.Sql.TableScaleModels.Plus;

namespace DataCore.Sql.TableScaleFkModels.PlusClipsFks;

/// <summary>
/// Table "PLUS_CLIPS_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluClipFkModel)} | {Plu.Number} | {Plu.Name} | {Clip.Name} | {Clip.Weight}")]
public class PluClipFkModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual ClipModel Clip { get; set; }
    [XmlElement] public virtual PluModel Plu { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluClipFkModel() : base(SqlFieldIdentity.Uid)
    {
        Clip = new();
        Plu = new();

    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluClipFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Plu = (PluModel)info.GetValue(nameof(Plu), typeof(PluModel));
        Clip = (ClipModel)info.GetValue(nameof(Clip), typeof(ClipModel));
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Plu)}: {Plu.Name}. " +
        $"{nameof(Clip)}: {Clip.Name}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluClipFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Clip.EqualsDefault() &&
        Plu.EqualsDefault();

    public override object Clone()
    {
        PluClipFkModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Clip = Clip.CloneCast();
        item.Plu = Plu.CloneCast();
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
        info.AddValue(nameof(Clip), Clip);
        info.AddValue(nameof(Plu), Plu);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Clip.FillProperties();
        Plu.FillProperties();
    }

    public override void UpdateProperties(ISqlTable item)
    {
        base.UpdateProperties(item);
        // Get properties from /api/send_nomenclatures/.
        if (item is not PluClipFkModel pluClipFk) return;
        Plu = pluClipFk.Plu;
        Clip = pluClipFk.Clip;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluClipFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Clip.Equals(item.Clip) &&
        Plu.Equals(item.Plu);

    public new virtual PluClipFkModel CloneCast() => (PluClipFkModel)Clone();

    #endregion
}