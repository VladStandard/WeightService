// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Mvc;

namespace DataCore.Sql.Models;

[Serializable]
public class SerializeBase : ISerializable
{
    #region Public and private methods - ISerializable

    /// <summary>
    /// Default constructor.
    /// </summary>
    public SerializeBase()
    {
        
    }

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
    public virtual ContentResult GetContentResult<T>(string formatString, HttpStatusCode statusCode) => 
        DataFormatUtils.GetContentResult<T>(this, formatString, statusCode);

    #endregion
}