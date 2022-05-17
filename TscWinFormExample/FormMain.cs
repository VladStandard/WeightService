// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using WeightCore.Print.Tsc;

namespace TSCLIB_DLL_IN_C_Sharp
{
    public partial class FormMain : Form
    {

        #region Public and private fields and properties

        private readonly TSCSDK.driver _driver = new();
        public TscPrintControlHelper TscPrintControl { get; private set; } = TscPrintControlHelper.Instance;

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
                MessageBox.Show($"Cann't open the port '{fieldPortName.Text}'!");
                return;
            }

            _driver.clearbuffer();
            if (!_driver.sendcommand(fieldCmd.Text))
            {
                MessageBox.Show($"Cann't send cmd!");
                return;
            }
            if (!_driver.closeport())
            {
                MessageBox.Show($"Cann't close the port!");
                return;
            }
        }

        private void ButtonLibInit_Click(object sender, EventArgs e)
        {
            TscPrintControl.Init(fieldPortName.Text);
            MessageBox.Show("Init complete.");
        }

        private void ButtonLibInitv2_Click(object sender, EventArgs e)
        {
            TscPrintControl.Init(fieldPortIp.Text, Convert.ToInt32(fieldPortPort.Text));
            MessageBox.Show("Init complete.");
        }

        private void ButtonLibSendCmd_Click(object sender, EventArgs e)
        {
            TscPrintControl.SendCmd(fieldCmd.Text);
            MessageBox.Show("Send cmd complete.");
        }

        #endregion
    }
}
