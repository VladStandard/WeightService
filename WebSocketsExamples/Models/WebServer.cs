// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Ninja.WebSockets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPF.Utils;

// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
// ReSharper disable CommentTypo

namespace WebSocketsExamples.Models
{
    public class WebServer : IDisposable
    {
        #region Public and private fields and properties

        private TcpListener _listener;
        private bool Disposed { get; set; }
        private readonly IWebSocketServerFactory _webSocketServerFactory;
        private LogHelper Log { get; set; } = LogHelper.Instance;
        private readonly HashSet<string> _supportedSubProtocols;
        const int BUFFER_SIZE = 4 * 1024 * 1024; // 4MB
        public bool Exit { get; private set; }
        private readonly object _locker = new();

        #endregion

        #region Constructor and destructor

        public WebServer(IWebSocketServerFactory webSocketServerFactory, IList<string> supportedSubProtocols = null)
        {
            _webSocketServerFactory = webSocketServerFactory;
            _supportedSubProtocols = new HashSet<string>(supportedSubProtocols ?? new string[0]);
        }

        ~WebServer()
        {
            Dispose();
        }

        private void DisposeManagedResources()
        {
            //
        }

        private void DisposeUnmanagedResources()
        {
            try
            {
                if (_listener != null)
                {
                    _listener.Server?.Close();
                    _listener.Stop();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            Log.Info("Web-server disposed.");
        }

        public void Dispose()
        {
            lock (_locker)
            {
                if (!Disposed)
                {
                    // Releasing managed resources
                    DisposeManagedResources();

                    // Releasing unmanaged resources
                    DisposeUnmanagedResources();

                    // Resource release flag
                    Disposed = true;
                }

                // Disable the garbage collector from calling the destructor
                GC.SuppressFinalize(this);
            }
        }

        private void CheckIfDisposed([CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            if (Disposed)
            {
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                var msg = $@"File {fileName}. Member {memberName}. Object has been disposed off!";
                Log.Info(msg);
                throw new ObjectDisposedException(msg);
            }
        }

        #endregion

        #region Public and private methods

        private string GetSubProtocol(ICollection<string> protocols)
        {
            foreach (var protocol in protocols)
            {
                if (_supportedSubProtocols.Contains(protocol))
                {
                    Log.Info($"Http header has requested sub protocol {protocol} which is supported");
                    return protocol;
                }
            }

            if (protocols.Count > 0)
            {
                Log.Warn($"Http header has requested the following sub protocols: {string.Join(", ", protocols)}. There are no supported protocols configured that match.");
            }

            return null;
        }

        private async Task ProcessTcpClientAsync(TcpClient tcpClient, TextBox textBox)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            try
            {
                CheckIfDisposed();

                // this worker thread stays alive until either of the following happens:
                // Client sends a close conection request OR
                // An unhandled exception is thrown OR
                // The server is disposed
                InvokeTextBox.AddTextFormat(textBox, "Connection opened. Reading Http header from stream");

                // get a secure or insecure stream
                WebSocketHttpContext context;
                using (Stream stream = tcpClient.GetStream())
                {
                    context = await _webSocketServerFactory.ReadHttpHeaderFromStreamAsync(stream, source.Token);
                }

                if (!(context is null))
                {
                    if (context.IsWebSocketRequest)
                    {
                        string subProtocol = GetSubProtocol(context.WebSocketRequestedProtocols);
                        var options = new WebSocketServerOptions()
                            {KeepAliveInterval = TimeSpan.FromSeconds(30), SubProtocol = subProtocol};
                        InvokeTextBox.AddTextFormat(textBox, "Http header has requested an upgrade to Web Socket protocol. Negotiating Web Socket handshake.");
                        WebSocket webSocket = await _webSocketServerFactory.AcceptWebSocketAsync(context, options, source.Token);
                        InvokeTextBox.AddTextFormat(textBox, "Web Socket handshake response sent. Stream ready.");
                        await RespondToWebSocketRequestAsync(webSocket, source.Token);
                    }
                    else
                    {
                        InvokeTextBox.AddTextFormat(textBox, "Http header contains no web socket upgrade request. Ignoring.");
                    }
                }
                else
                {
                    Log.Error("ReadHttpHeaderFromStreamAsync is null!");
                }

                InvokeTextBox.AddTextFormat(textBox, "Connection closed.");
            }
            catch (ObjectDisposedException exo)
            {
                InvokeTextBox.AddTextFormat(textBox, $"ObjectDisposedException: {exo.Message}");
                // do nothing. This will be thrown if the Listener has been stopped
            }
            catch (Exception ex)
            {
                InvokeTextBox.AddTextFormat(textBox, $"Exception: {ex.Message}");
            }
            finally
            {
                try
                {
                    tcpClient.Client.Close();
                    tcpClient.Close();
                    source.Cancel();
                }
                catch (Exception ex)
                {
                    InvokeTextBox.AddTextFormat(textBox, $"Failed to close TCP connection: {ex}");
                }
            }
        }

        public async Task RespondToWebSocketRequestAsync(WebSocket webSocket, CancellationToken token)
        {
            ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[BUFFER_SIZE]);

            while (true)
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(buffer, token);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    Log.Info($"Client initiated close. Status: {result.CloseStatus} Description: {result.CloseStatusDescription}");
                    break;
                }

                if (result.Count > BUFFER_SIZE)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.MessageTooBig,
                        $"Web socket frame cannot exceed buffer size of {BUFFER_SIZE:#,##0} bytes. Send multiple frames instead.",
                        token);
                    break;
                }

                // just echo the message back to the client
                //ArraySegment<byte> toSend = new ArraySegment<byte>(buffer.Array, buffer.Offset, result.Count);
                ArraySegment<byte> toSend = new ArraySegment<byte>(buffer.Array ?? Array.Empty<byte>(), buffer.Offset, result.Count);
                await webSocket.SendAsync(toSend, WebSocketMessageType.Binary, true, token);
            }
        }

        public async Task Open(int port, TextBox textBox)
        {
            try
            {
                //IPAddress localAddress = IPAddress.Any;
                IPAddress localAddress = IPAddress.Parse("127.0.0.1");
                Log.Info($@"IPAddress = [{localAddress}]");
                _listener = new TcpListener(localAddress, port);
                _listener.Start();
                InvokeTextBox.AddTextFormat(textBox, $@"Url = http://localhost:{port}");
                Exit = false;
                while (!Exit)
                {
                    TcpClient tcpClient = await _listener.AcceptTcpClientAsync();
                    var taskProcessTcpClient = ProcessTcpClientAsync(tcpClient, textBox);
                    await Task.Delay(TimeSpan.FromMilliseconds(100));
                }
            }
            catch (SocketException ex)
            {
                var message = $"Error listening on port {port}. Make sure IIS or another application is not running and consuming your port.";
                InvokeTextBox.AddTextFormat(textBox, message);
                throw new Exception(message, ex);
            }
        }

        public void Close()
        {
            Exit = true;
        }

        #endregion
    }
}
