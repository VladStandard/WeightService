// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Specialized;
using System.Configuration;

namespace ZplPrintSenderTool;

public partial class FormMain : Form
{

    #region Public and private fields and properties

    private readonly TSCSDK.driver _driver = new();
    private TscDriverHelper TscDriver { get; } = TscDriverHelper.Instance;
    private readonly object _lockTcpClient = new();
    private SimpleTcpClient? _wsTcpClient;
    private SimpleTcpClient WsTcpClient
    {
        get
        {
            if (_wsTcpClient is not null && !IsIpAddressChange)
                return _wsTcpClient;
            lock (_lockTcpClient)
            {
                if (_wsTcpClient is not null)
                {
                    _wsTcpClient.Events.Connected -= WsTcpClientConnected;
                    _wsTcpClient.Events.DataReceived -= WsTcpClientDataReceived;
                    _wsTcpClient.Events.DataSent -= WsTcpClientDataSent;
                    _wsTcpClient.Events.Disconnected -= WsTcpClientDisconnected;
                    _wsTcpClient.Disconnect();
                    _wsTcpClient.Dispose();
                    _wsTcpClient = null;
                    Application.DoEvents();
                }
                IpAddress = fieldIpAddress.Text;
                _wsTcpClient = new(IpAddress, 9100);
                _wsTcpClient.Events.Connected += WsTcpClientConnected;
                _wsTcpClient.Events.DataReceived += WsTcpClientDataReceived;
                _wsTcpClient.Events.DataSent += WsTcpClientDataSent;
                _wsTcpClient.Events.Disconnected += WsTcpClientDisconnected;
                // TCP keepalives are disabled by default. To enable them:
                _wsTcpClient.Keepalive.EnableTcpKeepAlives = true;
                _wsTcpClient.Keepalive.TcpKeepAliveInterval = 2; // wait before sending subsequent keepalive
                _wsTcpClient.Keepalive.TcpKeepAliveTime = 2; // wait before sending a keepalive
                _wsTcpClient.Keepalive.TcpKeepAliveRetryCount = 2; // number of failed keepalive probes before terminating connection
            }
            return _wsTcpClient;
        }
    }
    private bool IsConnected => _wsTcpClient is not null && _wsTcpClient.IsConnected;
    private string IpAddress { get; set; } = "";
    private bool IsIpAddressChange => !IpAddress.Equals(fieldIpAddress.Text);

    #endregion

    #region Constructor and destructor

    public FormMain()
    {
        InitializeComponent();

        SetupSendType();
        SetupLib();
        SetupLabelSize();
        SetupLabelDpi();
        LoadAppSettings();
    }

    #endregion

    #region Public and private methods

    private void SetupSendType() => 
        SetComboBoxItems(comboBoxSendType, typeof(WsEnumPrintSendType), WsEnumPrintSendType.Default);

    private void SetupLib() => 
        SetComboBoxItems(comboBoxLibrary, typeof(WsEnumPrintTscDll), WsEnumPrintTscDll.TscLibNet32);

    private void SetupLabelSize() => 
        SetComboBoxItems(comboBoxLabelSize, typeof(WsEnumPrintLabelSize), WsEnumPrintLabelSize.Size80x100);

    private void SetupLabelDpi() => 
        SetComboBoxItems(comboBoxLabelDpi, typeof(WsEnumPrintLabelDpi), WsEnumPrintLabelDpi.Dpi300);

    private static void SetComboBoxItems(ComboBox comboBox, Type type, object valueDefault)
    {
        comboBox.Items.Clear();
        int index = 0;
        int selectedIndex = 0;
        foreach (var value in Enum.GetValues(type))
        {
            comboBox.Items.Add(value);
            if (Equals(value, valueDefault))
                selectedIndex = index;
            index++;
        }
        comboBox.SelectedIndex = selectedIndex;
    }

    private void LoadAppSettings()
    {
        comboBoxSendType.SelectedItem = GetSendType(Settings.Default.SendType);
        fieldIpAddress.Text = !string.IsNullOrEmpty(Settings.Default.IpAddress) ? Settings.Default.IpAddress : "127.0.0.1";
        fieldIpPort.Text = !string.IsNullOrEmpty(Settings.Default.IpPort) ? Settings.Default.IpPort : "9100";
    }

    private WsEnumPrintSendType GetSendType(string sendType)
    {
        if (string.IsNullOrEmpty(sendType)) return WsEnumPrintSendType.Default;
        foreach (WsEnumPrintSendType item in comboBoxSendType.Items)
        {
            if (Equals(item.ToString(), sendType))
            {
                return item;
            }
        }
        return WsEnumPrintSendType.Default;
    }

    private WsEnumPrintLabelSize GetLabelSize()
    {
        if (comboBoxLabelSize.Items.Count > 0)
        {
            if (comboBoxLabelSize.Items[comboBoxLabelSize.SelectedIndex] is WsEnumPrintLabelSize printLabelSize)
                return printLabelSize;
        }
        return WsEnumPrintLabelSize.Size80x100;
    }

    private WsEnumPrintLabelDpi GetLabelDpi()
    {
        if (comboBoxLabelDpi.Items.Count > 0)
        {
            if (comboBoxLabelDpi.Items[comboBoxLabelDpi.SelectedIndex] is WsEnumPrintLabelDpi printLabelDpi)
                return printLabelDpi;
        }
        return WsEnumPrintLabelDpi.Dpi300;
    }

    private void ButtonLibInit_Click(object sender, EventArgs e)
    {
        TryAction(() =>
        {
            TscDriver.Setup(WsEnumPrintChannel.Name, fieldName.Text, GetLabelSize(), GetLabelDpi());
            toolStripStatusLabel.Text = @"Init complete.";
        });
    }

