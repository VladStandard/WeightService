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

        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;
        private readonly SessionStateHelper _sessionState = SessionStateHelper.Instance;
        private readonly DebugHelper _debug = DebugHelper.Instance;
        public bool UseOwnerSize { get; set; }
        public ProjectsEnums.Page Page { get; private set; }
        private ElementHost ElementHost { get; set; }
        private PagePluList PluList { get; set; }
        public PageSqlSettings SqlSettings { get; private set; }
        public PageMessageBox PageMessageBoxItem { get; private set; }
        public MessageBoxEntity MessageBox { get; set; } = new MessageBoxEntity();

        #endregion

        #region Constructor and destructor

        public WpfPageLoader()
        {
            InitializeComponent();

            Page = ProjectsEnums.Page.Default;
            _sessionState.IsWpfPageLoaderClose = false;
        }

        public WpfPageLoader(ProjectsEnums.Page page, bool useOwnerSize) : this()
        {
            try
            {
                Page = page;
                UseOwnerSize = useOwnerSize;
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        #endregion

        #region Public and private methods

        private void WpfPageLoader_Load(object sender, EventArgs e)
        {
            try
            {
                // Own GUI.
                //TopMost = !_debug.IsDebug;

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
                        Left = Owner.Left + Owner.Width / 2 - Width / 2;
                        Top = Owner.Top + Owner.Height / 2 - Height / 2;
                    }
                }
                else
                {
                    Screen screen = Screen.FromHandle(Process.GetCurrentProcess().MainWindowHandle);
                    System.Drawing.Rectangle workingRectangle = screen.WorkingArea;
                    Left = workingRectangle.Left + workingRectangle.Width / 2 - Width / 2;
                    Top = workingRectangle.Top + workingRectangle.Height / 2 - Height / 2;
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
                        PluList.Loaded += PluListOnLoaded;
                        _sessionState.WpfPageLoader_OnClose += WpfPageLoader_OnClose;
                        break;
                    case ProjectsEnums.Page.SqlSettings:
                        SqlSettings = new PageSqlSettings();
                        SqlSettings.InitializeComponent();
                        ElementHost.Child = SqlSettings;
                        SqlSettings.Loaded += SqlSettingsOnLoaded;
                        _sessionState.WpfPageLoader_OnClose += WpfPageLoader_OnClose;
                        break;
                    case ProjectsEnums.Page.MessageBox:
                        PageMessageBoxItem = new PageMessageBox();
                        PageMessageBoxItem.InitializeComponent();
                        ElementHost.Child = PageMessageBoxItem;
                        PageMessageBoxItem.MessageBox = MessageBox;
                        PageMessageBoxItem.Loaded += MessageBoxOnLoaded;
                        _sessionState.WpfPageLoader_OnClose += WpfPageLoader_OnClose;
                        break;
                    case ProjectsEnums.Page.Default:
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void PluListOnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // 
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void SqlSettingsOnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // 
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void MessageBoxOnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void WpfPageLoader_OnClose(object sender, RoutedEventArgs e)
        {
            try
            {
                _sessionState.WpfPageLoader_OnClose -= WpfPageLoader_OnClose;
                Close();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
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
                _exception.Catch(this, ref ex);
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
