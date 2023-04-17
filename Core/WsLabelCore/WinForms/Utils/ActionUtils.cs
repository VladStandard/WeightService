// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;

namespace WsLabelCore.WinForms.Utils;

public static class ActionUtils
{
    #region Public and private fields, properties, constructor

    private static WsDataAccessHelper DataAccess => WsDataAccessHelper.Instance;
    private static PluginMemoryHelper PluginMemory => PluginMemoryHelper.Instance;

    #endregion

    #region Public and private methods

    private static void MakeScreenShot(IWin32Window win32Window, ScaleModel scale)
    {
        if (win32Window is not Form form) return;
        using MemoryStream memoryStream = new();
        using Bitmap bitmap = new(form.Width, form.Height);
        using Graphics graphics = Graphics.FromImage(bitmap);
        graphics.CopyFromScreen(form.Location.X, form.Location.Y, 0, 0, form.Size);
        using Image img = bitmap;
        img.Save(memoryStream, ImageFormat.Png);
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
            PluginMemory.MemorySize.Execute();
            DataAccess.SaveLogMemory(PluginMemory.GetMemorySizeAppMb(), PluginMemory.GetMemorySizeFreeMb());
            GC.Collect();
        }
        catch (Exception ex)
        {
            WpfUtils.CatchException(ex, win32Window, true, false);
        }
    }

    #endregion
}