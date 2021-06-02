// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using ScalesMsi.Helpers;
using WixSharp;
using WixSharp.UI.Forms;

namespace ScalesMsi.Dialogs
{
    /// <summary>
    /// The standard Setup Type dialog
    /// </summary>
    public partial class SetupTypeDialog : ManagedForm
    {
        #region Public and private fields and properties

        private Type ProgressDialog
        {
            get
            {
                return Shell.Dialogs
                    .FirstOrDefault(d => d.GetInterfaces().Contains(typeof(IProgressDialog)));
            }
        }
        // Помощник компонент.
        private WixSharpHelper _wixSharpHelper = WixSharpHelper.Instance;

        #endregion

        #region Dialog methods

        /// <summary>
        /// Initializes a new instance of the <see cref="SetupTypeDialog"/> class.
        /// </summary>
        public SetupTypeDialog()
        {
            InitializeComponent();
            label1.MakeTransparentOn(banner);
            label2.MakeTransparentOn(banner);
        }

        void SetupTypeDialog_Load(object sender, EventArgs e)
        {
            banner.Image = Runtime.Session.GetResourceBitmap("WixUI_Bmp_Banner");

            ResetLayout();
        }

        void ResetLayout()
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

            middlePanel.Top = topBorder.Bottom + 5;
            middlePanel.Height = bottomPanel.Top - 5 - middlePanel.Top;
        }

        #endregion

        #region Private methods

        private void typical_Click(object sender, EventArgs e)
        {
            int index = Shell.Dialogs.IndexOf(ProgressDialog);
            if (index != -1)
                Shell.GoTo(index);
            else
                Shell.GoNext();
        }

        private void custom_Click(object sender, EventArgs e)
        {
            Shell.GoNext();
        }

        private void complete_Click(object sender, EventArgs e)
        {
            string[] names = Runtime.Session.Features.Select(x => x.Name).ToArray();
            Runtime.Session["ADDLOCAL"] = names.JoinBy(",");

            // Заполнить настройку фич.
            _wixSharpHelper.SetSettingsFeatures(Runtime.Session.Features.Select(x => x.Name).ToList());

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