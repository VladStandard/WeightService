// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Repository.Hierarchy;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace WeightServices.Common
{

    public static class ServiceMessagesWindow
    {

        public static string NameForm = "A8D1BD59-2634-4F9E-BB2A-7EEA0086F33C";
        private static readonly Font Font = new Font("Microsoft Sans Serif", 8.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
        private static readonly Size ButtonSize = new Size(100, 23);

        private static Button GetButtonCopy(TextBox fieldMessages)
        {
            var btnCopy = new Button
            {
                Font = Font,
                Location = new Point(3, 3),
                Size = ButtonSize,
                TabIndex = 2,
                Text = @"Копировать",
                UseVisualStyleBackColor = true,
            };
            btnCopy.Name = nameof(btnCopy);
            btnCopy.Click += (object sender, EventArgs e) =>
            {
                if (fieldMessages.TextLength > 0)
                    Clipboard.SetText(fieldMessages.Text);
            };
            return btnCopy;
        }

        private static Button GetButtonClear(TextBox fieldMessages)
        {
            var btnClear = new Button
            {
                Font = Font,
                Location = new Point(140, 3),
                Size = ButtonSize,
                TabIndex = 3,
                Text = @"Очистить",
                UseVisualStyleBackColor = true,
            };
            btnClear.Name = nameof(btnClear);
            btnClear.Click += (object sender, EventArgs e) =>
            {
                fieldMessages.Clear();
            };
            return btnClear;
        }

        private static Button GetButtonClose(Form form)
        {
            var btnClose = new Button
            {
                Font = Font,
                Location = new Point(180, 3),
                Size = ButtonSize,
                TabIndex = 4,
                Text = @"Закрыть",
                UseVisualStyleBackColor = true,
            };
            btnClose.Name = nameof(btnClose);
            btnClose.Click += (object sender, EventArgs e) =>
            {
                form.Close();
            };
            return btnClose;
        }

        private static TextBox GetFieldMessages(Form form, Control flowLayoutPanel)
        {
            var fieldMessages = new TextBox
            {
                Font = Font,
                Dock = DockStyle.Fill,
                TabIndex = 1,
                Multiline = true,
                Location = new Point(0, 31),
                Size = new Size(form.ClientSize.Width, form.ClientSize.Height - flowLayoutPanel.Size.Height),
                ScrollBars = ScrollBars.Vertical,
            };
            fieldMessages.Name = nameof(fieldMessages);
            return fieldMessages;
        }
        
        public static Form BuildServiceMessagesWindow(Form owner)
        {
            if (owner is null)
                return null;

            var assyGuid = Assembly.GetExecutingAssembly().GetCustomAttribute<GuidAttribute>().Value.ToUpper();
            foreach (Form itemForm in Application.OpenForms)
            {
                if (itemForm.Name == assyGuid + NameForm)
                {
                    //form.Show();
                    itemForm.Activate();
                    return itemForm;
                }
            }

            var form = new Form();
            var flowLayoutPanel = new FlowLayoutPanel();
            var fieldMessages = GetFieldMessages(form, flowLayoutPanel);
            flowLayoutPanel.SuspendLayout();
            form.SuspendLayout();

            form.Text = @"Service messages";
            form.Name = assyGuid + NameForm;
            form.Owner = owner;
            form.AutoScaleDimensions = new SizeF(6F, 13F);
            form.AutoScaleMode = AutoScaleMode.Font;
            //form.ClientSize = new Size((int)(owner.Width * 0.5), (int)(owner.Height * 0.25));
            form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            form.TopMost = true;
            form.Resize += (object sender, EventArgs e) =>
            {
                fieldMessages.Size = new Size(form.ClientSize.Width, form.ClientSize.Height - flowLayoutPanel.Size.Height);
            };
            form.MinimumSize = form.ClientSize = new Size((int)(owner.Width * 0.5), (int)(owner.Height * 0.25));

            var btnCopy = GetButtonCopy(fieldMessages);
            var btnClear = GetButtonClear(fieldMessages);
            var btnClose = GetButtonClose(form);

            flowLayoutPanel.Controls.Add(btnCopy);
            flowLayoutPanel.Controls.Add(btnClear);
            flowLayoutPanel.Controls.Add(btnClose);
            flowLayoutPanel.Dock = DockStyle.Top;
            flowLayoutPanel.Location = new Point(0, 0);
            flowLayoutPanel.Name = nameof(flowLayoutPanel);
            flowLayoutPanel.Size = new Size(form.ClientSize.Width, 30);
            flowLayoutPanel.TabIndex = 10;

            form.Controls.Add(flowLayoutPanel);
            form.Controls.Add(fieldMessages);

            flowLayoutPanel.ResumeLayout(false);
            fieldMessages.ResumeLayout(false);

            form.Load += (object sender, EventArgs e) =>
            {
                XmlConfigurator.Configure();
                TextBoxAppender.ConfigureTextBoxAppender(fieldMessages);
            };
            form.ClientSize += new Size(1, 1);

            btnClose.Select();
            form.ControlBox = false;
            form.Show();
            return form;
        }
    }

    public class TextBoxAppender : IAppender
    {
        private TextBox _textBox;
        private readonly object _lockObj = new object();

        public TextBoxAppender(TextBox textBox)
        {
            var frm = textBox.FindForm();
            if (frm == null)
                return;

            frm.FormClosing += delegate { Close(); };

            _textBox = textBox;
            Name = "TextBoxAppender";
        }

        public string Name { get; set; }

        public static void ConfigureTextBoxAppender(TextBox textBox)
        {
            var hierarchy = (Hierarchy)LogManager.GetRepository();
            var appender = new TextBoxAppender(textBox);
            hierarchy.Root.AddAppender(appender);
        }

        public void Close()
        {
            try
            {
                // This locking is required to avoid null reference exceptions
                // in situations where DoAppend() is writing to the TextBox while
                // Close() is nulling out the TextBox.
                lock (_lockObj)
                {
                    _textBox = null;
                }

                var hierarchy = (Hierarchy)LogManager.GetRepository();
                hierarchy.Root.RemoveAppender(this);
            }
            catch
            {
                // There is not much that can be done here, and
                // swallowing the error is desired in my situation.
            }
        }

        public void DoAppend(log4net.Core.LoggingEvent loggingEvent)
        {
            try
            {
                if (_textBox == null)
                    return;

                // For my situation, this quick and dirt filter was all that was 
                // needed. Depending on your situation, you may decide to delete 
                // this logic, modify it slightly, or implement something a 
                // little more sophisticated.
                if (loggingEvent.LoggerName.Contains("NHibernate"))
                    return;

                // Again, my requirements were simple; displaying the message was
                // all that was needed. Depending on your circumstances, you may
                // decide to add information to the displayed message 
                // (e.g. log level) or implement something a little more 
                // dynamic.
                var msg = string.Concat(loggingEvent.RenderedMessage, "\r\n");

                lock (_lockObj)
                {
                    // This check is required a second time because this class 
                    // is executing on multiple threads.
                    if (_textBox == null)
                        return;

                    // Because the logging is running on a different thread than
                    // the GUI, the control's "BeginInvoke" method has to be
                    // leveraged in order to append the message. Otherwise, a 
                    // threading exception will be thrown. 
                    var del = new Action<string>(s => _textBox.AppendText(s));
                    _textBox.BeginInvoke(del, msg);
                }
            }
            catch
            {
                // There is not much that can be done here, and
                // swallowing the error is desired in my situation.
            }
        }
    }

}