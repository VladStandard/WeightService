// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Windows.Forms;

namespace ScalesCore.Win.Utils
{
    public static class MessageBoxTimer
    {
        // Константы и переменные
        private static Win32.WindowsHookProc _hookProcDelegate;
        private static int _hHook;
        private static System.Threading.Timer _timeoutTimer;
        private static string _caption;
        private static DialogResult _result;
        //private static DialogResult _dialogResult;

        public static DialogResult Show(string text, string caption,
            int timeout = 10000,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon msgIcon = MessageBoxIcon.Information,
            MessageBoxDefaultButton defButton = MessageBoxDefaultButton.Button1)
        //DialogResult dialogResult = DialogResult.None
        {
            _hookProcDelegate = HookCallback;

#pragma warning disable 0618
            _hHook = Win32.SetWindowsHookEx(Win32.WH_CBT, _hookProcDelegate, IntPtr.Zero, AppDomain.GetCurrentThreadId());
#pragma warning restore 0618

            _caption = caption;

            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed, new AutoResetEvent(false), timeout, Timeout.Infinite);
            //_dialogResult = dialogResult;
            using (_timeoutTimer)
            {
                _result = MessageBox.Show(text, caption, buttons, msgIcon, defButton);
            }

            // Освободить память
            Unhook();

            return _result;
        }

        // Таймер автозакрытия
        private static void OnTimerElapsed(object state)
        {
            // #32770 - код Messageox
            var hWnd = Win32.FindWindow("#32770", _caption);
            if (hWnd != IntPtr.Zero)
            {
                Win32.SendMessage(hWnd, Win32.MsgConst.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
            _timeoutTimer?.Dispose();
            _result = DialogResult.None;
        }

        // Освободить память
        private static void Unhook()
        {
            Win32.UnhookWindowsHookEx(_hHook);
            _hHook = 0;
            _caption = string.Empty;
            _hookProcDelegate = null;
        }

        private static int HookCallback(int code, IntPtr wParam, IntPtr lParam)
        {
            var hHook = _hHook;

            var cls = Win32.GetClassName(wParam);
            if (cls == "#32770")
            {
                var caption = Win32.GetWindowText(wParam);
                //var text = Win32.GetDlgItemText(wParam, 0xFFFF);  // -1 aka IDC_STATIC
                if (caption == _caption)
                {
                    switch (code)
                    {
                        case Win32.HCBT_ACTIVATE:
                            // Центрировать MessageBox
                            CenterWindowOnParent(wParam);
                            // Освободить память
                            Unhook();
                            break;
                            //default:
                            //    // Освободить память
                            //    Unhook();
                            //    break;
                    }
                }
            }

            return Win32.CallNextHookEx(hHook, code, wParam, lParam);
        }

        // Центрировать MessageBox
        private static void CenterWindowOnParent(IntPtr hChildWnd)
        {
            #region Более не используется

            //IntPtr hParent;
            // Не подходит - нет в наличии родительского окна
            //if ((hParent = Win32.GetParent(hChildWnd)) == IntPtr.Zero)
            // Не подходит - главное окно может находится на другом мониторе, чем вызывающее окно
            //var currentProc = Process.GetCurrentProcess();
            //if ((hParent = currentProc.MainWindowHandle) == IntPtr.Zero)
            //hParent = Win32.GetDesktopWindow();
            //var rcParent = new Win32.Rect();
            //Win32.GetWindowRect(hParent, ref rcParent);
            //var cxParent = rcParent.right - rcParent.left;
            //var cyParent = rcParent.bottom - rcParent.top;

            #endregion

            var rcChild = new Win32.Rect();
            Win32.GetWindowRect(hChildWnd, ref rcChild);
            var cxChild = rcChild.right - rcChild.left;
            var cyChild = rcChild.bottom - rcChild.top;

            var screenNumber = 0;
            var targetScreen = Screen.FromPoint(Cursor.Position);
            for (var i = 0; i < Screen.AllScreens.Length; i++)
            {
                if (!Equals(targetScreen, Screen.AllScreens[i]))
                    continue;
                screenNumber = i;
                break;
            }
            var cxParent = Screen.AllScreens[screenNumber].Bounds.Right - Screen.AllScreens[screenNumber].Bounds.Left;
            var cyParent = Screen.AllScreens[screenNumber].Bounds.Bottom - Screen.AllScreens[screenNumber].Bounds.Top;
            var x = Screen.AllScreens[screenNumber].Bounds.Left + (cxParent - cxChild) / 2;
            var y = Screen.AllScreens[screenNumber].Bounds.Top + (cyParent - cyChild) / 2;

            Win32.SetWindowPos(hChildWnd, Win32.SetWindowPosConst.HWND_TOPMOST, x, y, 0, 0,
                Win32.SetWindowPosConst.SWP_NOSIZE | Win32.SetWindowPosConst.SWP_SHOWWINDOW);
        }
    }
}
