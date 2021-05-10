// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using log4net;
using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using WeightServices.Common;
// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo
// ReSharper disable CommentTypo

namespace ZabbixAgentLib
{

    public class ZabbixHttpListener
    {
        #region Public/Private fields and properties

        private readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly object _locker = new object();
        private CancellationToken _token;
        private readonly int _threadTimeOut;
        private HttpListener _listener;
        private Thread _thread;

        public HealthDataCollectorStatus DataCollectorStatus { get; set; }

        public HealthDataCollectorZebra DataCollectorZebra { get; set; }

        public HealthDataCollectorMassa DataCollectorMassa { get; set; }

        public HealthDataCollectorSqlCon DataCollectorSqlCon { get; set; }

        private StringBuilder _sbAccessUrl;

        #endregion

        #region Constructor and destructor

        [Obsolete(@"Используй второй конструктор")]
        public ZabbixHttpListener(Func<string> funcResponseStatus, CancellationToken token, int threadTimeOut) : this(token, threadTimeOut)
        {
            //
        }

        public ZabbixHttpListener(CancellationToken token, int threadTimeOut)
        {
            _token = token;
            _threadTimeOut = threadTimeOut;
            DataCollectorStatus = new HealthDataCollectorStatus();
            DataCollectorZebra = new HealthDataCollectorZebra();
            DataCollectorMassa = new HealthDataCollectorMassa();
            DataCollectorSqlCon = new HealthDataCollectorSqlCon();
            // Запустить.
            Start();
        }

        ~ZabbixHttpListener()
        {
            Stop();
        }

        #endregion

        #region Public and private methods

        public void Start()
        {
            if (_thread != null)
                return;
            if (_listener != null)
            {
                if (!_listener.IsListening)
                    _listener.Start();
                return;
            }

            try
            {
                _thread = new Thread(t =>
                {
                    using (_listener = new HttpListener())
                    {
                        if (CheckHttpAccessAll(_listener))
                        {
                            _listener.Start();
                            while (!_token.IsCancellationRequested)
                            {
                                lock (_locker)
                                {
                                    try
                                    {
                                        var context = _listener.GetContext();
                                        var request = context.Request;
                                        var methodName = request.Url.Segments[1].Replace("/", "");
                                        StartCommented();
                                        SetResponse(context, methodName);
                                    }
                                    catch (Exception ex)
                                    {
                                        _log.Error(ex.Message);
                                    }
                                }
                                Thread.Sleep(_threadTimeOut);
                            }
                            _listener.Stop();
                        }
                    }
                }
                )
                { IsBackground = true };
                _thread.Start();

            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                _log.Error(ex.StackTrace);
                _log.Error(ex.InnerException);
                _log.Error(ex.Source);
            }
        }

        private void StartCommented()
        {
            //////if (request.HasEntityBody)
            //////{
            //////    Encoding encoding = request.ContentEncoding;
            //////    long ContentLength = request.ContentLength64;
            //////    using (Stream bodyStream = request.InputStream)
            //////    {
            //////        using (StreamReader streamReader = new StreamReader(bodyStream, encoding))
            //////        {
            //////            documentContents = streamReader.ReadToEnd();
            //////            // тут обработать информацию если SOAP
            //////        }
            //////    }
            //////    EventLog.WriteEntry("In Action. GET start " + documentContents, EventLogEntryType.Information, eventId++);
            //////}
            //////if (request.HttpMethod == "POST")
            //////{
            //////    // тут надо получать параметры из формы
            //////    responseString = "<html><body><h1>It works! POST</h1></body></html>";
            //////}
            //////else if (request.HttpMethod == "GET")
            //////{
            //////    StringBuilder sb = new StringBuilder();
            //////    foreach (String key in request.QueryString.AllKeys)
            //////    {
            //////        sb.Append("Key: " + key + " Value: " + request.QueryString[key] + "</br>");
            //////    }
            //////    responseString = String.Format("<html><body><h1>It works! GET </br>{0}</br></h1></body></html>", sb.ToString());
            //////    EventLog.WriteEntry("In Action. GET stop " + responseString, EventLogEntryType.Information, eventId++);
            //////}
            //////else
            //////{
            //////    responseString = "<html><body><h1>It works! ANY</h1></body></html>";
            //////}
        }

        /// <summary>
        /// Подстрока http-ответа.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private string GetHttpSubString(StringBuilder response)
        {
            var result = string.Empty;
            foreach (var item in response.ToString().Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(item))
                    result += $@"        <div>{item}</div>" + Environment.NewLine;
            }
            return result.TrimEnd('\r', ' ', '\n');
        }

