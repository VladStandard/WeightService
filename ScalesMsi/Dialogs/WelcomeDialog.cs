// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using WixSharp.UI.Forms;

namespace ScalesMsi.Dialogs
{
    /// <summary>
    /// The standard Welcome dialog
    /// </summary>
    public partial class WelcomeDialog : ManagedForm
    {
        #region Dialog methods

        /// <summary>
        /// Initializes a new instance of the <see cref="WelcomeDialog"/> class.
        /// </summary>
        public WelcomeDialog()
        {
            InitializeComponent();
        }

        private void WelcomeDialog_Load(object sender, EventArgs e)
        {
            image.Image = Runtime.Session.GetResourceBitmap("WixUI_Bmp_Dialog");

            ResetLayout();
        }

        private void ResetLayout()
        {
            // The form controls are properly anchored and will be correctly resized on parent form
            // resizing. However the initial sizing by WinForm runtime doesn't a do good job with DPI
            // other than 96. Thus manual resizing is the only reliable option apart from going WPF.

            var bHeight = (int)(next.Height * 2.3);

            var upShift = bHeight - bottomPanel.Height;
            bottomPanel.Top -= upShift;
            bottomPanel.Height = bHeight;

            imgPanel.Height = ClientRectangle.Height - bottomPanel.Height;
            float height = image.Image.Height;
            float ratio = image.Image.Width / height;
            image.Width = (int)(image.Height * ratio);

            textPanel.Left = image.Right + 5;
            textPanel.Width = bottomPanel.Width - image.Width - 10;
        }

        #endregion

        #region Private methods

        private void Cancel_Click(object sender, EventArgs e)
        {
            Shell.Cancel();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            Shell.GoNext();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Shell.GoPrev();
        }

        #endregion
    }
}