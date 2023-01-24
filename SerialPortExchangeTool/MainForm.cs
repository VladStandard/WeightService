// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using DataCore.Utils;
using WsMassa.Helpers;
using WsMassa.Models;
using WsMassa.Utils;

namespace SerialPortExchangeTool;

public partial class MainForm : Form
{
    #region Public and private fields and properties

    private BytesHelper Bytes { get; set; } = BytesHelper.Instance;
    private SerialPortHelper PortController { get; set; }
    private int SendBytesCount { get; set; } = 0;
    private int ReceiveBytesCount { get; set; } = 0;

    #endregion

    #region Constructor and destructor

    public MainForm()
    {
        InitializeComponent();

        Initialize();
        PortController = new(PortOpenCallback, PortCloseCallback, PortResponseCallback, PortExceptionCallback);
        statusTimeLabel.Text = StringUtils.FormatDtRus(DateTime.Now, true);
        toolStripStatusTx.Text = @"Sent: 0";
        toolStripStatusRx.Text = @"Received: 0";
        MaximizeBox = false;
        MinimizeBox = false;
    }

    #endregion

    #region Public and private methods

    private void Initialize()
    {
        // BaudRate.
        baudRateCbx.Items.Clear();
        foreach (string baudRate in SerialPortUtils.GetBaudRates())
            baudRateCbx.Items.Add(baudRate);
        baudRateCbx.SelectedIndex = 5;

        // Data bits.
        dataBitsCbx.Items.Clear();
        foreach (string dataBits in SerialPortUtils.GetDataBits())
            dataBitsCbx.Items.Add(dataBits);
        dataBitsCbx.SelectedIndex = 3;

        // Stop bits.
        stopBitsCbx.Items.Clear();
        foreach (string stopBits in SerialPortUtils.GetStopBits())
            stopBitsCbx.Items.Add(stopBits);
        stopBitsCbx.SelectedIndex = 1;

        // Parity.
        parityCbx.Items.Clear();
        foreach (string parity in SerialPortUtils.GetParity())
            parityCbx.Items.Add(parity);
        parityCbx.SelectedIndex = 1;

        // Handshaking.
        handshakingcbx.Items.Clear();
        foreach (string handshaking in SerialPortUtils.GetHandshaking())
            handshakingcbx.Items.Add(handshaking);
        handshakingcbx.SelectedIndex = 3;

        // Com ports.
        comListCbx.Items.Clear();
        string[] ports = SerialPortUtils.GetPorts();
        if (ports.Length == 0)
        {
            statuslabel.Text = @"No COM found !";
            openCloseSpbtn.Enabled = false;
        }
        else
        {
            foreach (string port in ports)
                comListCbx.Items.Add(port);
            comListCbx.SelectedIndex = 0;
            openCloseSpbtn.Enabled = true;
        }
    }

    private void PortOpenCallback(object sender, SerialPortEventArgs e)
    {
        if (InvokeRequired)
        {
            Invoke(new Action<object, SerialPortEventArgs>(PortOpenCallback), sender, e);
            return;
        }

        if (e.SerialPort.IsOpen)
        {
            statuslabel.Text = comListCbx.Text + " Opend";
            openCloseSpbtn.Text = "Close";
            sendbtn.Enabled = true;
            autoSendcbx.Enabled = true;
            autoReplyCbx.Enabled = true;

            comListCbx.Enabled = false;
            baudRateCbx.Enabled = false;
            dataBitsCbx.Enabled = false;
            stopBitsCbx.Enabled = false;
            parityCbx.Enabled = false;
            handshakingcbx.Enabled = false;
            refreshbtn.Enabled = false;

            if (autoSendcbx.Checked)
            {
                autoSendtimer.Start();
                sendtbx.ReadOnly = true;
            }
        }
        else
        {
            statuslabel.Text = "Open failed !";
            sendbtn.Enabled = false;
            autoSendcbx.Enabled = false;
            autoReplyCbx.Enabled = false;
        }
    }

    private void PortCloseCallback(object sender, SerialPortEventArgs e)
    {
        if (InvokeRequired)
        {
            Invoke(new Action<object, SerialPortEventArgs>(PortCloseCallback), sender, e);
            return;
        }

        // Close successfully.
        if (!e.SerialPort.IsOpen)
        {
            statuslabel.Text = comListCbx.Text + " Closed";
            openCloseSpbtn.Text = "Open";

            sendbtn.Enabled = false;
            sendtbx.ReadOnly = false;
            autoSendcbx.Enabled = false;
            autoSendtimer.Stop();

            comListCbx.Enabled = true;
            baudRateCbx.Enabled = true;
            dataBitsCbx.Enabled = true;
            stopBitsCbx.Enabled = true;
            parityCbx.Enabled = true;
            handshakingcbx.Enabled = true;
            refreshbtn.Enabled = true;
        }
    }

