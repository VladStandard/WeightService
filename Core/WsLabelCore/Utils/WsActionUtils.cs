// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Utils;

public static class WsActionUtils
{
    #region Public and private fields, properties, constructor

    private static WsSqlAccessManagerHelper AccessManager => WsSqlAccessManagerHelper.Instance;
    private static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    private static WsPluginMemoryHelper PluginMemory => WsPluginMemoryHelper.Instance;

    #endregion

    #region Public and private methods

    private static void MakeScreenShot(IWin32Window win32Window, WsSqlScaleModel scale)
    {
        if (win32Window is not Form form) return;
        using MemoryStream memoryStream = new();
        using Bitmap bitmap = new(form.Width, form.Height);
        using Graphics graphics = Graphics.FromImage(bitmap);
        graphics.CopyFromScreen(form.Location.X, form.Location.Y, 0, 0, form.Size);
        using Image img = bitmap;
        img.Save(memoryStream, ImageFormat.Png);
        WsSqlScaleScreenShotModel scaleScreenShot = new() { Scale = scale, ScreenShot = memoryStream.ToArray() };
        AccessManager.AccessItem.Save(scaleScreenShot);
    }

    public static void ActionTryCatchFinally(IWin32Window win32Window, Action action, Action actionFinally)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            ActionMakeScreenShot(win32Window, WsUserSessionHelper.Instance.Scale);
            WsWpfUtils.CatchException(ex, win32Window, true, true);
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
            WsWpfUtils.CatchException(ex, true);
        }
    }

    public static void ActionTryCatch(IWin32Window win32Window, WsSqlScaleModel scale, Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            ActionMakeScreenShot(win32Window, scale);
            WsWpfUtils.CatchException(ex, win32Window, true, true);
        }
    }

    public static void ActionMakeScreenShot(IWin32Window win32Window, WsSqlScaleModel scale)
    {
        try
        {
            MakeScreenShot(win32Window, scale);
            PluginMemory.MemorySize.Execute();
            ContextManager.ContextItem.SaveLogMemory(PluginMemory.GetMemorySizeAppMb(), PluginMemory.GetMemorySizeFreeMb());
            GC.Collect();
        }
        catch (Exception ex)
        {
            WsWpfUtils.CatchException(ex, win32Window, true, false);
        }
    }

    #endregion
}