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
            this.labelWeightNetto = new System.Windows.Forms.Label();
            this.fieldWeightTare = new System.Windows.Forms.Label();
            this.fieldWeightNetto = new System.Windows.Forms.Label();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.fieldPrintShippingManager = new System.Windows.Forms.Label();
            this.fieldPrintProgressShipping = new System.Windows.Forms.ProgressBar();
            this.fieldPrintShipping = new System.Windows.Forms.Label();
            this.fieldMassaThreshold = new System.Windows.Forms.Label();
            this.fieldSscc = new System.Windows.Forms.Label();
            this.fieldTasks = new System.Windows.Forms.Label();
            this.fieldMemory = new System.Windows.Forms.Label();
            this.fieldPrintMainManager = new System.Windows.Forms.Label();
            this.fieldMassaManager = new System.Windows.Forms.Label();
            this.fieldMassaPluDescription = new System.Windows.Forms.Label();
            this.fieldMassaGet = new System.Windows.Forms.Label();
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.labelWeightTare = new System.Windows.Forms.Label();
            this.labelKneading = new System.Windows.Forms.Label();
            this.labelProductDate = new System.Windows.Forms.Label();
            this.fieldProductDate = new System.Windows.Forms.Label();
            this.fieldKneading = new System.Windows.Forms.Label();
            this.fieldPrintMain = new System.Windows.Forms.Label();
            this.fieldTitle = new System.Windows.Forms.Label();
            this.fieldPlu = new System.Windows.Forms.Label();
            this.tableLayoutPanelRight = new System.Windows.Forms.TableLayoutPanel();
            this.fieldLang = new System.Windows.Forms.ComboBox();
            this.fieldResolution = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            this.tableLayoutPanelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelWeightNetto
            // 
            this.labelWeightNetto.AutoSize = true;
            this.labelWeightNetto.BackColor = System.Drawing.Color.Transparent;
            this.labelWeightNetto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWeightNetto.Enabled = false;
            this.labelWeightNetto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWeightNetto.ForeColor = System.Drawing.Color.Black;
            this.labelWeightNetto.Location = new System.Drawing.Point(8, 109);
            this.labelWeightNetto.Margin = new System.Windows.Forms.Padding(3);
            this.labelWeightNetto.Name = "labelWeightNetto";
            this.labelWeightNetto.Size = new System.Drawing.Size(247, 47);
            this.labelWeightNetto.TabIndex = 12;
            this.labelWeightNetto.Text = "labelWeightNetto";
            this.labelWeightNetto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelWeightNetto.Visible = false;
            // 
            // fieldWeightTare
            // 
            this.fieldWeightTare.AutoSize = true;
            this.fieldWeightTare.BackColor = System.Drawing.Color.Transparent;
            this.fieldWeightTare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldWeightTare.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldWeightTare.ForeColor = System.Drawing.Color.Black;
            this.fieldWeightTare.Location = new System.Drawing.Point(261, 162);
            this.fieldWeightTare.Margin = new System.Windows.Forms.Padding(3);
            this.fieldWeightTare.Name = "fieldWeightTare";
            this.fieldWeightTare.Size = new System.Drawing.Size(648, 47);
            this.fieldWeightTare.TabIndex = 11;
            this.fieldWeightTare.Text = "0,000";
            this.fieldWeightTare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldWeightTare.Visible = false;
            // 
            // fieldWeightNetto
            // 
            this.fieldWeightNetto.AutoSize = true;
            this.fieldWeightNetto.BackColor = System.Drawing.Color.Transparent;
            this.fieldWeightNetto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldWeightNetto.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldWeightNetto.ForeColor = System.Drawing.Color.Black;
            this.fieldWeightNetto.Location = new System.Drawing.Point(261, 109);
            this.fieldWeightNetto.Margin = new System.Windows.Forms.Padding(3);
            this.fieldWeightNetto.Name = "fieldWeightNetto";
            this.fieldWeightNetto.Size = new System.Drawing.Size(648, 47);
            this.fieldWeightNetto.TabIndex = 10;
            this.fieldWeightNetto.Text = "0,000";
            this.fieldWeightNetto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldWeightNetto.Visible = false;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 5;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.5154651F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.74229F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.91757F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.30921F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.5154651F));
            this.tableLayoutPanelMain.Controls.Add(this.fieldPrintShippingManager, 3, 8);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPrintProgressShipping, 3, 9);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPrintShipping, 1, 8);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaThreshold, 1, 9);
            this.tableLayoutPanelMain.Controls.Add(this.fieldSscc, 1, 6);
            this.tableLayoutPanelMain.Controls.Add(this.fieldTasks, 3, 13);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMemory, 1, 13);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPrintMainManager, 3, 7);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaManager, 3, 11);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaPluDescription, 1, 10);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaGet, 1, 11);
            this.tableLayoutPanelMain.Controls.Add(this.pictureBoxClose, 3, 0);
            this.tableLayoutPanelMain.Controls.Add(this.labelWeightTare, 1, 3);
            this.tableLayoutPanelMain.Controls.Add(this.labelWeightNetto, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.fieldWeightNetto, 2, 2);
            this.tableLayoutPanelMain.Controls.Add(this.fieldWeightTare, 2, 3);
            this.tableLayoutPanelMain.Controls.Add(this.labelKneading, 1, 5);
            this.tableLayoutPanelMain.Controls.Add(this.labelProductDate, 1, 4);
            this.tableLayoutPanelMain.Controls.Add(this.fieldProductDate, 2, 4);
            this.tableLayoutPanelMain.Controls.Add(this.fieldKneading, 2, 5);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPrintMain, 1, 7);
            this.tableLayoutPanelMain.Controls.Add(this.fieldTitle, 2, 0);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPlu, 2, 1);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelRight, 3, 3);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 15;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1024, 668);
            this.tableLayoutPanelMain.TabIndex = 7;
            // 
            // fieldPrintShippingManager
            // 
            this.fieldPrintShippingManager.AutoSize = true;
            this.fieldPrintShippingManager.BackColor = System.Drawing.Color.Transparent;
            this.fieldPrintShippingManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPrintShippingManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPrintShippingManager.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldPrintShippingManager.Location = new System.Drawing.Point(915, 361);
            this.fieldPrintShippingManager.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPrintShippingManager.Name = "fieldPrintShippingManager";
            this.fieldPrintShippingManager.Size = new System.Drawing.Size(99, 17);
            this.fieldPrintShippingManager.TabIndex = 68;
            this.fieldPrintShippingManager.Text = "fieldPrintShippingManager";
            this.fieldPrintShippingManager.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldPrintShippingManager.Visible = false;
            // 
            // fieldPrintProgressShipping
            // 
            this.fieldPrintProgressShipping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPrintProgressShipping.Enabled = false;
            this.fieldPrintProgressShipping.Location = new System.Drawing.Point(915, 384);
            this.fieldPrintProgressShipping.Name = "fieldPrintProgressShipping";
            this.fieldPrintProgressShipping.Size = new System.Drawing.Size(99, 17);
            this.fieldPrintProgressShipping.TabIndex = 67;
            this.fieldPrintProgressShipping.Visible = false;
            // 
            // fieldPrintShipping
            // 
            this.fieldPrintShipping.AutoSize = true;
            this.fieldPrintShipping.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelMain.SetColumnSpan(this.fieldPrintShipping, 2);
            this.fieldPrintShipping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPrintShipping.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPrintShipping.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldPrintShipping.Location = new System.Drawing.Point(8, 361);
            this.fieldPrintShipping.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPrintShipping.Name = "fieldPrintShipping";
            this.fieldPrintShipping.Size = new System.Drawing.Size(901, 17);
            this.fieldPrintShipping.TabIndex = 66;
            this.fieldPrintShipping.Text = "fieldPrintLabelsShipping";
            this.fieldPrintShipping.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldPrintShipping.Visible = false;
            this.fieldPrintShipping.Click += new System.EventHandler(this.FieldPrintManager_Click);
            // 
            // fieldMassaThreshold
            // 
            this.fieldMassaThreshold.AutoSize = true;
            this.fieldMassaThreshold.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelMain.SetColumnSpan(this.fieldMassaThreshold, 2);
            this.fieldMassaThreshold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaThreshold.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaThreshold.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaThreshold.Location = new System.Drawing.Point(8, 384);
            this.fieldMassaThreshold.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaThreshold.Name = "fieldMassaThreshold";
            this.fieldMassaThreshold.Size = new System.Drawing.Size(901, 17);
            this.fieldMassaThreshold.TabIndex = 64;
            this.fieldMassaThreshold.Text = "fieldMassaThreshold";
            this.fieldMassaThreshold.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMassaThreshold.Visible = false;
            // 
            // fieldSscc
            // 
            this.fieldSscc.AutoSize = true;
            this.fieldSscc.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelMain.SetColumnSpan(this.fieldSscc, 2);
            this.fieldSscc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldSscc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldSscc.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldSscc.Location = new System.Drawing.Point(8, 315);
            this.fieldSscc.Margin = new System.Windows.Forms.Padding(3);
            this.fieldSscc.Name = "fieldSscc";
            this.fieldSscc.Size = new System.Drawing.Size(901, 17);
            this.fieldSscc.TabIndex = 61;
            this.fieldSscc.Text = "fieldSscc";
            this.fieldSscc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldSscc.Visible = false;
            this.fieldSscc.Click += new System.EventHandler(this.FieldSscc_Click);
            this.fieldSscc.DoubleClick += new System.EventHandler(this.FieldSscc_Click);
            // 
            // fieldTasks
            // 
            this.fieldTasks.AutoSize = true;
            this.fieldTasks.BackColor = System.Drawing.Color.Transparent;
            this.fieldTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldTasks.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldTasks.Location = new System.Drawing.Point(915, 476);
            this.fieldTasks.Margin = new System.Windows.Forms.Padding(3);
            this.fieldTasks.Name = "fieldTasks";
            this.fieldTasks.Size = new System.Drawing.Size(99, 17);
            this.fieldTasks.TabIndex = 57;
            this.fieldTasks.Text = "fieldTasks";
            this.fieldTasks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldTasks.Visible = false;
            this.fieldTasks.Click += new System.EventHandler(this.FieldTasks_Click);
            this.fieldTasks.DoubleClick += new System.EventHandler(this.FieldTasks_Click);
            // 
            // fieldMemory
            // 
            this.fieldMemory.AutoSize = true;
            this.fieldMemory.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelMain.SetColumnSpan(this.fieldMemory, 2);
            this.fieldMemory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMemory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMemory.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMemory.Location = new System.Drawing.Point(8, 476);
            this.fieldMemory.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMemory.Name = "fieldMemory";
            this.fieldMemory.Size = new System.Drawing.Size(901, 17);
            this.fieldMemory.TabIndex = 53;
            this.fieldMemory.Text = "fieldMemory";
            this.fieldMemory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMemory.Visible = false;
            // 
            // fieldPrintMainManager
            // 
            this.fieldPrintMainManager.AutoSize = true;
            this.fieldPrintMainManager.BackColor = System.Drawing.Color.Transparent;
            this.fieldPrintMainManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPrintMainManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPrintMainManager.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldPrintMainManager.Location = new System.Drawing.Point(915, 338);
            this.fieldPrintMainManager.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPrintMainManager.Name = "fieldPrintMainManager";
            this.fieldPrintMainManager.Size = new System.Drawing.Size(99, 17);
            this.fieldPrintMainManager.TabIndex = 52;
            this.fieldPrintMainManager.Text = "fieldPrintMainManager";
            this.fieldPrintMainManager.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldPrintMainManager.Visible = false;
            // 
            // fieldMassaManager
            // 
            this.fieldMassaManager.AutoSize = true;
            this.fieldMassaManager.BackColor = System.Drawing.Color.Transparent;
            this.fieldMassaManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaManager.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaManager.Location = new System.Drawing.Point(915, 430);
            this.fieldMassaManager.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaManager.Name = "fieldMassaManager";
            this.fieldMassaManager.Size = new System.Drawing.Size(99, 17);
            this.fieldMassaManager.TabIndex = 51;
            this.fieldMassaManager.Text = "fieldMassaManager";
            this.fieldMassaManager.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldMassaManager.Visible = false;
            // 
            // fieldMassaPluDescription
            // 
            this.fieldMassaPluDescription.AutoSize = true;
            this.fieldMassaPluDescription.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelMain.SetColumnSpan(this.fieldMassaPluDescription, 2);
            this.fieldMassaPluDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaPluDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaPluDescription.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaPluDescription.Location = new System.Drawing.Point(8, 407);
            this.fieldMassaPluDescription.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaPluDescription.Name = "fieldMassaPluDescription";
            this.fieldMassaPluDescription.Size = new System.Drawing.Size(901, 17);
            this.fieldMassaPluDescription.TabIndex = 49;
            this.fieldMassaPluDescription.Text = "fieldMassaPluDescription";
            this.fieldMassaPluDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMassaPluDescription.Visible = false;
            // 
            // fieldMassaGet
            // 
            this.fieldMassaGet.AutoSize = true;
            this.fieldMassaGet.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelMain.SetColumnSpan(this.fieldMassaGet, 2);
            this.fieldMassaGet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaGet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaGet.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaGet.Location = new System.Drawing.Point(8, 430);
            this.fieldMassaGet.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaGet.Name = "fieldMassaGet";
            this.fieldMassaGet.Size = new System.Drawing.Size(901, 17);
            this.fieldMassaGet.TabIndex = 46;
            this.fieldMassaGet.Text = "fieldMassaGet";
            this.fieldMassaGet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMassaGet.Visible = false;
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxClose.Image = global::ScalesUI.Properties.Resources.exit_1;
            this.pictureBoxClose.Location = new System.Drawing.Point(915, 3);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.tableLayoutPanelMain.SetRowSpan(this.pictureBoxClose, 2);
            this.pictureBoxClose.Size = new System.Drawing.Size(99, 100);
            this.pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxClose.TabIndex = 19;
            this.pictureBoxClose.TabStop = false;
            this.pictureBoxClose.Visible = false;
            this.pictureBoxClose.Click += new System.EventHandler(this.ActionClose_Click);
            // 
            // labelWeightTare
            // 
            this.labelWeightTare.AutoSize = true;
            this.labelWeightTare.BackColor = System.Drawing.Color.Transparent;
            this.labelWeightTare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWeightTare.Enabled = false;
            this.labelWeightTare.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWeightTare.ForeColor = System.Drawing.Color.Black;
            this.labelWeightTare.Location = new System.Drawing.Point(8, 162);
            this.labelWeightTare.Margin = new System.Windows.Forms.Padding(3);
            this.labelWeightTare.Name = "labelWeightTare";
            this.labelWeightTare.Size = new System.Drawing.Size(247, 47);
            this.labelWeightTare.TabIndex = 17;
            this.labelWeightTare.Text = "labelWeightTare";
            this.labelWeightTare.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelWeightTare.Visible = false;
            // 
            // labelKneading
            // 
            this.labelKneading.AutoSize = true;
            this.labelKneading.BackColor = System.Drawing.Color.Transparent;
            this.labelKneading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelKneading.Enabled = false;
            this.labelKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelKneading.ForeColor = System.Drawing.Color.Black;
            this.labelKneading.Location = new System.Drawing.Point(8, 265);
            this.labelKneading.Margin = new System.Windows.Forms.Padding(3);
            this.labelKneading.Name = "labelKneading";
            this.labelKneading.Size = new System.Drawing.Size(247, 44);
            this.labelKneading.TabIndex = 27;
            this.labelKneading.Text = "labelKneading";
            this.labelKneading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelKneading.Visible = false;
            // 
            // labelProductDate
            // 
            this.labelProductDate.AutoSize = true;
            this.labelProductDate.BackColor = System.Drawing.Color.Transparent;
            this.labelProductDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProductDate.Enabled = false;
            this.labelProductDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelProductDate.ForeColor = System.Drawing.Color.Black;
            this.labelProductDate.Location = new System.Drawing.Point(8, 215);
            this.labelProductDate.Margin = new System.Windows.Forms.Padding(3);
            this.labelProductDate.Name = "labelProductDate";
            this.labelProductDate.Size = new System.Drawing.Size(247, 44);
            this.labelProductDate.TabIndex = 28;
            this.labelProductDate.Text = "labelProductDate";
            this.labelProductDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelProductDate.Visible = false;
            // 
            // fieldProductDate
            // 
            this.fieldProductDate.AutoSize = true;
            this.fieldProductDate.BackColor = System.Drawing.Color.Transparent;
            this.fieldProductDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldProductDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldProductDate.ForeColor = System.Drawing.Color.Black;
            this.fieldProductDate.Location = new System.Drawing.Point(261, 215);
            this.fieldProductDate.Margin = new System.Windows.Forms.Padding(3);
            this.fieldProductDate.Name = "fieldProductDate";
            this.fieldProductDate.Size = new System.Drawing.Size(648, 44);
            this.fieldProductDate.TabIndex = 31;
            this.fieldProductDate.Text = "ProductDate";
            this.fieldProductDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldProductDate.Visible = false;
            // 
            // fieldKneading
            // 
            this.fieldKneading.AutoSize = true;
            this.fieldKneading.BackColor = System.Drawing.Color.Transparent;
            this.fieldKneading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldKneading.ForeColor = System.Drawing.Color.Black;
            this.fieldKneading.Location = new System.Drawing.Point(261, 265);
            this.fieldKneading.Margin = new System.Windows.Forms.Padding(3);
            this.fieldKneading.Name = "fieldKneading";
            this.fieldKneading.Size = new System.Drawing.Size(648, 44);
            this.fieldKneading.TabIndex = 32;
            this.fieldKneading.Text = "Kneading";
            this.fieldKneading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldKneading.Visible = false;
            // 
            // fieldPrintMain
            // 
            this.fieldPrintMain.AutoSize = true;
            this.fieldPrintMain.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelMain.SetColumnSpan(this.fieldPrintMain, 2);
            this.fieldPrintMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPrintMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPrintMain.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldPrintMain.Location = new System.Drawing.Point(8, 338);
            this.fieldPrintMain.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPrintMain.Name = "fieldPrintMain";
            this.fieldPrintMain.Size = new System.Drawing.Size(901, 17);
            this.fieldPrintMain.TabIndex = 37;
            this.fieldPrintMain.Text = "fieldPrintLabelsMain";
            this.fieldPrintMain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldPrintMain.Visible = false;
            this.fieldPrintMain.Click += new System.EventHandler(this.FieldPrintManager_Click);
            // 
            // fieldTitle
            // 
            this.fieldTitle.AutoSize = true;
            this.fieldTitle.BackColor = System.Drawing.Color.Transparent;
            this.fieldTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldTitle.ForeColor = System.Drawing.Color.Black;
            this.fieldTitle.Location = new System.Drawing.Point(261, 0);
            this.fieldTitle.Name = "fieldTitle";
            this.fieldTitle.Size = new System.Drawing.Size(648, 53);
            this.fieldTitle.TabIndex = 20;
            this.fieldTitle.Text = "ScalesUI";
            this.fieldTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldTitle.Visible = false;
            // 
            // fieldPlu
            // 
            this.fieldPlu.AutoSize = true;
            this.fieldPlu.BackColor = System.Drawing.Color.Transparent;
            this.fieldPlu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPlu.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPlu.ForeColor = System.Drawing.Color.Black;
            this.fieldPlu.Location = new System.Drawing.Point(261, 53);
            this.fieldPlu.Name = "fieldPlu";
            this.fieldPlu.Size = new System.Drawing.Size(648, 53);
            this.fieldPlu.TabIndex = 14;
            this.fieldPlu.Text = "PLU";
            this.fieldPlu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldPlu.Visible = false;
            this.fieldPlu.Click += new System.EventHandler(this.ActionPlu_Click);
            // 
            // tableLayoutPanelRight
            // 
            this.tableLayoutPanelRight.ColumnCount = 1;
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRight.Controls.Add(this.fieldLang, 0, 1);
            this.tableLayoutPanelRight.Controls.Add(this.fieldResolution, 0, 0);
            this.tableLayoutPanelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelRight.Location = new System.Drawing.Point(915, 162);
            this.tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            this.tableLayoutPanelRight.RowCount = 2;
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRight.Size = new System.Drawing.Size(99, 47);
            this.tableLayoutPanelRight.TabIndex = 62;
            // 
            // fieldLang
            // 
            this.fieldLang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fieldLang.Enabled = false;
            this.fieldLang.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldLang.FormattingEnabled = true;
            this.fieldLang.Location = new System.Drawing.Point(3, 26);
            this.fieldLang.Name = "fieldLang";
            this.fieldLang.Size = new System.Drawing.Size(93, 21);
            this.fieldLang.TabIndex = 57;
            this.fieldLang.Visible = false;
            // 
            // fieldResolution
            // 
            this.fieldResolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fieldResolution.Enabled = false;
            this.fieldResolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldResolution.FormattingEnabled = true;
            this.fieldResolution.Location = new System.Drawing.Point(3, 3);
            this.fieldResolution.Name = "fieldResolution";
            this.fieldResolution.Size = new System.Drawing.Size(93, 21);
            this.fieldResolution.TabIndex = 30;
            this.fieldResolution.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 668);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Печать этикеток";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            this.tableLayoutPanelRight.ResumeLayout(false);
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
        private System.Windows.Forms.Label labelKneading;
        private System.Windows.Forms.Label labelProductDate;
        private System.Windows.Forms.Label fieldProductDate;
        private System.Windows.Forms.Label fieldKneading;
        private System.Windows.Forms.Label fieldPrintMain;
        private System.Windows.Forms.Label fieldMassaGet;
        private System.Windows.Forms.Label fieldMassaPluDescription;
        private System.Windows.Forms.Label fieldPrintMainManager;
        private System.Windows.Forms.Label fieldMassaManager;
        private System.Windows.Forms.Label fieldMemory;
        private System.Windows.Forms.Label fieldTasks;
        private System.Windows.Forms.Label fieldSscc;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRight;
        private System.Windows.Forms.ComboBox fieldLang;
        private System.Windows.Forms.ComboBox fieldResolution;
        private System.Windows.Forms.Label fieldMassaThreshold;
        private System.Windows.Forms.Label fieldPrintShipping;
        private System.Windows.Forms.ProgressBar fieldPrintProgressShipping;
        private System.Windows.Forms.Label fieldPrintShippingManager;
    }
}

