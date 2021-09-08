// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Terra.Common
{
    public class TaskHelper
    {
        #region Design pattern "Lazy Singleton"

        private static TaskHelper _instance;
        public static TaskHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        //

        #endregion

        #region Constructor and destructor

        public TaskHelper() { Setup(); }

        public void Setup()
        {
            
        }

        #endregion

        #region Public and private methods

        public ContentResult RunTask(Task<ContentResult> task, 
            [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                Stopwatch sw = Stopwatch.StartNew();
                string msg = $"Method: {memberName} is started.";
                task?.Start();
                ContentResult result = task?.GetAwaiter().GetResult();
                sw.Stop();
                msg = $"Method: {memberName} is finished. Elapsed: {sw.Elapsed}.";
                return result;
            }
            catch (Exception ex)
            {
                string msg = $"Method: {memberName}. Line: {lineNumber}. {ex.Message}";
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    msg += Environment.NewLine + $"Method: {memberName}. Line: {lineNumber}. {ex.InnerException.Message}";
                }
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = @$"<html><body>{msg}</body></html>"
                };
            }
            //finally
            //{
            //    // Сборка мусора.
            //    GC.Collect();
            //}
        }

        #endregion

    }
}
