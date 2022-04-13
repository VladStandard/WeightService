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
            this.fieldThreshold = new System.Windows.Forms.Label();
            this.labelThreshold = new System.Windows.Forms.Label();
            this.fieldSscc = new System.Windows.Forms.Label();
            this.labelSscc = new System.Windows.Forms.Label();
            this.fieldTasks = new System.Windows.Forms.Label();
            this.fieldMassaQueriesProgress = new System.Windows.Forms.ProgressBar();
            this.fieldMemoryProgress = new System.Windows.Forms.ProgressBar();
            this.fieldMemoryManagerTotal = new System.Windows.Forms.Label();
            this.fieldMassaSetCrc = new System.Windows.Forms.Label();
            this.fieldMassaGetCrc = new System.Windows.Forms.Label();
            this.fieldMassaComPort = new System.Windows.Forms.Label();
            this.fieldMassaScalePar = new System.Windows.Forms.Label();
            this.fieldMassaSet = new System.Windows.Forms.Label();
            this.fieldMassaQueries = new System.Windows.Forms.Label();
            this.fieldMassaGet = new System.Windows.Forms.Label();
            this.fieldMassaManager = new System.Windows.Forms.Label();
            this.fieldPrintManager = new System.Windows.Forms.Label();
            this.fieldMemoryManager = new System.Windows.Forms.Label();
            this.fieldPrintProgress = new System.Windows.Forms.ProgressBar();
            this.fieldCurrentTime = new System.Windows.Forms.Label();
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.labelWeightTare = new System.Windows.Forms.Label();
            this.labelKneading = new System.Windows.Forms.Label();
            this.labelProductDate = new System.Windows.Forms.Label();
            this.fieldProductDate = new System.Windows.Forms.Label();
            this.fieldKneading = new System.Windows.Forms.Label();
            this.fieldPrintLabels = new System.Windows.Forms.Label();
            this.fieldTitle = new System.Windows.Forms.Label();
            this.fieldPlu = new System.Windows.Forms.Label();
            this.labelPlu = new System.Windows.Forms.Label();
            this.tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelRight = new System.Windows.Forms.TableLayoutPanel();
            this.fieldLang = new System.Windows.Forms.ComboBox();
            this.fieldResolution = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            this.tableLayoutPanelButtons.SuspendLayout();
            this.tableLayoutPanelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelWeightNetto
            // 
            this.labelWeightNetto.AutoSize = true;
            this.labelWeightNetto.BackColor = System.Drawing.Color.Transparent;
            this.labelWeightNetto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWeightNetto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWeightNetto.Location = new System.Drawing.Point(8, 69);
            this.labelWeightNetto.Margin = new System.Windows.Forms.Padding(3);
            this.labelWeightNetto.Name = "labelWeightNetto";
            this.labelWeightNetto.Size = new System.Drawing.Size(311, 80);
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
            this.fieldWeightTare.Location = new System.Drawing.Point(325, 155);
            this.fieldWeightTare.Margin = new System.Windows.Forms.Padding(3);
            this.fieldWeightTare.Name = "fieldWeightTare";
            this.fieldWeightTare.Size = new System.Drawing.Size(587, 80);
            this.fieldWeightTare.TabIndex = 11;
            this.fieldWeightTare.Text = "0,000";
            this.fieldWeightTare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldWeightTare.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldWeightNetto
            // 
            this.fieldWeightNetto.AutoSize = true;
            this.fieldWeightNetto.BackColor = System.Drawing.Color.Transparent;
            this.fieldWeightNetto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldWeightNetto.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldWeightNetto.Location = new System.Drawing.Point(325, 69);
            this.fieldWeightNetto.Margin = new System.Windows.Forms.Padding(3);
            this.fieldWeightNetto.Name = "fieldWeightNetto";
            this.fieldWeightNetto.Size = new System.Drawing.Size(587, 80);
            this.fieldWeightNetto.TabIndex = 10;
            this.fieldWeightNetto.Text = "0,000";
            this.fieldWeightNetto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldWeightNetto.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 5;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.5000009F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.00003F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.00005F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.999924F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 0.5000009F));
            this.tableLayoutPanelMain.Controls.Add(this.fieldThreshold, 2, 6);
            this.tableLayoutPanelMain.Controls.Add(this.labelThreshold, 1, 6);
            this.tableLayoutPanelMain.Controls.Add(this.fieldSscc, 2, 7);
            this.tableLayoutPanelMain.Controls.Add(this.labelSscc, 1, 7);
            this.tableLayoutPanelMain.Controls.Add(this.fieldTasks, 3, 5);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaQueriesProgress, 3, 11);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMemoryProgress, 3, 12);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMemoryManagerTotal, 2, 12);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaSetCrc, 3, 10);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaGetCrc, 3, 9);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaComPort, 1, 10);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaScalePar, 2, 9);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaSet, 2, 11);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaQueries, 1, 11);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaGet, 2, 10);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaManager, 1, 9);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPrintManager, 1, 8);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMemoryManager, 1, 12);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPrintProgress, 3, 8);
            this.tableLayoutPanelMain.Controls.Add(this.fieldCurrentTime, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.pictureBoxClose, 3, 2);
            this.tableLayoutPanelMain.Controls.Add(this.labelWeightTare, 1, 3);
            this.tableLayoutPanelMain.Controls.Add(this.labelWeightNetto, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.fieldWeightNetto, 2, 2);
            this.tableLayoutPanelMain.Controls.Add(this.fieldWeightTare, 2, 3);
            this.tableLayoutPanelMain.Controls.Add(this.labelKneading, 1, 4);
            this.tableLayoutPanelMain.Controls.Add(this.labelProductDate, 1, 5);
            this.tableLayoutPanelMain.Controls.Add(this.fieldProductDate, 2, 5);
            this.tableLayoutPanelMain.Controls.Add(this.fieldKneading, 2, 4);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPrintLabels, 2, 8);
            this.tableLayoutPanelMain.Controls.Add(this.fieldTitle, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPlu, 2, 1);
            this.tableLayoutPanelMain.Controls.Add(this.labelPlu, 0, 4);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelButtons, 0, 13);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelRight, 3, 3);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 14;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1024, 668);
            this.tableLayoutPanelMain.TabIndex = 7;
            this.tableLayoutPanelMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldThreshold
            // 
            this.fieldThreshold.AutoSize = true;
            this.fieldThreshold.BackColor = System.Drawing.Color.Transparent;
            this.fieldThreshold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldThreshold.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldThreshold.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldThreshold.Location = new System.Drawing.Point(325, 353);
            this.fieldThreshold.Margin = new System.Windows.Forms.Padding(3);
            this.fieldThreshold.Name = "fieldThreshold";
            this.fieldThreshold.Size = new System.Drawing.Size(587, 20);
            this.fieldThreshold.TabIndex = 64;
            this.fieldThreshold.Text = "Threshold value";
            this.fieldThreshold.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelThreshold
            // 
            this.labelThreshold.AutoSize = true;
            this.labelThreshold.BackColor = System.Drawing.Color.Transparent;
            this.labelThreshold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelThreshold.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelThreshold.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.labelThreshold.Location = new System.Drawing.Point(8, 353);
            this.labelThreshold.Margin = new System.Windows.Forms.Padding(3);
            this.labelThreshold.Name = "labelThreshold";
            this.labelThreshold.Size = new System.Drawing.Size(311, 20);
            this.labelThreshold.TabIndex = 63;
            this.labelThreshold.Text = "Threshold";
            this.labelThreshold.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fieldSscc
            // 
            this.fieldSscc.AutoSize = true;
            this.fieldSscc.BackColor = System.Drawing.Color.Transparent;
            this.fieldSscc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldSscc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldSscc.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldSscc.Location = new System.Drawing.Point(325, 379);
            this.fieldSscc.Margin = new System.Windows.Forms.Padding(3);
            this.fieldSscc.Name = "fieldSscc";
            this.fieldSscc.Size = new System.Drawing.Size(587, 20);
            this.fieldSscc.TabIndex = 61;
            this.fieldSscc.Text = "SSCC value";
            this.fieldSscc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldSscc.DoubleClick += new System.EventHandler(this.FieldSscc_DoubleClick);
            // 
            // labelSscc
            // 
            this.labelSscc.AutoSize = true;
            this.labelSscc.BackColor = System.Drawing.Color.Transparent;
            this.labelSscc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSscc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSscc.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.labelSscc.Location = new System.Drawing.Point(8, 379);
            this.labelSscc.Margin = new System.Windows.Forms.Padding(3);
            this.labelSscc.Name = "labelSscc";
            this.labelSscc.Size = new System.Drawing.Size(311, 20);
            this.labelSscc.TabIndex = 60;
            this.labelSscc.Text = "SSCC";
            this.labelSscc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelSscc.DoubleClick += new System.EventHandler(this.FieldSscc_DoubleClick);
            // 
            // fieldTasks
            // 
            this.fieldTasks.AutoSize = true;
            this.fieldTasks.BackColor = System.Drawing.Color.Transparent;
            this.fieldTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldTasks.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldTasks.Location = new System.Drawing.Point(918, 327);
            this.fieldTasks.Margin = new System.Windows.Forms.Padding(3);
            this.fieldTasks.Name = "fieldTasks";
            this.fieldTasks.Size = new System.Drawing.Size(96, 20);
            this.fieldTasks.TabIndex = 57;
            this.fieldTasks.Text = "Tasks";
            this.fieldTasks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldTasks.Visible = false;
            this.fieldTasks.DoubleClick += new System.EventHandler(this.FieldTasks_DoubleClick);
            // 
            // fieldMassaQueriesProgress
            // 
            this.fieldMassaQueriesProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaQueriesProgress.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaQueriesProgress.Location = new System.Drawing.Point(918, 483);
            this.fieldMassaQueriesProgress.Name = "fieldMassaQueriesProgress";
            this.fieldMassaQueriesProgress.Size = new System.Drawing.Size(96, 20);
            this.fieldMassaQueriesProgress.TabIndex = 55;
            // 
            // fieldMemoryProgress
            // 
            this.fieldMemoryProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMemoryProgress.Location = new System.Drawing.Point(918, 509);
            this.fieldMemoryProgress.Name = "fieldMemoryProgress";
            this.fieldMemoryProgress.Size = new System.Drawing.Size(96, 20);
            this.fieldMemoryProgress.TabIndex = 54;
            this.fieldMemoryProgress.Visible = false;
            // 
            // fieldMemoryManagerTotal
            // 
            this.fieldMemoryManagerTotal.AutoSize = true;
            this.fieldMemoryManagerTotal.BackColor = System.Drawing.Color.Transparent;
            this.fieldMemoryManagerTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMemoryManagerTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMemoryManagerTotal.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMemoryManagerTotal.Location = new System.Drawing.Point(325, 509);
            this.fieldMemoryManagerTotal.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMemoryManagerTotal.Name = "fieldMemoryManagerTotal";
            this.fieldMemoryManagerTotal.Size = new System.Drawing.Size(587, 20);
            this.fieldMemoryManagerTotal.TabIndex = 53;
            this.fieldMemoryManagerTotal.Text = "Всего памяти: ";
            this.fieldMemoryManagerTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMemoryManagerTotal.Visible = false;
            // 
            // fieldMassaSetCrc
            // 
            this.fieldMassaSetCrc.AutoSize = true;
            this.fieldMassaSetCrc.BackColor = System.Drawing.Color.Transparent;
            this.fieldMassaSetCrc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaSetCrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaSetCrc.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaSetCrc.Location = new System.Drawing.Point(918, 457);
            this.fieldMassaSetCrc.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaSetCrc.Name = "fieldMassaSetCrc";
            this.fieldMassaSetCrc.Size = new System.Drawing.Size(96, 20);
            this.fieldMassaSetCrc.TabIndex = 52;
            this.fieldMassaSetCrc.Text = "CRC: ";
            this.fieldMassaSetCrc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fieldMassaGetCrc
            // 
            this.fieldMassaGetCrc.AutoSize = true;
            this.fieldMassaGetCrc.BackColor = System.Drawing.Color.Transparent;
            this.fieldMassaGetCrc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaGetCrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaGetCrc.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaGetCrc.Location = new System.Drawing.Point(918, 431);
            this.fieldMassaGetCrc.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaGetCrc.Name = "fieldMassaGetCrc";
            this.fieldMassaGetCrc.Size = new System.Drawing.Size(96, 20);
            this.fieldMassaGetCrc.TabIndex = 51;
            this.fieldMassaGetCrc.Text = "CRC: ";
            this.fieldMassaGetCrc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fieldMassaComPort
            // 
            this.fieldMassaComPort.AutoSize = true;
            this.fieldMassaComPort.BackColor = System.Drawing.Color.Transparent;
            this.fieldMassaComPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaComPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaComPort.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaComPort.Location = new System.Drawing.Point(8, 457);
            this.fieldMassaComPort.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaComPort.Name = "fieldMassaComPort";
            this.fieldMassaComPort.Size = new System.Drawing.Size(311, 20);
            this.fieldMassaComPort.TabIndex = 50;
            this.fieldMassaComPort.Text = "Состояние COM-порта: ";
            this.fieldMassaComPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fieldMassaScalePar
            // 
            this.fieldMassaScalePar.AutoSize = true;
            this.fieldMassaScalePar.BackColor = System.Drawing.Color.Transparent;
            this.fieldMassaScalePar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaScalePar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaScalePar.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaScalePar.Location = new System.Drawing.Point(325, 431);
            this.fieldMassaScalePar.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaScalePar.Name = "fieldMassaScalePar";
            this.fieldMassaScalePar.Size = new System.Drawing.Size(587, 20);
            this.fieldMassaScalePar.TabIndex = 49;
            this.fieldMassaScalePar.Text = "Запрос параметров: ";
            this.fieldMassaScalePar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fieldMassaSet
            // 
            this.fieldMassaSet.AutoSize = true;
            this.fieldMassaSet.BackColor = System.Drawing.Color.Transparent;
            this.fieldMassaSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaSet.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaSet.Location = new System.Drawing.Point(325, 483);
            this.fieldMassaSet.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaSet.Name = "fieldMassaSet";
            this.fieldMassaSet.Size = new System.Drawing.Size(587, 20);
            this.fieldMassaSet.TabIndex = 48;
            this.fieldMassaSet.Text = "Команда для весов: ";
            this.fieldMassaSet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fieldMassaQueries
            // 
            this.fieldMassaQueries.AutoSize = true;
            this.fieldMassaQueries.BackColor = System.Drawing.Color.Transparent;
            this.fieldMassaQueries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaQueries.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaQueries.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaQueries.Location = new System.Drawing.Point(8, 483);
            this.fieldMassaQueries.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaQueries.Name = "fieldMassaQueries";
            this.fieldMassaQueries.Size = new System.Drawing.Size(311, 20);
            this.fieldMassaQueries.TabIndex = 47;
            this.fieldMassaQueries.Text = "Очередь сообщений весов:  ";
            this.fieldMassaQueries.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fieldMassaGet
            // 
            this.fieldMassaGet.AutoSize = true;
            this.fieldMassaGet.BackColor = System.Drawing.Color.Transparent;
            this.fieldMassaGet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaGet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaGet.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaGet.Location = new System.Drawing.Point(325, 457);
            this.fieldMassaGet.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaGet.Name = "fieldMassaGet";
            this.fieldMassaGet.Size = new System.Drawing.Size(587, 20);
            this.fieldMassaGet.TabIndex = 46;
            this.fieldMassaGet.Text = "Сообщение взвешивания: ";
            this.fieldMassaGet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fieldMassaManager
            // 
            this.fieldMassaManager.AutoSize = true;
            this.fieldMassaManager.BackColor = System.Drawing.Color.Transparent;
            this.fieldMassaManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaManager.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMassaManager.Location = new System.Drawing.Point(8, 431);
            this.fieldMassaManager.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaManager.Name = "fieldMassaManager";
            this.fieldMassaManager.Size = new System.Drawing.Size(311, 20);
            this.fieldMassaManager.TabIndex = 44;
            this.fieldMassaManager.Text = "Менеджер весов";
            this.fieldMassaManager.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMassaManager.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldPrintManager
            // 
            this.fieldPrintManager.AutoSize = true;
            this.fieldPrintManager.BackColor = System.Drawing.Color.Transparent;
            this.fieldPrintManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPrintManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPrintManager.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldPrintManager.Location = new System.Drawing.Point(8, 405);
            this.fieldPrintManager.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPrintManager.Name = "fieldPrintManager";
            this.fieldPrintManager.Size = new System.Drawing.Size(311, 20);
            this.fieldPrintManager.TabIndex = 43;
            this.fieldPrintManager.Text = "Менеджер принтера";
            this.fieldPrintManager.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldPrintManager.DoubleClick += new System.EventHandler(this.FieldPrintManager_DoubleClick);
            this.fieldPrintManager.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldMemoryManager
            // 
            this.fieldMemoryManager.AutoSize = true;
            this.fieldMemoryManager.BackColor = System.Drawing.Color.Transparent;
            this.fieldMemoryManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMemoryManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMemoryManager.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldMemoryManager.Location = new System.Drawing.Point(8, 509);
            this.fieldMemoryManager.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMemoryManager.Name = "fieldMemoryManager";
            this.fieldMemoryManager.Size = new System.Drawing.Size(311, 20);
            this.fieldMemoryManager.TabIndex = 42;
            this.fieldMemoryManager.Text = "Менеджер памяти";
            this.fieldMemoryManager.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMemoryManager.Visible = false;
            this.fieldMemoryManager.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldPrintProgress
            // 
            this.fieldPrintProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPrintProgress.Location = new System.Drawing.Point(918, 405);
            this.fieldPrintProgress.Name = "fieldPrintProgress";
            this.fieldPrintProgress.Size = new System.Drawing.Size(96, 20);
            this.fieldPrintProgress.TabIndex = 30;
            this.fieldPrintProgress.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldCurrentTime
            // 
            this.fieldCurrentTime.AutoSize = true;
            this.fieldCurrentTime.BackColor = System.Drawing.Color.Transparent;
            this.fieldCurrentTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldCurrentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldCurrentTime.Location = new System.Drawing.Point(8, 36);
            this.fieldCurrentTime.Margin = new System.Windows.Forms.Padding(3);
            this.fieldCurrentTime.Name = "fieldCurrentTime";
            this.fieldCurrentTime.Size = new System.Drawing.Size(311, 27);
            this.fieldCurrentTime.TabIndex = 25;
            this.fieldCurrentTime.Text = "Дата время";
            this.fieldCurrentTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldCurrentTime.Visible = false;
            this.fieldCurrentTime.Click += new System.EventHandler(this.FieldCurrentTime_Click);
            this.fieldCurrentTime.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxClose.Image = global::ScalesUI.Properties.Resources.exit_1;
            this.pictureBoxClose.Location = new System.Drawing.Point(918, 69);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(96, 80);
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
            this.labelWeightTare.Location = new System.Drawing.Point(8, 155);
            this.labelWeightTare.Margin = new System.Windows.Forms.Padding(3);
            this.labelWeightTare.Name = "labelWeightTare";
            this.labelWeightTare.Size = new System.Drawing.Size(311, 80);
            this.labelWeightTare.TabIndex = 17;
            this.labelWeightTare.Text = "Вес тары";
            this.labelWeightTare.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelWeightTare.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // labelKneading
            // 
            this.labelKneading.AutoSize = true;
            this.labelKneading.BackColor = System.Drawing.Color.Transparent;
            this.labelKneading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelKneading.Location = new System.Drawing.Point(8, 241);
            this.labelKneading.Margin = new System.Windows.Forms.Padding(3);
            this.labelKneading.Name = "labelKneading";
            this.labelKneading.Size = new System.Drawing.Size(311, 80);
            this.labelKneading.TabIndex = 27;
            this.labelKneading.Text = "Замес";
            this.labelKneading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelKneading.Visible = false;
            this.labelKneading.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // labelProductDate
            // 
            this.labelProductDate.AutoSize = true;
            this.labelProductDate.BackColor = System.Drawing.Color.Transparent;
            this.labelProductDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProductDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelProductDate.Location = new System.Drawing.Point(8, 327);
            this.labelProductDate.Margin = new System.Windows.Forms.Padding(3);
            this.labelProductDate.Name = "labelProductDate";
            this.labelProductDate.Size = new System.Drawing.Size(311, 20);
            this.labelProductDate.TabIndex = 28;
            this.labelProductDate.Text = "Дата производства";
            this.labelProductDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelProductDate.Visible = false;
            this.labelProductDate.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldProductDate
            // 
            this.fieldProductDate.AutoSize = true;
            this.fieldProductDate.BackColor = System.Drawing.Color.Transparent;
            this.fieldProductDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldProductDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldProductDate.Location = new System.Drawing.Point(325, 327);
            this.fieldProductDate.Margin = new System.Windows.Forms.Padding(3);
            this.fieldProductDate.Name = "fieldProductDate";
            this.fieldProductDate.Size = new System.Drawing.Size(587, 20);
            this.fieldProductDate.TabIndex = 31;
            this.fieldProductDate.Text = " Дата производства";
            this.fieldProductDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldProductDate.Visible = false;
            this.fieldProductDate.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldKneading
            // 
            this.fieldKneading.AutoSize = true;
            this.fieldKneading.BackColor = System.Drawing.Color.Transparent;
            this.fieldKneading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldKneading.Location = new System.Drawing.Point(325, 241);
            this.fieldKneading.Margin = new System.Windows.Forms.Padding(3);
            this.fieldKneading.Name = "fieldKneading";
            this.fieldKneading.Size = new System.Drawing.Size(587, 80);
            this.fieldKneading.TabIndex = 32;
            this.fieldKneading.Text = " Замес";
            this.fieldKneading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldKneading.Visible = false;
            this.fieldKneading.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldPrintLabels
            // 
            this.fieldPrintLabels.AutoSize = true;
            this.fieldPrintLabels.BackColor = System.Drawing.Color.Transparent;
            this.fieldPrintLabels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPrintLabels.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPrintLabels.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fieldPrintLabels.Location = new System.Drawing.Point(325, 405);
            this.fieldPrintLabels.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPrintLabels.Name = "fieldPrintLabels";
            this.fieldPrintLabels.Size = new System.Drawing.Size(587, 20);
            this.fieldPrintLabels.TabIndex = 37;
            this.fieldPrintLabels.Text = "Этикетки: 0 / 0";
            this.fieldPrintLabels.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldPrintLabels.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
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
            this.fieldTitle.DoubleClick += new System.EventHandler(this.FieldTitle_DoubleClick);
            this.fieldTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldPlu
            // 
            this.fieldPlu.AutoSize = true;
            this.fieldPlu.BackColor = System.Drawing.Color.Transparent;
            this.fieldPlu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPlu.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPlu.Location = new System.Drawing.Point(325, 33);
            this.fieldPlu.Name = "fieldPlu";
            this.fieldPlu.Size = new System.Drawing.Size(587, 33);
            this.fieldPlu.TabIndex = 14;
            this.fieldPlu.Text = "PLU";
            this.fieldPlu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldPlu.Visible = false;
            this.fieldPlu.Click += new System.EventHandler(this.ButtonPlu_Click);
            this.fieldPlu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // labelPlu
            // 
            this.labelPlu.AutoSize = true;
            this.labelPlu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPlu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPlu.Location = new System.Drawing.Point(3, 238);
            this.labelPlu.Name = "labelPlu";
            this.labelPlu.Size = new System.Drawing.Size(1, 86);
            this.labelPlu.TabIndex = 33;
            this.labelPlu.Text = "PLU";
            this.labelPlu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelPlu.Click += new System.EventHandler(this.ButtonPlu_Click);
            this.labelPlu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // tableLayoutPanelButtons
            // 
            this.tableLayoutPanelButtons.ColumnCount = 9;
            this.tableLayoutPanelMain.SetColumnSpan(this.tableLayoutPanelButtons, 5);
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelButtons.Location = new System.Drawing.Point(3, 535);
            this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            this.tableLayoutPanelButtons.RowCount = 1;
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelButtons.Size = new System.Drawing.Size(1018, 130);
            this.tableLayoutPanelButtons.TabIndex = 59;
            // 
            // tableLayoutPanelRight
            // 
            this.tableLayoutPanelRight.ColumnCount = 1;
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRight.Controls.Add(this.fieldLang, 0, 1);
            this.tableLayoutPanelRight.Controls.Add(this.fieldResolution, 0, 0);
            this.tableLayoutPanelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelRight.Location = new System.Drawing.Point(918, 155);
            this.tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            this.tableLayoutPanelRight.RowCount = 2;
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelRight.Size = new System.Drawing.Size(96, 80);
            this.tableLayoutPanelRight.TabIndex = 62;
            // 
            // fieldLang
            // 
            this.fieldLang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fieldLang.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldLang.FormattingEnabled = true;
            this.fieldLang.Location = new System.Drawing.Point(3, 43);
            this.fieldLang.Name = "fieldLang";
            this.fieldLang.Size = new System.Drawing.Size(90, 21);
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
            this.fieldResolution.Size = new System.Drawing.Size(90, 21);
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
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            this.tableLayoutPanelButtons.ResumeLayout(false);
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
        private System.Windows.Forms.Label fieldCurrentTime;
        private System.Windows.Forms.Label labelKneading;
        private System.Windows.Forms.Label labelProductDate;
        private System.Windows.Forms.ProgressBar fieldPrintProgress;
        private System.Windows.Forms.Label fieldProductDate;
        private System.Windows.Forms.Label fieldKneading;
        private System.Windows.Forms.Label labelPlu;
        private System.Windows.Forms.Label fieldPrintLabels;
        private System.Windows.Forms.Label fieldMassaManager;
        private System.Windows.Forms.Label fieldPrintManager;
        private System.Windows.Forms.Label fieldMemoryManager;
        private System.Windows.Forms.Label fieldMassaGet;
        private System.Windows.Forms.Label fieldMassaQueries;
        private System.Windows.Forms.Label fieldMassaSet;
        private System.Windows.Forms.Label fieldMassaScalePar;
        private System.Windows.Forms.Label fieldMassaComPort;
        private System.Windows.Forms.Label fieldMassaSetCrc;
        private System.Windows.Forms.Label fieldMassaGetCrc;
        private System.Windows.Forms.Label fieldMemoryManagerTotal;
        private System.Windows.Forms.ProgressBar fieldMemoryProgress;
        private System.Windows.Forms.ProgressBar fieldMassaQueriesProgress;
        private System.Windows.Forms.Label fieldTasks;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
        private System.Windows.Forms.Label labelSscc;
        private System.Windows.Forms.Label fieldSscc;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRight;
        private System.Windows.Forms.ComboBox fieldLang;
        private System.Windows.Forms.ComboBox fieldResolution;
        private System.Windows.Forms.Label labelThreshold;
        private System.Windows.Forms.Label fieldThreshold;
    }
}

