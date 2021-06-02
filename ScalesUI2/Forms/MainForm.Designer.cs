namespace ScalesUI.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelWeightNetto = new System.Windows.Forms.Label();
            this.fieldWeightTare = new System.Windows.Forms.Label();
            this.fieldWeightNetto = new System.Windows.Forms.Label();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.fieldCountBox = new System.Windows.Forms.ProgressBar();
            this.fieldResolution = new System.Windows.Forms.ComboBox();
            this.fieldCurrentTime = new System.Windows.Forms.Label();
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.labelWeightTare = new System.Windows.Forms.Label();
            this.lbKneading = new System.Windows.Forms.Label();
            this.lbProductDate = new System.Windows.Forms.Label();
            this.fieldProductDate = new System.Windows.Forms.Label();
            this.fieldKneading = new System.Windows.Forms.Label();
            this.fieldPalletSize = new System.Windows.Forms.Label();
            this.fieldTitle = new System.Windows.Forms.Label();
            this.fieldPlu = new System.Windows.Forms.Label();
            this.labelPlu = new System.Windows.Forms.Label();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.flowLayoutPanelBottom = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.buttonSetKneading = new System.Windows.Forms.Button();
            this.buttonSelectPlu = new System.Windows.Forms.Button();
            this.btAddKneading = new System.Windows.Forms.Button();
            this.btNewPallet = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSetZero = new System.Windows.Forms.Button();
            this.buttonSelectOrder = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.fieldMemoryManager = new System.Windows.Forms.Label();
            this.fieldPrintManager = new System.Windows.Forms.Label();
            this.fieldMassaManager = new System.Windows.Forms.Label();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            this.flowLayoutPanelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelWeightNetto
            // 
            this.labelWeightNetto.AutoSize = true;
            this.labelWeightNetto.BackColor = System.Drawing.Color.Transparent;
            this.labelWeightNetto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWeightNetto.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWeightNetto.Location = new System.Drawing.Point(14, 63);
            this.labelWeightNetto.Margin = new System.Windows.Forms.Padding(3);
            this.labelWeightNetto.Name = "labelWeightNetto";
            this.labelWeightNetto.Size = new System.Drawing.Size(323, 93);
            this.labelWeightNetto.TabIndex = 12;
            this.labelWeightNetto.Text = "Вес нетто";
            this.labelWeightNetto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelWeightNetto.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldWeightTare
            // 
            this.fieldWeightTare.AutoSize = true;
            this.fieldWeightTare.BackColor = System.Drawing.Color.Transparent;
            this.fieldWeightTare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldWeightTare.Enabled = false;
            this.fieldWeightTare.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldWeightTare.Location = new System.Drawing.Point(343, 162);
            this.fieldWeightTare.Margin = new System.Windows.Forms.Padding(3);
            this.fieldWeightTare.Name = "fieldWeightTare";
            this.fieldWeightTare.Size = new System.Drawing.Size(702, 111);
            this.fieldWeightTare.TabIndex = 11;
            this.fieldWeightTare.Text = "0,000";
            this.fieldWeightTare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldWeightTare.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldWeightNetto
            // 
            this.fieldWeightNetto.AutoSize = true;
            this.fieldWeightNetto.BackColor = System.Drawing.SystemColors.Control;
            this.fieldWeightNetto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldWeightNetto.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldWeightNetto.Location = new System.Drawing.Point(343, 63);
            this.fieldWeightNetto.Margin = new System.Windows.Forms.Padding(3);
            this.fieldWeightNetto.Name = "fieldWeightNetto";
            this.fieldWeightNetto.Size = new System.Drawing.Size(702, 93);
            this.fieldWeightNetto.TabIndex = 10;
            this.fieldWeightNetto.Text = "0,000";
            this.fieldWeightNetto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldWeightNetto.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 5;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.977479F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.9559F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0563F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.00938F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.000938F));
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaManager, 1, 11);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPrintManager, 1, 10);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMemoryManager, 1, 9);
            this.tableLayoutPanelMain.Controls.Add(this.fieldCountBox, 3, 10);
            this.tableLayoutPanelMain.Controls.Add(this.fieldResolution, 3, 8);
            this.tableLayoutPanelMain.Controls.Add(this.fieldCurrentTime, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.pictureBoxClose, 3, 2);
            this.tableLayoutPanelMain.Controls.Add(this.labelWeightTare, 1, 3);
            this.tableLayoutPanelMain.Controls.Add(this.labelWeightNetto, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.fieldWeightNetto, 2, 2);
            this.tableLayoutPanelMain.Controls.Add(this.fieldWeightTare, 2, 3);
            this.tableLayoutPanelMain.Controls.Add(this.lbKneading, 1, 5);
            this.tableLayoutPanelMain.Controls.Add(this.lbProductDate, 1, 7);
            this.tableLayoutPanelMain.Controls.Add(this.fieldProductDate, 2, 7);
            this.tableLayoutPanelMain.Controls.Add(this.fieldKneading, 2, 5);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPalletSize, 3, 9);
            this.tableLayoutPanelMain.Controls.Add(this.fieldTitle, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPlu, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.labelPlu, 0, 5);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 13;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1179, 619);
            this.tableLayoutPanelMain.TabIndex = 7;
            this.tableLayoutPanelMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldCountBox
            // 
            this.fieldCountBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.fieldCountBox.Location = new System.Drawing.Point(1051, 461);
            this.fieldCountBox.Name = "fieldCountBox";
            this.fieldCountBox.Size = new System.Drawing.Size(112, 21);
            this.fieldCountBox.TabIndex = 30;
            this.fieldCountBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldResolution
            // 
            this.fieldResolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fieldResolution.FormattingEnabled = true;
            this.fieldResolution.Items.AddRange(new object[] {
            "Максимальное",
            "1024х768",
            "1366х768",
            "1920х1080"});
            this.fieldResolution.Location = new System.Drawing.Point(1051, 401);
            this.fieldResolution.Name = "fieldResolution";
            this.fieldResolution.Size = new System.Drawing.Size(112, 21);
            this.fieldResolution.TabIndex = 29;
            this.fieldResolution.Visible = false;
            this.fieldResolution.SelectedIndexChanged += new System.EventHandler(this.fieldResolution_SelectedIndexChanged);
            this.fieldResolution.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldCurrentTime
            // 
            this.fieldCurrentTime.AutoSize = true;
            this.fieldCurrentTime.BackColor = System.Drawing.Color.Transparent;
            this.fieldCurrentTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldCurrentTime.Enabled = false;
            this.fieldCurrentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldCurrentTime.Location = new System.Drawing.Point(14, 33);
            this.fieldCurrentTime.Margin = new System.Windows.Forms.Padding(3);
            this.fieldCurrentTime.Name = "fieldCurrentTime";
            this.fieldCurrentTime.Size = new System.Drawing.Size(323, 24);
            this.fieldCurrentTime.TabIndex = 25;
            this.fieldCurrentTime.Text = "Дата время";
            this.fieldCurrentTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldCurrentTime.DoubleClick += new System.EventHandler(this.fieldDt_DoubleClick);
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxClose.Image = global::ScalesUI.Properties.Resources.exit_2;
            this.pictureBoxClose.Location = new System.Drawing.Point(1051, 63);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(112, 93);
            this.pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxClose.TabIndex = 19;
            this.pictureBoxClose.TabStop = false;
            this.pictureBoxClose.Click += new System.EventHandler(this.pictureBoxClose_Click);
            // 
            // labelWeightTare
            // 
            this.labelWeightTare.AutoSize = true;
            this.labelWeightTare.BackColor = System.Drawing.Color.Transparent;
            this.labelWeightTare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWeightTare.Enabled = false;
            this.labelWeightTare.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWeightTare.Location = new System.Drawing.Point(14, 162);
            this.labelWeightTare.Margin = new System.Windows.Forms.Padding(3);
            this.labelWeightTare.Name = "labelWeightTare";
            this.labelWeightTare.Size = new System.Drawing.Size(323, 111);
            this.labelWeightTare.TabIndex = 17;
            this.labelWeightTare.Text = "Вес тары";
            this.labelWeightTare.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelWeightTare.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // lbKneading
            // 
            this.lbKneading.AutoSize = true;
            this.lbKneading.BackColor = System.Drawing.Color.Transparent;
            this.lbKneading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbKneading.Location = new System.Drawing.Point(14, 285);
            this.lbKneading.Margin = new System.Windows.Forms.Padding(3);
            this.lbKneading.Name = "lbKneading";
            this.lbKneading.Size = new System.Drawing.Size(323, 74);
            this.lbKneading.TabIndex = 27;
            this.lbKneading.Text = "Замес";
            this.lbKneading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbKneading.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // lbProductDate
            // 
            this.lbProductDate.AutoSize = true;
            this.lbProductDate.BackColor = System.Drawing.Color.Transparent;
            this.lbProductDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbProductDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbProductDate.Location = new System.Drawing.Point(14, 371);
            this.lbProductDate.Margin = new System.Windows.Forms.Padding(3);
            this.lbProductDate.Name = "lbProductDate";
            this.lbProductDate.Size = new System.Drawing.Size(323, 24);
            this.lbProductDate.TabIndex = 28;
            this.lbProductDate.Text = "Дата производства";
            this.lbProductDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbProductDate.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldProductDate
            // 
            this.fieldProductDate.AutoSize = true;
            this.fieldProductDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldProductDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldProductDate.Location = new System.Drawing.Point(343, 371);
            this.fieldProductDate.Margin = new System.Windows.Forms.Padding(3);
            this.fieldProductDate.Name = "fieldProductDate";
            this.fieldProductDate.Size = new System.Drawing.Size(702, 24);
            this.fieldProductDate.TabIndex = 31;
            this.fieldProductDate.Text = " Дата производства";
            this.fieldProductDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fieldKneading
            // 
            this.fieldKneading.AutoSize = true;
            this.fieldKneading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldKneading.Location = new System.Drawing.Point(343, 285);
            this.fieldKneading.Margin = new System.Windows.Forms.Padding(3);
            this.fieldKneading.Name = "fieldKneading";
            this.fieldKneading.Size = new System.Drawing.Size(702, 74);
            this.fieldKneading.TabIndex = 32;
            this.fieldKneading.Text = " Замес";
            this.fieldKneading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fieldPalletSize
            // 
            this.fieldPalletSize.AutoSize = true;
            this.fieldPalletSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPalletSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPalletSize.Location = new System.Drawing.Point(1051, 431);
            this.fieldPalletSize.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPalletSize.Name = "fieldPalletSize";
            this.fieldPalletSize.Size = new System.Drawing.Size(112, 24);
            this.fieldPalletSize.TabIndex = 37;
            this.fieldPalletSize.Text = "1 / 12";
            this.fieldPalletSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fieldTitle
            // 
            this.fieldTitle.AutoSize = true;
            this.fieldTitle.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanelMain.SetColumnSpan(this.fieldTitle, 5);
            this.fieldTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldTitle.Location = new System.Drawing.Point(3, 0);
            this.fieldTitle.Name = "fieldTitle";
            this.fieldTitle.Size = new System.Drawing.Size(1173, 30);
            this.fieldTitle.TabIndex = 20;
            this.fieldTitle.Text = "ScalesUI";
            this.fieldTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldPlu
            // 
            this.fieldPlu.AutoSize = true;
            this.fieldPlu.BackColor = System.Drawing.SystemColors.Control;
            this.fieldPlu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPlu.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.73585F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPlu.Location = new System.Drawing.Point(3, 60);
            this.fieldPlu.Name = "fieldPlu";
            this.fieldPlu.Size = new System.Drawing.Size(5, 99);
            this.fieldPlu.TabIndex = 14;
            this.fieldPlu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldPlu.Click += new System.EventHandler(this.buttonSelectPlu_Click);
            this.fieldPlu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // labelPlu
            // 
            this.labelPlu.AutoSize = true;
            this.labelPlu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPlu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPlu.Location = new System.Drawing.Point(3, 282);
            this.labelPlu.Name = "labelPlu";
            this.labelPlu.Size = new System.Drawing.Size(5, 80);
            this.labelPlu.TabIndex = 33;
            this.labelPlu.Text = "PLU";
            this.labelPlu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelPlu.Click += new System.EventHandler(this.buttonSelectPlu_Click);
            this.labelPlu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // buttonSettings
            // 
            this.buttonSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSettings.Location = new System.Drawing.Point(179, 3);
            this.buttonSettings.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(120, 122);
            this.buttonSettings.TabIndex = 0;
            this.buttonSettings.Text = "Настройки";
            this.buttonSettings.UseVisualStyleBackColor = false;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            this.buttonSettings.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // flowLayoutPanelBottom
            // 
            this.flowLayoutPanelBottom.Controls.Add(this.buttonPrint);
            this.flowLayoutPanelBottom.Controls.Add(this.buttonSetKneading);
            this.flowLayoutPanelBottom.Controls.Add(this.buttonSelectPlu);
            this.flowLayoutPanelBottom.Controls.Add(this.btAddKneading);
            this.flowLayoutPanelBottom.Controls.Add(this.btNewPallet);
            this.flowLayoutPanelBottom.Controls.Add(this.label1);
            this.flowLayoutPanelBottom.Controls.Add(this.buttonSetZero);
            this.flowLayoutPanelBottom.Controls.Add(this.buttonSettings);
            this.flowLayoutPanelBottom.Controls.Add(this.buttonSelectOrder);
            this.flowLayoutPanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelBottom.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelBottom.Location = new System.Drawing.Point(0, 619);
            this.flowLayoutPanelBottom.Name = "flowLayoutPanelBottom";
            this.flowLayoutPanelBottom.Size = new System.Drawing.Size(1179, 128);
            this.flowLayoutPanelBottom.TabIndex = 17;
            this.flowLayoutPanelBottom.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // buttonPrint
            // 
            this.buttonPrint.BackColor = System.Drawing.Color.Transparent;
            this.buttonPrint.Enabled = false;
            this.buttonPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPrint.Location = new System.Drawing.Point(1049, 3);
            this.buttonPrint.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(120, 122);
            this.buttonPrint.TabIndex = 6;
            this.buttonPrint.Text = "Печать";
            this.buttonPrint.UseVisualStyleBackColor = false;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            this.buttonPrint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // buttonSetKneading
            // 
            this.buttonSetKneading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonSetKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSetKneading.Location = new System.Drawing.Point(909, 3);
            this.buttonSetKneading.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.buttonSetKneading.Name = "buttonSetKneading";
            this.buttonSetKneading.Size = new System.Drawing.Size(120, 122);
            this.buttonSetKneading.TabIndex = 5;
            this.buttonSetKneading.Text = "ЕЩЁ";
            this.buttonSetKneading.UseVisualStyleBackColor = false;
            this.buttonSetKneading.Click += new System.EventHandler(this.buttonSetKneading_Click);
            this.buttonSetKneading.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // buttonSelectPlu
            // 
            this.buttonSelectPlu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonSelectPlu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSelectPlu.Location = new System.Drawing.Point(769, 3);
            this.buttonSelectPlu.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.buttonSelectPlu.Name = "buttonSelectPlu";
            this.buttonSelectPlu.Size = new System.Drawing.Size(120, 122);
            this.buttonSelectPlu.TabIndex = 3;
            this.buttonSelectPlu.Text = "Выбрать\r\nPLU";
            this.buttonSelectPlu.UseVisualStyleBackColor = false;
            this.buttonSelectPlu.Click += new System.EventHandler(this.buttonSelectPlu_Click);
            this.buttonSelectPlu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // btAddKneading
            // 
            this.btAddKneading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btAddKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btAddKneading.Location = new System.Drawing.Point(629, 3);
            this.btAddKneading.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btAddKneading.Name = "btAddKneading";
            this.btAddKneading.Size = new System.Drawing.Size(120, 122);
            this.btAddKneading.TabIndex = 35;
            this.btAddKneading.Text = "ЗаМЕС";
            this.btAddKneading.UseVisualStyleBackColor = false;
            this.btAddKneading.Click += new System.EventHandler(this.btAddKneading_Click);
            // 
            // btNewPallet
            // 
            this.btNewPallet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btNewPallet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btNewPallet.Location = new System.Drawing.Point(489, 3);
            this.btNewPallet.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.btNewPallet.Name = "btNewPallet";
            this.btNewPallet.Size = new System.Drawing.Size(120, 122);
            this.btNewPallet.TabIndex = 34;
            this.btNewPallet.Text = "Новая палета";
            this.btNewPallet.UseVisualStyleBackColor = false;
            this.btNewPallet.Click += new System.EventHandler(this.btNewPallet_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(454, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 118);
            this.label1.TabIndex = 33;
            // 
            // buttonSetZero
            // 
            this.buttonSetZero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonSetZero.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSetZero.Location = new System.Drawing.Point(319, 3);
            this.buttonSetZero.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.buttonSetZero.Name = "buttonSetZero";
            this.buttonSetZero.Size = new System.Drawing.Size(120, 122);
            this.buttonSetZero.TabIndex = 1;
            this.buttonSetZero.Text = ">0<";
            this.buttonSetZero.UseVisualStyleBackColor = false;
            this.buttonSetZero.Click += new System.EventHandler(this.buttonSetZero_Click);
            this.buttonSetZero.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // buttonSelectOrder
            // 
            this.buttonSelectOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buttonSelectOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSelectOrder.Location = new System.Drawing.Point(39, 3);
            this.buttonSelectOrder.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.buttonSelectOrder.Name = "buttonSelectOrder";
            this.buttonSelectOrder.Size = new System.Drawing.Size(120, 122);
            this.buttonSelectOrder.TabIndex = 4;
            this.buttonSelectOrder.Text = "Выбрать\r\nзаказ";
            this.buttonSelectOrder.UseVisualStyleBackColor = false;
            this.buttonSelectOrder.Click += new System.EventHandler(this.buttonSelectOrder_Click);
            this.buttonSelectOrder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldMemoryManager
            // 
            this.fieldMemoryManager.AutoSize = true;
            this.fieldMemoryManager.BackColor = System.Drawing.Color.Transparent;
            this.fieldMemoryManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMemoryManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMemoryManager.Location = new System.Drawing.Point(14, 431);
            this.fieldMemoryManager.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMemoryManager.Name = "fieldMemoryManager";
            this.fieldMemoryManager.Size = new System.Drawing.Size(323, 24);
            this.fieldMemoryManager.TabIndex = 42;
            this.fieldMemoryManager.Text = "Менеджер памяти";
            this.fieldMemoryManager.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fieldPrintManager
            // 
            this.fieldPrintManager.AutoSize = true;
            this.fieldPrintManager.BackColor = System.Drawing.Color.Transparent;
            this.fieldPrintManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPrintManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPrintManager.Location = new System.Drawing.Point(14, 461);
            this.fieldPrintManager.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPrintManager.Name = "fieldPrintManager";
            this.fieldPrintManager.Size = new System.Drawing.Size(323, 24);
            this.fieldPrintManager.TabIndex = 43;
            this.fieldPrintManager.Text = "Менеджер принтера";
            this.fieldPrintManager.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fieldMassaManager
            // 
            this.fieldMassaManager.AutoSize = true;
            this.fieldMassaManager.BackColor = System.Drawing.Color.Transparent;
            this.fieldMassaManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaManager.Location = new System.Drawing.Point(14, 491);
            this.fieldMassaManager.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaManager.Name = "fieldMassaManager";
            this.fieldMassaManager.Size = new System.Drawing.Size(323, 24);
            this.fieldMassaManager.TabIndex = 44;
            this.fieldMassaManager.Text = "Менеджер весов";
            this.fieldMassaManager.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 747);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Controls.Add(this.flowLayoutPanelBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Печать этикеток";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            this.flowLayoutPanelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelWeightNetto;
        private System.Windows.Forms.Label fieldWeightTare;
        private System.Windows.Forms.Label fieldWeightNetto;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Label fieldPlu;
        private System.Windows.Forms.Label labelWeightTare;
        private System.Windows.Forms.PictureBox pictureBoxClose;
        private System.Windows.Forms.Label fieldTitle;
        private System.Windows.Forms.Label fieldCurrentTime;
        private System.Windows.Forms.Label lbKneading;
        private System.Windows.Forms.Label lbProductDate;
        private System.Windows.Forms.ProgressBar fieldCountBox;
        private System.Windows.Forms.ComboBox fieldResolution;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBottom;
        private System.Windows.Forms.Button buttonSetZero;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.Button buttonSetKneading;
        private System.Windows.Forms.Button buttonSelectOrder;
        private System.Windows.Forms.Button buttonSelectPlu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Label fieldProductDate;
        private System.Windows.Forms.Label fieldKneading;
        private System.Windows.Forms.Label labelPlu;
        private System.Windows.Forms.Button btAddKneading;
        private System.Windows.Forms.Button btNewPallet;
        private System.Windows.Forms.Label fieldPalletSize;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label fieldMassaManager;
        private System.Windows.Forms.Label fieldPrintManager;
        private System.Windows.Forms.Label fieldMemoryManager;
    }
}

