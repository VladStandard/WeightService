// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace WeightCore.Gui
{
    public partial class CustomMessageBox : Form
    {
        #region Public and private fields and properties

        public DialogResult Result { get; private set; } = DialogResult.None;
        private bool IsExit { get; set; }

        #endregion

        #region Constructor and destructor

        public CustomMessageBox()
        {
            InitializeComponent();
        }

        #endregion

        #region Public and private methods
        
        public static CustomMessageBox Show(IWin32Window owner, string label, string caption, MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon messageBoxIcon = MessageBoxIcon.Information, int buttonSelect = 0)
        {
            CustomMessageBox customMessageBox = new()
            {
                Owner = owner is Form form ? form : null,
                fieldMessage = { Text = label },
                Text = caption,
                IsExit = false,
            };
            ShowButtonsTranslate(customMessageBox, buttons);
            ShowButtonsVisible(customMessageBox, buttons);
            ShowButtonSelect(customMessageBox, buttonSelect);
            ShowButtonsResize(customMessageBox);
            ShowAlign(customMessageBox);

            return customMessageBox;
        }

        private static void ShowButtonsTranslate(CustomMessageBox customMessageBox, MessageBoxButtons buttons)
        {
            customMessageBox.buttonYes.Text = LocalizationData.Buttons.Yes;
            customMessageBox.buttonRetry.Text = LocalizationData.Buttons.Retry;
            customMessageBox.buttonNo.Text = LocalizationData.Buttons.No;
            customMessageBox.buttonIgnore.Text = LocalizationData.Buttons.Ignore;
            customMessageBox.buttonCancel.Text = LocalizationData.Buttons.Cancel;
            customMessageBox.buttonAbort.Text = LocalizationData.Buttons.Abort;
            customMessageBox.buttonOk.Text = LocalizationData.Buttons.Ok;
        }

        private static void ShowButtonsVisible(CustomMessageBox customMessageBox, MessageBoxButtons buttons)
        {
            switch (buttons)
            {
                case MessageBoxButtons.YesNo:
                    {
                        customMessageBox.buttonYes.Visible = true;
                        customMessageBox.buttonNo.Visible = true;
                        break;
                    }
                case MessageBoxButtons.OK:
                    {
                        customMessageBox.buttonOk.Visible = true;
                        break;
                    }
                case MessageBoxButtons.OKCancel:
                    {
                        customMessageBox.buttonOk.Visible = true;
                        customMessageBox.buttonCancel.Visible = true;
                        break;
                    }
                case MessageBoxButtons.RetryCancel:
                    {
                        customMessageBox.buttonRetry.Visible = true;
                        customMessageBox.buttonCancel.Visible = true;
                        break;
                    }
                case MessageBoxButtons.AbortRetryIgnore:
                    {
                        customMessageBox.buttonAbort.Visible = true;
                        customMessageBox.buttonRetry.Visible = true;
                        customMessageBox.buttonIgnore.Visible = true;
                        break;
                    }
                case MessageBoxButtons.YesNoCancel:
                    {
                        customMessageBox.buttonYes.Visible = true;
                        customMessageBox.buttonNo.Visible = true;
                        customMessageBox.buttonCancel.Visible = true;
                        break;
                    }
                default:
                    {
                        customMessageBox.buttonOk.Visible = true;
                        break;
                    }
            }
        }

        private static void ShowButtonSelect(CustomMessageBox customMessageBox, int buttonSelect)
        {
            int i = -1;
            foreach (Button button in customMessageBox.flowLayoutPanel.Controls.OfType<Button>())
            {
                if (button.Visible)
                    i++;
                if (buttonSelect == i)
                    button.Select();
            }
        }

        private static void ShowButtonsResize(CustomMessageBox customMessageBox)
        {
            foreach (Button button in customMessageBox.flowLayoutPanel.Controls.OfType<Button>())
            {
                button.Height = 80;
                button.Width = 140;
                button.Font = new("Arial", 16, FontStyle.Bold);
            }
        }

        private static void ShowAlign(CustomMessageBox customMessageBox)
        {
            if (customMessageBox.Owner != null)
            {
                customMessageBox.TopMost = customMessageBox.Owner.TopMost;
                customMessageBox.Show(customMessageBox.Owner);
            }
            else
            {
                customMessageBox.Show();
            }
        }

        public void Wait()
        {
            while (!IsExit)
            {
                Thread.Sleep(10);
                Application.DoEvents();
            }
        }

        private void OnYes_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Yes;
            IsExit = true;
            Close();
        }

        private void OnOk_Click(object sender, EventArgs e)
        {
            Result = DialogResult.OK;
            IsExit = true;
            Close();
        }

        private void OnRetry_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Retry;
            IsExit = true;
            Close();
        }

        private void OnNo_Click(object sender, EventArgs e)
        {
            Result = DialogResult.No;
            IsExit = true;
            Close();
        }

        private void OnIgnore_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Ignore;
            IsExit = true;
            Close();
        }

        private void OnCancel_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Cancel;
            IsExit = true;
            Close();
        }

        private void OnAbort_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Abort;
            IsExit = true;
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
