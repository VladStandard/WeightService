// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using WeightCore.Helpers;
using WeightCore.Wpf.Utils;

namespace WeightCore.Utils;

public static class ActionUtils
{
	#region Public and private fields, properties, constructor

	private static DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
	private static UserSessionHelper UserSession { get; } = UserSessionHelper.Instance;

	#endregion

	#region Public and private methods

	public static void MakeScreenShot()
	{
		using MemoryStream memoryStream = new();

		Rectangle bounds = Screen.GetBounds(System.Drawing.Point.Empty);
		using Bitmap bitmap = new(bounds.Width, bounds.Height);
		using Graphics graphics = Graphics.FromImage(bitmap);
		graphics.CopyFromScreen(System.Drawing.Point.Empty, System.Drawing.Point.Empty, bounds.Size);
		Image img = bitmap;
		img.Save(memoryStream, ImageFormat.Png);

		ScaleScreenShotModel scaleScreenShot = new() { Scale = UserSession.Scale, ScreenShot = memoryStream.ToArray() };
		DataAccess.Save(scaleScreenShot);
	}

	private static void MakeScreenShot(IWin32Window win32Window)
	{
		using MemoryStream memoryStream = new();

		if (win32Window is Form form)
		{
			using Bitmap bitmap = new(form.Width, form.Height);
			using Graphics graphics = Graphics.FromImage(bitmap);
			graphics.CopyFromScreen(form.Location.X, form.Location.Y, 0, 0, form.Size);
			using Image img = bitmap;
			img.Save(memoryStream, ImageFormat.Png);
		}

		ScaleScreenShotModel scaleScreenShot = new() { Scale = UserSession.Scale, ScreenShot = memoryStream.ToArray() };
		DataAccess.Save(scaleScreenShot);
	}

	public static void ActionTryCatchFinally(IWin32Window win32Window, Action action, Action actionFinally)
	{
		try
		{
			action.Invoke();
		}
		catch (Exception ex)
		{
			ActionMakeScreenShot(win32Window);
			WpfUtils.CatchException(ex, win32Window, true, true, true);
		}
		finally
		{
			actionFinally.Invoke();
		}
	}

	public static void ActionTryCatch(IWin32Window win32Window, Action action)
	{
		try
		{
			action.Invoke();
		}
		catch (Exception ex)
		{
			ActionMakeScreenShot(win32Window);
			WpfUtils.CatchException(ex, win32Window, true, true, true);
		}
	}

	public static void ActionMakeScreenShot(IWin32Window win32Window)
	{
		try
		{
			MakeScreenShot(win32Window);
		}
		catch (Exception ex)
		{
			WpfUtils.CatchException(ex, win32Window, true, true, true);
		}
	}

	#endregion
}