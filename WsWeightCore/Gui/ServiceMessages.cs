//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace WeightCore.Gui;

//    public class ServiceMessagesWindow
//    {
//        #region Public and private fields and properties

//        private Font Font { get; set; } = new("Microsoft Sans Serif", 8.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
//        private Size ButtonSize { get; set; } = new(100, 23);

//        #endregion

//        #region Public and private methods

//        private Button GetButtonCopy(TextBox fieldMessages)
//        {
//            Button btnCopy = new()
//            {
//                Font = Font,
//                Location = new Point(3, 3),
//                Size = ButtonSize,
//                TabIndex = 2,
//                Text = @"Копировать",
//                UseVisualStyleBackColor = true,
//            };
//            btnCopy.Name = nameof(btnCopy);
//            btnCopy.Click += (object sender, EventArgs e) =>
//            {
//                if (fieldMessages.TextLength > 0)
//                    Clipboard.SetText(fieldMessages.Text);
//            };
//            return btnCopy;
//        }

//        private Button GetButtonClear(TextBox fieldMessages)
//        {
//            Button btnClear = new()
//            {
//                Font = Font,
//                Location = new Point(140, 3),
//                Size = ButtonSize,
//                TabIndex = 3,
//                Text = @"Очистить",
//                UseVisualStyleBackColor = true,
//            };
//            btnClear.Name = nameof(btnClear);
//            btnClear.Click += (object sender, EventArgs e) =>
//            {
//                fieldMessages.Clear();
//            };
//            return btnClear;
//        }

//        private Button GetButtonClose(Form form)
//        {
//            Button btnClose = new()
//            {
//                Font = Font,
//                Location = new Point(180, 3),
//                Size = ButtonSize,
//                TabIndex = 4,
//                Text = @"Закрыть",
//                UseVisualStyleBackColor = true,
//            };
//            btnClose.Name = nameof(btnClose);
//            btnClose.Click += (object sender, EventArgs e) =>
//            {
//                form.Close();
//            };
//            return btnClose;
//        }

//        private TextBox GetFieldMessages(Form form, Control flowLayoutPanel)
//        {
//            TextBox fieldMessages = new()
//            {
//                Font = Font,
//                Dock = DockStyle.Fill,
//                TabIndex = 1,
//                Multiline = true,
//                Location = new Point(0, 31),
//                Size = new Size(form.ClientSize.Width, form.ClientSize.Height - flowLayoutPanel.Size.Height),
//                ScrollBars = ScrollBars.Vertical,
//            };
//            fieldMessages.Name = nameof(fieldMessages);
//            return fieldMessages;
//        }

//        public void BuildServiceMessagesWindow(Form owner)
//        {
//            if (owner is null)
//                return;

//            Form form = new();
//            FlowLayoutPanel flowLayoutPanel = new();
//            TextBox fieldMessages = GetFieldMessages(form, flowLayoutPanel);
//            flowLayoutPanel.SuspendLayout();
//            form.SuspendLayout();

//            form.Name = Assembly.GetExecutingAssembly().GetCustomAttribute<GuidAttribute>().Value.ToUpper();
//            form.Text = @"Service messages";
//            form.Owner = owner;
//            form.AutoScaleDimensions = new SizeF(6F, 13F);
//            form.AutoScaleMode = AutoScaleMode.Font;
//            //form.ClientSize = new Size((int)(owner.Width * 0.5), (int)(owner.Height * 0.25));
//            form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
//            form.TopMost = Debug.IsDebug;
//            form.Resize += (object sender, EventArgs e) =>
//            {
//                fieldMessages.Size = new Size(form.ClientSize.Width, form.ClientSize.Height - flowLayoutPanel.Size.Height);
//            };
//            form.MinimumSize = form.ClientSize = new Size((int)(owner.Width * 0.5), (int)(owner.Height * 0.25));

//            Button btnCopy = GetButtonCopy(fieldMessages);
//            Button btnClear = GetButtonClear(fieldMessages);
//            Button btnClose = GetButtonClose(form);

//            flowLayoutPanel.Controls.Add(btnCopy);
//            flowLayoutPanel.Controls.Add(btnClear);
//            flowLayoutPanel.Controls.Add(btnClose);
//            flowLayoutPanel.Dock = DockStyle.Top;
//            flowLayoutPanel.Location = new Point(0, 0);
//            flowLayoutPanel.Name = nameof(flowLayoutPanel);
//            flowLayoutPanel.Size = new Size(form.ClientSize.Width, 30);
//            flowLayoutPanel.TabIndex = 10;

//            form.Controls.Add(flowLayoutPanel);
//            form.Controls.Add(fieldMessages);

//            flowLayoutPanel.ResumeLayout(false);
//            fieldMessages.ResumeLayout(false);

//            form.Load += (object sender, EventArgs e) =>
//            {
//                XmlConfigurator.Configure();
//                TextBoxAppender.ConfigureTextBoxAppender(fieldMessages);
//            };
//            form.ClientSize += new Size(1, 1);

//            btnClose.Select();
//            form.ControlBox = false;
//            if (owner is not null)
//                form.ShowDialog(owner);
//            else
//                form.ShowDialog();

//            form.Close();
//            form.Dispose();
//        }

//        #endregion
//    }
