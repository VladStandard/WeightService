// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using System.Windows.Controls;
using WeightCore.Helpers;

namespace WeightCore.XamlPages;
#nullable enable

public class XamlPageBase : UserControl
{
	#region Public and private fields, properties, constructor

	public UserSessionHelper UserSession { get; private set; }
	public System.Windows.Forms.DialogResult Result { get; protected set; }
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

	protected void Setup()
	{
		if (!Resources.Contains(nameof(UserSession)))
			Resources.Add(nameof(UserSession), UserSessionHelper.Instance);

		object context = FindResource(nameof(UserSession));
		if (context is UserSessionHelper userSession)
			UserSession = userSession;
		else
			UserSession = UserSessionHelper.Instance;
	}

	#endregion
}