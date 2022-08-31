// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql;
using DataCore.Sql.TableScaleModels;
using System.Windows;
using WeightCore.Helpers;

namespace WeightCore.Gui.XamlPages;

/// <summary>
/// Interaction logic for PageSqlSettings.xaml
/// </summary>
public partial class PageScaleChange
{
    #region Public and private fields, properties, constructor

    private UserSessionHelper UserSession { get; } = UserSessionHelper.Instance;
    public SqlViewModelHelper SqlViewModel { get; }
    public System.Windows.Forms.DialogResult Result { get; private set; }
    public RoutedEventHandler OnClose { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PageScaleChange()
    {
        InitializeComponent();

        object context = FindResource("SqlViewModel");
        if (context is SqlViewModelHelper sqlViewModel)
        {
            SqlViewModel = sqlViewModel;
            //sqlViewModel = SqlViewModel = UserSessionHelper.Instance.SqlViewModel;
            int i = 0;
            foreach (string area in SqlViewModel.Areas)
            {
	            if (Equals(area, SqlViewModel.Area?.Name))
	            {
		            comboBoxArea.SelectedIndex = i;
		            break;
	            }
				i++;
            }
        }
    }

    #endregion

    #region Public and private methods

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        //UserSession.SqlViewModel.SetupTasks(UserSession.SqlViewModel.Scale.Identity.Id);
    }

    //[Obsolete(@"Deprecated method")]
    //private void CreateGridTasks()
    //{
    //    System.Windows.Controls.Grid gridTasks = new();

    //    // Columns.
    //    for (int col = 0; col < 2; col++)
    //    {
    //        System.Windows.Controls.ColumnDefinition column = new()
    //        {
    //            Width = new(1, GridUnitType.Star)
    //        };
    //        gridTasks.ColumnDefinitions.Add(column);
    //    }

    //    // Rows.
    //    for (int row = 0; row < UserSession.SqlViewModel.Tasks.Count; row++)
    //    {
    //        // Row.
    //        System.Windows.Controls.RowDefinition rows = new()
    //        {
    //            Height = new(1, GridUnitType.Star)
    //        };
    //        gridTasks.RowDefinitions.Add(rows);
    //        // Task caption.
    //        System.Windows.Controls.Label labelTaskCaption = new()
    //        {
    //            Content = UserSession.SqlViewModel.Tasks[row].TaskType.Name,
    //            HorizontalAlignment = HorizontalAlignment.Center,
    //            VerticalAlignment = VerticalAlignment.Center,
    //        };
    //        System.Windows.Controls.Grid.SetColumn(labelTaskCaption, 0);
    //        System.Windows.Controls.Grid.SetRow(labelTaskCaption, row);
    //        gridTasks.Children.Add(labelTaskCaption);
    //        // Task enabled.
    //        System.Windows.Controls.ComboBox comboBoxTaskEnabled = new()
    //        {
    //            Width = 100,
    //            Height = 30,
    //            HorizontalAlignment = HorizontalAlignment.Center,
    //            VerticalContentAlignment = VerticalAlignment.Center,
    //            Tag = UserSession.SqlViewModel.Tasks[row]
    //        };
    //        System.Windows.Controls.ComboBoxItem itemTrue = new() { Content = "True" };
    //        comboBoxTaskEnabled.Items.Add(itemTrue);
    //        System.Windows.Controls.ComboBoxItem itemFalse = new() { Content = "False" };
    //        comboBoxTaskEnabled.Items.Add(itemFalse);
    //        comboBoxTaskEnabled.SelectedItem = UserSession.SqlViewModel.Tasks[row].Enabled ? itemTrue : itemFalse;
    //        System.Windows.Controls.Grid.SetColumn(comboBoxTaskEnabled, 1);
    //        System.Windows.Controls.Grid.SetRow(comboBoxTaskEnabled, row);
    //        gridTasks.Children.Add(comboBoxTaskEnabled);
    //    }
    //    // Fill tab.
    //    //tabTasks.Content = gridTasks;
    //}

    private void ButtonApply_OnClick(object sender, RoutedEventArgs e)
    {
        string scaleDescription = comboBoxDevice.Items[comboBoxDevice.SelectedIndex].ToString();
        ScaleModel scale = SqlUtils.GetScale(scaleDescription);
        string areaName = comboBoxArea.Items[comboBoxArea.SelectedIndex].ToString();
        SqlViewModel.Setup(scale.Identity.Id, areaName);
        UserSessionHelper.Instance.SqlViewModel.Setup(scale.Identity.Id, areaName);
		Result = System.Windows.Forms.DialogResult.OK;
        OnClose.Invoke(sender, e);
    }

    private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
    {
        ScaleModel scale = SqlUtils.GetScaleFromHost(UserSession.SqlViewModel.Scale.Host.Identity.Id);
        SqlViewModel.Setup(scale.Identity.Id, "");
        UserSessionHelper.Instance.SqlViewModel.Setup(scale.Identity.Id, "");
		Result = System.Windows.Forms.DialogResult.Cancel;
        OnClose.Invoke(sender, e);
    }

    #endregion
}