    private void ButtonLibInitv2_Click(object sender, EventArgs e)
    {
        TryAction(() =>
        {
            TscDriver.Setup(WsEnumPrintChannel.Ethernet, fieldIpAddress.Text, Convert.ToInt32(fieldIpPort.Text),
                GetLabelSize(), GetLabelDpi());
            toolStripStatusLabel.Text = @"Init complete.";
        });
    }

    private void ButtonLibSendCmd_Click(object sender, EventArgs e)
    {
        TryAction(() =>
        {
            TscDriver.SendCmd(fieldCmd.Text);
            toolStripStatusLabel.Text = @"Send cmd complete.";
        });
    }

    private void ButtonPrintSendCmd_Click(object sender, EventArgs e)
    {
        TryAction(() =>
        {
            if (!_driver.openport(fieldName.Text))
            {
                toolStripStatusLabel.Text = $@"Cann't open the port '{fieldName.Text}'!";
                return;
            }

            _driver.clearbuffer();
            if (!_driver.sendcommand(fieldCmd.Text))
            {
                toolStripStatusLabel.Text = @"Cann't send cmd!";
                return;
            }
            if (!_driver.closeport())
            {
                toolStripStatusLabel.Text = @"Cann't close the port!";
            }
        });
    }

    private void comboBoxSendType_SelectedIndexChanged(object sender, EventArgs e)
    {
        TryAction(() =>
        {
            if (sender is not ComboBox cb)
                return;
            if (cb.SelectedItem is not WsEnumPrintSendType sendType)
                return;
            switch (sendType)
            {
                case WsEnumPrintSendType.Default:
                    SetUiEnable(false, false, false);
                    break;
                case WsEnumPrintSendType.Serial:
                    SetUiEnable(false, false, false);
                    break;
                case WsEnumPrintSendType.Tcp:
                    SetUiEnable(true, false, true);
                    break;
                case WsEnumPrintSendType.ZebraDriver:
                    SetUiEnable(true, true, false);
                    break;
                case WsEnumPrintSendType.HprtDriver:
                    SetUiEnable(true, true, false);
                    break;
                case WsEnumPrintSendType.TscDriver:
                    SetUiEnable(true, true, false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });
    }

    private void SetUiEnable(bool isEnabledNetwork, bool isEnabledDriver, bool isEnbaledSendTcp)
    {
        // Network.
        labelIpAddress.Enabled = isEnabledNetwork;
        fieldIpAddress.Enabled = isEnabledNetwork;
        labelPort.Enabled = isEnabledNetwork;
        fieldIpPort.Enabled = isEnabledNetwork;
        // Driver.
        labelName.Enabled = isEnabledDriver;
        fieldName.Enabled = isEnabledDriver;
        labelLibrary.Enabled = isEnabledDriver;
        comboBoxLibrary.Enabled = isEnabledDriver;
        buttonLibraryInit.Enabled = isEnabledDriver;
        labelSize.Enabled = isEnabledDriver;
        comboBoxLabelSize.Enabled = isEnabledDriver;
        buttonLibInitv2.Enabled = isEnabledDriver;
        labelLabelDpi.Enabled = isEnabledDriver;
        comboBoxLabelDpi.Enabled = isEnabledDriver;
        buttonLibrarySendCmd.Enabled = isEnabledDriver;
        buttonPrintSendCmd.Enabled = isEnabledDriver;
        // Send TCP.
        buttonPrintSendCmdByTcp.Enabled = isEnbaledSendTcp;
    }

    private void TryAction(Action action)
    {
        try
        {
            fieldException.Clear();
            action();
        }
        catch (Exception ex)
        {
            fieldException.Text = ex.InnerException is null ? ex.Message : ex.Message + Environment.NewLine + ex.InnerException.Message;
        }
    }

    private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
    {
        Settings.Default.SendType = comboBoxSendType.SelectedItem.ToString();
        Settings.Default.IpAddress = fieldIpAddress.Text;
        Settings.Default.IpPort = fieldIpPort.Text;
        Settings.Default.Save();
    }

    #endregion

    #region Public and private methods - SimpleTcpClient

    private void buttonPrintSendCmdByTcp_Click(object sender, EventArgs e)
    {
        TryAction(() =>
        {
            ReopenTcp();
            if (!IsConnected)
                return;
            WsTcpClient.Send(fieldCmd.Text);
        });
    }
    
    private void ReopenTcp()
    {
        if (!IsConnected)
            WsTcpClient.ConnectWithRetries(1_000);
    }

    private void WsTcpClientConnected(object sender, ConnectionEventArgs e)
    {
        toolStripStatusLabel.Text = $@"Server {e.IpPort} connected";
    }

    private void WsTcpClientDataReceived(object sender, SuperSimpleTcp.DataReceivedEventArgs e)
    {
        string received = e.Data.Array is null ? string.Empty : Encoding.UTF8.GetString(e.Data.Array, 0, e.Data.Count);
        received = string.IsNullOrEmpty(received) ? "0" : $"{received.Length} bytes with data '{received}'";
        toolStripStatusLabel.Text = $@"Server {e.IpPort} data received {received}";
    }

    private void WsTcpClientDataSent(object sender, DataSentEventArgs e)
    {
        toolStripStatusLabel.Text = $@"Server {e.IpPort} data sent {e.BytesSent} bytes";
    }

    private void WsTcpClientDisconnected(object sender, ConnectionEventArgs e)
    {
        toolStripStatusLabel.Text = $@"Server {e.IpPort} disconnected by {e.Reason} reason";
    }

    #endregion
}