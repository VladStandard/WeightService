// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using DataCore.Sql.TableScaleModels;
using WeightCore.Helpers;

namespace WeightCore.XamlPages;

/// <summary>
/// Interaction logic for PageSqlSettings.xaml
/// </summary>
public partial class PageDevice
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Constructor.
	/// </summary>
	public PageDevice()
	{
		InitializeComponent();
		Setup();

		int i = 0;
		foreach (ScaleModel scale in UserSession.Scales)
		{
			if (UserSession.Scale.Equals(scale))
			{
				comboBoxScale.SelectedIndex = i;
				break;
			}
			i++;
		}
		if (comboBoxScale.SelectedIndex == -1)
			comboBoxScale.SelectedIndex = 0;

		i = 0;
		if (UserSession.Area.Identity.IsNew())
		{
			comboBoxArea.SelectedIndex = 0;
		}
		else
		{
			foreach (ProductionFacilityModel area in UserSession.Areas)
			{
				if (UserSession.Area.Equals(area))
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
	//    for (int row = 0; row < UserSession.Tasks.Count; row++)
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
	//            Content = UserSession.Tasks[row].TaskType.Name,
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
	//            Tag = UserSession.Tasks[row]
	//        };
	//        System.Windows.Controls.ComboBoxItem itemTrue = new() { Content = "True" };
	//        comboBoxTaskEnabled.Items.Add(itemTrue);
	//        System.Windows.Controls.ComboBoxItem itemFalse = new() { Content = "False" };
	//        comboBoxTaskEnabled.Items.Add(itemFalse);
	//        comboBoxTaskEnabled.SelectedItem = UserSession.Tasks[row].Enabled ? itemTrue : itemFalse;
	//        System.Windows.Controls.Grid.SetColumn(comboBoxTaskEnabled, 1);
	//        System.Windows.Controls.Grid.SetRow(comboBoxTaskEnabled, row);
	//        gridTasks.Children.Add(comboBoxTaskEnabled);
	//    }
	//    // Fill tab.
	//    //tabTasks.Content = gridTasks;
	//}

	private void ButtonApply_OnClick(object sender, RoutedEventArgs e)
	{
		UserSession.Setup(UserSession.Scale.Identity.Id, UserSession.Area.Name);
		Result = System.Windows.Forms.DialogResult.OK;
		OnClose?.Invoke(sender, e);
	}

	private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
	{
		if (UserSession.Scale.Host is not null)
		{
			UserSession.Setup(UserSession.Scale.Identity.Id, string.Empty);
		}
		Result = System.Windows.Forms.DialogResult.Cancel;
		OnClose?.Invoke(sender, e);
	}

	#endregion
}