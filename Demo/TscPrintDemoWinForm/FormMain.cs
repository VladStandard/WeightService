// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using MDSoft.BarcodePrintUtils.Tsc;
using WsPrintCore.Common;

namespace TscPrintDemoWinForm;

public partial class FormMain : Form
{

    #region Public and private fields and properties

    private readonly TSCSDK.driver _driver = new();
    private TscDriverHelper TscDriver { get; } = TscDriverHelper.Instance;

    #endregion

    #region Constructor and destructor

    public FormMain()
    {
        InitializeComponent();

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

    #endregion
}