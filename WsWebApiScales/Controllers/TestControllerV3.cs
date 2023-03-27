// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Reflection;
using System.Runtime.CompilerServices;
using DataCore.Utils;
using WsLocalization.Utils;
using WsWebApi.Controllers;
using WsWebApi.Models;
using WsWebApi.Utils;

namespace WsWebApiScales.Controllers;

/// <summary>
/// Test controller v3.
/// </summary>
public class TestControllerV3 : WebControllerBase
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
    /// <param name="host"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route(UrlWebService.GetInfo)]
    public ContentResult GetInfo([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "is_debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "")
    {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = ControllerHelp.GetContentResult(() => 
            DataFormatUtils.GetContentResult<ServiceInfoModel>(
            WebResponseUtils.NewServiceInfo(Assembly.GetExecutingAssembly(), SessionFactory), format, HttpStatusCode.OK), format);
        ControllerHelp.LogWebServiceFk(nameof(WsWebApiScales), UrlWebService.GetInfo,
            requestStampDt, string.Empty, result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route(UrlWebService.GetException)]
    public ContentResult GetException([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "is_debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "",
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        ControllerHelp.GetContentResult(() => 
            DataFormatUtils.GetContentResult<ServiceExceptionModel>(
                new ServiceExceptionModel(filePath, lineNumber, memberName, "Test Exception!", "Test inner exception!"), 
                format, HttpStatusCode.InternalServerError), format);

    [AllowAnonymous]
    [HttpGet]
    [Route(UrlWebService.GetSimple)]
    public ContentResult GetSimple([FromQuery(Name = "format")] string format = "", [FromQuery(Name = "is_debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") =>
        new TestControllerV2(SessionFactory).GetSimple(format, isDebug);

    #endregion
}