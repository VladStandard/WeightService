// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using MDSoft.BarcodePrintUtils;
using MDSoft.BarcodePrintUtils.Tsc;

namespace TscPrintDemoWinForm
{
    public partial class FormMain : Form
    {

        #region Public and private fields and properties

        private readonly TSCSDK.driver _driver = new();
        private TscDriverHelper TscPrintControl { get; } = TscDriverHelper.Instance;

        #endregion

        #region Constructor and destructor

        public FormMain()
        {
            InitializeComponent();
        }

        #endregion

        #region Public and private methods

        private void ButtonPrintSendCmd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(fieldPortName.Text))
                return;
            if (string.IsNullOrEmpty(fieldCmd.Text))
                return;

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

        private void ButtonLibInit_Click(object sender, EventArgs e)
        {
            TscPrintControl.Setup(PrintChannel.Name, fieldPortName.Text, PrintLabelSize.Size80x100, PrintDpi.Dpi300);
            toolStripStatusLabel.Text = @"Init complete.";
        }

        private void ButtonLibInitv2_Click(object sender, EventArgs e)
        {
            TscPrintControl.Setup(PrintChannel.Ethernet, fieldPortIp.Text, Convert.ToInt32(fieldPortPort.Text),
                PrintLabelSize.Size80x100, PrintDpi.Dpi300);
            toolStripStatusLabel.Text = @"Init complete.";
        }

        private void ButtonLibSendCmd_Click(object sender, EventArgs e)
        {
            TscPrintControl.SendCmd(fieldCmd.Text);
            toolStripStatusLabel.Text = @"Send cmd complete.";
        }

        #endregion
    }
}
