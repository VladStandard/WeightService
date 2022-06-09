// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using WeightCore.Gui.XamlPages;
using WeightCore.Helpers;

namespace WeightCore.Gui
{
    public partial class WpfPageLoader : Form
    {
        #region Public and private fields and properties

        private DebugHelper Debug { get; } = DebugHelper.Instance;
        private ElementHost ElementHost { get; set; }
        private PagePluList PluList { get; set; }
        public bool UseOwnerSize { get; }
        public MessageBoxEntity MessageBox { get; } = new MessageBoxEntity();
        public PageMessageBox PageMessageBox { get; private set; }
        public PagePinCode PagePinCode { get; set; }
        public PageScaleChange PageScaleChange { get; private set; }
        public PageSqlSettings PageSqlSettings { get; set; }
        public ProjectsEnums.Page Page { get; }

        #endregion

        #region Constructor and destructor

        public WpfPageLoader()
        {
            InitializeComponent();

            Page = ProjectsEnums.Page.Default;
        }

        public WpfPageLoader(ProjectsEnums.Page page, bool useOwnerSize, FormBorderStyle formBorderStyle = FormBorderStyle.None,
            double fontSizeCaption = 30, double fontSizeMessage = 26, double fontSizeButton = 22,
            ushort sizeCaption = 1, ushort sizeMessage = 5, ushort sizeButton = 1) : this()
        {
            try
            {
                Page = page;
                FormBorderStyle = formBorderStyle;
                UseOwnerSize = useOwnerSize;
                MessageBox.FontSizeCaption = fontSizeCaption;
                MessageBox.FontSizeMessage = fontSizeMessage;
                MessageBox.FontSizeButton = fontSizeButton;
                MessageBox.SizeCaption = sizeCaption;
                MessageBox.SizeMessage = sizeMessage;
                MessageBox.SizeButton = sizeButton;
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        #endregion

        #region Public and private methods

        private void WpfPageLoader_Load(object sender, EventArgs e)
        {
            try
            {
                // Own GUI.
                TopMost = !Debug.IsDebug;

                if (Owner != null)
                {
                    if (UseOwnerSize)
                    {
                        Width = Owner.Width;
                        Height = Owner.Height;
                        Left = Owner.Left;
                        Top = Owner.Top;
                    }
                    else
                    {
                        Left = Owner.Left + (Owner.Width / 2) - (Width / 2);
                        Top = Owner.Top + (Owner.Height / 2) - (Height / 2);
                    }
                }
                else
                {
                    Screen screen = Screen.FromHandle(Process.GetCurrentProcess().MainWindowHandle);
                    System.Drawing.Rectangle workingRectangle = screen.WorkingArea;
                    Left = workingRectangle.Left + (workingRectangle.Width / 2) - (Width / 2);
                    Top = workingRectangle.Top + (workingRectangle.Height / 2) - (Height / 2);
                }

                // WPF element.
                if (Page != ProjectsEnums.Page.Default)
                {
                    ElementHost = new ElementHost { Dock = DockStyle.Fill };
                    panelMain.Controls.Add(ElementHost);
                }
                switch (Page)
                {
                    case ProjectsEnums.Page.PluList:
                        PluList = new PagePluList();
                        PluList.InitializeComponent();
                        ElementHost.Child = PluList;
                        PluList.OnClose += WpfPageLoader_OnClose;
                        break;
                    case ProjectsEnums.Page.ScaleChange:
                        PageScaleChange = new PageScaleChange();
                        PageScaleChange.InitializeComponent();
                        ElementHost.Child = PageScaleChange;
                        PageScaleChange.OnClose += WpfPageLoader_OnClose;
                        break;
                    case ProjectsEnums.Page.MessageBox:
                        PageMessageBox = new PageMessageBox();
                        PageMessageBox.InitializeComponent();
                        ElementHost.Child = PageMessageBox;
                        PageMessageBox.MessageBox = MessageBox;
                        PageMessageBox.OnClose += WpfPageLoader_OnClose;
                        break;
                    case ProjectsEnums.Page.PinCode:
                        PagePinCode = new PagePinCode();
                        PagePinCode.InitializeComponent();
                        ElementHost.Child = PagePinCode;
                        PagePinCode.OnClose += WpfPageLoader_OnClose;
                        break;
                    case ProjectsEnums.Page.SqlSettings:
                        PageSqlSettings = new PageSqlSettings();
                        PageSqlSettings.InitializeComponent();
                        ElementHost.Child = PageSqlSettings;
                        PageSqlSettings.OnClose += WpfPageLoader_OnClose;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void WpfPageLoader_OnClose(object sender, RoutedEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void WpfPageLoader_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                switch (Page)
                {
                    case ProjectsEnums.Page.PluList:
                        if (PluList != null)
                            DialogResult = PluList.Result;
                        break;
                    case ProjectsEnums.Page.ScaleChange:
                        if (PageScaleChange != null)
                            DialogResult = PageScaleChange.Result;
                        break;
                    case ProjectsEnums.Page.MessageBox:
                        if (MessageBox != null)
                            DialogResult = MessageBox.Result;
                        break;
                    case ProjectsEnums.Page.PinCode:
                        if (PagePinCode != null)
                            DialogResult = PagePinCode.Result;
                        break;
                    case ProjectsEnums.Page.SqlSettings:
                        if (PageSqlSettings != null)
                            DialogResult = PageSqlSettings.Result;
                        break;
                    case ProjectsEnums.Page.Default:
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void WpfPageLoader_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        #endregion
    }
}
