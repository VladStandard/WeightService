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
            this.fieldPrintProgressShipping = new System.Windows.Forms.ProgressBar();
            this.fieldPrintShipping = new System.Windows.Forms.Label();
            this.fieldThreshold = new System.Windows.Forms.Label();
            this.fieldSscc = new System.Windows.Forms.Label();
            this.fieldTasks = new System.Windows.Forms.Label();
            this.fieldMassaQueriesProgress = new System.Windows.Forms.ProgressBar();
            this.fieldMemoryProgress = new System.Windows.Forms.ProgressBar();
            this.fieldMemoryManagerTotal = new System.Windows.Forms.Label();
            this.fieldMassaSetCrc = new System.Windows.Forms.Label();
            this.fieldMassaGetCrc = new System.Windows.Forms.Label();
            this.fieldMassaScalePar = new System.Windows.Forms.Label();
            this.fieldMassaSet = new System.Windows.Forms.Label();
            this.fieldMassaGet = new System.Windows.Forms.Label();
            this.fieldPrintProgressMain = new System.Windows.Forms.ProgressBar();
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
            this.labelWeightNetto.Location = new System.Drawing.Point(8, 96);
            this.labelWeightNetto.Margin = new System.Windows.Forms.Padding(3);
            this.labelWeightNetto.Name = "labelWeightNetto";
            this.labelWeightNetto.Size = new System.Drawing.Size(247, 54);
            this.labelWeightNetto.TabIndex = 12;
            this.labelWeightNetto.Text = "labelWeightNetto";
            this.labelWeightNetto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelWeightNetto.Visible = false;
            this.labelWeightNetto.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldWeightTare
            // 
            this.fieldWeightTare.AutoSize = true;
            this.fieldWeightTare.BackColor = System.Drawing.Color.Transparent;
            this.fieldWeightTare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldWeightTare.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldWeightTare.ForeColor = System.Drawing.Color.Black;
            this.fieldWeightTare.Location = new System.Drawing.Point(261, 156);
            this.fieldWeightTare.Margin = new System.Windows.Forms.Padding(3);
            this.fieldWeightTare.Name = "fieldWeightTare";
            this.fieldWeightTare.Size = new System.Drawing.Size(648, 54);
            this.fieldWeightTare.TabIndex = 11;
            this.fieldWeightTare.Text = "0,000";
            this.fieldWeightTare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldWeightTare.Visible = false;
            this.fieldWeightTare.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldWeightNetto
            // 
            this.fieldWeightNetto.AutoSize = true;
            this.fieldWeightNetto.BackColor = System.Drawing.Color.Transparent;
            this.fieldWeightNetto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldWeightNetto.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldWeightNetto.ForeColor = System.Drawing.Color.Black;
            this.fieldWeightNetto.Location = new System.Drawing.Point(261, 96);
            this.fieldWeightNetto.Margin = new System.Windows.Forms.Padding(3);
            this.fieldWeightNetto.Name = "fieldWeightNetto";
            this.fieldWeightNetto.Size = new System.Drawing.Size(648, 54);
            this.fieldWeightNetto.TabIndex = 10;
            this.fieldWeightNetto.Text = "0,000";
            this.fieldWeightNetto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldWeightNetto.Visible = false;
            this.fieldWeightNetto.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 5;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.5154652F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.74229F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.91758F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.30921F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.5154652F));
            this.tableLayoutPanelMain.Controls.Add(this.fieldPrintProgressShipping, 3, 9);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPrintShipping, 1, 8);
            this.tableLayoutPanelMain.Controls.Add(this.fieldThreshold, 1, 9);
            this.tableLayoutPanelMain.Controls.Add(this.fieldSscc, 1, 6);
            this.tableLayoutPanelMain.Controls.Add(this.fieldTasks, 3, 7);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaQueriesProgress, 3, 12);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMemoryProgress, 3, 13);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMemoryManagerTotal, 1, 13);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaSetCrc, 3, 11);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaGetCrc, 3, 10);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaScalePar, 1, 10);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaSet, 1, 12);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaGet, 1, 11);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPrintProgressMain, 3, 8);
            this.tableLayoutPanelMain.Controls.Add(this.pictureBoxClose, 3, 1);
            this.tableLayoutPanelMain.Controls.Add(this.labelWeightTare, 1, 3);
            this.tableLayoutPanelMain.Controls.Add(this.labelWeightNetto, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.fieldWeightNetto, 2, 2);
            this.tableLayoutPanelMain.Controls.Add(this.fieldWeightTare, 2, 3);
            this.tableLayoutPanelMain.Controls.Add(this.labelKneading, 1, 5);
            this.tableLayoutPanelMain.Controls.Add(this.labelProductDate, 1, 4);
            this.tableLayoutPanelMain.Controls.Add(this.fieldProductDate, 2, 4);
            this.tableLayoutPanelMain.Controls.Add(this.fieldKneading, 2, 5);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPrintMain, 1, 7);
            this.tableLayoutPanelMain.Controls.Add(this.fieldTitle, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPlu, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelRight, 3, 2);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 15;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
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
            this.tableLayoutPanelMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldPrintProgressShipping
            // 
            this.fieldPrintProgressShipping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPrintProgressShipping.Enabled = false;
            this.fieldPrintProgressShipping.Location = new System.Drawing.Point(915, 385);
            this.fieldPrintProgressShipping.Name = "fieldPrintProgressShipping";
            this.fieldPrintProgressShipping.Size = new System.Drawing.Size(99, 17);
            this.fieldPrintProgressShipping.TabIndex = 67;
            this.fieldPrintProgressShipping.Visible = false;
            this.fieldPrintProgressShipping.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldPrintShipping
            // 
            this.fieldPrintShipping.AutoSize = true;
            this.fieldPrintShipping.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelMain.SetColumnSpan(this.fieldPrintShipping, 2);
            this.fieldPrintShipping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPrintShipping.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPrintShipping.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldPrintShipping.Location = new System.Drawing.Point(8, 362);
            this.fieldPrintShipping.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPrintShipping.Name = "fieldPrintShipping";
            this.fieldPrintShipping.Size = new System.Drawing.Size(901, 17);
            this.fieldPrintShipping.TabIndex = 66;
            this.fieldPrintShipping.Text = "fieldPrintLabelsShipping";
            this.fieldPrintShipping.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldPrintShipping.Visible = false;
            this.fieldPrintShipping.Click += new System.EventHandler(this.FieldPrintManager_Click);
            this.fieldPrintShipping.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldThreshold
            // 
            this.fieldThreshold.AutoSize = true;
            this.fieldThreshold.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelMain.SetColumnSpan(this.fieldThreshold, 2);
            this.fieldThreshold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldThreshold.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldThreshold.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldThreshold.Location = new System.Drawing.Point(8, 385);
            this.fieldThreshold.Margin = new System.Windows.Forms.Padding(3);
            this.fieldThreshold.Name = "fieldThreshold";
            this.fieldThreshold.Size = new System.Drawing.Size(901, 17);
            this.fieldThreshold.TabIndex = 64;
            this.fieldThreshold.Text = "fieldThreshold";
            this.fieldThreshold.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldThreshold.Visible = false;
            this.fieldThreshold.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldSscc
            // 
            this.fieldSscc.AutoSize = true;
            this.fieldSscc.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelMain.SetColumnSpan(this.fieldSscc, 2);
            this.fieldSscc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldSscc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldSscc.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldSscc.Location = new System.Drawing.Point(8, 316);
            this.fieldSscc.Margin = new System.Windows.Forms.Padding(3);
            this.fieldSscc.Name = "fieldSscc";
            this.fieldSscc.Size = new System.Drawing.Size(901, 17);
            this.fieldSscc.TabIndex = 61;
            this.fieldSscc.Text = "fieldSscc";
            this.fieldSscc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldSscc.Visible = false;
            this.fieldSscc.Click += new System.EventHandler(this.FieldSscc_Click);
            this.fieldSscc.DoubleClick += new System.EventHandler(this.FieldSscc_Click);
            this.fieldSscc.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldTasks
            // 
            this.fieldTasks.AutoSize = true;
            this.fieldTasks.BackColor = System.Drawing.Color.Transparent;
            this.fieldTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldTasks.Enabled = false;
            this.fieldTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldTasks.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldTasks.Location = new System.Drawing.Point(915, 339);
            this.fieldTasks.Margin = new System.Windows.Forms.Padding(3);
            this.fieldTasks.Name = "fieldTasks";
            this.fieldTasks.Size = new System.Drawing.Size(99, 17);
            this.fieldTasks.TabIndex = 57;
            this.fieldTasks.Text = "Tasks";
            this.fieldTasks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldTasks.Visible = false;
            this.fieldTasks.DoubleClick += new System.EventHandler(this.FieldTasks_DoubleClick);
            this.fieldTasks.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldMassaQueriesProgress
            // 
            this.fieldMassaQueriesProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaQueriesProgress.Enabled = false;
            this.fieldMassaQueriesProgress.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaQueriesProgress.Location = new System.Drawing.Point(915, 454);
            this.fieldMassaQueriesProgress.Name = "fieldMassaQueriesProgress";
            this.fieldMassaQueriesProgress.Size = new System.Drawing.Size(99, 17);
            this.fieldMassaQueriesProgress.TabIndex = 55;
            this.fieldMassaQueriesProgress.Visible = false;
            this.fieldMassaQueriesProgress.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldMemoryProgress
            // 
            this.fieldMemoryProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMemoryProgress.Enabled = false;
            this.fieldMemoryProgress.Location = new System.Drawing.Point(915, 477);
            this.fieldMemoryProgress.Name = "fieldMemoryProgress";
            this.fieldMemoryProgress.Size = new System.Drawing.Size(99, 17);
            this.fieldMemoryProgress.TabIndex = 54;
            this.fieldMemoryProgress.Visible = false;
            this.fieldMemoryProgress.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldMemoryManagerTotal
            // 
            this.fieldMemoryManagerTotal.AutoSize = true;
            this.fieldMemoryManagerTotal.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelMain.SetColumnSpan(this.fieldMemoryManagerTotal, 2);
            this.fieldMemoryManagerTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMemoryManagerTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMemoryManagerTotal.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMemoryManagerTotal.Location = new System.Drawing.Point(8, 477);
            this.fieldMemoryManagerTotal.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMemoryManagerTotal.Name = "fieldMemoryManagerTotal";
            this.fieldMemoryManagerTotal.Size = new System.Drawing.Size(901, 17);
            this.fieldMemoryManagerTotal.TabIndex = 53;
            this.fieldMemoryManagerTotal.Text = "fieldMemoryManagerTotal";
            this.fieldMemoryManagerTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMemoryManagerTotal.Visible = false;
            this.fieldMemoryManagerTotal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldMassaSetCrc
            // 
            this.fieldMassaSetCrc.AutoSize = true;
            this.fieldMassaSetCrc.BackColor = System.Drawing.Color.Transparent;
            this.fieldMassaSetCrc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaSetCrc.Enabled = false;
            this.fieldMassaSetCrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaSetCrc.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaSetCrc.Location = new System.Drawing.Point(915, 431);
            this.fieldMassaSetCrc.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaSetCrc.Name = "fieldMassaSetCrc";
            this.fieldMassaSetCrc.Size = new System.Drawing.Size(99, 17);
            this.fieldMassaSetCrc.TabIndex = 52;
            this.fieldMassaSetCrc.Text = "CRC: ";
            this.fieldMassaSetCrc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldMassaSetCrc.Visible = false;
            this.fieldMassaSetCrc.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldMassaGetCrc
            // 
            this.fieldMassaGetCrc.AutoSize = true;
            this.fieldMassaGetCrc.BackColor = System.Drawing.Color.Transparent;
            this.fieldMassaGetCrc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaGetCrc.Enabled = false;
            this.fieldMassaGetCrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaGetCrc.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaGetCrc.Location = new System.Drawing.Point(915, 408);
            this.fieldMassaGetCrc.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaGetCrc.Name = "fieldMassaGetCrc";
            this.fieldMassaGetCrc.Size = new System.Drawing.Size(99, 17);
            this.fieldMassaGetCrc.TabIndex = 51;
            this.fieldMassaGetCrc.Text = "CRC: ";
            this.fieldMassaGetCrc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldMassaGetCrc.Visible = false;
            this.fieldMassaGetCrc.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldMassaScalePar
            // 
            this.fieldMassaScalePar.AutoSize = true;
            this.fieldMassaScalePar.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelMain.SetColumnSpan(this.fieldMassaScalePar, 2);
            this.fieldMassaScalePar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaScalePar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaScalePar.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaScalePar.Location = new System.Drawing.Point(8, 408);
            this.fieldMassaScalePar.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaScalePar.Name = "fieldMassaScalePar";
            this.fieldMassaScalePar.Size = new System.Drawing.Size(901, 17);
            this.fieldMassaScalePar.TabIndex = 49;
            this.fieldMassaScalePar.Text = "fieldMassaScalePar";
            this.fieldMassaScalePar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMassaScalePar.Visible = false;
            this.fieldMassaScalePar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldMassaSet
            // 
            this.fieldMassaSet.AutoSize = true;
            this.fieldMassaSet.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelMain.SetColumnSpan(this.fieldMassaSet, 2);
            this.fieldMassaSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaSet.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaSet.Location = new System.Drawing.Point(8, 454);
            this.fieldMassaSet.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaSet.Name = "fieldMassaSet";
            this.fieldMassaSet.Size = new System.Drawing.Size(901, 17);
            this.fieldMassaSet.TabIndex = 48;
            this.fieldMassaSet.Text = "fieldMassaSet";
            this.fieldMassaSet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMassaSet.Visible = false;
            this.fieldMassaSet.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldMassaGet
            // 
            this.fieldMassaGet.AutoSize = true;
            this.fieldMassaGet.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelMain.SetColumnSpan(this.fieldMassaGet, 2);
            this.fieldMassaGet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaGet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaGet.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaGet.Location = new System.Drawing.Point(8, 431);
            this.fieldMassaGet.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaGet.Name = "fieldMassaGet";
            this.fieldMassaGet.Size = new System.Drawing.Size(901, 17);
            this.fieldMassaGet.TabIndex = 46;
            this.fieldMassaGet.Text = "fieldMassaGet";
            this.fieldMassaGet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMassaGet.Visible = false;
            this.fieldMassaGet.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldPrintProgressMain
            // 
            this.fieldPrintProgressMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPrintProgressMain.Enabled = false;
            this.fieldPrintProgressMain.Location = new System.Drawing.Point(915, 362);
            this.fieldPrintProgressMain.Name = "fieldPrintProgressMain";
            this.fieldPrintProgressMain.Size = new System.Drawing.Size(99, 17);
            this.fieldPrintProgressMain.TabIndex = 30;
            this.fieldPrintProgressMain.Visible = false;
            this.fieldPrintProgressMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxClose.Image = global::ScalesUI.Properties.Resources.exit_1;
            this.pictureBoxClose.Location = new System.Drawing.Point(915, 36);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(99, 54);
            this.pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxClose.TabIndex = 19;
            this.pictureBoxClose.TabStop = false;
            this.pictureBoxClose.Click += new System.EventHandler(this.PictureBoxClose_Click);
            this.pictureBoxClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // labelWeightTare
            // 
            this.labelWeightTare.AutoSize = true;
            this.labelWeightTare.BackColor = System.Drawing.Color.Transparent;
            this.labelWeightTare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWeightTare.Enabled = false;
            this.labelWeightTare.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWeightTare.ForeColor = System.Drawing.Color.Black;
            this.labelWeightTare.Location = new System.Drawing.Point(8, 156);
            this.labelWeightTare.Margin = new System.Windows.Forms.Padding(3);
            this.labelWeightTare.Name = "labelWeightTare";
            this.labelWeightTare.Size = new System.Drawing.Size(247, 54);
            this.labelWeightTare.TabIndex = 17;
            this.labelWeightTare.Text = "labelWeightTare";
            this.labelWeightTare.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelWeightTare.Visible = false;
            this.labelWeightTare.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // labelKneading
            // 
            this.labelKneading.AutoSize = true;
            this.labelKneading.BackColor = System.Drawing.Color.Transparent;
            this.labelKneading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelKneading.Enabled = false;
            this.labelKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelKneading.ForeColor = System.Drawing.Color.Black;
            this.labelKneading.Location = new System.Drawing.Point(8, 266);
            this.labelKneading.Margin = new System.Windows.Forms.Padding(3);
            this.labelKneading.Name = "labelKneading";
            this.labelKneading.Size = new System.Drawing.Size(247, 44);
            this.labelKneading.TabIndex = 27;
            this.labelKneading.Text = "labelKneading";
            this.labelKneading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelKneading.Visible = false;
            this.labelKneading.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // labelProductDate
            // 
            this.labelProductDate.AutoSize = true;
            this.labelProductDate.BackColor = System.Drawing.Color.Transparent;
            this.labelProductDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProductDate.Enabled = false;
            this.labelProductDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelProductDate.ForeColor = System.Drawing.Color.Black;
            this.labelProductDate.Location = new System.Drawing.Point(8, 216);
            this.labelProductDate.Margin = new System.Windows.Forms.Padding(3);
            this.labelProductDate.Name = "labelProductDate";
            this.labelProductDate.Size = new System.Drawing.Size(247, 44);
            this.labelProductDate.TabIndex = 28;
            this.labelProductDate.Text = "labelProductDate";
            this.labelProductDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelProductDate.Visible = false;
            this.labelProductDate.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldProductDate
            // 
            this.fieldProductDate.AutoSize = true;
            this.fieldProductDate.BackColor = System.Drawing.Color.Transparent;
            this.fieldProductDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldProductDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldProductDate.ForeColor = System.Drawing.Color.Black;
            this.fieldProductDate.Location = new System.Drawing.Point(261, 216);
            this.fieldProductDate.Margin = new System.Windows.Forms.Padding(3);
            this.fieldProductDate.Name = "fieldProductDate";
            this.fieldProductDate.Size = new System.Drawing.Size(648, 44);
            this.fieldProductDate.TabIndex = 31;
            this.fieldProductDate.Text = "fieldProductDate";
            this.fieldProductDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldProductDate.Visible = false;
            this.fieldProductDate.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldKneading
            // 
            this.fieldKneading.AutoSize = true;
            this.fieldKneading.BackColor = System.Drawing.Color.Transparent;
            this.fieldKneading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldKneading.ForeColor = System.Drawing.Color.Black;
            this.fieldKneading.Location = new System.Drawing.Point(261, 266);
            this.fieldKneading.Margin = new System.Windows.Forms.Padding(3);
            this.fieldKneading.Name = "fieldKneading";
            this.fieldKneading.Size = new System.Drawing.Size(648, 44);
            this.fieldKneading.TabIndex = 32;
            this.fieldKneading.Text = "fieldKneading";
            this.fieldKneading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldKneading.Visible = false;
            this.fieldKneading.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldPrintMain
            // 
            this.fieldPrintMain.AutoSize = true;
            this.fieldPrintMain.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelMain.SetColumnSpan(this.fieldPrintMain, 2);
            this.fieldPrintMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPrintMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPrintMain.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldPrintMain.Location = new System.Drawing.Point(8, 339);
            this.fieldPrintMain.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPrintMain.Name = "fieldPrintMain";
            this.fieldPrintMain.Size = new System.Drawing.Size(901, 17);
            this.fieldPrintMain.TabIndex = 37;
            this.fieldPrintMain.Text = "fieldPrintLabelsMain";
            this.fieldPrintMain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldPrintMain.Visible = false;
            this.fieldPrintMain.Click += new System.EventHandler(this.FieldPrintManager_Click);
            this.fieldPrintMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldTitle
            // 
            this.fieldTitle.AutoSize = true;
            this.fieldTitle.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tableLayoutPanelMain.SetColumnSpan(this.fieldTitle, 5);
            this.fieldTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldTitle.ForeColor = System.Drawing.Color.Blue;
            this.fieldTitle.Location = new System.Drawing.Point(3, 0);
            this.fieldTitle.Name = "fieldTitle";
            this.fieldTitle.Size = new System.Drawing.Size(1018, 33);
            this.fieldTitle.TabIndex = 20;
            this.fieldTitle.Text = "ScalesUI";
            this.fieldTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldTitle.Visible = false;
            this.fieldTitle.Click += new System.EventHandler(this.FieldsHiddenVisible_Click);
            this.fieldTitle.DoubleClick += new System.EventHandler(this.FieldTitle_DoubleClick);
            this.fieldTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldPlu
            // 
            this.fieldPlu.AutoSize = true;
            this.fieldPlu.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelMain.SetColumnSpan(this.fieldPlu, 2);
            this.fieldPlu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPlu.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPlu.ForeColor = System.Drawing.Color.Black;
            this.fieldPlu.Location = new System.Drawing.Point(8, 33);
            this.fieldPlu.Name = "fieldPlu";
            this.fieldPlu.Size = new System.Drawing.Size(901, 60);
            this.fieldPlu.TabIndex = 14;
            this.fieldPlu.Text = "PLU";
            this.fieldPlu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldPlu.Visible = false;
            this.fieldPlu.Click += new System.EventHandler(this.ButtonPlu_Click);
            this.fieldPlu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // tableLayoutPanelRight
            // 
            this.tableLayoutPanelRight.ColumnCount = 1;
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRight.Controls.Add(this.fieldLang, 0, 1);
            this.tableLayoutPanelRight.Controls.Add(this.fieldResolution, 0, 0);
            this.tableLayoutPanelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelRight.Location = new System.Drawing.Point(915, 96);
            this.tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            this.tableLayoutPanelRight.RowCount = 2;
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRight.Size = new System.Drawing.Size(99, 54);
            this.tableLayoutPanelRight.TabIndex = 62;
            // 
            // fieldLang
            // 
            this.fieldLang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fieldLang.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldLang.FormattingEnabled = true;
            this.fieldLang.Location = new System.Drawing.Point(3, 30);
            this.fieldLang.Name = "fieldLang";
            this.fieldLang.Size = new System.Drawing.Size(93, 21);
            this.fieldLang.TabIndex = 57;
            this.fieldLang.Visible = false;
            this.fieldLang.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldResolution
            // 
            this.fieldResolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fieldResolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldResolution.FormattingEnabled = true;
            this.fieldResolution.Location = new System.Drawing.Point(3, 3);
            this.fieldResolution.Name = "fieldResolution";
            this.fieldResolution.Size = new System.Drawing.Size(93, 21);
            this.fieldResolution.TabIndex = 30;
            this.fieldResolution.Visible = false;
            this.fieldResolution.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
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
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
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
        private System.Windows.Forms.ProgressBar fieldPrintProgressMain;
        private System.Windows.Forms.Label fieldProductDate;
        private System.Windows.Forms.Label fieldKneading;
        private System.Windows.Forms.Label fieldPrintMain;
        private System.Windows.Forms.Label fieldMassaGet;
        private System.Windows.Forms.Label fieldMassaSet;
        private System.Windows.Forms.Label fieldMassaScalePar;
        private System.Windows.Forms.Label fieldMassaSetCrc;
        private System.Windows.Forms.Label fieldMassaGetCrc;
        private System.Windows.Forms.Label fieldMemoryManagerTotal;
        private System.Windows.Forms.ProgressBar fieldMemoryProgress;
        private System.Windows.Forms.ProgressBar fieldMassaQueriesProgress;
        private System.Windows.Forms.Label fieldTasks;
        private System.Windows.Forms.Label fieldSscc;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRight;
        private System.Windows.Forms.ComboBox fieldLang;
        private System.Windows.Forms.ComboBox fieldResolution;
        private System.Windows.Forms.Label fieldThreshold;
        private System.Windows.Forms.Label fieldPrintShipping;
        private System.Windows.Forms.ProgressBar fieldPrintProgressShipping;
    }
}

