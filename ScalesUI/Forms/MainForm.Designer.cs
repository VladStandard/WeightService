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
            if (disposing && (components is not null))
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
            this.labelNettoWeight = new System.Windows.Forms.Label();
            this.fieldPackageWeight = new System.Windows.Forms.Label();
            this.fieldNettoWeight = new System.Windows.Forms.Label();
            this.layoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.fieldPrintShippingManager = new System.Windows.Forms.Label();
            this.fieldPrintShipping = new System.Windows.Forms.Label();
            this.fieldSscc = new System.Windows.Forms.Label();
            this.fieldTasks = new System.Windows.Forms.Label();
            this.fieldMemory = new System.Windows.Forms.Label();
            this.fieldPrintMainManager = new System.Windows.Forms.Label();
            this.fieldMassaManager = new System.Windows.Forms.Label();
            this.fieldMassaGet = new System.Windows.Forms.Label();
            this.labelPackageWeight = new System.Windows.Forms.Label();
            this.labelKneading = new System.Windows.Forms.Label();
            this.labelProductDate = new System.Windows.Forms.Label();
            this.fieldProductDate = new System.Windows.Forms.Label();
            this.fieldKneading = new System.Windows.Forms.Label();
            this.fieldPrintMain = new System.Windows.Forms.Label();
            this.fieldPlu = new System.Windows.Forms.Label();
            this.layoutPanelTop = new System.Windows.Forms.TableLayoutPanel();
            this.fieldLang = new System.Windows.Forms.ComboBox();
            this.fieldResolution = new System.Windows.Forms.ComboBox();
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.fieldTitle = new System.Windows.Forms.Label();
            this.layoutPanel.SuspendLayout();
            this.layoutPanelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            this.SuspendLayout();
            // 
            // labelNettoWeight
            // 
            this.labelNettoWeight.AutoSize = true;
            this.labelNettoWeight.BackColor = System.Drawing.Color.Transparent;
            this.labelNettoWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNettoWeight.Enabled = false;
            this.labelNettoWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNettoWeight.ForeColor = System.Drawing.Color.Black;
            this.labelNettoWeight.Location = new System.Drawing.Point(110, 96);
            this.labelNettoWeight.Margin = new System.Windows.Forms.Padding(3);
            this.labelNettoWeight.Name = "labelNettoWeight";
            this.labelNettoWeight.Size = new System.Drawing.Size(198, 47);
            this.labelNettoWeight.TabIndex = 12;
            this.labelNettoWeight.Text = "labelNettoWeight";
            this.labelNettoWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // fieldPackageWeight
            // 
            this.fieldPackageWeight.AutoSize = true;
            this.fieldPackageWeight.BackColor = System.Drawing.Color.Transparent;
            this.fieldPackageWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPackageWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPackageWeight.ForeColor = System.Drawing.Color.Black;
            this.fieldPackageWeight.Location = new System.Drawing.Point(314, 149);
            this.fieldPackageWeight.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPackageWeight.Name = "fieldPackageWeight";
            this.fieldPackageWeight.Size = new System.Drawing.Size(700, 47);
            this.fieldPackageWeight.TabIndex = 11;
            this.fieldPackageWeight.Text = "0,000";
            this.fieldPackageWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fieldNettoWeight
            // 
            this.fieldNettoWeight.AutoSize = true;
            this.fieldNettoWeight.BackColor = System.Drawing.Color.Transparent;
            this.fieldNettoWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldNettoWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldNettoWeight.ForeColor = System.Drawing.Color.Black;
            this.fieldNettoWeight.Location = new System.Drawing.Point(314, 96);
            this.fieldNettoWeight.Margin = new System.Windows.Forms.Padding(3);
            this.fieldNettoWeight.Name = "fieldNettoWeight";
            this.fieldNettoWeight.Size = new System.Drawing.Size(700, 47);
            this.fieldNettoWeight.TabIndex = 10;
            this.fieldNettoWeight.Text = "0,000";
            this.fieldNettoWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // layoutPanel
            // 
            this.layoutPanel.ColumnCount = 5;
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.5F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69F));
            this.layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.5F));
            this.layoutPanel.Controls.Add(this.fieldPrintShippingManager, 1, 8);
            this.layoutPanel.Controls.Add(this.fieldPrintShipping, 2, 8);
            this.layoutPanel.Controls.Add(this.fieldSscc, 2, 6);
            this.layoutPanel.Controls.Add(this.fieldTasks, 1, 13);
            this.layoutPanel.Controls.Add(this.fieldMemory, 2, 13);
            this.layoutPanel.Controls.Add(this.fieldPrintMainManager, 1, 7);
            this.layoutPanel.Controls.Add(this.fieldMassaManager, 1, 9);
            this.layoutPanel.Controls.Add(this.fieldMassaGet, 2, 9);
            this.layoutPanel.Controls.Add(this.labelPackageWeight, 2, 3);
            this.layoutPanel.Controls.Add(this.labelNettoWeight, 2, 2);
            this.layoutPanel.Controls.Add(this.fieldNettoWeight, 3, 2);
            this.layoutPanel.Controls.Add(this.fieldPackageWeight, 3, 3);
            this.layoutPanel.Controls.Add(this.labelKneading, 2, 5);
            this.layoutPanel.Controls.Add(this.labelProductDate, 2, 4);
            this.layoutPanel.Controls.Add(this.fieldProductDate, 3, 4);
            this.layoutPanel.Controls.Add(this.fieldKneading, 3, 5);
            this.layoutPanel.Controls.Add(this.fieldPrintMain, 2, 7);
            this.layoutPanel.Controls.Add(this.fieldPlu, 1, 1);
            this.layoutPanel.Controls.Add(this.layoutPanelTop, 1, 0);
            this.layoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanel.Location = new System.Drawing.Point(0, 0);
            this.layoutPanel.Name = "layoutPanel";
            this.layoutPanel.RowCount = 15;
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.75F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.75F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.75F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.75F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.75F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.75F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.75F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.75F));
            this.layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.layoutPanel.Size = new System.Drawing.Size(1024, 668);
            this.layoutPanel.TabIndex = 7;
            // 
            // fieldPrintShippingManager
            // 
            this.fieldPrintShippingManager.AutoSize = true;
            this.fieldPrintShippingManager.BackColor = System.Drawing.Color.Transparent;
            this.fieldPrintShippingManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPrintShippingManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPrintShippingManager.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldPrintShippingManager.Location = new System.Drawing.Point(8, 352);
            this.fieldPrintShippingManager.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPrintShippingManager.Name = "fieldPrintShippingManager";
            this.fieldPrintShippingManager.Size = new System.Drawing.Size(96, 19);
            this.fieldPrintShippingManager.TabIndex = 68;
            this.fieldPrintShippingManager.Text = "fieldPrintShippingManager";
            this.fieldPrintShippingManager.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldPrintShippingManager.Visible = false;
            // 
            // fieldPrintShipping
            // 
            this.fieldPrintShipping.AutoSize = true;
            this.fieldPrintShipping.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanel.SetColumnSpan(this.fieldPrintShipping, 2);
            this.fieldPrintShipping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPrintShipping.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPrintShipping.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldPrintShipping.Location = new System.Drawing.Point(110, 352);
            this.fieldPrintShipping.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPrintShipping.Name = "fieldPrintShipping";
            this.fieldPrintShipping.Size = new System.Drawing.Size(904, 19);
            this.fieldPrintShipping.TabIndex = 66;
            this.fieldPrintShipping.Text = "fieldPrintLabelsShipping";
            this.fieldPrintShipping.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldPrintShipping.Visible = false;
            this.fieldPrintShipping.Click += new System.EventHandler(this.FieldPrintManager_Click);
            // 
            // fieldSscc
            // 
            this.fieldSscc.AutoSize = true;
            this.fieldSscc.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanel.SetColumnSpan(this.fieldSscc, 2);
            this.fieldSscc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldSscc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldSscc.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldSscc.Location = new System.Drawing.Point(110, 302);
            this.fieldSscc.Margin = new System.Windows.Forms.Padding(3);
            this.fieldSscc.Name = "fieldSscc";
            this.fieldSscc.Size = new System.Drawing.Size(904, 19);
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
            this.fieldTasks.Location = new System.Drawing.Point(8, 477);
            this.fieldTasks.Margin = new System.Windows.Forms.Padding(3);
            this.fieldTasks.Name = "fieldTasks";
            this.fieldTasks.Size = new System.Drawing.Size(96, 19);
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
            this.layoutPanel.SetColumnSpan(this.fieldMemory, 2);
            this.fieldMemory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMemory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMemory.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMemory.Location = new System.Drawing.Point(110, 477);
            this.fieldMemory.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMemory.Name = "fieldMemory";
            this.fieldMemory.Size = new System.Drawing.Size(904, 19);
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
            this.fieldPrintMainManager.Location = new System.Drawing.Point(8, 327);
            this.fieldPrintMainManager.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPrintMainManager.Name = "fieldPrintMainManager";
            this.fieldPrintMainManager.Size = new System.Drawing.Size(96, 19);
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
            this.fieldMassaManager.Location = new System.Drawing.Point(8, 377);
            this.fieldMassaManager.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaManager.Name = "fieldMassaManager";
            this.fieldMassaManager.Size = new System.Drawing.Size(96, 19);
            this.fieldMassaManager.TabIndex = 51;
            this.fieldMassaManager.Text = "fieldMassaManager";
            this.fieldMassaManager.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldMassaManager.Visible = false;
            // 
            // fieldMassaGet
            // 
            this.fieldMassaGet.AutoSize = true;
            this.fieldMassaGet.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanel.SetColumnSpan(this.fieldMassaGet, 2);
            this.fieldMassaGet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaGet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaGet.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaGet.Location = new System.Drawing.Point(110, 377);
            this.fieldMassaGet.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaGet.Name = "fieldMassaGet";
            this.fieldMassaGet.Size = new System.Drawing.Size(904, 19);
            this.fieldMassaGet.TabIndex = 46;
            this.fieldMassaGet.Text = "fieldMassaGet";
            this.fieldMassaGet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMassaGet.Visible = false;
            // 
            // labelPackageWeight
            // 
            this.labelPackageWeight.AutoSize = true;
            this.labelPackageWeight.BackColor = System.Drawing.Color.Transparent;
            this.labelPackageWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPackageWeight.Enabled = false;
            this.labelPackageWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPackageWeight.ForeColor = System.Drawing.Color.Black;
            this.labelPackageWeight.Location = new System.Drawing.Point(110, 149);
            this.labelPackageWeight.Margin = new System.Windows.Forms.Padding(3);
            this.labelPackageWeight.Name = "labelPackageWeight";
            this.labelPackageWeight.Size = new System.Drawing.Size(198, 47);
            this.labelPackageWeight.TabIndex = 17;
            this.labelPackageWeight.Text = "labelPackageWeight";
            this.labelPackageWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelKneading
            // 
            this.labelKneading.AutoSize = true;
            this.labelKneading.BackColor = System.Drawing.Color.Transparent;
            this.labelKneading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelKneading.Enabled = false;
            this.labelKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelKneading.ForeColor = System.Drawing.Color.Black;
            this.labelKneading.Location = new System.Drawing.Point(110, 252);
            this.labelKneading.Margin = new System.Windows.Forms.Padding(3);
            this.labelKneading.Name = "labelKneading";
            this.labelKneading.Size = new System.Drawing.Size(198, 44);
            this.labelKneading.TabIndex = 27;
            this.labelKneading.Text = "labelKneading";
            this.labelKneading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelProductDate
            // 
            this.labelProductDate.AutoSize = true;
            this.labelProductDate.BackColor = System.Drawing.Color.Transparent;
            this.labelProductDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProductDate.Enabled = false;
            this.labelProductDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelProductDate.ForeColor = System.Drawing.Color.Black;
            this.labelProductDate.Location = new System.Drawing.Point(110, 202);
            this.labelProductDate.Margin = new System.Windows.Forms.Padding(3);
            this.labelProductDate.Name = "labelProductDate";
            this.labelProductDate.Size = new System.Drawing.Size(198, 44);
            this.labelProductDate.TabIndex = 28;
            this.labelProductDate.Text = "labelProductDate";
            this.labelProductDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // fieldProductDate
            // 
            this.fieldProductDate.AutoSize = true;
            this.fieldProductDate.BackColor = System.Drawing.Color.Transparent;
            this.fieldProductDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldProductDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldProductDate.ForeColor = System.Drawing.Color.Black;
            this.fieldProductDate.Location = new System.Drawing.Point(314, 202);
            this.fieldProductDate.Margin = new System.Windows.Forms.Padding(3);
            this.fieldProductDate.Name = "fieldProductDate";
            this.fieldProductDate.Size = new System.Drawing.Size(700, 44);
            this.fieldProductDate.TabIndex = 31;
            this.fieldProductDate.Text = "ProductDate";
            this.fieldProductDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fieldKneading
            // 
            this.fieldKneading.AutoSize = true;
            this.fieldKneading.BackColor = System.Drawing.Color.Transparent;
            this.fieldKneading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldKneading.ForeColor = System.Drawing.Color.Black;
            this.fieldKneading.Location = new System.Drawing.Point(314, 252);
            this.fieldKneading.Margin = new System.Windows.Forms.Padding(3);
            this.fieldKneading.Name = "fieldKneading";
            this.fieldKneading.Size = new System.Drawing.Size(700, 44);
            this.fieldKneading.TabIndex = 32;
            this.fieldKneading.Text = "Kneading";
            this.fieldKneading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fieldPrintMain
            // 
            this.fieldPrintMain.AutoSize = true;
            this.fieldPrintMain.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanel.SetColumnSpan(this.fieldPrintMain, 2);
            this.fieldPrintMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPrintMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPrintMain.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldPrintMain.Location = new System.Drawing.Point(110, 327);
            this.fieldPrintMain.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPrintMain.Name = "fieldPrintMain";
            this.fieldPrintMain.Size = new System.Drawing.Size(904, 19);
            this.fieldPrintMain.TabIndex = 37;
            this.fieldPrintMain.Text = "fieldPrintLabelsMain";
            this.fieldPrintMain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldPrintMain.Visible = false;
            this.fieldPrintMain.Click += new System.EventHandler(this.FieldPrintManager_Click);
            // 
            // fieldPlu
            // 
            this.fieldPlu.AutoSize = true;
            this.fieldPlu.BackColor = System.Drawing.Color.Transparent;
            this.layoutPanel.SetColumnSpan(this.fieldPlu, 3);
            this.fieldPlu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPlu.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPlu.ForeColor = System.Drawing.Color.Black;
            this.fieldPlu.Location = new System.Drawing.Point(8, 43);
            this.fieldPlu.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPlu.Name = "fieldPlu";
            this.fieldPlu.Size = new System.Drawing.Size(1006, 47);
            this.fieldPlu.TabIndex = 14;
            this.fieldPlu.Text = "PLU";
            this.fieldPlu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // layoutPanelTop
            // 
            this.layoutPanelTop.ColumnCount = 5;
            this.layoutPanel.SetColumnSpan(this.layoutPanelTop, 3);
            this.layoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.layoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.layoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.layoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.layoutPanelTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.layoutPanelTop.Controls.Add(this.fieldLang, 3, 0);
            this.layoutPanelTop.Controls.Add(this.fieldResolution, 1, 0);
            this.layoutPanelTop.Controls.Add(this.pictureBoxClose, 4, 0);
            this.layoutPanelTop.Controls.Add(this.fieldTitle, 2, 0);
            this.layoutPanelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutPanelTop.Location = new System.Drawing.Point(8, 3);
            this.layoutPanelTop.Name = "layoutPanelTop";
            this.layoutPanelTop.RowCount = 1;
            this.layoutPanelTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layoutPanelTop.Size = new System.Drawing.Size(1006, 34);
            this.layoutPanelTop.TabIndex = 69;
            // 
            // fieldLang
            // 
            this.fieldLang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fieldLang.Enabled = false;
            this.fieldLang.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldLang.FormattingEnabled = true;
            this.fieldLang.Location = new System.Drawing.Point(806, 3);
            this.fieldLang.Name = "fieldLang";
            this.fieldLang.Size = new System.Drawing.Size(144, 21);
            this.fieldLang.TabIndex = 58;
            this.fieldLang.Visible = false;
            // 
            // fieldResolution
            // 
            this.fieldResolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fieldResolution.Enabled = false;
            this.fieldResolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldResolution.FormattingEnabled = true;
            this.fieldResolution.Location = new System.Drawing.Point(53, 3);
            this.fieldResolution.Name = "fieldResolution";
            this.fieldResolution.Size = new System.Drawing.Size(144, 21);
            this.fieldResolution.TabIndex = 31;
            this.fieldResolution.Visible = false;
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxClose.Image = global::ScalesUI.Properties.Resources.exit_1;
            this.pictureBoxClose.Location = new System.Drawing.Point(956, 3);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(47, 28);
            this.pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxClose.TabIndex = 22;
            this.pictureBoxClose.TabStop = false;
            this.pictureBoxClose.Click += new System.EventHandler(this.ActionClose);
            // 
            // fieldTitle
            // 
            this.fieldTitle.AutoSize = true;
            this.fieldTitle.BackColor = System.Drawing.Color.Transparent;
            this.fieldTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldTitle.ForeColor = System.Drawing.Color.Black;
            this.fieldTitle.Location = new System.Drawing.Point(203, 3);
            this.fieldTitle.Margin = new System.Windows.Forms.Padding(3);
            this.fieldTitle.Name = "fieldTitle";
            this.fieldTitle.Size = new System.Drawing.Size(597, 28);
            this.fieldTitle.TabIndex = 21;
            this.fieldTitle.Text = "ScalesUI";
            this.fieldTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldTitle.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 668);
            this.Controls.Add(this.layoutPanel);
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
            this.layoutPanel.ResumeLayout(false);
            this.layoutPanel.PerformLayout();
            this.layoutPanelTop.ResumeLayout(false);
            this.layoutPanelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelNettoWeight;
        private System.Windows.Forms.Label fieldPackageWeight;
        private System.Windows.Forms.Label fieldNettoWeight;
        private System.Windows.Forms.TableLayoutPanel layoutPanel;
        private System.Windows.Forms.Label fieldPlu;
        private System.Windows.Forms.Label labelPackageWeight;
        private System.Windows.Forms.Label labelKneading;
        private System.Windows.Forms.Label labelProductDate;
        private System.Windows.Forms.Label fieldProductDate;
        private System.Windows.Forms.Label fieldKneading;
        private System.Windows.Forms.Label fieldPrintMain;
        private System.Windows.Forms.Label fieldMassaGet;
        private System.Windows.Forms.Label fieldPrintMainManager;
        private System.Windows.Forms.Label fieldMassaManager;
        private System.Windows.Forms.Label fieldMemory;
        private System.Windows.Forms.Label fieldTasks;
        private System.Windows.Forms.Label fieldSscc;
        private System.Windows.Forms.Label fieldPrintShipping;
        private System.Windows.Forms.Label fieldPrintShippingManager;
        private TableLayoutPanel layoutPanelTop;
        private PictureBox pictureBoxClose;
        private Label fieldTitle;
        private ComboBox fieldLang;
        private ComboBox fieldResolution;
    }
}

