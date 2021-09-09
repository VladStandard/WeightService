// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
        public bool IsExit { get; set; }

        #endregion

        #region Constructor and destructor

        public CustomMessageBox()
        {
            InitializeComponent();
        }

        #endregion

        #region Public and private methods - static Show
        
        //public static DialogResult Show(IWin32Window owner, string label, MessageBoxButtons buttons = MessageBoxButtons.OK)
        //{
        //    return Show(owner, label, "", buttons);
        //}

        public static CustomMessageBox Show(IWin32Window owner, string label, string caption, MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon messageBoxIcon = MessageBoxIcon.Information, int selectButton = 0)
        {
            CustomMessageBox messageBox = new()
            {
                Owner = owner is Form form ? form : null,
                fieldMessage = { Text = label },
                Text = caption,
                IsExit = false,
            };

            switch (buttons)
            {
                case MessageBoxButtons.YesNo:
                    {
                        messageBox.btYes.Visible = true;
                        messageBox.btNo.Visible = true;
                        break;
                    }
                case MessageBoxButtons.OK:
                    {
                        messageBox.btOk.Visible = true;
                        break;
                    }
                case MessageBoxButtons.OKCancel:
                    {
                        messageBox.btOk.Visible = true;
                        messageBox.btCancel.Visible = true;
                        break;
                    }
                case MessageBoxButtons.RetryCancel:
                    {
                        messageBox.btRetry.Visible = true;
                        messageBox.btCancel.Visible = true;
                        break;
                    }
                case MessageBoxButtons.AbortRetryIgnore:
                    {
                        messageBox.btAbort.Visible = true;
                        messageBox.btRetry.Visible = true;
                        messageBox.btIgnore.Visible = true;
                        break;
                    }
                case MessageBoxButtons.YesNoCancel:
                    {
                        messageBox.btYes.Visible = true;
                        messageBox.btNo.Visible = true;
                        messageBox.btCancel.Visible = true;
                        break;
                    }
                default:
                    {
                        messageBox.btOk.Visible = true;
                        break;
                    }
            }

            int i = -1;
            foreach (Button button in messageBox.flowLayoutPanel1.Controls.OfType<Button>())
            {
                if (button.Visible)
                    i++;
                if (selectButton == i)
                    button.Select();
            }

            Font f = new("Arial", 16, FontStyle.Bold);
            foreach (Button button in messageBox.flowLayoutPanel1.Controls.OfType<Button>())
            {
                button.Height = 80;
                button.Width = 140;
                button.Font = f;
            }

            // Выровнять.
            messageBox.StartPosition = FormStartPosition.CenterScreen;
            if (messageBox.Owner != null)
            {
                messageBox.TopMost = messageBox.Owner.TopMost;
                //messageBox.Width = messageBox.Owner.Width;
                //messageBox.Height = messageBox.Owner.Height;
                //messageBox.Left = messageBox.Owner.Left;
                //messageBox.Top = messageBox.Owner.Top;
                messageBox.Show(messageBox.Owner);
            }
            else
            {
                messageBox.Show();
            }
            return messageBox;
        }

        #endregion

        #region Public and private methods

        public void Wait()
        {
            while (!IsExit)
            {
                Application.DoEvents();
                Thread.Sleep(10);
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
