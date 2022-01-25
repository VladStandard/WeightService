// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using WeightCore.Helpers;
using WeightCore.XamlPages;

namespace WeightCore.Gui
{
    public partial class WpfPageLoader : Form
    {
        #region Public and private fields and properties

        private DebugHelper Debug { get; set; } = DebugHelper.Instance;
        private ElementHost ElementHost { get; set; }
        private ExceptionHelper Exception { get; set; } = ExceptionHelper.Instance;
        private PagePluList PluList { get; set; }
        private SessionStateHelper SessionState { get; set; } = SessionStateHelper.Instance;
        public bool UseOwnerSize { get; set; }
        public MessageBoxEntity MessageBox { get; set; } = new MessageBoxEntity();
        public PageMessageBox PageMessageBoxItem { get; private set; }
        public PageSqlSettings SqlSettings { get; private set; }
        public ProjectsEnums.Page Page { get; private set; }

        #endregion

        #region Constructor and destructor

        public WpfPageLoader()
        {
            InitializeComponent();

            Page = ProjectsEnums.Page.Default;
            SessionState.IsWpfPageLoaderClose = false;
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
                Exception.Catch(this, ref ex, false);
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
                        //PluList.Loaded += PluListOnLoaded;
                        SessionState.WpfPageLoader_OnClose += WpfPageLoader_OnClose;
                        break;
                    case ProjectsEnums.Page.SqlSettings:
                        SqlSettings = new PageSqlSettings();
                        SqlSettings.InitializeComponent();
                        ElementHost.Child = SqlSettings;
                        //SqlSettings.Loaded += SqlSettingsOnLoaded;
                        SessionState.WpfPageLoader_OnClose += WpfPageLoader_OnClose;
                        break;
                    case ProjectsEnums.Page.MessageBox:
                        PageMessageBoxItem = new PageMessageBox();
                        PageMessageBoxItem.InitializeComponent();
                        ElementHost.Child = PageMessageBoxItem;
                        PageMessageBoxItem.MessageBox = MessageBox;
                        //PageMessageBoxItem.Loaded += MessageBoxOnLoaded;
                        SessionState.WpfPageLoader_OnClose += WpfPageLoader_OnClose;
                        break;
                    case ProjectsEnums.Page.Default:
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, false);
            }
        }

        private void WpfPageLoader_OnClose(object sender, RoutedEventArgs e)
        {
            try
            {
                SessionState.WpfPageLoader_OnClose -= WpfPageLoader_OnClose;
                Close();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, false);
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
                        {
                            DialogResult = PluList.Result;
                        }
                        break;
                    case ProjectsEnums.Page.SqlSettings:
                        if (SqlSettings != null)
                        {
                            DialogResult = SqlSettings.Result;
                        }
                        break;
                    case ProjectsEnums.Page.Default:
                    default:
                        DialogResult = DialogResult.Cancel;
                        break;
                }
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, false);
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