    private void PortResponseCallback(object sender, SerialPortEventArgs e)
    {
        if (InvokeRequired)
        {
            try
            {
                Invoke(new Action<object, SerialPortEventArgs>(PortResponseCallback), sender, e);
            }
            catch (Exception)
            {
                //disable form destroy exception
            }
            return;
        }

        if (recStrRadiobtn.Checked) //display as string
        {
            receivetbx.AppendText(Encoding.Default.GetString(e.ReceivedBytes) + Environment.NewLine);
        }
        else //display as hex
        {
            receivetbx.AppendText(Bytes.Bytes2Hex(e.ReceivedBytes) + Environment.NewLine);
        }
        // update status bar
        ReceiveBytesCount += e.ReceivedBytes.Length;
        toolStripStatusRx.Text = "Received: " + ReceiveBytesCount.ToString();

        // auto reply
        if (autoReplyCbx.Checked)
        {
            Sendbtn_Click(this, new EventArgs());
        }
    }

    private void PortExceptionCallback(Exception ex,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        if (InvokeRequired)
        {
            Invoke(new Action<Exception, string, int, string>(PortExceptionCallback), ex);
            return;
        }

        string message = ex.Message;
        if (ex.InnerException != null)
            message += Environment.NewLine + ex.InnerException.ToString();
        MessageBox.Show(message);
    }

    private void Receivetbx_TextChanged(object sender, EventArgs e)
    {
        receivetbx.SelectionStart = receivetbx.Text.Length;
        receivetbx.ScrollToCaret();
    }

    private void Statustimer_Tick(object sender, EventArgs e)
    {
        statusTimeLabel.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
    }

    private void OpenCloseSpbtn_Click(object sender, EventArgs e)
    {
        if (openCloseSpbtn.Text == "Open")
        {
            PortController.Open(comListCbx.Text, baudRateCbx.Text, parityCbx.Text, dataBitsCbx.Text, stopBitsCbx.Text, handshakingcbx.Text);
        }
        else
        {
            PortController.Close();
        }
    }

    private void Refreshbtn_Click(object sender, EventArgs e)
    {
        comListCbx.Items.Clear();
        //Com Ports
        string[] ArrayComPortsNames = SerialPort.GetPortNames();
        if (ArrayComPortsNames.Length == 0)
        {
            statuslabel.Text = "No COM found !";
            openCloseSpbtn.Enabled = false;
        }
        else
        {
            Array.Sort(ArrayComPortsNames);
            for (int i = 0; i < ArrayComPortsNames.Length; i++)
            {
                comListCbx.Items.Add(ArrayComPortsNames[i]);
            }
            comListCbx.Text = ArrayComPortsNames[0];
            openCloseSpbtn.Enabled = true;
            statuslabel.Text = "OK !";
        }
    }

    private void Sendbtn_Click(object sender, EventArgs e)
    {
        string sendText = sendtbx.Text;
        if (sendText == null)
        {
            return;
        }
        //set select index to the end
        sendtbx.SelectionStart = sendtbx.TextLength;

        bool flag;
        if (sendHexRadiobtn.Checked)
        {
            //If hex radio checked
            //send bytes to serial port
            byte[] bytes = Bytes.Hex2Bytes(sendText);
            sendbtn.Enabled = false;//wait return
            flag = PortController.Send(bytes);
            sendbtn.Enabled = true;
            SendBytesCount += bytes.Length;
        }
        else
        {
            //send String to serial port
            sendbtn.Enabled = false;//wait return
            flag = PortController.Send(sendText);
            sendbtn.Enabled = true;
            SendBytesCount += sendText.Length;
        }
        if (flag)
        {
            statuslabel.Text = "Send OK !";
        }
        else
        {
            statuslabel.Text = "Send failed !";
        }
        //update status bar
        toolStripStatusTx.Text = "Sent: " + SendBytesCount.ToString();
    }

    private void ClearSendbtn_Click(object sender, EventArgs e)
    {
        sendtbx.Text = "";
        toolStripStatusTx.Text = "Sent: 0";
        SendBytesCount = 0;
        addCRCcbx.Checked = false;
    }

    private void ClearReceivebtn_Click(object sender, EventArgs e)
    {
        receivetbx.Text = "";
        toolStripStatusRx.Text = "Received: 0";
        ReceiveBytesCount = 0;
    }

    private void RecHexRadiobtn_CheckedChanged(object sender, EventArgs e)
    {
        if (recHexRadiobtn.Checked)
        {
            if (receivetbx.Text == null)
            {
                return;
            }
            receivetbx.Text = Bytes.String2Hex(receivetbx.Text);
        }
    }

