// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using WeightCore.Helpers;
using WeightCore.XamlPages;

namespace WeightCore.Gui;
#nullable enable

public partial class WpfPageLoader : Form
{
    #region Public and private fields and properties

    private UserSessionHelper UserSession => UserSessionHelper.Instance;
    private ElementHost ElementHost { get; }
    private bool UseOwnerSize { get; }
    public MessageBoxModel MessageBox { get; }
    private PageMessageBox? PageMessageBox { get; set; }
    private PagePinCode? PagePinCode { get; set; }
    public PageDevice? PageDevice { get; private set; }
    private PagePluBundleFk? PagePluBundleFk { get; set; }
	private PageSqlSettings? PageSqlSettings { get; set; }
    private DataCore.Models.PageEnum Page { get; }

    #endregion

    #region Constructor and destructor

    private WpfPageLoader()
    {
        InitializeComponent();

        ElementHost = new() { Dock = DockStyle.Fill };
        MessageBox = new();
        Page = DataCore.Models.PageEnum.Default;
    }

    public WpfPageLoader(DataCore.Models.PageEnum page, bool useOwnerSize, FormBorderStyle formBorderStyle = FormBorderStyle.None,
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
            GuiUtils.WpfForm.CatchException(ex, this, true, true, true);
        }
    }

    #endregion

    #region Public and private methods

    private void WpfPageLoader_Load(object sender, EventArgs e)
    {
        try
        {
            // Own GUI.
            TopMost = !UserSession.Debug.IsDebug;

            if (Owner is not null)
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
            if (Page is not DataCore.Models.PageEnum.Default)
                panelMain.Controls.Add(ElementHost);
            SetElementHostChild();
        }
        catch (Exception ex)
        {
            GuiUtils.WpfForm.CatchException(ex, this, true, true, true);
        }
    }

    private void SetElementHostChild()
    {
        switch (Page)
        {
            case DataCore.Models.PageEnum.MessageBox:
                PageMessageBox = new();
                PageMessageBox.InitializeComponent();
                ElementHost.Child = PageMessageBox;
                PageMessageBox.MessageBox = MessageBox;
                PageMessageBox.OnClose += WpfPageLoader_OnClose;
                break;
            case DataCore.Models.PageEnum.Device:
                PageDevice = new();
                PageDevice.InitializeComponent();
                ElementHost.Child = PageDevice;
                PageDevice.OnClose += WpfPageLoader_OnClose;
                break;
            case DataCore.Models.PageEnum.PluBundleFk:
                PagePluBundleFk = new();
                PagePluBundleFk.InitializeComponent();
                ElementHost.Child = PagePluBundleFk;
                PagePluBundleFk.OnClose += WpfPageLoader_OnClose;
                break;
            case DataCore.Models.PageEnum.PinCode:
                PagePinCode = new();
                PagePinCode.InitializeComponent();
                ElementHost.Child = PagePinCode;
                PagePinCode.OnClose += WpfPageLoader_OnClose;
                break;
            case DataCore.Models.PageEnum.SqlSettings:
                PageSqlSettings = new();
                PageSqlSettings.InitializeComponent();
                ElementHost.Child = PageSqlSettings;
                PageSqlSettings.OnClose += WpfPageLoader_OnClose;
                break;
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
            GuiUtils.WpfForm.CatchException(ex, this, true, true, true);
        }
    }

    private void WpfPageLoader_FormClosed(object sender, FormClosedEventArgs e)
    {
        try
        {
	        DialogResult = Page switch
	        {
		        DataCore.Models.PageEnum.MessageBox => MessageBox.Result,
		        DataCore.Models.PageEnum.Device => PageDevice?.Result ?? DialogResult.Cancel,
		        DataCore.Models.PageEnum.PluBundleFk => PagePluBundleFk?.Result ?? DialogResult.Cancel,
                DataCore.Models.PageEnum.PinCode => PagePinCode?.Result ?? DialogResult.Cancel,
                DataCore.Models.PageEnum.SqlSettings => PageSqlSettings?.Result ?? DialogResult.Cancel,
                _ => DialogResult
	        };
        }
        catch (Exception ex)
        {
            GuiUtils.WpfForm.CatchException(ex, this, true, true, true);
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
