using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ScalesUI.Forms
{
    public partial class CustomMessageBox : Form
    {
        #region Public and private fields and properties

        public DialogResult Result { get; private set; } = DialogResult.None;
        public MessageBoxDefaultButton DefaultButton { get; private set; } = MessageBoxDefaultButton.Button1;

        #endregion

        #region Constructor and destructor

        public CustomMessageBox()
        {
            InitializeComponent();
        }

        #endregion

        #region Public and private methods - static Show
        
        public static DialogResult Show(IWin32Window owner, string label, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            if (owner is Form form)
                return Show(form, label, "", buttons, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            return Show(label, "", buttons);
        }

        public static DialogResult Show(string label, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            return Show(label, "", buttons);
        }

        public static DialogResult Show(string label, string caption, MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon messageBoxIcon = MessageBoxIcon.Information, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            return Show(null, label, caption, buttons, messageBoxIcon, defaultButton);
        }

        public static DialogResult Show(Form formOwner, string label, string caption, MessageBoxButtons buttons,
            MessageBoxIcon messageBoxIcon, MessageBoxDefaultButton defaultButton)
        {
            var messageBox = new CustomMessageBox
            {
                Owner = formOwner, 
                fieldMessage = {Text = label}, 
                Text = caption,
                DefaultButton = defaultButton,
            };

            var i = 0;
            foreach (var button in messageBox.flowLayoutPanel1.Controls.OfType<Button>())
            {
                button.Visible = false;
                if (defaultButton == MessageBoxDefaultButton.Button1 && i == 0)
                    button.Select();
                else if (defaultButton == MessageBoxDefaultButton.Button2 && i == 1)
                    button.Select();
                else if (defaultButton == MessageBoxDefaultButton.Button3 && i == 2)
                    button.Select();
                i++;
            }

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

            var f = new Font("Arial", 16, FontStyle.Bold);
            foreach (var button in messageBox.flowLayoutPanel1.Controls.OfType<Button>())
            {
                button.Height = 80;
                button.Width = 140;
                button.Font = f;
            }

            messageBox.ShowDialog();
            return messageBox.Result;
        }

        #endregion

        #region Public and private methods

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
                var offset = Owner.OwnedForms.Length * 38;
                Location = new Point(
                    Owner.Left + Owner.Width / 2 - Width / 2 + offset, 
                    Owner.Top + Owner.Height / 2 - Height / 2 + offset);
            }
        }

        #endregion
    }
}
