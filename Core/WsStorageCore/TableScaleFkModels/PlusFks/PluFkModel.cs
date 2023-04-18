// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusFks;

/// <summary>
/// Table "PLUS_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluFkModel)} | {ToString()}")]
public class PluFkModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructornomenclatureCharacteristicsFk

    private PluModel _plu;
    [XmlElement] public virtual PluModel Plu { get => _plu; set => _plu = value; }
    private PluModel _parent;
    [XmlElement] public virtual PluModel Parent { get => _parent; set => _parent = value; }
    private PluModel? _category;
    [XmlElement] public virtual PluModel? Category { get => _category; set => _category = value; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluFkModel() : base(WsSqlFieldIdentity.Uid)
    {
        _plu = new();
        _parent = new();
        _category = null;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        _plu = (PluModel)info.GetValue(nameof(_plu), typeof(PluModel));
        _parent = (PluModel)info.GetValue(nameof(_parent), typeof(PluModel));
        _category = (PluModel)info.GetValue(nameof(_category), typeof(PluModel));
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Plu)}: {Plu}. " +
        $"{nameof(Parent)}: {Parent}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Plu.EqualsDefault() &&
        Parent.EqualsDefault() &&
        Category is null;

    public override object Clone()
    {
        PluFkModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Plu = Plu.CloneCast();
        item.Parent = Parent.CloneCast();
        item.Category = Category?.CloneCast();
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
        info.AddValue(nameof(Plu), Plu);
        info.AddValue(nameof(Parent), Parent);
        info.AddValue(nameof(Category), Category);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Plu.FillProperties();
        Parent.FillProperties();
        Category?.FillProperties();
    }

    public override void UpdateProperties(WsSqlTableBase item)
    {
        base.UpdateProperties(item, true);
        // Get properties from /api/send_nomenclatures/.
        if (item is not PluFkModel pluFk) return;
        Plu = pluFk.Plu;
        Parent = pluFk.Parent;
        Category = pluFk.Category;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Plu.Equals(item.Plu) &&
        Parent.Equals(item.Parent) &&
        (Category is null && item.Category is null || Category is not null && item.Category is not null && Category.Equals(item.Category));

    public new virtual PluFkModel CloneCast() => (PluFkModel)Clone();

    #endregion
}