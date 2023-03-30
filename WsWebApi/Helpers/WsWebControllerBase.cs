// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApi.Helpers;

/// <summary>
/// Base controller.
/// </summary>
[ApiController]
public class WsWebControllerBase : ControllerBase // ApiController
{
    #region Public and private fields and properties

    /// <summary>
    /// Controller helper.
    /// </summary>
    protected WsControllerHelper ControllerHelp { get; } = WsControllerHelper.Instance;

    /// <summary>
    /// AppVersion helper.
    /// </summary>
    protected AppVersionHelper AppVersion { get; } = AppVersionHelper.Instance;
    /// <summary>
    /// NHibernate session.
    /// </summary>
    protected ISessionFactory SessionFactory { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    public WsWebControllerBase(ISessionFactory sessionFactory)
    {
        SessionFactory = sessionFactory;
    }

    /// <summary>
    /// Get AcceptVersion from string value.
    /// </summary>
    /// <returns></returns>
    protected AcceptVersion GetAcceptVersion(string value) =>
        value.ToUpper() switch
        {
            "V2" => AcceptVersion.V2,
            "V3" => AcceptVersion.V3,
            "*/*" or _ => AcceptVersion.V1
        };

    #endregion
}