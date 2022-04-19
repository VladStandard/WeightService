// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace WebApiTerra1000.Common
{
    public class ControllerHelper
    {
        #region Design pattern "Lazy Singleton"

        private static ControllerHelper _instance;
        public static ControllerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        //

        #endregion

        #region Constructor and destructor

        public ControllerHelper() { }

        #endregion

        #region Public and private methods

        public ContentResult RunTask(Task<ContentResult> task, FormatType format,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                task?.Start();
                ContentResult result = task?.GetAwaiter().GetResult();
                return result;
            }
            catch (Exception ex)
            {
                filePath = Path.GetFileName(filePath);
                ServiceExceptionEntity serviceException = new(filePath, lineNumber, memberName, ex);
                return serviceException.GetResult(format, HttpStatusCode.OK);
            }
            finally
            {
                GC.Collect();
            }
        }
        
        #endregion

    }
}
