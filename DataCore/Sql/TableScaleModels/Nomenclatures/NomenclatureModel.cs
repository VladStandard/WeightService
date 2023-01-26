// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels.Nomenclatures;

/// <summary>
/// Table "Nomenclature".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(NomenclatureModel)} | Code = {Code}")]
[Obsolete(@"Use PluModel")]
public class NomenclatureModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual string Code { get; set; }
    [XmlElement(IsNullable = true)] public virtual string? Xml { get; set; }
   
    /// <summary>
    /// Is weighted or pcs.
    /// </summary>
    [XmlElement] public virtual bool Weighted { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public NomenclatureModel() : base(SqlFieldIdentityEnum.Id)
    {
        Code = string.Empty;
        Xml = string.Empty;
        Weighted = false;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected NomenclatureModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        IdentityValueUid = Guid.Parse(info.GetString(nameof(IdentityValueUid)));
        Code = info.GetString(nameof(Code));
        Xml = (string?)info.GetValue(nameof(Xml), typeof(string));
        Weighted = info.GetBoolean(nameof(Weighted));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IdentityValueUid)}: {IdentityValueUid}  ." +
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Code)}: {Code}. " +
        $"{nameof(Xml)}.Length: {Xml?.Length ?? 0}. " +
        $"{nameof(Weighted)}: {Weighted}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((NomenclatureModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public new virtual bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Code, string.Empty) &&
        Equals(Xml, string.Empty) &&
        Equals(Weighted, false);

    public override object Clone()
    {
        NomenclatureModel item = new();
        item.Code = Code;
        item.Xml = Xml;
        item.Weighted = Weighted;
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
        info.AddValue(nameof(IdentityValueUid), IdentityValueUid);
        info.AddValue(nameof(Code), Code);
        info.AddValue(nameof(Xml), Xml);
        info.AddValue(nameof(Weighted), Weighted);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Code = LocaleCore.Sql.SqlItemFieldCode;
        Xml = LocaleCore.Sql.SqlItemFieldProductXml;
        Weighted = false;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(NomenclatureModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Code, item.Code) &&
        Equals(Xml, item.Xml) &&
        Equals(Weighted, item.Weighted);

    public new virtual NomenclatureModel CloneCast() => (NomenclatureModel)Clone();

    #endregion
}