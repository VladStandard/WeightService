// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Serialization.Models;

namespace WsWebApi.Models;

[XmlRoot(WebConstants.Exception, Namespace = "", IsNullable = false)]
public class WsServiceExceptionModel : SerializeBase
{
    #region Public and private fields and properties

    public string FilePath { get; set; }
    public int LineNumber { get; set; }
    public string MemberName { get; set; }
    public string Exception { get; set; }
    public string InnerException { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsServiceExceptionModel()
    {
        FilePath = string.Empty;
        LineNumber = 0;
        MemberName = string.Empty;
        Exception = string.Empty;
        InnerException = string.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="lineNumber"></param>
    /// <param name="memberName"></param>
    /// <param name="exception"></param>
    /// <param name="innerException"></param>
    public WsServiceExceptionModel(string filePath, int lineNumber, string memberName, string exception, string innerException) : this()
    {
        FilePath = filePath;
        LineNumber = lineNumber;
        MemberName = memberName;
        Exception = exception;
        InnerException = innerException;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="lineNumber"></param>
    /// <param name="memberName"></param>
    /// <param name="ex"></param>
    public WsServiceExceptionModel(string filePath, int lineNumber, string memberName, Exception ex) :
        this(filePath, lineNumber, memberName, ex.Message, 
        ex.InnerException is not null ? ex.InnerException.Message : string.Empty) { }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private WsServiceExceptionModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        FilePath = info.GetString(nameof(FilePath)) ?? string.Empty;
        LineNumber = info.GetInt32(nameof(LineNumber));
        MemberName = info.GetString(nameof(MemberName)) ?? string.Empty;
        Exception = info.GetString(nameof(Exception)) ?? string.Empty;
        InnerException = info.GetString(nameof(InnerException)) ?? string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
        @$"{nameof(FilePath)}: {FilePath}. " + Environment.NewLine +
        @$"{nameof(LineNumber)}: {LineNumber}. " + Environment.NewLine +
        @$"{nameof(MemberName)}: {MemberName}. " + Environment.NewLine +
        @$"{nameof(Exception)}: {Exception}. " + Environment.NewLine +
        @$"{nameof(InnerException)}: {InnerException}. ";

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(FilePath), FilePath);
        info.AddValue(nameof(LineNumber), LineNumber);
        info.AddValue(nameof(MemberName), MemberName);
        info.AddValue(nameof(Exception), Exception);
        info.AddValue(nameof(InnerException), InnerException);
    }

    #endregion
}