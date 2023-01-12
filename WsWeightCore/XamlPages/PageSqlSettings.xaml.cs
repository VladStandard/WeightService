// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WeightCore.XamlPages;

/// <summary>
/// Interaction logic for PageSettings.xaml
/// </summary>
public partial class PageSqlSettings
{
	#region Public and private fields, properties, constructor

	public PageSqlSettings()
	{
		InitializeComponent();
	}

	#endregion

	#region Public methods

	private void PageSettings_OnLoaded(object sender, RoutedEventArgs e)
	{
		ButtonDefault_Click(sender, e);
	}

	private void ButtonDefault_Click(object sender, RoutedEventArgs e)
	{
		ButtonSqlLoadConfig_Click(sender, e);
	}

	private void fieldSqlIntegratedSecurity_Checked(object sender, RoutedEventArgs e)
	{
		//if (sender is CheckBox field)
		//{
		//    LabeldSqlUser.IsEnabled = fieldSqlUser.IsEnabled =
		//        LabeldSqlPassword.IsEnabled = fieldSqlPassword.IsEnabled = !(field.IsChecked == true);
		//}
		//// Изменилось поле SQL.
		//fieldSql_TextChanged(sender, null);
	}

	private void SqlLoadGui()
	{
		//fieldSqlServer.TextChanged -= fieldSql_TextChanged;
		//fieldSqlDb.TextChanged -= fieldSql_TextChanged;
		//fieldSqlUser.TextChanged -= fieldSql_TextChanged;
		//fieldSqlPassword.TextChanged -= fieldSql_TextChanged;
		//fieldSqlIntegratedSecurity.Checked -= fieldSqlIntegratedSecurity_Checked;

		//fieldSqlServer.Text = _settings.SqlHelp.Authentication.Server;
		//fieldSqlDb.Text = _settings.SqlHelp.Authentication.Database;
		//fieldSqlUser.Text = _settings.SqlHelp.Authentication.UserId;
		//fieldSqlPassword.Text = _settings.SqlHelp.Authentication.Password;
		//fieldSqlIntegratedSecurity.IsChecked = _settings.SqlHelp.Authentication.IntegratedSecurity;

		//fieldSqlServer.TextChanged += fieldSql_TextChanged;
		//fieldSqlDb.TextChanged += fieldSql_TextChanged;
		//fieldSqlUser.TextChanged += fieldSql_TextChanged;
		//fieldSqlPassword.TextChanged += fieldSql_TextChanged;
		//fieldSqlIntegratedSecurity.Checked += fieldSqlIntegratedSecurity_Checked;
	}

	private void fieldSql_TextChanged(object sender, TextChangedEventArgs e)
	{
		//_settings.SqlHelp.Authentication.Server = fieldSqlServer.Text;
		//_settings.SqlHelp.Authentication.Database = fieldSqlDb.Text;
		//_settings.SqlHelp.Authentication.UserId = fieldSqlUser.Text;
		//_settings.SqlHelp.Authentication.Password = fieldSqlPassword.Text;
		//_settings.SqlHelp.Authentication.IntegratedSecurity = fieldSqlIntegratedSecurity.IsChecked == true;
		//fieldSqlConnectionString.Text = _settings.SqlHelp.Authentication.AsString(ConStringLevel.Middle);
	}

	private void ButtonSqlLoadConfig_Click(object sender, RoutedEventArgs e)
	{
		SqlLoadGui();
		//fieldSqlConnectionString.Text = _settings.SqlHelp.Authentication.AsString(ConStringLevel.Middle);
	}

	private void SetGuiSqlEnabled(bool enabled)
	{
		//MDSoft.WpfUtils.InvokeControl.SetIsEnabled(LabeldSqlConnectionString, enabled);
		//MDSoft.WpfUtils.InvokeControl.SetIsEnabled(fieldSqlConnectionString, enabled);
		//MDSoft.WpfUtils.InvokeControl.SetIsEnabled(LabeldSqlServer, enabled);
		//MDSoft.WpfUtils.InvokeControl.SetIsEnabled(fieldSqlServer, enabled);
		//MDSoft.WpfUtils.InvokeControl.SetIsEnabled(LabeldSqlDb, enabled);
		//MDSoft.WpfUtils.InvokeControl.SetIsEnabled(fieldSqlDb, enabled);
		//MDSoft.WpfUtils.InvokeControl.SetIsEnabled(LabeldSqlUser, enabled);
		//MDSoft.WpfUtils.InvokeControl.SetIsEnabled(fieldSqlUser, enabled);
		//MDSoft.WpfUtils.InvokeControl.SetIsEnabled(LabeldSqlPassword, enabled);
		//MDSoft.WpfUtils.InvokeControl.SetIsEnabled(fieldSqlPassword, enabled);
		//MDSoft.WpfUtils.InvokeControl.SetIsEnabled(fieldSqlIntegratedSecurity, enabled);
	}

	private void ButtonSqlCheckConnect_Click(object sender, RoutedEventArgs e)
	{
		SqlCheckConnectAsync();
	}

	private Task SqlCheckConnectAsync()
	{
		return Task.Run(() =>
		{
			//MDSoft.WpfUtils.InvokeControl.SetBackground(fieldSqlConnectionString, Brushes.Yellow);
			// Задать доступ к GUI SQL.
			SetGuiSqlEnabled(false);
			// Проверить SQL-подключение.
			//if (_settings.AppHelp.SqlConCheck(Lang.Russian))
			//{
			//    WPF.Utils.InvokeControl.SetBackground(fieldSqlConnectionString, Brushes.Transparent);
			//}
			//// Задать доступ к GUI SQL.
			//SetGuiSqlEnabled(true);
			//MessageBox.Show(_settings.SqlHelp.Status);
		});
	}

	#endregion
}