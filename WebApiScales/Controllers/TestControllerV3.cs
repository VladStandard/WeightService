// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System.Reflection;
using WebApiCore.Controllers;

namespace WebApiScales.Controllers;

/// <summary>
/// Test controller v3.
/// </summary>
public class TestControllerV3 : BaseController
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
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v3/info/")]
    public ContentResult GetInfo(FormatTypeEnum format = FormatTypeEnum.Xml) =>
        new TestControllerV2(SessionFactory).GetInfo(Assembly.GetExecutingAssembly(), format);

    #endregion
}
