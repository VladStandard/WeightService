// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using ScalesUI.Common;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScalesUI.Helpers
{
    public class MouseHookHelper
    {
        #region Design pattern "Lazy Singleton"

        private static MouseHookHelper _instance;
        public static MouseHookHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Private fields and properties

        private readonly SessionState _ws = SessionState.Instance;
        private event EventHandler MiddleMouseEvent = delegate { };
        private LowLevelMouseProc _levelMouseProc;
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        private IntPtr _hookId = IntPtr.Zero;
        public IWin32Window Owner { get; set; }

        #endregion

        #region DllImport methods

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion

        #region Public and private methods

        public MouseHookHelper()
        {
            Open();
        }

        public void Open()
        {
            _levelMouseProc = HookCallback;
            _hookId = SetHook(_levelMouseProc);
            MiddleMouseEvent += OnMouseEvent;
        }

        public void Close()
        {
            UnhookWindowsHookEx(_hookId);
        }

        private IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                if (!(curModule is null))
                    return SetWindowsHookEx((int)EnumWindowMessages.WH_MOUSELL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
            return IntPtr.Zero;
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (EnumWindowMessages)wParam == EnumWindowMessages.WM_MIDDLEBUTTONDOWN)
            {
                //var hookStruct = (StructMarshalHook)Marshal.PtrToStructure(lParam, typeof(StructMarshalHook));
                MiddleMouseEvent(null, new EventArgs());
            }
            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }

        public void OnMouseEvent(object sender, EventArgs e)
        {
            _ws?.ProcessWeighingResult(Owner);
        }

        private async Task OnMouseEventAsync()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            if (_ws != null)
            {
                _ws.ProcessWeighingResult(Owner);
                Application.DoEvents();
                await Task.Delay(TimeSpan.FromMilliseconds(800)).ConfigureAwait(true);
            }
        }

        #endregion
    }


    #region Enums

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum EnumWindowMessages
    {
        MK_LBUTTON = 0x0001,          // 1
        MK_RBUTTON = 0x0002,          // 2
        WH_MOUSELL = 0xE,             // 14
        WM_MOUSEMOVE = 0x0200,        // 512
        WM_LBUTTONDOWN = 0x0201,      // 513
        WM_LBUTTONUP = 0x0202,        // 514
        WM_RBUTTONDOWN = 0x0204,      // 516
        WM_RBUTTONUP = 0x0205,        // 517
        WM_MIDDLEBUTTONDOWN = 0x0207, // 519
        WM_MIDDLEBUTTONUP = 0x0208,   // 520
        WmMouseWheel = 0x020A,        // 522
    }

    #endregion

    #region Structures

    public struct StructMousePoint
    {
        public int X;
        public int Y;
    }

    public struct StructMarshalHook
    {
        public StructMousePoint MousePoint;
        public uint MouseData;
        public uint Flags;
        public uint Time;
        public IntPtr DwExtraInfo;
    }

    #endregion
}
