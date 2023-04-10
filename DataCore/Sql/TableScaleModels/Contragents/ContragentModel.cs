// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;

namespace DataCore.Sql.TableScaleModels.Contragents;

/// <summary>
/// Table "CONTRAGENTS_V2".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(ContragentModel)}")]
public class ContragentModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual string FullName { get; set; }
    [XmlElement] public virtual Guid IdRRef { get; set; }
    public virtual string IdRRefAsString { get => IdRRef.ToString(); set => IdRRef = Guid.Parse(value); }
    [XmlElement] public virtual int DwhId { get; set; }
    [XmlElement] public virtual string Xml { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public ContragentModel() : base(WsSqlFieldIdentity.Uid)
    {
        FullName = string.Empty;
        IdRRef = Guid.Empty;
        DwhId = 0;
        Xml = string.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected ContragentModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        FullName = info.GetString(nameof(FullName));
        IdRRef = (Guid)info.GetValue(nameof(IdRRef), typeof(Guid));
        DwhId = info.GetInt32(nameof(DwhId));
        Xml = info.GetString(nameof(Xml));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(DwhId)}: {DwhId}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ContragentModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(FullName, string.Empty) &&
        Equals(IdRRef, Guid.Empty) &&
        Equals(DwhId, 0) &&
        Equals(Xml, string.Empty);

    public override object Clone()
    {
        ContragentModel item = new();
        item.CloneSetup(base.CloneCast());
        item.FullName = FullName;
        item.IdRRef = IdRRef;
        item.DwhId = DwhId;
        item.Xml = Xml;
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
        info.AddValue(nameof(FullName), FullName);
        info.AddValue(nameof(IdRRef), IdRRef);
        info.AddValue(nameof(DwhId), DwhId);
        info.AddValue(nameof(Xml), Xml);
    }

    public override void FillProperties()
    {
        base.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(ContragentModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(FullName, item.FullName) &&
        Equals(IdRRef, item.IdRRef) &&
        Equals(DwhId, item.DwhId) &&
        Equals(Xml, item.Xml);

    public new virtual ContragentModel CloneCast() => (ContragentModel)Clone();

    #endregion
}
