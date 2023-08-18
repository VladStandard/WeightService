// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace TscPrintDemoWinForm;

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
            // Открыть подключение.
            if (_wsTcpClient is not null)
                return _wsTcpClient;
            lock (_lockTcpClient)
            {
                _wsTcpClient = new(fieldPortIp.Text, 9100);
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
            //if (!IsConnected)
            //    _wsTcpClient.ConnectWithRetries(1_000);
            return _wsTcpClient;
        }
    }
    public bool IsConnected => _wsTcpClient is not null && _wsTcpClient.IsConnected;

    #endregion

    #region Constructor and destructor

    public FormMain()
    {
        InitializeComponent();

        WindowState = FormWindowState.Maximized;
        SetupLib();
        SetupLabelSize();
        SetupLabelDpi();
    }

    #endregion

    #region Public and private methods

    private void SetupLib()
    {
        SetComboBoxItems(comboBoxLib, typeof(WsEnumPrintTscDll), WsEnumPrintTscDll.TscLibNet32);
    }

    private void SetupLabelSize()
    {
        SetComboBoxItems(comboBoxLabelSize, typeof(WsEnumPrintLabelSize), WsEnumPrintLabelSize.Size80x100);
    }

    private void SetupLabelDpi()
    {
        SetComboBoxItems(comboBoxLabelDpi, typeof(WsEnumPrintLabelDpi), WsEnumPrintLabelDpi.Dpi300);
    }

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
        TscDriver.Setup(WsEnumPrintChannel.Name, fieldPortName.Text, GetLabelSize(), GetLabelDpi());
        toolStripStatusLabel.Text = @"Init complete.";
    }

    private void ButtonLibInitv2_Click(object sender, EventArgs e)
    {
        TscDriver.Setup(WsEnumPrintChannel.Ethernet, fieldPortIp.Text, Convert.ToInt32(fieldPortPort.Text),
            GetLabelSize(), GetLabelDpi());
        toolStripStatusLabel.Text = @"Init complete.";
    }

    private void ButtonLibSendCmd_Click(object sender, EventArgs e)
    {
        TscDriver.SendCmd(fieldCmd.Text);
        toolStripStatusLabel.Text = @"Send cmd complete.";
    }

    private void ButtonPrintSendCmd_Click(object sender, EventArgs e)
    {
        if (!_driver.openport(fieldPortName.Text))
        {
            toolStripStatusLabel.Text = $@"Cann't open the port '{fieldPortName.Text}'!";
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
    }

    private void buttonPrintSendCmdByTcp_Click(object sender, EventArgs e)
    {
        ReopenTcp();
        if (!IsConnected) return;
        WsTcpClient.Send(fieldCmd.Text);
    }

    #endregion

    #region Public and private methods - SimpleTcpClient

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