    private void RecStrRadiobtn_CheckedChanged(object sender, EventArgs e)
    {
        if (recStrRadiobtn.Checked)
        {
            if (receivetbx.Text == null)
            {
                return;
            }
            receivetbx.Text = Bytes.Hex2String(receivetbx.Text);
        }
    }

    private void SendHexRadiobtn_CheckedChanged(object sender, EventArgs e)
    {
        if (sendHexRadiobtn.Checked)
        {
            if (sendtbx.Text == null)
            {
                return;
            }
            sendtbx.Text = Bytes.String2Hex(sendtbx.Text);
            addCRCcbx.Enabled = true;
        }
    }

    private void SendStrRadiobtn_CheckedChanged(object sender, EventArgs e)
    {
        if (sendStrRadiobtn.Checked)
        {
            if (sendtbx.Text == null)
            {
                return;
            }
            sendtbx.Text = Bytes.Hex2String(sendtbx.Text);
            addCRCcbx.Enabled = false;
        }
    }

    private void Sendtbx_KeyPress(object sender, KeyPressEventArgs e)
    {
        // Input Hex, should like: AF-1B-09
        if (sendHexRadiobtn.Checked)
        {
            e.Handled = true;
            int length = sendtbx.SelectionStart;
            switch (length % 3)
            {
                case 0:
                case 1:
                    if ((e.KeyChar >= 'a' && e.KeyChar <= 'f')
                        || (e.KeyChar >= 'A' && e.KeyChar <= 'F')
                        || char.IsDigit(e.KeyChar)
                        || (char.IsControl(e.KeyChar) && e.KeyChar != (char)13))
                    {
                        e.Handled = false;
                    }
                    break;
                case 2:
                    if (e.KeyChar == '-'
                        || (char.IsControl(e.KeyChar) && e.KeyChar != (char)13))
                    {
                        e.Handled = false;
                    }
                    break;
            }
        }
    }

    private void AutoSendcbx_CheckedChanged(object sender, EventArgs e)
    {
        if (autoSendcbx.Checked)
        {
            autoSendtimer.Enabled = true;
            autoSendtimer.Interval = int.Parse(sendIntervalTimetbx.Text);
            autoSendtimer.Start();

            // disable send botton and textbox
            sendIntervalTimetbx.Enabled = false;
            sendtbx.ReadOnly = true;
            sendbtn.Enabled = false;
        }
        else
        {
            autoSendtimer.Enabled = false;
            autoSendtimer.Stop();

            // enable send botton and textbox
            sendIntervalTimetbx.Enabled = true;
            sendtbx.ReadOnly = false;
            sendbtn.Enabled = true;
        }
    }

    private void AutoSendtimer_Tick(object sender, EventArgs e)
    {
        Sendbtn_Click(sender, e);
    }

    private void SendIntervalTimetbx_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
        {
            e.Handled = false;
        }
        else
        {
            e.Handled = true;
        }
    }

    private void AddCRCcbx_CheckedChanged(object sender, EventArgs e)
    {
        string sendText = sendtbx.Text;
        if (sendText == null || sendText == "")
        {
            addCRCcbx.Checked = false;
            return;
        }
        if (addCRCcbx.Checked)
        {
            //Add 2 bytes CRC to the end of the data
            byte[] senddata = Bytes.Hex2Bytes(sendText);
            byte[] crcbytes = BitConverter.GetBytes(NullFX.CRC.Crc16.ComputeChecksum(NullFX.CRC.Crc16Algorithm.Ccitt, senddata));
            sendText += "-" + BitConverter.ToString(crcbytes, 1, 1);
            sendText += "-" + BitConverter.ToString(crcbytes, 0, 1);
        }
        else
        {
            //Delete 2 bytes CRC to the end of the data
            if (sendText.Length >= 6)
            {
                sendText = sendText.Substring(0, sendText.Length - 6);
            }
        }
        sendtbx.Text = sendText;
    }

    private void ReceivedDataToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SaveFileDialog saveFileDialog = new()
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            Filter = "txt file|*.txt",
            DefaultExt = ".txt",
            FileName = "received.txt"
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            string fName = saveFileDialog.FileName;
            System.IO.File.WriteAllText(fName, receivetbx.Text);
        }
    }

    private void SendDataToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SaveFileDialog saveFileDialog = new()
        {
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            Filter = "txt file|*.txt",
            DefaultExt = ".txt",
            FileName = "send.txt"
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            string fName = saveFileDialog.FileName;
            System.IO.File.WriteAllText(fName, sendtbx.Text);
        }
    }

    private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void ButtonCmdGetMassa_Click(object sender, EventArgs e)
    {
        byte[] bytes = new byte[] { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0x23, 0x23, 0x00 };
        sendtbx.Text = BitConverter.ToString(bytes);
    }

    #endregion
}