        /// <summary>
        /// Строка http-ответа.
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private string GetHttpString(string methodName)
        {
            var result = "<html><head><meta charset='utf8'></head><body>Ошибка запроса!</body></html>";
            switch (methodName)
            {
                case @"status":
                    result = $@"
<html>
    <head>
        <meta charset = 'utf8'>
    </head>
    <body>
{GetHttpSubString(DataCollectorStatus.Response())}
    </body>
</html>".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n');
                    break;
                case @"zebra":
                    result = $@"
<html>
    <head>
        <meta charset = 'utf8'>
    </head>
    <body>
{GetHttpSubString(DataCollectorZebra.Response())}
    </body>
</html>".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n');
                    break;
                case @"massa":
                    result = $@"
<html>
    <head>
        <meta charset = 'utf8'>
    </head>
    <body>
{GetHttpSubString(DataCollectorMassa.Response())}
    </body>
</html>".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n');
                    break;
                case @"sql":
                    result = $@"
<html>
    <head>
        <meta charset = 'utf8'>
    </head>
    <body>
{GetHttpSubString(DataCollectorSqlCon.Response())}
    </body>
</html>".TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n');
                    break;
            }
            return result;
        }

        /// <summary>
        /// Http-ответ.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="methodName"></param>
        private void SetResponse(HttpListenerContext context, string methodName)
        {
            var httpString = GetHttpString(methodName);
            var response = context.Response;
            response.StatusCode = 200;
            //response.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            //response.AddHeader("Content-Type", "text/csv");
            response.AddHeader("Content-Type", "text/html");
            var buffer = Encoding.UTF8.GetBytes(httpString);
            response.ContentLength64 = buffer.Length;
            //var output = response.OutputStream;
            //output.Write(buffer, 0, buffer.Length);
            //output.Close();
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        }

        public void Stop()
        {
            try
            {
                if (_thread != null && _thread.IsAlive)
                {
                    if (_listener != null && _listener.IsListening)
                    {
                        _token.ThrowIfCancellationRequested();
                        Thread.Sleep(1000);
                    }
                    _thread.Join(1000);
                    _thread.Abort();
                    _thread = null;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                _log.Error(ex.StackTrace);
                _log.Error(ex.InnerException);
                _log.Error(ex.Source);
            }
        }

        #endregion

        #region Public and private methods - Http access

        /// <summary>
        /// Проверить права доступа url.
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool CheckHttpAccessUrl(HttpListener listener, string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;
            // netsh http add urlacl url="http://+:18086/status" user=Все
            // netsh http add urlacl url="http://+:18086/zebra" user=Все
            // netsh http add urlacl url="http://+:18086/massa" user=Все

            if (_sbAccessUrl == null || _sbAccessUrl.Length == 0)
            {
                using (var process = new Process
                {
                    //StartInfo = new ProcessStartInfo(@"netsh", $@"http show urlacl ""{url}""")
                    StartInfo = new ProcessStartInfo(@"netsh", $@"http show urlacl")
                    {
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                    },
                })
                {
                    process.Start();
                    _sbAccessUrl = new StringBuilder();
                    while (!process.StandardOutput.EndOfStream)
                    {
                        _sbAccessUrl.AppendLine(process.StandardOutput.ReadLine());
                    }
                    process.WaitForExit(3_0000);
                }
            }
            foreach (var str in _sbAccessUrl.ToString().Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.None))
            {
                if (str.Contains(url))
                {
                    if (listener != null)
                    {
                        if (!listener.Prefixes.Contains(url))
                            listener.Prefixes.Add(url);
                    }
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Проверить права доступа url.
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="httpAccess"></param>
        /// <returns></returns>
        public bool CheckHttpAccess(HttpListener listener, EnumCheckHttpAccess httpAccess)
        {
            switch (httpAccess)
            {
                case EnumCheckHttpAccess.Status:
                    return CheckHttpAccessUrl(listener, @"http://+:18086/status/");
                case EnumCheckHttpAccess.Zebra:
                    return CheckHttpAccessUrl(listener, @"http://+:18086/zebra/");
                case EnumCheckHttpAccess.Massa:
                    return CheckHttpAccessUrl(listener, @"http://+:18086/massa/");
                case EnumCheckHttpAccess.SqlCon:
                    return CheckHttpAccessUrl(listener, @"http://+:18086/sql/");
                default:
                    throw new ArgumentOutOfRangeException(nameof(httpAccess), httpAccess, null);
            }
        }

        /// <summary>
        /// Проверить права доступа всех url.
        /// </summary>
        /// <param name="listener"></param>
        /// <param name="httpAccess"></param>
        /// <returns></returns>
        public bool CheckHttpAccessAll(HttpListener listener)
        {
            var result = CheckHttpAccess(listener, EnumCheckHttpAccess.Status);
            if (CheckHttpAccess(listener, EnumCheckHttpAccess.Zebra))
                result = true;
            if (CheckHttpAccess(listener, EnumCheckHttpAccess.Massa))
                result = true;
            if (CheckHttpAccess(listener, EnumCheckHttpAccess.SqlCon))
                result = true;
            return result;
        }

        #endregion
    }
}
