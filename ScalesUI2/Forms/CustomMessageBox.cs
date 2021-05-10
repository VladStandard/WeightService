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
            var mb = new CustomMessageBox
            {
                Owner = formOwner, 
                fieldMessage = {Text = label}, 
                Text = caption,
                DefaultButton = defaultButton,
            };

            var i = 0;
            foreach (var button in mb.flowLayoutPanel1.Controls.OfType<Button>())
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
                        mb.btYes.Visible = true;
                        mb.btNo.Visible = true;
                        break;
                    }
                case MessageBoxButtons.OK:
                    {
                        mb.btOk.Visible = true;
                        break;
                    }
                case MessageBoxButtons.OKCancel:
                    {
                        mb.btOk.Visible = true;
                        mb.btCancel.Visible = true;
                        break;
                    }
                case MessageBoxButtons.RetryCancel:
                    {
                        mb.btRetry.Visible = true;
                        mb.btCancel.Visible = true;
                        break;
                    }
                case MessageBoxButtons.AbortRetryIgnore:
                    {
                        mb.btAbort.Visible = true;
                        mb.btRetry.Visible = true;
                        mb.btIgnore.Visible = true;
                        break;
                    }
                case MessageBoxButtons.YesNoCancel:
                    {
                        mb.btYes.Visible = true;
                        mb.btNo.Visible = true;
                        mb.btCancel.Visible = true;
                        break;
                    }
                default:
                    {
                        mb.btOk.Visible = true;
                        break;
                    }
            }

            var f = new Font("Arial", 16, FontStyle.Bold);
            foreach (var button in mb.flowLayoutPanel1.Controls.OfType<Button>())
            {
                button.Height = 80;
                button.Width = 140;
                button.Font = f;
            }

            mb.ShowDialog();
            return mb.Result;
        }

        #endregion

        #region Public and private methods

        private void btYes_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Yes;
            Close();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            Result = DialogResult.OK;
            Close();
        }

        private void btRetry_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Retry;
            Close();
        }

        private void btNo_Click(object sender, EventArgs e)
        {
            Result = DialogResult.No;
            Close();
        }

        private void btIgnore_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Ignore;
            Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Cancel;
            Close();
        }

        private void btAbort_Click(object sender, EventArgs e)
        {
            Result = DialogResult.Abort;
            Close();
        }

        private void CustomMessageBox_Shown(object sender, EventArgs e)
        {
            //base.OnShown(e);
            if (Owner != null)
            // && StartPosition == FormStartPosition.Manual)
            {
                int offset = Owner.OwnedForms.Length * 38;  // approx. 10mm
                Point p = new Point(Owner.Left + Owner.Width / 2 - Width / 2 + offset, Owner.Top + Owner.Height / 2 - Height / 2 + offset);
                Location = p;
            }
        }

        private void CustomMessageBox_Load(object sender, EventArgs e)
        {
            TopMost = true;
        }

        #endregion
    }
}
