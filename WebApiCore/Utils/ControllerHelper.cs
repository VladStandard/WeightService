// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Net;
using System.Runtime.CompilerServices;
using DataCore.Models;
using Microsoft.AspNetCore.Mvc;
using WebApiCore.Common;

namespace WebApiCore.Utils;

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

    public ContentResult RunTask(Task<ContentResult> task, FormatTypeEnum format,
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
