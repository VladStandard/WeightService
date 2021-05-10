// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScalesCore.Win.Utils
{
    public class MessageBoxTask : IDisposable
    {
        private bool Disposed { get; set; }

        private string Text { get; set; }
        private string Caption { get; set; }
        private int Timeout { get; set; }
        private MessageBoxButtons Buttons { get; }
        private MessageBoxIcon MessageBoxIcon { get; }
        private MessageBoxDefaultButton DefaultButton { get; }
        private DateTime DtStart { get; }

        private DialogResult _result;

        public MessageBoxTask(string text, string caption,
            int timeout = 10,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon messageBoxIcon = MessageBoxIcon.Information,
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            // Флаг высвобождения ресурсов
            Disposed = false;

            Text = text;
            Caption = caption;
            Timeout = timeout;
            Buttons = buttons;
            MessageBoxIcon = messageBoxIcon;
            DefaultButton = defaultButton;
            DtStart = DateTime.Now;
        }

        public DialogResult Show(IWin32Window owner)
        {
            CheckIfDisposed();

            var hOwner = IntPtr.Zero;
            if (owner != null) hOwner = owner.Handle;

            // Задача на отслеживание MessageBox и изменение таймера
            Task.Run(() =>
            {
                try
                {
                    IntPtr hWnd;

                    // Ждать окно сообщения. Работа перейдёт вниз по коду.
                    while ((hWnd = Win32.FindWindow("#32770", Caption)) == IntPtr.Zero)
                    {
                        Thread.Sleep(25);
                        // Выйти по таймеру
                        if (DateTime.Now - DtStart > TimeSpan.FromSeconds(Timeout))
                            break;
                    }

                    // Отцентрировать один раз
                    if (hWnd != IntPtr.Zero)
                    {
                        CenterWindow(hOwner, hWnd);
                    }
                    // Возобновить поток
                    while (hWnd != IntPtr.Zero)
                    {
                        // Изменить заголовок
                        Win32.SendMessage(hWnd, Win32.MsgConst.WM_SETTEXT, IntPtr.Zero, Caption +
                            @"  [" + (DtStart - DateTime.Now.AddSeconds(-Timeout)).Seconds + @"]");
                        Thread.Sleep(100);
                        // Таймер
                        if (DateTime.Now - DtStart > TimeSpan.FromSeconds(Timeout))
                        {
                            // Изменить заголовок
                            Win32.SendMessage(hWnd, Win32.MsgConst.WM_SETTEXT, IntPtr.Zero, Caption);
                            // Закрыть окно, если возможно
                            Win32.SendMessage(hWnd, Win32.MsgConst.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                            // Подождать
                            Thread.Sleep(250);
                            // Нажать Enter на окнах без кнопки закрытия
                            Win32.PostMessage(hWnd, Win32.MsgConst.WM_KEYDOWN, 0x0D, 0x0);
                            // Завершить цикл
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                    //
                }
            });

            _result = owner == null
                ? MessageBox.Show(Text, Caption, Buttons, MessageBoxIcon, DefaultButton)
                : MessageBox.Show(owner, Text, Caption, Buttons, MessageBoxIcon, DefaultButton);

            return _result;
        }

        private static void CenterWindow(IntPtr hOwner, IntPtr hMessageBox)
        {
            try
            {
                // Размер MessageBox
                var rectChild = new Win32.Rect();
                var widthChild = 0;
                var heightChild = 0;
                if (hMessageBox != IntPtr.Zero)
                {
                    Win32.GetWindowRect(hMessageBox, ref rectChild);
                    widthChild = rectChild.right - rectChild.left;
                    heightChild = rectChild.bottom - rectChild.top;

                }

                // Получить размер и позицию формы
                var rectParent = new Win32.Rect();
                var widthParent = 0;
                var heightParent = 0;
                if (hOwner != IntPtr.Zero)
                {
                    Win32.GetWindowRect(hOwner, ref rectParent);
                    widthParent = rectParent.right - rectParent.left;
                    heightParent = rectParent.bottom - rectParent.top;
                }

                // Центровка MessageBox на форме
                var left = rectParent.left + (widthParent - widthChild) / 2;
                var top = rectParent.top + (heightParent - heightChild) / 2;
                Win32.SetWindowPos(hMessageBox, Win32.SetWindowPosConst.HWND_TOPMOST, left, top, 0, 0,
                    Win32.SetWindowPosConst.SWP_NOSIZE |
                    Win32.SetWindowPosConst.SWP_NOOWNERZORDER |
                    Win32.SetWindowPosConst.SWP_SHOWWINDOW);
            }
            catch (Exception)
            {
                //
            }
        }

        ~MessageBoxTask()
        {
            Dispose();
        }

        // Высвободить управляемые ресурсы
        private void DisposeManagedResources()
        {
            Text = null;
            Caption = null;
            Timeout = 0;
        }

        // Высвободить неуправляемые ресурсы
        private void DisposeUnmanagedResources()
        {
        }

        private void CheckIfDisposed()
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(ToString() + @": объект уже высвобожден!");
            }
        }

        public virtual void Dispose()
        {
            lock (this)
            {
                if (!Disposed)
                {
                    // Высвободить управляемые ресурсы
                    DisposeManagedResources();

                    // Высвободить неуправляемые ресурсы
                    DisposeUnmanagedResources();

                    // Флаг высвобождения ресурсов
                    Disposed = true;
                }

                // Запретить сборщику мусора вызывать деструктор
                GC.SuppressFinalize(this);
            }
        }
    }
}
