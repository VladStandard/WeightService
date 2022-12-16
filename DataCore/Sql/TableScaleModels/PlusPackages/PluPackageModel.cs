// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Packages;
using DataCore.Sql.TableScaleModels.Plus;

namespace DataCore.Sql.TableScaleModels.PlusPackages;

/// <summary>
/// Table "PLUS_PACKAGES".
/// </summary>
[Serializable]
[DebuggerDisplay("Type = {nameof(PluPackageModel)}")]
public class PluPackageModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual bool IsActive { get; set; }
    [XmlElement] public virtual PackageModel Package { get; set; }
    [XmlElement] public virtual PluModel Plu { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluPackageModel() : base(SqlFieldIdentityEnum.Uid)
    {
        IsActive = false;
        Package = new();
        Plu = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluPackageModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        IsActive = info.GetBoolean(nameof(IsActive));
        Package = (PackageModel)info.GetValue(nameof(Package), typeof(PackageModel));
        Plu = (PluModel)info.GetValue(nameof(Plu), typeof(PluModel));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(IsActive)}: {IsActive}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Package)}: {Package}. " +
        $"{nameof(Plu)}: {Plu}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluPackageModel)obj);
    }

    public override int GetHashCode() => (Name, Package, Plu).GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(IsActive, false) &&
        Package.EqualsDefault() &&
        Plu.EqualsDefault();

    public override object Clone()
    {
        PluPackageModel item = new();
        item.IsActive = IsActive;
        item.Package = Package.CloneCast();
        item.Plu = Plu.CloneCast();
        item.CloneSetup(base.CloneCast());
        return item;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(IsActive), IsActive);
        info.AddValue(nameof(Package), Package);
        info.AddValue(nameof(Plu), Plu);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        IsActive = false;
        Package.FillProperties();
        Plu.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluPackageModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(IsActive, item.IsActive) &&
        Package.Equals(item.Package) &&
        Plu.Equals(item.Plu);

    public new virtual PluPackageModel CloneCast() => (PluPackageModel)Clone();

    #endregion
}
