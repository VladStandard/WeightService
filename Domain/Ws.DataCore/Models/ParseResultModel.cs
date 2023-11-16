using Ws.DataCore.Serialization;

namespace Ws.DataCore.Models;

[XmlRoot("ParseResult", Namespace = "", IsNullable = false)]
[Serializable]
public sealed class ParseResultModel : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlAttribute] public EnumParseStatus Status { get; set; }
    [XmlAttribute] public string Message { get; set; }
    [XmlAttribute] public string Exception { get; set; }
    [XmlAttribute] public string InnerException { get; set; }
    
    [XmlIgnore] public bool IsStatusSuccess => Equals(Status, EnumParseStatus.Success);
    [XmlIgnore] public bool IsStatusError => Equals(Status, EnumParseStatus.Error);

    public ParseResultModel()
    {
        Status = EnumParseStatus.Unknown;
        Message = string.Empty;
        Exception = string.Empty;
        InnerException = string.Empty;
    }

    private ParseResultModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Status = (EnumParseStatus)info.GetValue(nameof(Status), typeof(EnumParseStatus));
        Message = info.GetString(nameof(Message));
        Exception = info.GetString(nameof(Exception));
        InnerException = info.GetString(nameof(InnerException));
    }

    public ParseResultModel(ParseResultModel item)
    {
        Status = item.Status;
        Message = item.Message;
        Exception = item.Exception;
        InnerException = item.InnerException;

    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => string.IsNullOrEmpty(Exception) 
        ? $"{nameof(Status)}: {Status}. {nameof(Message)}: {Message}"
        : string.IsNullOrEmpty($"{nameof(InnerException)}: {InnerException}. ") 
            ? $"{nameof(Status)}: {Status}. {nameof(Message)}: {Message}. " +
              $"{nameof(Exception)}: {Exception}."
            : $"{nameof(Status)}: {Status}. {nameof(Message)}: {Message}. " +
              $"{nameof(Exception)}: {Exception}. {nameof(InnerException)}: {InnerException}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ParseResultModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public bool EqualsNew() => Equals(new());

    public bool EqualsDefault() =>
        Equals(Status, EnumParseStatus.Unknown) &&
        Equals(Message, string.Empty) &&
        Equals(Exception, string.Empty) &&
        Equals(InnerException, string.Empty);

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Status), Status);
        info.AddValue(nameof(Message), Message);
        info.AddValue(nameof(Exception), Exception);
        info.AddValue(nameof(InnerException), InnerException);
    }

    public void FillProperties()
    {
        Message = LocaleCore.Sql.SqlItemFieldMessage;
        Exception = LocaleCore.Sql.SqlItemFieldException;
        InnerException = LocaleCore.Sql.SqlItemFieldInnerException;
    }

    #endregion

    #region Public and private methods - virtual

    public bool Equals(ParseResultModel item) =>
        ReferenceEquals(this, item) || 
        Equals(Status, item.Status) &&
        Equals(Message, item.Message) &&
        Equals(Exception, item.Exception) &&
        Equals(InnerException, item.InnerException);

    #endregion
}