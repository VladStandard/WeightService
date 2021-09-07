// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WeightCore.Zabbix;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using ZplSdkExamples.Models;
using ZplSdkExamples.Utils;
using ZplSdkExamples.ViewModels;
using ZebraConnectionBuilder = Zebra.Sdk.Comm.ConnectionBuilder;
using ZebraPrinterStatus = Zebra.Sdk.Printer.PrinterStatus;
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace ZplSdkExamples.Views
{
    public partial class MainWindow
    {
        #region Private fields and properties

        private readonly ConnectionBuilderViewModel _viewModel;
        private Connection _connection;
        private readonly LogHelper _log = LogHelper.Instance;
        private HealthDataCollectorDummy _healthDataCollector;
        private CancellationTokenSource _cancelTokenSource;
        private CancellationToken _token;
        private FakeCheckThreadByLog _fakeCheckThreadByLog;
        private CancellationTokenSource _cancelTokenSourceHttpListener;
        private CancellationToken _tokenHttpListener;
        private ZabbixHttpListener _zabbixHttpListener;

        #endregion

        #region Constructor and destructor

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = DataContext as ConnectionBuilderViewModel;
        }

        #endregion

        #region Private methods

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _log.Setup(logData);
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            ButtonStopHttpListener_OnClick(sender, null);
        }

        private void TestConnectionString()
        {
            try
            {
                ClearProgress();
                _connection = ZebraConnectionBuilder.Build(GetConnectionStringForSdk());
                PublishProgress("Connection string evaluated as class type " + _connection.GetType().Name);
                _connection.Open();

                PublishProgress("Connection opened successfully");

                if (IsAttemptingStatusConnection())
                {
                    ZebraPrinterLinkOs printer = ZebraPrinterFactory.GetLinkOsPrinter(_connection);
                    PublishProgress("Created a printer, attempting to retrieve status");

                    ZebraPrinterStatus status = printer.GetCurrentStatus();
                    PublishProgress("Is printer ready to print? " + status.isReadyToPrint);
                }
                else
                {
                    ZebraPrinter printer = ZebraPrinterFactory.GetInstance(_connection);
                    PublishProgress("Created a printer, attempting to print a config label");
                    printer.PrintConfigurationLabel();
                }

                PublishProgress("Closing connection");
            }
            catch (ConnectionException)
            {
                MessageBoxCreator.ShowError("Connection could not be opened", "Error");
            }
            catch (ZebraPrinterLanguageUnknownException)
            {
                MessageBoxCreator.ShowError("Could not create printer", "Error");
            }
            finally
            {
                if (_connection != null)
                {
                    try
                    {
                        _connection.Close();
                    }
                    catch (ConnectionException) { }
                    finally
                    {
                        _connection = null;
                        SetTestButtonState(true);
                    }
                }
                else
                {
                    SetTestButtonState(true);
                }
            }
        }

        private void SetTestButtonState(bool state)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                testConnectionStringButton.IsEnabled = state;
            });
        }

        private void ClearProgress()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                logData.Text = "Log:\nTesting string: " + GetConnectionStringForSdk() + "\n";
            });
        }

        private string GetConnectionStringForSdk()
        {
            string finalConnectionString = "";
            Application.Current.Dispatcher.Invoke(() =>
            {
                string selectedPrefix = "";
                if (connectionPrefixDropdown.SelectedIndex > 0)
                {
                    selectedPrefix = connectionPrefixDropdown.SelectedValue + ":";
                }

                string userSuppliedDescriptionString = usbDriverIpAddress.Text;
                finalConnectionString = selectedPrefix + userSuppliedDescriptionString;
            });
            return finalConnectionString;
        }

        private bool IsAttemptingStatusConnection()
        {
            return _connection.GetType().Name.Contains("Status");
        }

        private void PublishProgress(string progress)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                logData.Text = logData.Text + progress + Environment.NewLine;
            });
        }

        private void ConnectionPrefixDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            connectionString.Text = GetConnectionStringForSdk();
            SetAddressTextBlock();
        }

        private void SetAddressTextBlock()
        {
            switch (connectionPrefixDropdown.SelectedValue)
            {
                case ConnectionPrefix.Tcp:
                case ConnectionPrefix.TcpMulti:
                case ConnectionPrefix.TcpStatus:
                    AddressTextBlock.Text = "IP Address:";
                    break;
                case ConnectionPrefix.Bluetooth:
                case ConnectionPrefix.BluetoothMulti:
                    AddressTextBlock.Text = "BT Address:";
                    break;
                case ConnectionPrefix.Usb:
                    AddressTextBlock.Text = "USB Driver:";
                    break;
                case ConnectionPrefix.UsbDirect:
                    AddressTextBlock.Text = "Symbolic Name:";
                    break;
                default:
                    AddressTextBlock.Text = "Address:";
                    break;
            };
        }

        private void TestConnectionStringButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _viewModel.LogData = "Log:\n\n";
                SetTestButtonState(false);

                TestConnectionString();
            }
            catch (Exception ex)
            {
                MessageBoxCreator.ShowError(ex.Message, "Connection Builder Error");
            }
        }

        private void UsbDriverIpAddress_KeyUp(object sender, KeyEventArgs e)
        {
            connectionString.Text = GetConnectionStringForSdk();
        }

        /// <summary>
        /// Start ZabbixHttpListener.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStartHttpListener_OnClick(object sender, RoutedEventArgs e)
        {
            _log.Info("OnStart.");

            try
            {
                _log.Info("new HealthDataCollectorDummy()");
                _healthDataCollector = new HealthDataCollectorDummy();
                _healthDataCollector.LoadValues();

                _log.Info("new FakeCheckThreadByLog");
                _cancelTokenSource = new CancellationTokenSource();
                _token = _cancelTokenSource.Token;

                _fakeCheckThreadByLog = new FakeCheckThreadByLog(_healthDataCollector.LoadValues, _token, 2500);
                _fakeCheckThreadByLog.Start();

                _log.Info("new ZabbixHttpListener");
                _log.Info("http://localhost:18086/status");
                _cancelTokenSourceHttpListener = new CancellationTokenSource();
                _tokenHttpListener = _cancelTokenSource.Token;

                //_zabbixHttpListener = new ZabbixHttpListener(_healthDataCollector.ResponseBuilderFunc, _tokenHttpListener, 10);
                _zabbixHttpListener = new ZabbixHttpListener();
                _zabbixHttpListener.Start();

                _log.Info("fakeCheckThreadByLog.StartED.");
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
        }

        /// <summary>
        /// Stop ZabbixHttpListener.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStopHttpListener_OnClick(object sender, RoutedEventArgs e)
        {
            _log.Info("fakeCheckThreadByLog.Stop()");
            try
            {
                _zabbixHttpListener?.Stop();
                _token.ThrowIfCancellationRequested();
                _tokenHttpListener.ThrowIfCancellationRequested();
                _fakeCheckThreadByLog.Start();
                //zabbixHttpListener.Stop();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }

            _log.Info("In OnStop.");
        }

        #endregion
    }
}
