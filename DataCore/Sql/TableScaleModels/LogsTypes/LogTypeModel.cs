// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels.LogsTypes;

/// <summary>
/// Table "LOG_TYPES".
/// </summary>
[Serializable]
public class LogTypeModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual byte Number { get; set; }
    [XmlElement] public virtual string Icon { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public LogTypeModel() : base(SqlFieldIdentityEnum.Uid)
    {
        Number = 0x00;
        Icon = string.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private LogTypeModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Number = info.GetByte(nameof(Number));
        Icon = info.GetString(nameof(Icon));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Number)}: {Number}. " +
        $"{nameof(Icon)}: {Icon}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((LogTypeModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Number, (byte)0x00) &&
        Equals(Icon, string.Empty);

    public override object Clone()
    {
        LogTypeModel item = new();
        item.Number = Number;
        item.Icon = Icon;
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
        info.AddValue(nameof(Number), Number);
        info.AddValue(nameof(Icon), Icon);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Icon = LocaleCore.Sql.SqlItemFieldIcon;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(LogTypeModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Number, item.Number) &&
        Equals(Icon, item.Icon);

    public new virtual LogTypeModel CloneCast() => (LogTypeModel)Clone();

    #endregion
}
