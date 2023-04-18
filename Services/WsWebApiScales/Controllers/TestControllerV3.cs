// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Reflection;
using System.Runtime.CompilerServices;
using WsWebApiCore.Models;

namespace WsWebApiScales.Controllers;

/// <summary>
/// Test controller v3.
/// </summary>
[Tags(WsWebServiceConsts.Tests)]
public sealed class TestControllerV3 : WsControllerBase
{
    #region Public and private fields and properties

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    public TestControllerV3(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Get info.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <param name="host"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route(WsWebServiceUrls.GetInfo)]
    public ContentResult GetInfo([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "")
    {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = GetContentResult(() => 
            WsDataFormatUtils.GetContentResult<WsServiceInfoModel>(
            WsWebResponseUtils.NewServiceInfo(Assembly.GetExecutingAssembly(), SessionFactory), format, HttpStatusCode.OK), format);
        LogWebServiceFk(nameof(WsWebApiScales), WsWebServiceUrls.GetInfo,
            requestStampDt, string.Empty, result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route(WsWebServiceUrls.GetException)]
    public ContentResult GetException([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "",
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        GetContentResult(() => 
            WsDataFormatUtils.GetContentResult<WsServiceExceptionModel>(
                new WsServiceExceptionModel(filePath, lineNumber, memberName, "Test Exception!", "Test inner exception!"), 
                format, HttpStatusCode.InternalServerError), format);

    [AllowAnonymous]
    [HttpGet]
    [Route(WsWebServiceUrls.GetSimple)]
    public ContentResult GetSimple([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") =>
        new WsTestV2Helper(SessionFactory).GetSimple(format, isDebug);

    #endregion
}