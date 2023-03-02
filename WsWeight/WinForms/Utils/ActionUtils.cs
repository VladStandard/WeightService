// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWeight.WinForms.Utils;

public static class ActionUtils
{
    #region Public and private fields, properties, constructor

    private static DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;

    #endregion

    #region Public and private methods

    public static void MakeScreenShot(ScaleModel scale)
    {
        using MemoryStream memoryStream = new();

        Rectangle bounds = Screen.GetBounds(Point.Empty);
        using Bitmap bitmap = new(bounds.Width, bounds.Height);
        using Graphics graphics = Graphics.FromImage(bitmap);
        graphics.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
        Image img = bitmap;
        img.Save(memoryStream, ImageFormat.Png);

        ScaleScreenShotModel scaleScreenShot = new() { Scale = scale, ScreenShot = memoryStream.ToArray() };
        DataAccess.Save(scaleScreenShot);
    }

    private static void MakeScreenShot(IWin32Window win32Window, ScaleModel scale)
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

        ScaleScreenShotModel scaleScreenShot = new() { Scale = scale, ScreenShot = memoryStream.ToArray() };
        DataAccess.Save(scaleScreenShot);
    }

    public static void ActionTryCatchFinally(IWin32Window win32Window, ScaleModel scale, Action action, Action actionFinally)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            ActionMakeScreenShot(win32Window, scale);
            WpfUtils.CatchException(ex, win32Window, true, true);
        }
        finally
        {
            actionFinally();
        }
    }

    public static void ActionTryCatch(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            WpfUtils.CatchException(ex, true);
        }
    }

    public static void ActionTryCatch(IWin32Window win32Window, ScaleModel scale, Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            ActionMakeScreenShot(win32Window, scale);
            WpfUtils.CatchException(ex, win32Window, true, true);
        }
    }

    public static void ActionMakeScreenShot(IWin32Window win32Window, ScaleModel scale)
    {
        try
        {
            MakeScreenShot(win32Window, scale);
        }
        catch (Exception ex)
        {
            WpfUtils.CatchException(ex, win32Window, true, false);
        }
    }

    #endregion
}