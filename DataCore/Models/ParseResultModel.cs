// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using NHibernate.Engine;

namespace DataCore.Models;

public enum ParseStatusEnum
{
    Unknown,
    Success,
    Error,
}

[XmlRoot("ParseResult", Namespace = "", IsNullable = false)]
public class ParseResultModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    [XmlAttribute] public virtual ParseStatusEnum Status { get; set; }
    [XmlAttribute] public virtual string Message { get; set; }
    [XmlAttribute] public virtual Exception Exception { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public ParseResultModel() : base(SqlFieldIdentityEnum.Uid)
    {
        Status = ParseStatusEnum.Unknown;
        Message = string.Empty;
        Exception = new Exception();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private ParseResultModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Status = (ParseStatusEnum)info.GetValue(nameof(Status), typeof(ParseStatusEnum));
        Message = info.GetString(nameof(Message));
        Exception = (Exception)info.GetValue(nameof(Exception), typeof(Exception));
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// To string.
    /// </summary>
    /// <returns></returns>
    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Status)}: {Status}. " + 
        $"{nameof(Message)}: {Message}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ParseResultModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Status, ParseStatusEnum.Unknown) &&
        Equals(Message, string.Empty) &&
        Equals(Exception, new Exception());

    public override object Clone()
    {
        ParseResultModel item = new();
        item.Status = Status;
        item.Message = Message;
        item.Exception = Exception;
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
        info.AddValue(nameof(Status), Status);
        info.AddValue(nameof(Message), Message);
        info.AddValue(nameof(Exception), Exception);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Message = LocaleCore.Sql.SqlItemFieldMessage;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(ParseResultModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Status, item.Status) &&
        Equals(Message, item.Message) &&
        Equals(Exception, item.Exception);

    public new virtual ParseResultModel CloneCast() => (ParseResultModel)Clone();

    #endregion
}
