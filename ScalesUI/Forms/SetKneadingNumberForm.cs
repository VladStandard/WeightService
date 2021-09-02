// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataBaseCore;
using ScalesUI.Common;
using System;
using System.Windows.Forms;
using WeightCore.Gui;

namespace ScalesUI.Forms
{
    public partial class SetKneadingNumberForm : Form
    {
        #region Private fields and properties

        // Состояние устройства.
        private readonly SessionState _ws = SessionState.Instance;
        private int OldKneading { get; }
        private int OldPalletSize { get; }
        private DateTime OldProductDate { get; }

        #endregion

        #region Constructor and destructor

        public SetKneadingNumberForm()
        {
            InitializeComponent();
            // WindowState = FormWindowState.Maximized;
            OldKneading = _ws.Kneading;
            OldProductDate = _ws.ProductDate;
            OldPalletSize = _ws.PalletSize;
            buttonOk.Select();
        }

        #endregion

        #region Public and private methods

        private void SetKneadingNumberForm_Load(object sender, EventArgs e)
        {
            TopMost = !_ws.IsDebug;
            Width = Owner.Width;
            Height = Owner.Height;
            Left = Owner.Left;
            Top = Owner.Top;

            StartPosition = FormStartPosition.CenterScreen;
            ShowPalletSize();
        }

        private void ShowProductDate()
        {
            fieldProdDate.Text = _ws.ProductDate.ToString("dd.MM.yyyy");
        }

        private void ShowKneading()
        {
            fieldKneading.Text = _ws.Kneading.ToString();
        }

        private void buttonKneadingLeft_Click(object sender, EventArgs e)
        {
            //_ws.RotateKneading(Direction.back);

            var numberInputForm = new NumberInputForm();
            numberInputForm.InputValue = 0;// _ws.Kneading;
            if (numberInputForm.ShowDialog() == DialogResult.OK)
            {
                _ws.Kneading = numberInputForm.InputValue;
            }
            ShowKneading();

        }

        private void SetKneadingNumberForm_Shown(object sender, EventArgs e)
        {
            ShowKneading();
            ShowProductDate();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            _ws.Kneading = OldKneading;
            _ws.ProductDate = OldProductDate;
            _ws.PalletSize = OldPalletSize;
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonDtRight_Click(object sender, EventArgs e)
        {
            _ws.RotateProductDate(Direction.Forward);
            ShowProductDate();
        }

        private void buttonDtLeft_Click(object sender, EventArgs e)
        {
            _ws.RotateProductDate(Direction.Back);
            ShowProductDate();
        }

        private void buttonPalletSize10_Click(object sender, EventArgs e)
        {
            var n = _ws.PalletSize == 1 ? 9 : 10;
            for (var i = 0; i < n; i++)
            {
                _ws.RotatePalletSize(Direction.Forward);
                ShowPalletSize();
            }
        }

        private void ShowPalletSize()
        {
            fieldPalletSize.Text = _ws.PalletSize.ToString();
        }

        private void buttonPalletSizeNext_Click(object sender, EventArgs e)
        {
            _ws.RotatePalletSize(Direction.Forward);
            ShowPalletSize();
        }

        private void buttonPalletSizePrev_Click(object sender, EventArgs e)
        {
            _ws.RotatePalletSize(Direction.Back);
            ShowPalletSize();
        }

        private void buttonSet40_Click(object sender, EventArgs e)
        {
            _ws.PalletSize = 40;
            ShowPalletSize();
        }

        private void buttonSet60_Click(object sender, EventArgs e)
        {
            _ws.PalletSize = 60;
            ShowPalletSize();

        }

        private void buttonSet120_Click(object sender, EventArgs e)
        {
            _ws.PalletSize = 120;
            ShowPalletSize();

        }

        private void buttonSet1_Click(object sender, EventArgs e)
        {
            _ws.PalletSize = 1;
            ShowPalletSize();
        }

        #endregion

        private void SetKneadingNumberForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                buttonClose_Click(sender, e);
            }
        }
    }
}