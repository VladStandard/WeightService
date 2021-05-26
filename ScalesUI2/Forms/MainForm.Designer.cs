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
            this.progressBarCountBox = new System.Windows.Forms.ProgressBar();
            this.fieldResolution = new System.Windows.Forms.ComboBox();
            this.lbCurrentTime = new System.Windows.Forms.Label();
            this.fieldTitle = new System.Windows.Forms.Label();
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.fieldPlu = new System.Windows.Forms.Label();
            this.labelWeightTare = new System.Windows.Forms.Label();
            this.buttonSetWeightTare = new System.Windows.Forms.Button();
            this.pictureBoxScales = new System.Windows.Forms.PictureBox();
            this.lbKneading = new System.Windows.Forms.Label();
            this.lbProductDate = new System.Windows.Forms.Label();
            this.fieldProductDate = new System.Windows.Forms.Label();
            this.fieldKneading = new System.Windows.Forms.Label();
            this.labelPlu = new System.Windows.Forms.Label();
            this.tableLayoutPanelState = new System.Windows.Forms.TableLayoutPanel();
            this.panelPrint = new System.Windows.Forms.Panel();
            this.panelMassa = new System.Windows.Forms.Panel();
            this.fieldGrossWeight = new System.Windows.Forms.Label();
            this.fieldPalletSize = new System.Windows.Forms.Label();
            this.labelMemory = new System.Windows.Forms.Label();
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
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScales)).BeginInit();
            this.tableLayoutPanelState.SuspendLayout();
            this.flowLayoutPanelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelWeightNetto
            // 
            this.labelWeightNetto.AutoSize = true;
            this.labelWeightNetto.BackColor = System.Drawing.Color.Transparent;
            this.labelWeightNetto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWeightNetto.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWeightNetto.Location = new System.Drawing.Point(14, 136);
            this.labelWeightNetto.Margin = new System.Windows.Forms.Padding(3);
            this.labelWeightNetto.Name = "labelWeightNetto";
            this.labelWeightNetto.Size = new System.Drawing.Size(323, 112);
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
            this.fieldWeightTare.Location = new System.Drawing.Point(343, 262);
            this.fieldWeightTare.Margin = new System.Windows.Forms.Padding(3);
            this.fieldWeightTare.Name = "fieldWeightTare";
            this.fieldWeightTare.Size = new System.Drawing.Size(702, 78);
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
            this.fieldWeightNetto.Location = new System.Drawing.Point(343, 136);
            this.fieldWeightNetto.Margin = new System.Windows.Forms.Padding(3);
            this.fieldWeightNetto.Name = "fieldWeightNetto";
            this.fieldWeightNetto.Size = new System.Drawing.Size(702, 112);
            this.fieldWeightNetto.TabIndex = 10;
            this.fieldWeightNetto.Text = "0,000";
            this.fieldWeightNetto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldWeightNetto.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 5;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.9774789F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.9559F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0563F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.00938F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.000938F));
            this.tableLayoutPanelMain.Controls.Add(this.progressBarCountBox, 3, 10);
            this.tableLayoutPanelMain.Controls.Add(this.fieldResolution, 3, 11);
            this.tableLayoutPanelMain.Controls.Add(this.lbCurrentTime, 2, 12);
            this.tableLayoutPanelMain.Controls.Add(this.fieldTitle, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.pictureBoxClose, 3, 1);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPlu, 2, 1);
            this.tableLayoutPanelMain.Controls.Add(this.labelWeightTare, 1, 4);
            this.tableLayoutPanelMain.Controls.Add(this.labelWeightNetto, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.fieldWeightNetto, 2, 2);
            this.tableLayoutPanelMain.Controls.Add(this.fieldWeightTare, 2, 4);
            this.tableLayoutPanelMain.Controls.Add(this.buttonSetWeightTare, 3, 6);
            this.tableLayoutPanelMain.Controls.Add(this.pictureBoxScales, 3, 2);
            this.tableLayoutPanelMain.Controls.Add(this.lbKneading, 1, 7);
            this.tableLayoutPanelMain.Controls.Add(this.lbProductDate, 1, 8);
            this.tableLayoutPanelMain.Controls.Add(this.fieldProductDate, 2, 8);
            this.tableLayoutPanelMain.Controls.Add(this.fieldKneading, 2, 7);
            this.tableLayoutPanelMain.Controls.Add(this.labelPlu, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelState, 1, 9);
            this.tableLayoutPanelMain.Controls.Add(this.fieldGrossWeight, 2, 11);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPalletSize, 3, 9);
            this.tableLayoutPanelMain.Controls.Add(this.labelMemory, 1, 12);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 13;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.537183F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.02718F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.06301F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.292407F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.57027F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.292407F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.331179F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.537182F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.537182F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.537182F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.537182F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.481812F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.05961F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1179, 619);
            this.tableLayoutPanelMain.TabIndex = 7;
            this.tableLayoutPanelMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // progressBarCountBox
            // 
            this.progressBarCountBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBarCountBox.Location = new System.Drawing.Point(1051, 489);
            this.progressBarCountBox.Name = "progressBarCountBox";
            this.progressBarCountBox.Size = new System.Drawing.Size(112, 21);
            this.progressBarCountBox.TabIndex = 30;
            this.progressBarCountBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
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
            this.fieldResolution.Location = new System.Drawing.Point(1051, 523);
            this.fieldResolution.Name = "fieldResolution";
            this.fieldResolution.Size = new System.Drawing.Size(112, 21);
            this.fieldResolution.TabIndex = 29;
            this.fieldResolution.Visible = false;
            this.fieldResolution.SelectedIndexChanged += new System.EventHandler(this.fieldResolution_SelectedIndexChanged);
            this.fieldResolution.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // lbCurrentTime
            // 
            this.lbCurrentTime.AutoSize = true;
            this.lbCurrentTime.BackColor = System.Drawing.Color.Transparent;
            this.lbCurrentTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCurrentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.73585F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbCurrentTime.Location = new System.Drawing.Point(343, 556);
            this.lbCurrentTime.Margin = new System.Windows.Forms.Padding(3);
            this.lbCurrentTime.Name = "lbCurrentTime";
            this.lbCurrentTime.Size = new System.Drawing.Size(702, 60);
            this.lbCurrentTime.TabIndex = 25;
            this.lbCurrentTime.Text = "Дата время";
            this.lbCurrentTime.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbCurrentTime.DoubleClick += new System.EventHandler(this.fieldDt_DoubleClick);
            // 
            // fieldTitle
            // 
            this.fieldTitle.AutoSize = true;
            this.fieldTitle.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanelMain.SetColumnSpan(this.fieldTitle, 5);
            this.fieldTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.26415F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldTitle.Location = new System.Drawing.Point(3, 0);
            this.fieldTitle.Name = "fieldTitle";
            this.fieldTitle.Size = new System.Drawing.Size(1173, 34);
            this.fieldTitle.TabIndex = 20;
            this.fieldTitle.Text = "ScalesUI";
            this.fieldTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxClose.Image = global::ScalesUI.Properties.Resources.exit_2;
            this.pictureBoxClose.Location = new System.Drawing.Point(1051, 37);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(112, 93);
            this.pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxClose.TabIndex = 19;
            this.pictureBoxClose.TabStop = false;
            this.pictureBoxClose.Click += new System.EventHandler(this.pictureBoxClose_Click);
            // 
            // fieldPlu
            // 
            this.fieldPlu.AutoSize = true;
            this.fieldPlu.BackColor = System.Drawing.SystemColors.Control;
            this.fieldPlu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPlu.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.73585F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPlu.Location = new System.Drawing.Point(343, 34);
            this.fieldPlu.Name = "fieldPlu";
            this.fieldPlu.Size = new System.Drawing.Size(702, 99);
            this.fieldPlu.TabIndex = 14;
            this.fieldPlu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldPlu.Click += new System.EventHandler(this.buttonSelectPlu_Click);
            this.fieldPlu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // labelWeightTare
            // 
            this.labelWeightTare.AutoSize = true;
            this.labelWeightTare.BackColor = System.Drawing.Color.Transparent;
            this.labelWeightTare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWeightTare.Enabled = false;
            this.labelWeightTare.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWeightTare.Location = new System.Drawing.Point(14, 262);
            this.labelWeightTare.Margin = new System.Windows.Forms.Padding(3);
            this.labelWeightTare.Name = "labelWeightTare";
            this.labelWeightTare.Size = new System.Drawing.Size(323, 78);
            this.labelWeightTare.TabIndex = 17;
            this.labelWeightTare.Text = "Вес тары";
            this.labelWeightTare.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelWeightTare.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // buttonSetWeightTare
            // 
            this.buttonSetWeightTare.BackColor = System.Drawing.Color.Transparent;
            this.buttonSetWeightTare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSetWeightTare.Enabled = false;
            this.buttonSetWeightTare.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.26415F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSetWeightTare.Location = new System.Drawing.Point(1051, 354);
            this.buttonSetWeightTare.Name = "buttonSetWeightTare";
            this.buttonSetWeightTare.Size = new System.Drawing.Size(112, 27);
            this.buttonSetWeightTare.TabIndex = 13;
            this.buttonSetWeightTare.Text = ">T<";
            this.buttonSetWeightTare.UseVisualStyleBackColor = false;
            this.buttonSetWeightTare.Visible = false;
            this.buttonSetWeightTare.Click += new System.EventHandler(this.buttonSetWeightTare_Click);
            this.buttonSetWeightTare.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // pictureBoxScales
            // 
            this.pictureBoxScales.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxScales.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxScales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxScales.Location = new System.Drawing.Point(1051, 136);
            this.pictureBoxScales.Name = "pictureBoxScales";
            this.pictureBoxScales.Size = new System.Drawing.Size(112, 112);
            this.pictureBoxScales.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxScales.TabIndex = 15;
            this.pictureBoxScales.TabStop = false;
            this.pictureBoxScales.Visible = false;
            // 
            // lbKneading
            // 
            this.lbKneading.AutoSize = true;
            this.lbKneading.BackColor = System.Drawing.Color.Transparent;
            this.lbKneading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbKneading.Location = new System.Drawing.Point(14, 384);
            this.lbKneading.Name = "lbKneading";
            this.lbKneading.Size = new System.Drawing.Size(323, 34);
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
            this.lbProductDate.Location = new System.Drawing.Point(14, 418);
            this.lbProductDate.Name = "lbProductDate";
            this.lbProductDate.Size = new System.Drawing.Size(323, 34);
            this.lbProductDate.TabIndex = 28;
            this.lbProductDate.Text = "Дата производства";
            this.lbProductDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbProductDate.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldProductDate
            // 
            this.fieldProductDate.AutoSize = true;
            this.fieldProductDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldProductDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldProductDate.Location = new System.Drawing.Point(343, 418);
            this.fieldProductDate.Name = "fieldProductDate";
            this.fieldProductDate.Size = new System.Drawing.Size(702, 34);
            this.fieldProductDate.TabIndex = 31;
            this.fieldProductDate.Text = " ";
            this.fieldProductDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fieldKneading
            // 
            this.fieldKneading.AutoSize = true;
            this.fieldKneading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldKneading.Location = new System.Drawing.Point(343, 384);
            this.fieldKneading.Name = "fieldKneading";
            this.fieldKneading.Size = new System.Drawing.Size(702, 34);
            this.fieldKneading.TabIndex = 32;
            this.fieldKneading.Text = " ";
            this.fieldKneading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelPlu
            // 
            this.labelPlu.AutoSize = true;
            this.labelPlu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPlu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPlu.Location = new System.Drawing.Point(14, 34);
            this.labelPlu.Name = "labelPlu";
            this.labelPlu.Size = new System.Drawing.Size(323, 99);
            this.labelPlu.TabIndex = 33;
            this.labelPlu.Text = "PLU";
            this.labelPlu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelPlu.Click += new System.EventHandler(this.buttonSelectPlu_Click);
            this.labelPlu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // tableLayoutPanelState
            // 
            this.tableLayoutPanelState.ColumnCount = 9;
            this.tableLayoutPanelState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanelState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanelState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanelState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanelState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanelState.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelState.Controls.Add(this.panelPrint, 5, 0);
            this.tableLayoutPanelState.Controls.Add(this.panelMassa, 7, 0);
            this.tableLayoutPanelState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelState.Location = new System.Drawing.Point(14, 455);
            this.tableLayoutPanelState.Name = "tableLayoutPanelState";
            this.tableLayoutPanelState.RowCount = 1;
            this.tableLayoutPanelMain.SetRowSpan(this.tableLayoutPanelState, 3);
            this.tableLayoutPanelState.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelState.Size = new System.Drawing.Size(323, 95);
            this.tableLayoutPanelState.TabIndex = 35;
            // 
            // panelPrint
            // 
            this.panelPrint.Location = new System.Drawing.Point(168, 3);
            this.panelPrint.Name = "panelPrint";
            this.panelPrint.Size = new System.Drawing.Size(58, 89);
            this.panelPrint.TabIndex = 1;
            // 
            // panelMassa
            // 
            this.panelMassa.Location = new System.Drawing.Point(241, 3);
            this.panelMassa.Name = "panelMassa";
            this.panelMassa.Size = new System.Drawing.Size(58, 89);
            this.panelMassa.TabIndex = 2;
            // 
            // fieldGrossWeight
            // 
            this.fieldGrossWeight.AutoSize = true;
            this.fieldGrossWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldGrossWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldGrossWeight.Location = new System.Drawing.Point(343, 520);
            this.fieldGrossWeight.Name = "fieldGrossWeight";
            this.fieldGrossWeight.Size = new System.Drawing.Size(702, 33);
            this.fieldGrossWeight.TabIndex = 36;
            this.fieldGrossWeight.Text = "Вес брутто";
            this.fieldGrossWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // fieldPalletSize
            // 
            this.fieldPalletSize.AutoSize = true;
            this.fieldPalletSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPalletSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPalletSize.Location = new System.Drawing.Point(1051, 452);
            this.fieldPalletSize.Name = "fieldPalletSize";
            this.fieldPalletSize.Size = new System.Drawing.Size(112, 34);
            this.fieldPalletSize.TabIndex = 37;
            this.fieldPalletSize.Text = "123 / 321";
            this.fieldPalletSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMemory
            // 
            this.labelMemory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMemory.AutoSize = true;
            this.labelMemory.Location = new System.Drawing.Point(14, 553);
            this.labelMemory.Name = "labelMemory";
            this.labelMemory.Size = new System.Drawing.Size(323, 66);
            this.labelMemory.TabIndex = 38;
            this.labelMemory.Text = "Memory size";
            this.labelMemory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonSettings
            // 
            this.buttonSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSettings.Location = new System.Drawing.Point(130, 3);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(140, 122);
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
            this.buttonPrint.Location = new System.Drawing.Point(1036, 3);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(140, 122);
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
            this.buttonSetKneading.Location = new System.Drawing.Point(890, 3);
            this.buttonSetKneading.Name = "buttonSetKneading";
            this.buttonSetKneading.Size = new System.Drawing.Size(140, 122);
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
            this.buttonSelectPlu.Location = new System.Drawing.Point(744, 3);
            this.buttonSelectPlu.Name = "buttonSelectPlu";
            this.buttonSelectPlu.Size = new System.Drawing.Size(140, 122);
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
            this.btAddKneading.Location = new System.Drawing.Point(598, 3);
            this.btAddKneading.Name = "btAddKneading";
            this.btAddKneading.Size = new System.Drawing.Size(140, 122);
            this.btAddKneading.TabIndex = 35;
            this.btAddKneading.Text = "ЗаМЕС";
            this.btAddKneading.UseVisualStyleBackColor = false;
            this.btAddKneading.Click += new System.EventHandler(this.btAddKneading_Click);
            // 
            // btNewPallet
            // 
            this.btNewPallet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btNewPallet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btNewPallet.Location = new System.Drawing.Point(452, 3);
            this.btNewPallet.Name = "btNewPallet";
            this.btNewPallet.Size = new System.Drawing.Size(140, 122);
            this.btNewPallet.TabIndex = 34;
            this.btNewPallet.Text = "Новая палета";
            this.btNewPallet.UseVisualStyleBackColor = false;
            this.btNewPallet.Click += new System.EventHandler(this.btNewPallet_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(424, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 118);
            this.label1.TabIndex = 33;
            // 
            // buttonSetZero
            // 
            this.buttonSetZero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.buttonSetZero.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSetZero.Location = new System.Drawing.Point(276, 3);
            this.buttonSetZero.Name = "buttonSetZero";
            this.buttonSetZero.Size = new System.Drawing.Size(140, 122);
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
            this.buttonSelectOrder.Location = new System.Drawing.Point(44, 3);
            this.buttonSelectOrder.Name = "buttonSelectOrder";
            this.buttonSelectOrder.Size = new System.Drawing.Size(80, 122);
            this.buttonSelectOrder.TabIndex = 4;
            this.buttonSelectOrder.Text = "Выбрать\r\nзаказ";
            this.buttonSelectOrder.UseVisualStyleBackColor = false;
            this.buttonSelectOrder.Click += new System.EventHandler(this.buttonSelectOrder_Click);
            this.buttonSelectOrder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
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
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScales)).EndInit();
            this.tableLayoutPanelState.ResumeLayout(false);
            this.flowLayoutPanelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelWeightNetto;
        private System.Windows.Forms.Label fieldWeightTare;
        private System.Windows.Forms.Label fieldWeightNetto;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Button buttonSetWeightTare;
        private System.Windows.Forms.Label fieldPlu;
        private System.Windows.Forms.PictureBox pictureBoxScales;
        private System.Windows.Forms.Label labelWeightTare;
        private System.Windows.Forms.PictureBox pictureBoxClose;
        private System.Windows.Forms.Label fieldTitle;
        private System.Windows.Forms.Label lbCurrentTime;
        private System.Windows.Forms.Label lbKneading;
        private System.Windows.Forms.Label lbProductDate;
        private System.Windows.Forms.ProgressBar progressBarCountBox;
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelState;
        private System.Windows.Forms.Label fieldGrossWeight;
        private System.Windows.Forms.Button btAddKneading;
        private System.Windows.Forms.Button btNewPallet;
        private System.Windows.Forms.Label fieldPalletSize;
        private System.Windows.Forms.Panel panelPrint;
        private System.Windows.Forms.Panel panelMassa;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelMemory;
    }
}

