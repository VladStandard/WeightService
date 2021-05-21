// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UICommon.WinForms.Utils
{
    public static class AsyncControl
    {
        public static class Properties
        {
            public static class SetText
            {
                private delegate void Delegate(Control control, string value);

                private static void Work(Control control, string value)
                {
                    control.Text = value;
                }

                public static Task Async(Control control, string value)
                {
                    return Task.Run(() => Sync(control, value));
                }

                public static void Sync(Control control, string value)
                {
                    if (control != null)
                    {
                        if (control.InvokeRequired)
                        {
                            control.BeginInvoke(new Delegate(Work), control, value);
                        }
                        else
                        {
                            Work(control, value);
                        }
                    }
                }
            }

            public static class SetEnabled
            {
                private delegate void Delegate(Control control, bool value);

                private static void Work(Control control, bool value)
                {
                    control.Enabled = value;
                }

                public static Task Async(Control control, bool value)
                {
                    return Task.Run(() => Sync(control, value));
                }

                public static void Sync(Control control, bool value)
                {
                    if (control != null)
                    {
                        if (control.InvokeRequired)
                        {
                            control.BeginInvoke(new Delegate(Work), control, value);
                        }
                        else
                        {
                            Work(control, value);
                        }
                    }
                }
            }

            public static class SetBackColor
            {
                private delegate void Delegate(Control control, Color value);
                private static void Work(Control control, Color value)
                {
                    control.BackColor = value;
                }

                public static Task Async(Control control, Color value)
                {
                    return Task.Run(() => Sync(control, value));
                }

                public static void Sync(Control control, Color value)
                {
                    if (control != null)
                    {
                        if (control.InvokeRequired)
                        {
                            control.BeginInvoke(new Delegate(Work), control, value);
                        }
                        else
                        {
                            Work(control, value);
                        }
                    }
                }
            }
        }

        public static class SetForeColor
        {
            private static void Work(Control control, Color value)
            {
                control.ForeColor = value;
            }

            public static void Invoke(Control control, Color value)
            {
                if (control != null)
                {
                    if (control.InvokeRequired)
                    {
                        control.Invoke(new MethodInvoker(() => Work(control, value)));
                    }
                    else
                    {
                        Work(control, value);
                    }
                }
            }
        }

        public static class SetVisible
        {
            private static void Work(Control control, bool value)
            {
                control.Visible = value;
            }

            public static void Invoke(Control control, bool value)
            {
                if (control != null)
                {
                    if (control.InvokeRequired)
                    {
                        control.Invoke(new MethodInvoker(() => Work(control, value)));
                    }
                    else
                    {
                        Work(control, value);
                    }
                }
            }
        }

        public static class GetHandle
        {
            public static IntPtr Invoke(Control control)
            {
                IntPtr Work(Control inControl)
                {
                    return inControl.Handle;
                }
                IntPtr result = default;
                if (control != null)
                {
                    if (control.InvokeRequired)
                    {
                        control.Invoke(new MethodInvoker(() => result = Work(control)));
                    }
                    else
                    {
                        result = Work(control);
                    }
                }
                return result;
            }
        }

        public static class GetControl
        {
            private static Control Work(Control control)
            {
                return control;
            }

            public static Control Invoke(Control control)
            {
                Control result = default;
                if (control != null)
                {
                    if (control.InvokeRequired)
                    {
                        control.Invoke(new MethodInvoker(() => result = Work(control)));
                    }
                    else
                    {
                        result = Work(control);
                    }
                }
                return result;
            }
        }

        public static class Select
        {

            private static void Work(Control control)
            {
                control.Select();
            }

            public static void Invoke(Control control)
            {
                if (control != null)
                {
                    if (control.InvokeRequired)
                    {
                        control.Invoke(new MethodInvoker(() => Work(control)));
                    }
                    else
                    {
                        Work(control);
                    }
                }
            }
        }

        public static class Focus
        {
            private static void Work(Control control)
            {
                control.Focus();
            }

            public static void Invoke(Control control)
            {
                if (control != null)
                {
                    if (control.InvokeRequired)
                    {
                        control.Invoke(new MethodInvoker(() => Work(control)));
                    }
                    else
                    {
                        Work(control);
                    }
                }
            }
        }

        public static class SetParent
        {
            private static void Work(Control control, Control parent)
            {
                control.Parent = parent;
            }

            public static void Invoke(Control control, Control parent)
            {
                if (control != null && parent != null)
                {
                    if (control.InvokeRequired || parent.InvokeRequired)
                    {
                        control.Invoke(new MethodInvoker(() => Work(control, parent)));
                    }
                    else
                    {
                        Work(control, parent);
                    }
                }
            }
        }

        public static class AddControl
        {
            private static void Work(Control control, Control child)
            {
                control.Controls.Add(child);
            }

            public static void Invoke(Control control, Control child)
            {
                if (control != null && child != null)
                {
                    if (control.InvokeRequired || control.InvokeRequired)
                    {
                        control.Invoke(new MethodInvoker(() => Work(control, child)));
                    }
                    else
                    {
                        Work(control, child);
                    }
                }
            }
        }

        public static class SetCellPosition
        {
            private static void Work(TableLayoutPanel panel, Control control, TableLayoutPanelCellPosition cellPosition)
            {
                panel.SetCellPosition(control, cellPosition);
            }

            public static void Invoke(TableLayoutPanel panel, Control control, TableLayoutPanelCellPosition cellPosition)
            {
                if (panel != null)
                {
                    if (panel.InvokeRequired)
                    {
                        panel.Invoke(new MethodInvoker(() => Work(panel, control, cellPosition)));
                    }
                    else
                    {
                        Work(panel, control, cellPosition);
                    }
                }
            }
        }

        public static class SetDock
        {
            private static void Work(Control control, DockStyle dockStyle)
            {
                control.Dock = dockStyle;
            }

            public static void Invoke(Control control, DockStyle dockStyle)
            {
                if (control != null)
                {
                    if (control.InvokeRequired)
                    {
                        control.Invoke(new MethodInvoker(() => Work(control, dockStyle)));
                    }
                    else
                    {
                        Work(control, dockStyle);
                    }
                }
            }
        }
    }
}
