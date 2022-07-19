// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NHibernate;
using WebApiCore.Utils;

namespace WebApiCore.Controllers;

/// <summary>
/// Base controller.
/// </summary>
[ApiController]
public class BaseController : ControllerBase
{
    #region Public and private fields and properties

    /// <summary>
    /// Controller helper.
    /// </summary>
    protected ControllerHelper ControllerHelp { get; } = ControllerHelper.Instance;

    /// <summary>
    /// AppVersion helper.
    /// </summary>
    protected AppVersionHelper AppVersion { get; } = AppVersionHelper.Instance;
    /// <summary>
    /// Microsoft Logger.
    /// </summary>
    public ILogger<BaseController> Logger { get; }
    /// <summary>
    /// NHibernate session.
    /// </summary>
    protected ISessionFactory SessionFactory { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="sessionFactory"></param>
    public BaseController(ILogger<BaseController> logger, ISessionFactory sessionFactory)
    {
        Logger = logger;
        SessionFactory = sessionFactory;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    public BaseController(ISessionFactory sessionFactory)
    {
        SessionFactory = sessionFactory;
    }

    #endregion
}
