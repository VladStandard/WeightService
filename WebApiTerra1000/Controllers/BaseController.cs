// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NHibernate;
using WebApiTerra1000.Common;

namespace WebApiTerra1000.Controllers
{
    [Authorize]
    [ApiController]
    public class BaseController : Controller
    {
        #region Public and private fields and properties

        public TaskHelper TaskHelper = TaskHelper.Instance;
        public readonly ILogger<BaseController> Logger;
        public readonly ISessionFactory SessionFactory;

        #endregion

        #region Constructor and destructor

        public BaseController(ILogger<BaseController> logger, ISessionFactory sessionFactory)
        {
            Logger = logger;
            SessionFactory = sessionFactory;
        }

        #endregion
    }
}
