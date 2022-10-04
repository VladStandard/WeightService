// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using System.Windows.Controls;
using WeightCore.Helpers;

namespace WeightCore.Gui.XamlPages;

public class XamlPageBase : UserControl
{
	#region Public and private fields, properties, constructor

	public UserSessionHelper UserSession { get; private set; }
	public System.Windows.Forms.DialogResult Result { get; set; }
	public RoutedEventHandler? OnClose { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	protected XamlPageBase()
	{
		UserSession = UserSessionHelper.Instance;
		Result = System.Windows.Forms.DialogResult.Cancel;
		OnClose = null;
	}

	#endregion

	#region Public and private methods

	public void Setup()
	{
		object context = FindResource(nameof(UserSession));
		UserSession = context as UserSessionHelper ?? UserSessionHelper.Instance;
	}

	#endregion
}
