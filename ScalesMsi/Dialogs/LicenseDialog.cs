// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WixSharp;
using WixSharp.UI.Forms;

namespace ScalesMsi.Dialogs
{
    /// <summary>
    /// The standard Licence dialog
    /// </summary>
    public partial class LicenseDialog : ManagedForm
    {
        #region Dialog methods

        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseDialog"/> class.
        /// </summary>
        public LicenseDialog()
        {
            InitializeComponent();
            titleLbl.MakeTransparentOn(banner);
            label2.MakeTransparentOn(banner);
        }

        private void LicenseDialog_Load(object sender, EventArgs e)
        {
            banner.Image = Runtime.Session.GetResourceBitmap("WixUI_Bmp_Banner");
            agreement.Rtf = Runtime.Session.GetResourceString("WixSharp_LicenceFile");
            accepted.Checked = Runtime.Session["LastLicenceAcceptedChecked"] == "True";

            ResetLayout();
        }

        private void ResetLayout()
        {
            // The form controls are properly anchored and will be correctly resized on parent form
            // resizing. However the initial sizing by WinForm runtime doesn't a do good job with DPI
            // other than 96. Thus manual resizing is the only reliable option apart from going WPF.
            float ratio = banner.Image.Width / (float)banner.Image.Height;
            topPanel.Height = (int)(banner.Width / ratio);
            topBorder.Top = topPanel.Height + 1;

            var upShift = (int)(next.Height * 2.3) - bottomPanel.Height;
            bottomPanel.Top -= upShift;
            bottomPanel.Height += upShift;

            middlePanel.Top = topBorder.Bottom + 1;
            middlePanel.Height = bottomPanel.Top - 1 - middlePanel.Top;
        }

        #endregion

        #region Private methods

        private void back_Click(object sender, EventArgs e)
        {
            Shell.GoPrev();
        }

        private void next_Click(object sender, EventArgs e)
        {
            Shell.GoNext();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Shell.Cancel();
        }

        private void accepted_CheckedChanged(object sender, EventArgs e)
        {
            next.Enabled = accepted.Checked;
            Runtime.Session["LastLicenceAcceptedChecked"] = accepted.Checked.ToString();
        }

        private void print_Click(object sender, EventArgs e)
        {
            try
            {
                var file = Path.Combine(Path.GetTempPath(), Runtime.Session.Property("ProductName") + ".licence.rtf");
                System.IO.File.WriteAllText(file, agreement.Rtf);
                Process.Start(file);
            }
            catch
            {
                //Catch all, we don't want the installer to crash in an
                //attempt to write to a file.
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var data = new DataObject();

                if (agreement.SelectedText.Length > 0)
                {
                    data.SetData(DataFormats.UnicodeText, agreement.SelectedText);
                    data.SetData(DataFormats.Rtf, agreement.SelectedRtf);
                }
                else
                {
                    data.SetData(DataFormats.Rtf, agreement.Rtf);
                    data.SetData(DataFormats.Text, agreement.Text);
                }

                Clipboard.SetDataObject(data);
            }
            catch
            {
                //Catch all, we don't want the installer to crash in an
                //attempt at setting data on the clipboard.
            }
        }

        #endregion
    }
}