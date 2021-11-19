// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WeightCore.Gui
{
    public partial class CustomMessageBox : Form
    {
        #region Public and private fields and properties

        public DialogResult Result { get; private set; } = DialogResult.None;

        #endregion

        #region Constructor and destructor

        public CustomMessageBox()
        {
            InitializeComponent();
        }

        #endregion

        #region Public and private methods

        public void Show(IWin32Window owner, string label, string caption, MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon messageBoxIcon = MessageBoxIcon.Information, int buttonSelect = 0)
        {
            Owner = owner is Form form ? form : null;
            fieldMessage.Text = label;
            Text = caption;

            ButtonsLocalization();
            SetButtonsVisible(buttons);
            SelectButton(buttonSelect);
            ResizeButtons();
            ShowAlign(false);
        }

        public void ShowDialog(IWin32Window owner, string label, string caption, MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon messageBoxIcon = MessageBoxIcon.Information, int buttonSelect = 0)
        {
            Owner = owner is Form form ? form : null;
            fieldMessage.Text = label;
            Text = caption;

            ButtonsLocalization();
            SetButtonsVisible(buttons);
            SelectButton(buttonSelect);
            ResizeButtons();
            ShowAlign(true);
        }

        private void ButtonsLocalization()
        {
            buttonYes.Text = LocalizationData.Buttons.Yes;
            buttonRetry.Text = LocalizationData.Buttons.Retry;
            buttonNo.Text = LocalizationData.Buttons.No;
            buttonIgnore.Text = LocalizationData.Buttons.Ignore;
            buttonCancel.Text = LocalizationData.Buttons.Cancel;
            buttonAbort.Text = LocalizationData.Buttons.Abort;
            buttonOk.Text = LocalizationData.Buttons.Ok;
        }

        private void SetButtonsVisible(MessageBoxButtons buttons)
        {
            switch (buttons)
            {
                case MessageBoxButtons.YesNo:
                    {
                        buttonYes.Visible = true;
                        buttonNo.Visible = true;
                        break;
                    }
                case MessageBoxButtons.OKCancel:
                    {
                        buttonOk.Visible = true;
                        buttonCancel.Visible = true;
                        break;
                    }
                case MessageBoxButtons.RetryCancel:
                    {
                        buttonRetry.Visible = true;
                        buttonCancel.Visible = true;
                        break;
                    }
                case MessageBoxButtons.AbortRetryIgnore:
                    {
                        buttonAbort.Visible = true;
                        buttonRetry.Visible = true;
                        buttonIgnore.Visible = true;
                        break;
                    }
                case MessageBoxButtons.YesNoCancel:
                    {
                        buttonYes.Visible = true;
                        buttonNo.Visible = true;
                        buttonCancel.Visible = true;
                        break;
                    }
                default:
                    {
                        buttonOk.Visible = true;
                        break;
                    }
            }
        }

        private void SelectButton(int buttonSelect)
        {
            List<Button> buttons = new();
            foreach (object control in Controls.OfType<Button>())
            {
                if (control is Button button)
                {
                    if (button.Visible)
                        buttons.Add(button);
                }
            }
            if (buttons.Count() == 1)
            {
                buttons.First().Select();
                return;
            }

            int i = -1;
            foreach (Button button in buttons)
            {
                if (button.Visible)
                    i++;
                if (buttonSelect == i)
                    button.Select();
            }
        }

        private void ResizeButtons()
        {
            foreach (Button button in Controls.OfType<Button>())
            {
                button.Height = 80;
                button.Width = 140;
                button.Font = new("Arial", 16, FontStyle.Bold);
            }
        }

        private void ShowAlign(bool isDialog)
        {
            if (Owner != null)
            {
                TopMost = Owner.TopMost;
                if (isDialog)
                    ShowDialog(Owner);
                else
                    Show(Owner);
            }
            else
            {
                Show();
            }
        }

        private void OnYes_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Yes;
            Close();
        }

        private void OnOk_Click(object sender, EventArgs e)
        {
            Result = DialogResult.OK;
            Close();
        }

        private void OnRetry_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Retry;
            Close();
        }

        private void OnNo_Click(object sender, EventArgs e)
        {
            Result = DialogResult.No;
            Close();
        }

        private void OnIgnore_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Ignore;
            Close();
        }

        private void OnCancel_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Cancel;
            Close();
        }

        private void OnAbort_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Abort;
            Close();
        }

        private void CustomMessageBox_Shown(object sender, EventArgs e)
        {
            if (Owner != null)
            {
                int offset = Owner.OwnedForms.Length * 38;
                Location = new Point(
                    Owner.Left + Owner.Width / 2 - Width / 2 + offset,
                    Owner.Top + Owner.Height / 2 - Height / 2 + offset);
            }
        }

        private void CustomMessageBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                OnCancel_Click(sender, e);
            }
        }

        #endregion
    }
}
