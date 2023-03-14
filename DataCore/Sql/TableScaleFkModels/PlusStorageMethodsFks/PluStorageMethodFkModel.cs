// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Core.Interfaces;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusStorageMethods;

namespace DataCore.Sql.TableScaleFkModels.PlusStorageMethodsFks;

/// <summary>
/// Table "PLUS_STORAGE_METHODS_FK".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluStorageMethodFkModel)}")]
public class PluStorageMethodFkModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual PluModel Plu { get; set; }
    [XmlElement] public virtual PluStorageMethodModel Method { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluStorageMethodFkModel() : base(SqlFieldIdentity.Uid)
    {
        Plu = new();
        Method = new();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluStorageMethodFkModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Plu = (PluModel)info.GetValue(nameof(Plu), typeof(PluModel));
        Method = (PluStorageMethodModel)info.GetValue(nameof(Method),  typeof(PluStorageMethodModel));
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{nameof(Method)}: {Method}. " +
        $"{nameof(Plu)}: {Plu}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluStorageMethodFkModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() && 
        Plu.EqualsDefault() &&
        Method.EqualsDefault();

    public override object Clone()
    {
        PluStorageMethodFkModel item = new();
        item.CloneSetup(base.CloneCast());
        item.Plu = Plu.CloneCast();
        item.Method = Method.CloneCast();
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
        info.AddValue(nameof(Method), Method);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Plu.FillProperties();
        Method.FillProperties();
    }

    public override void UpdateProperties(ISqlTable item)
    {
        base.UpdateProperties(item);
        // Get properties from /api/send_nomenclatures/.
        if (item is not PluStorageMethodFkModel pluStorageMethodFk) return;
        Plu = pluStorageMethodFk.Plu;
        Method = pluStorageMethodFk.Method;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluStorageMethodFkModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Plu.Equals(item.Plu) &&
        Method.Equals(item.Method);

    public new virtual PluStorageMethodFkModel CloneCast() => (PluStorageMethodFkModel)Clone();

    #endregion
}