using MDSoft.WpfUtils;
using Ninja.WebSockets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WebSocketsExamples.Models;

namespace WebSocketsExamples.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private fields and properties

        private LogHelper Log { get; set; } = LogHelper.Instance;
        static IWebSocketServerFactory _webSocketServerFactory;
        private Task _taskWebServer;
        private WebServer _webServer;

        #endregion

        #region Constructor and destructor

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _webSocketServerFactory = new WebSocketServerFactory();
            Log.Setup(fieldOut);
        }

        #endregion

        #region Private methods

        private async Task StartWebServer(TextBox textBox)
        {
            InvokeTextBox.AddTextFormat(textBox, DateTime.Now, @"WebServer is starting.");
            try
            {
                var port = 27416;
                IList<string> supportedSubProtocols = new[] { "chatV1", "chatV2", "chatV3" };
                InvokeTextBox.AddTextFormat(textBox, $@"WebServer protocols = [{string.Join(", ", supportedSubProtocols)}].");
                using (_webServer = new WebServer(_webSocketServerFactory, supportedSubProtocols))
                {
                    InvokeTextBox.AddTextFormat(textBox, $@"WebServer listen.");
                    await _webServer.Open(port, textBox);
                }
            }
            catch (Exception ex)
            {
                Log.Error(nameof(WebServer), nameof(StartWebServer), ex.ToString());
                InvokeTextBox.AddTextFormat(textBox, $@"WebServer exception: {ex.Message}.");
                if (ex.InnerException != null)
                    InvokeTextBox.AddTextFormat(textBox, 
                        $@"WebServer inner exception: {ex.InnerException.Message}.");
            }

            InvokeTextBox.AddTextFormat(textBox, @"WebServer is stopped.");
        }

        private void ButtonServerStart_OnClick(object sender, RoutedEventArgs e)
        {
            if (_taskWebServer == null)
            {
                _taskWebServer = StartWebServer(fieldOut);
                Task.Run(async () =>
                {
                    while (!_webServer.Exit)
                    {
                        await Task.Delay(TimeSpan.FromMilliseconds(100));
                    }

                    var timeout = 30;
                    while (_taskWebServer.Status != TaskStatus.RanToCompletion && _taskWebServer.Status != TaskStatus.Faulted && _taskWebServer.Status != TaskStatus.Canceled &&
                           timeout > 0)
                    {
                        timeout--;
                        await Task.Delay(TimeSpan.FromMilliseconds(100));
                    }
                    _taskWebServer.Dispose();
                    //_taskWebServer = null;
                });
            }
        }

        private void ButtonServerStop_OnClick(object sender, RoutedEventArgs e)
        {
            _webServer?.Close();
        }

        #endregion
    }
}
