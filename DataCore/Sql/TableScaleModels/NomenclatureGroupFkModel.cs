// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "NOMENCLATURES_GROUPS_FK".
/// </summary>
[Serializable]
public class NomenclatureGroupFkModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual NomenclatureGroupModel NomenclatureGroup { get; set; }
	[XmlElement] public virtual NomenclatureGroupModel NomenclatureGroupParent { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public NomenclatureGroupFkModel() : base(SqlFieldIdentityEnum.Uid)
	{
        NomenclatureGroup = new();
        NomenclatureGroupParent = new();
	}

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private NomenclatureGroupFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        NomenclatureGroup = (NomenclatureGroupModel)info.GetValue(nameof(NomenclatureGroup), typeof(NomenclatureGroupModel));
        NomenclatureGroupParent = (NomenclatureGroupModel)info.GetValue(nameof(NomenclatureGroupParent), typeof(NomenclatureGroupModel));
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(NomenclatureGroup)}: {NomenclatureGroup}. " +
        $"{nameof(NomenclatureGroupParent)}: {NomenclatureGroupParent}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((NomenclatureGroupFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        NomenclatureGroup.EqualsDefault() &&
        NomenclatureGroupParent.EqualsDefault();

    public override object Clone()
    {
        NomenclatureGroupFkModel item = new();
        item.NomenclatureGroup = NomenclatureGroup.CloneCast();
        item.NomenclatureGroupParent = NomenclatureGroupParent.CloneCast();
        item.CloneSetup(base.CloneCast());
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
        info.AddValue(nameof(NomenclatureGroup), NomenclatureGroup);
        info.AddValue(nameof(NomenclatureGroupParent), NomenclatureGroupParent);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        NomenclatureGroup.FillProperties();
        NomenclatureGroupParent.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(NomenclatureGroupFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        NomenclatureGroup.Equals(item.NomenclatureGroup) &&
        NomenclatureGroupParent.Equals(item.NomenclatureGroupParent);

    public new virtual NomenclatureGroupFkModel CloneCast() => (NomenclatureGroupFkModel)Clone();

    #endregion
}
