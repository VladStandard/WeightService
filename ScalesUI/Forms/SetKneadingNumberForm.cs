// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using System;
using System.Windows.Forms;
using WeightCore.Gui;
using WeightCore.Helpers;
using WeightCore.Models;

namespace ScalesUI.Forms
{
    public partial class SetKneadingNumberForm : Form
    {
        #region Private fields and properties

        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;
        private readonly SessionState _ws = SessionState.Instance;
        private int OldKneading { get; }
        private int OldPalletSize { get; }
        private DateTime OldProductDate { get; }

        #endregion

        #region Constructor and destructor

        public SetKneadingNumberForm()
        {
            InitializeComponent();

            OldKneading = _ws.Kneading;
            OldProductDate = _ws.ProductDate;
            OldPalletSize = _ws.LabelsCount;
            buttonOk.Select();
        }

        #endregion

        #region Public and private methods

        private void SetKneadingNumberForm_Load(object sender, EventArgs e)
        {
            try
            {
                TopMost = !_ws.IsDebug;
                Width = Owner.Width;
                Height = Owner.Height;
                Left = Owner.Left;
                Top = Owner.Top;

                StartPosition = FormStartPosition.CenterScreen;
                ShowPalletSize();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void ShowProductDate()
        {
            try
            {
                fieldProdDate.Text = _ws.ProductDate.ToString("dd.MM.yyyy");
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void ShowKneading()
        {
            try
            {
                fieldKneading.Text = _ws.Kneading.ToString();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void buttonKneadingLeft_Click(object sender, EventArgs e)
        {
            try
            {
                NumberInputForm numberInputForm = new();
                numberInputForm.InputValue = 0;// _ws.Kneading;
                if (numberInputForm.ShowDialog() == DialogResult.OK)
                {
                    _ws.Kneading = numberInputForm.InputValue;
                }
                ShowKneading();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void SetKneadingNumberForm_Shown(object sender, EventArgs e)
        {
            try
            {
                ShowKneading();
                ShowProductDate();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
                _ws.Kneading = OldKneading;
                _ws.ProductDate = OldProductDate;
                _ws.LabelsCount = OldPalletSize;
                Close();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void buttonDtRight_Click(object sender, EventArgs e)
        {
            try
            {
                _ws.RotateProductDate(ProjectsEnums.Direction.Forward);
                ShowProductDate();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void buttonDtLeft_Click(object sender, EventArgs e)
        {
            try
            {
                _ws.RotateProductDate(ProjectsEnums.Direction.Back);
                ShowProductDate();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void buttonPalletSize10_Click(object sender, EventArgs e)
        {
            try
            {
                int n = _ws.LabelsCount == 1 ? 9 : 10;
                for (int i = 0; i < n; i++)
                {
                    _ws.RotatePalletSize(ProjectsEnums.Direction.Forward);
                    ShowPalletSize();
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void ShowPalletSize()
        {
            try
            {
                fieldPalletSize.Text = _ws.LabelsCount.ToString();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void buttonPalletSizeNext_Click(object sender, EventArgs e)
        {
            try
            {
                _ws.RotatePalletSize(ProjectsEnums.Direction.Forward);
                ShowPalletSize();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void buttonPalletSizePrev_Click(object sender, EventArgs e)
        {
            try
            {
                _ws.RotatePalletSize(ProjectsEnums.Direction.Back);
                ShowPalletSize();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void buttonSet40_Click(object sender, EventArgs e)
        {
            SetLabelsCount(40);
        }

        private void buttonSet60_Click(object sender, EventArgs e)
        {
            SetLabelsCount(60);
        }

        private void buttonSet120_Click(object sender, EventArgs e)
        {
            SetLabelsCount(120);
        }

        private void buttonSet1_Click(object sender, EventArgs e)
        {
            SetLabelsCount(1);
        }
        
        private void SetLabelsCount(int count)
        {
            try
            {
                _ws.LabelsCount = count;
                ShowPalletSize();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }
        
        private void SetKneadingNumberForm_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    buttonClose_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        #endregion
    }
}
