// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Enums;
using DataCore.Sql.Tables;

namespace DataCore.Models;

[XmlRoot("ParseResult", Namespace = "", IsNullable = false)]
public class ParseResultModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    [XmlAttribute] public virtual ParseStatus Status { get; set; }
    [XmlAttribute] public virtual string Message { get; set; }
    [XmlAttribute] public virtual string Exception { get; set; }
    [XmlAttribute] public virtual string InnerException { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public ParseResultModel() : base(SqlFieldIdentityEnum.Uid)
    {
        Status = ParseStatus.Unknown;
        Message = string.Empty;
        Exception = string.Empty;
        InnerException = string.Empty;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private ParseResultModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Status = (ParseStatus)info.GetValue(nameof(Status), typeof(ParseStatus));
        Message = info.GetString(nameof(Message));
        Exception = info.GetString(nameof(Exception));
        InnerException = info.GetString(nameof(InnerException));
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
        $"{nameof(Message)}: {Message}. " + 
        $"{nameof(Exception)}: {Exception}. " + 
        $"{nameof(InnerException)}: {InnerException}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ParseResultModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault()
    {
        bool foo1 = base.EqualsDefault();
        bool foo2 = Equals(Status, ParseStatus.Unknown);
        bool foo3 = Equals(Message, string.Empty);
        bool foo4 = Equals(Exception, string.Empty);
        bool foo5 = Equals(InnerException, string.Empty);
        return foo1 && foo2 && foo3 && foo4;
    }

    public override object Clone()
    {
        ParseResultModel item = new();
        item.Status = Status;
        item.Message = Message;
        item.Exception = Exception;
        item.InnerException = InnerException;
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
        info.AddValue(nameof(InnerException), InnerException);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Message = LocaleCore.Sql.SqlItemFieldMessage;
        Exception = LocaleCore.Sql.SqlItemFieldException;
        InnerException = LocaleCore.Sql.SqlItemFieldInnerException;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(ParseResultModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Status, item.Status) &&
        Equals(Message, item.Message) &&
        Equals(Exception, item.Exception) &&
        Equals(InnerException, item.InnerException);

    public new virtual ParseResultModel CloneCast() => (ParseResultModel)Clone();

    #endregion
}
