// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Controls;

namespace WeightCore.Wpf.Pages;

/// <summary>
/// Interaction logic for WpfPagePinCode.xaml
/// </summary>
public partial class WpfPagePinCode
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Constructor.
	/// </summary>
	public WpfPagePinCode()
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
		Result = System.Windows.Forms.DialogResult.OK;
		OnClose?.Invoke(sender, e);
	}

	#endregion
}