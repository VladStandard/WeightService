// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Controls;

namespace WeightCore.XamlPages;

/// <summary>
/// Interaction logic for PagePin.xaml
/// </summary>
public partial class PagePinCode
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Constructor.
	/// </summary>
	public PagePinCode()
	{
		InitializeComponent();
	}

	#endregion

	#region Private methods

	private void PagePin_OnLoaded(object sender, RoutedEventArgs e)
	{
		// Очистить.
		ButtonClear_Click(sender, e);
	}

	private void ButtonNum_Click(object sender, EventArgs e)
	{
		if (sender is Button button)
		{
			string num = (string)button.Content;
			//_settings.PinCode.Input = int.Parse(_settings.PinCode.Input.ToString() + num);
		}
	}

	private void ButtonClear_Click(object sender, RoutedEventArgs e)
	{
		//_settings.PinCode.Input = 0;
	}

	private void ButtonEnter_Click(object sender, RoutedEventArgs e)
	{
		//if (_settings.PinCode.AccessGranted)
		//{
		//    _settings.ActivePage = WpfActivePage.Settings;
		//}
		//else
		//{
		//    ButtonClear_Click(sender, e);
		//}
		Result = System.Windows.Forms.DialogResult.OK;
		OnClose?.Invoke(sender, e);
	}

	#endregion
}