// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using ScalesMsi.Helpers;
using System;
using System.Linq;
using WixSharp;
using WixSharp.UI.Forms;
// ReSharper disable ArrangeTypeMemberModifiers

namespace ScalesMsi.Dialogs
{
    /// <summary>
    /// The standard Maintenance Type dialog
    /// </summary>
    public partial class MaintenanceTypeDialog : ManagedForm
    {
        #region Private fields and properties

        private Type ProgressDialog => Shell.Dialogs.FirstOrDefault(d => d.GetInterfaces().Contains(typeof(IProgressDialog)));

        #endregion

        #region Dialog methods

        /// <summary>
        /// Initializes a new instance of the <see cref="MaintenanceTypeDialog"/> class.
        /// </summary>
        public MaintenanceTypeDialog()
        {
            InitializeComponent();
            //label1.MakeTransparentOn(banner);
            //label2.MakeTransparentOn(banner);
        }

        private void ResetLayout()
        {
            // The form controls are properly anchored and will be correctly resized on parent form
            // resizing. However the initial sizing by WinForm runtime doesn't a do good job with DPI
            // other than 96. Thus manual resizing is the only reliable option apart from going WPF.
            //float ratio = banner.Image.Width / (float)banner.Image.Height;
            //topPanel.Height = (int)(banner.Width / ratio);
            topBorder.Top = topPanel.Height + 1;

            var upShift = (int)(next.Height * 2.3) - bottomPanel.Height;
            bottomPanel.Top -= upShift;
            bottomPanel.Height += upShift;

            middlePanel.Top = topBorder.Bottom + 5;
            middlePanel.Height = bottomPanel.Top - 5 - middlePanel.Top;
        }

        /// <summary>
        /// Локализация.
        /// </summary>
        private void Localization()
        {
            label1.Text = @"[MaintenanceTypeDlgTitle]";
            label2.Text = @"[MaintenanceTypeDlgDescription]";
            change.Text = @"[MaintenanceTypeDlgChangeButton]";
            label3.Text = @"[MaintenanceTypeDlgChangeText]";
            repair.Text = @"[MaintenanceTypeDlgRepairButton]";
            label4.Text = @"[MaintenanceTypeDlgRepairText]";
            remove.Text = @"[MaintenanceTypeDlgRemoveButton]";
            label5.Text = @"[MaintenanceTypeDlgRemoveText]";
            back.Text = @"[WixUIBack]";
            next.Text = @"[WixUINext]";
            cancel.Text = @"[WixUICancel]";

            Localize();
        }

        private void MaintenanceTypeDialog_Load(object sender, EventArgs e)
        {
            //banner.Image = Runtime.Session.GetResourceBitmap("WixUI_Bmp_Banner");

            ResetLayout();
            Localization();
        }

        #endregion

        #region Private methods

        private void change_Click(object sender, EventArgs e)
        {
            Runtime.Session["MODIFY_ACTION"] = "Change";
            Shell.GoNext();
        }

        private void repair_Click(object sender, EventArgs e)
        {
            Runtime.Session["MODIFY_ACTION"] = "Repair";
            int index = Shell.Dialogs.IndexOf(ProgressDialog);
            if (index != -1)
                Shell.GoTo(index);
            else
                Shell.GoNext();
        }

        private void remove_Click(object sender, EventArgs e)
        {
            Runtime.Session["REMOVE"] = "ALL";
            Runtime.Session["MODIFY_ACTION"] = "Remove";

            int index = Shell.Dialogs.IndexOf(ProgressDialog);
            if (index != -1)
                Shell.GoTo(index);
            else
                Shell.GoNext();
        }

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

        #endregion
    }
}