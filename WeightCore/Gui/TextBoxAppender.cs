//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace WeightCore.Gui;

//    public class TextBoxAppender : IAppender
//    {
//        #region Public and private fields and properties

//        private TextBox TextBox { get; set; }

//        #endregion

//        public TextBoxAppender(TextBox textBox)
//        {
//            Form frm = textBox.FindForm();
//            if (frm is null)
//                return;

//            frm.FormClosing += delegate { Close(); };

//            TextBox = textBox;
//            Name = "TextBoxAppender";
//        }

//        public string Name { get; set; }

//        public static void ConfigureTextBoxAppender(TextBox textBox)
//        {
//            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
//            TextBoxAppender appender = new(textBox);
//            hierarchy.Root.AddAppender(appender);
//        }

//        public void Close()
//        {
//            lock (_locker)
//            {
//                try
//                {
//                    TextBox = null;
//                    Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
//                    hierarchy.Root.RemoveAppender(this);
//                }
//                catch
//                {
//                    // There is not much that can be done here, and
//                    // swallowing the error is desired in my situation.
//                }
//            }
//        }

//        public void DoAppend(log4net.Core.LoggingEvent loggingEvent)
//        {
//            lock (_locker)
//            {
//                try
//                {
//                    if (TextBox is null)
//                        return;

//                    if (loggingEvent.LoggerName.Contains("NHibernate"))
//                        return;

//                    string msg = string.Concat(loggingEvent.RenderedMessage, "\r\n");

//                        if (TextBox is null)
//                            return;

//                        Action<string> del = new(s => TextBox.AppendText(s));
//                        TextBox.BeginInvoke(del, msg);
//                }
//                catch
//                {
//                    //
//                }
//            }
//        }
//    }
