// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WeightCore.Managers
{
    internal class TaskManagerHelperDeprecated
    {
        //private void StartHttpListener()
        //{
        //    _logUtils.Information("Запустить http-listener. начало.");
        //    _logUtils.Information("http://localhost:18086/status");
        //    try
        //    {
        //    //    CancellationTokenSource cancelTokenSource = new();
        //    //    _token = cancelTokenSource.Token;
        //    //    _threadChecker = new ThreadChecker(_token, 2_500);
        //    //    // Подписка на событие.
        //    //    //_threadChecker.EventReloadValues += EventHttpListenerReloadValues;
        //    //    _tokenHttpListener = cancelTokenSource.Token;
        //    //    HttpListener = new ZabbixHttpListener(_tokenHttpListener, 10);
        //    }
        //    catch (Exception ex)
        //    {
        //    //    _sessionState?.Log.SaveError(filePath, lineNumber, memberName, ex.Message);
        //    //    if (ex.InnerException != null)
        //    //        _sessionState?.Log.SaveError(filePath, lineNumber, memberName, ex.InnerException.Message);
        //    //    string msg = ex.Message;
        //    //    if (ex.InnerException != null)
        //    //        msg += Environment.NewLine + ex.InnerException.Message;
        //    //    CustomMessageBox.Show(this, StartHttpListener + Environment.NewLine + msg, Messages.Exception);
        //    }
        //    //catch (Exception ex)
        //    //{
        //    //    Log.SaveError(filePath, lineNumber, memberName, ex.Message);
        //    //    if (ex.InnerException != null)
        //    //        Log.SaveError(filePath, lineNumber, memberName, ex.InnerException.Message);
        //    //    _log.Error(ex.Message);
        //    //}
        //    //_logUtils.Information("Запистить http-listener. Финиш.");
        //}

        //private void StopHttpListener([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0,
        //    [CallerMemberName] string memberName = "")
        //{
        //    //try
        //    //{
        //    //    _log?.Info("Остановить http-listener. Начало.");
        //    //    HttpListener?.Stop();
        //    //    _token.ThrowIfCancellationRequested();
        //    //    _tokenHttpListener.ThrowIfCancellationRequested();
        //    //    _threadChecker?.Stop();
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Log.SaveError(filePath, lineNumber, memberName, ex.Message);
        //    //    if (ex.InnerException != null)
        //    //        Log.SaveError(filePath, lineNumber, memberName, ex.InnerException.Message);
        //    //    _log?.Error(ex.Message);
        //    //}
        //    //_log?.Info("Остановить http-listener. Финиш.");
        //}
    }
}
