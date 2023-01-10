// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DataCore.Sql.Models;

[Serializable]
public class SerializeBase : ISerializable
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Contrsuctor.
	/// </summary>
	public SerializeBase()
	{
		//
	}

    #endregion

    #region Public and private methods - ISerializable

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	protected SerializeBase(SerializationInfo info, StreamingContext context)
	{
		//
	}

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		//
	}

    #endregion

    #region Public and private methods - Obsolete

    [Obsolete(@"Use DataFormatUtils")]
    public virtual string SerializeAsXmlString<T>(bool isAddEmptyNamespace, bool isUtf16) => DataFormatUtils.SerializeAsXmlString<T>(this, isAddEmptyNamespace, isUtf16);

    [Obsolete(@"Use DataFormatUtils")]
    public virtual string SerializeAsText() => DataFormatUtils.SerializeAsText<string>(this);

	[Obsolete(@"Use DataFormatUtils")]
	public virtual XmlDocument SerializeAsXmlDocument<T>(bool isAddEmptyNamespace, bool isUtf16) => DataFormatUtils.SerializeAsXmlDocument<T>(this, isAddEmptyNamespace, isUtf16);

    [Obsolete(@"Use DataFormatUtils")]
    public virtual T DeserializeFromXml<T>(string xml) => DataFormatUtils.DeserializeFromXml<T>(xml);

	[Obsolete(@"Use DataFormatUtils")]
	public virtual T DeserializeFromXml<T>(string xml, Encoding encoding) => DataFormatUtils.DeserializeFromXml<T>(xml, encoding);

    [Obsolete(@"Use DataFormatUtils")]
    public virtual ContentResult GetContentResultCore(FormatType formatType, object content, HttpStatusCode statusCode) => DataFormatUtils.GetContentResultCore(formatType, content, statusCode);

    [Obsolete(@"Use DataFormatUtils")]
    public virtual ContentResult GetContentResultCore(string formatString, object content, HttpStatusCode statusCode) => DataFormatUtils.GetContentResultCore(formatString, content, statusCode);

    [Obsolete(@"Use DataFormatUtils")]
    public virtual ContentResult GetContentResult(FormatType formatType, object content, HttpStatusCode statusCode) => DataFormatUtils.GetContentResultCore(formatType, content, statusCode);

    [Obsolete(@"Use DataFormatUtils")]
    public virtual ContentResult GetContentResult<T>(string formatString, HttpStatusCode statusCode) => DataFormatUtils.GetContentResult<T>(this, formatString, statusCode);

    #endregion
}