// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com


using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleFkModels.NestingFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;

namespace DataCore.Sql.TableScaleFkModels.PlusNestingFks;

/// <summary>
/// Table "PLUS_NESTING_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluNestingFkModel)}")]
public class PluNestingFkModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual NestingFkModel Nesting { get; set; }
    [XmlElement] public virtual PluBundleFkModel PluBundle { get; set; }
    [XmlElement] public virtual bool IsDefault { get; set; }
    [XmlIgnore] public virtual decimal WeightTare { get => PluBundle.Bundle.Weight * Nesting.BundleCount + Nesting.Box.Weight; set => _ = value; }
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluNestingFkModel() : base(SqlFieldIdentityEnum.Uid)
    {
        Nesting = new();
        PluBundle = new();
        IsDefault = false;

    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluNestingFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        PluBundle = (PluBundleFkModel)info.GetValue(nameof(PluBundle), typeof(PluBundleFkModel));
        Nesting = (NestingFkModel)info.GetValue(nameof(Nesting), typeof(NestingFkModel));
        IsDefault = info.GetBoolean(nameof(IsDefault));
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(PluBundle)}: {PluBundle.Plu.Name}. " +
        $"{nameof(PluBundle)}: {PluBundle.Bundle.Name}. " +
        $"{nameof(WeightTare)}: {WeightTare}. " +
        $"{nameof(IsDefault)}: {IsDefault}. " +
        $"{nameof(Nesting)}: {Nesting.Name}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluNestingFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(IsDefault, false) &&
        Nesting.EqualsDefault() &&
        PluBundle.EqualsDefault();

    public override object Clone()
    {
        PluNestingFkModel item = new();
        item.IsDefault = IsDefault;
        item.Nesting = Nesting.CloneCast();
        item.PluBundle = PluBundle.CloneCast();
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
        info.AddValue(nameof(IsDefault), IsDefault);
        info.AddValue(nameof(Nesting), Nesting);
        info.AddValue(nameof(PluBundle), PluBundle);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Nesting.FillProperties();
        PluBundle.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluNestingFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Nesting.Equals(item.Nesting) &&
        PluBundle.Equals(item.PluBundle) &&
        Equals(IsDefault, item.IsDefault);

    public new virtual PluNestingFkModel CloneCast() => (PluNestingFkModel)Clone();

    #endregion
}