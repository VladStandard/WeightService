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
            this.fieldLang = new System.Windows.Forms.ComboBox();
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
            this.fieldCountBox = new System.Windows.Forms.ProgressBar();
            this.fieldResolution = new System.Windows.Forms.ComboBox();
            this.fieldCurrentTime = new System.Windows.Forms.Label();
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.labelWeightTare = new System.Windows.Forms.Label();
            this.labelKneading = new System.Windows.Forms.Label();
            this.labelProductDate = new System.Windows.Forms.Label();
            this.fieldProductDate = new System.Windows.Forms.Label();
            this.fieldKneading = new System.Windows.Forms.Label();
            this.fieldLabelsCount = new System.Windows.Forms.Label();
            this.fieldTitle = new System.Windows.Forms.Label();
            this.fieldPlu = new System.Windows.Forms.Label();
            this.labelPlu = new System.Windows.Forms.Label();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.flowLayoutPanelBottom = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.buttonSetKneading = new System.Windows.Forms.Button();
            this.buttonSelectPlu = new System.Windows.Forms.Button();
            this.buttonAddKneading = new System.Windows.Forms.Button();
            this.buttonNewPallet = new System.Windows.Forms.Button();
            this.buttonSelectOrder = new System.Windows.Forms.Button();
            this.buttonScalesInit = new System.Windows.Forms.Button();
            this.buttonRunScalesTerminal = new System.Windows.Forms.Button();
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
            this.labelWeightNetto.Location = new System.Drawing.Point(14, 65);
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
            this.fieldWeightTare.Location = new System.Drawing.Point(343, 164);
            this.fieldWeightTare.Margin = new System.Windows.Forms.Padding(3);
            this.fieldWeightTare.Name = "fieldWeightTare";
            this.fieldWeightTare.Size = new System.Drawing.Size(702, 112);
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
            this.fieldWeightNetto.Location = new System.Drawing.Point(343, 65);
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
            this.tableLayoutPanelMain.Controls.Add(this.fieldLang, 3, 14);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaQueriesProgress, 3, 12);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMemoryProgress, 3, 13);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMemoryManagerTotal, 2, 13);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaSetCrc, 3, 11);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaGetCrc, 3, 10);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaComPort, 1, 11);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaScalePar, 2, 10);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaSet, 2, 12);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaQueries, 1, 12);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaGet, 2, 11);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMassaManager, 1, 10);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPrintManager, 1, 9);
            this.tableLayoutPanelMain.Controls.Add(this.fieldMemoryManager, 1, 13);
            this.tableLayoutPanelMain.Controls.Add(this.fieldCountBox, 3, 9);
            this.tableLayoutPanelMain.Controls.Add(this.fieldResolution, 3, 1);
            this.tableLayoutPanelMain.Controls.Add(this.fieldCurrentTime, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.pictureBoxClose, 3, 2);
            this.tableLayoutPanelMain.Controls.Add(this.labelWeightTare, 1, 3);
            this.tableLayoutPanelMain.Controls.Add(this.labelWeightNetto, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.fieldWeightNetto, 2, 2);
            this.tableLayoutPanelMain.Controls.Add(this.fieldWeightTare, 2, 3);
            this.tableLayoutPanelMain.Controls.Add(this.labelKneading, 1, 5);
            this.tableLayoutPanelMain.Controls.Add(this.labelProductDate, 1, 7);
            this.tableLayoutPanelMain.Controls.Add(this.fieldProductDate, 2, 7);
            this.tableLayoutPanelMain.Controls.Add(this.fieldKneading, 2, 5);
            this.tableLayoutPanelMain.Controls.Add(this.fieldLabelsCount, 2, 9);
            this.tableLayoutPanelMain.Controls.Add(this.fieldTitle, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.fieldPlu, 2, 1);
            this.tableLayoutPanelMain.Controls.Add(this.labelPlu, 0, 5);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 15;
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
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1180, 622);
            this.tableLayoutPanelMain.TabIndex = 7;
            this.tableLayoutPanelMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldLang
            // 
            this.fieldLang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fieldLang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldLang.FormattingEnabled = true;
            this.fieldLang.Items.AddRange(new object[] {
            "Russian",
            "English"});
            this.fieldLang.Location = new System.Drawing.Point(1051, 591);
            this.fieldLang.Name = "fieldLang";
            this.fieldLang.Size = new System.Drawing.Size(112, 24);
            this.fieldLang.TabIndex = 56;
            this.fieldLang.Visible = false;
            this.fieldLang.SelectedIndexChanged += new System.EventHandler(this.FieldLang_SelectedIndexChanged);
            this.fieldLang.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldMassaQueriesProgress
            // 
            this.fieldMassaQueriesProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldMassaQueriesProgress.Location = new System.Drawing.Point(1051, 529);
            this.fieldMassaQueriesProgress.Name = "fieldMassaQueriesProgress";
            this.fieldMassaQueriesProgress.Size = new System.Drawing.Size(112, 25);
            this.fieldMassaQueriesProgress.TabIndex = 55;
            this.fieldMassaQueriesProgress.Visible = false;
            // 
            // fieldMemoryProgress
            // 
            this.fieldMemoryProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldMemoryProgress.Location = new System.Drawing.Point(1051, 560);
            this.fieldMemoryProgress.Name = "fieldMemoryProgress";
            this.fieldMemoryProgress.Size = new System.Drawing.Size(112, 25);
            this.fieldMemoryProgress.TabIndex = 54;
            this.fieldMemoryProgress.Visible = false;
            // 
            // fieldMemoryManagerTotal
            // 
            this.fieldMemoryManagerTotal.AutoSize = true;
            this.fieldMemoryManagerTotal.BackColor = System.Drawing.Color.Transparent;
            this.fieldMemoryManagerTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMemoryManagerTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMemoryManagerTotal.Location = new System.Drawing.Point(343, 560);
            this.fieldMemoryManagerTotal.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMemoryManagerTotal.Name = "fieldMemoryManagerTotal";
            this.fieldMemoryManagerTotal.Size = new System.Drawing.Size(702, 25);
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
            this.fieldMassaSetCrc.Location = new System.Drawing.Point(1051, 498);
            this.fieldMassaSetCrc.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaSetCrc.Name = "fieldMassaSetCrc";
            this.fieldMassaSetCrc.Size = new System.Drawing.Size(112, 25);
            this.fieldMassaSetCrc.TabIndex = 52;
            this.fieldMassaSetCrc.Text = "CRC: ";
            this.fieldMassaSetCrc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMassaSetCrc.Visible = false;
            // 
            // fieldMassaGetCrc
            // 
            this.fieldMassaGetCrc.AutoSize = true;
            this.fieldMassaGetCrc.BackColor = System.Drawing.Color.Transparent;
            this.fieldMassaGetCrc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaGetCrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaGetCrc.Location = new System.Drawing.Point(1051, 467);
            this.fieldMassaGetCrc.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaGetCrc.Name = "fieldMassaGetCrc";
            this.fieldMassaGetCrc.Size = new System.Drawing.Size(112, 25);
            this.fieldMassaGetCrc.TabIndex = 51;
            this.fieldMassaGetCrc.Text = "CRC: ";
            this.fieldMassaGetCrc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMassaGetCrc.Visible = false;
            // 
            // fieldMassaComPort
            // 
            this.fieldMassaComPort.AutoSize = true;
            this.fieldMassaComPort.BackColor = System.Drawing.Color.Transparent;
            this.fieldMassaComPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaComPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaComPort.Location = new System.Drawing.Point(14, 498);
            this.fieldMassaComPort.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaComPort.Name = "fieldMassaComPort";
            this.fieldMassaComPort.Size = new System.Drawing.Size(323, 25);
            this.fieldMassaComPort.TabIndex = 50;
            this.fieldMassaComPort.Text = "Состояние COM-порта: ";
            this.fieldMassaComPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMassaComPort.Visible = false;
            // 
            // fieldMassaScalePar
            // 
            this.fieldMassaScalePar.AutoSize = true;
            this.fieldMassaScalePar.BackColor = System.Drawing.Color.Transparent;
            this.fieldMassaScalePar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaScalePar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaScalePar.Location = new System.Drawing.Point(343, 467);
            this.fieldMassaScalePar.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaScalePar.Name = "fieldMassaScalePar";
            this.fieldMassaScalePar.Size = new System.Drawing.Size(702, 25);
            this.fieldMassaScalePar.TabIndex = 49;
            this.fieldMassaScalePar.Text = "Запрос параметров: ";
            this.fieldMassaScalePar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMassaScalePar.Visible = false;
            // 
            // fieldMassaSet
            // 
            this.fieldMassaSet.AutoSize = true;
            this.fieldMassaSet.BackColor = System.Drawing.Color.Transparent;
            this.fieldMassaSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaSet.Location = new System.Drawing.Point(343, 529);
            this.fieldMassaSet.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaSet.Name = "fieldMassaSet";
            this.fieldMassaSet.Size = new System.Drawing.Size(702, 25);
            this.fieldMassaSet.TabIndex = 48;
            this.fieldMassaSet.Text = "Команда для весов: ";
            this.fieldMassaSet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMassaSet.Visible = false;
            // 
            // fieldMassaQueries
            // 
            this.fieldMassaQueries.AutoSize = true;
            this.fieldMassaQueries.BackColor = System.Drawing.Color.Transparent;
            this.fieldMassaQueries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaQueries.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaQueries.Location = new System.Drawing.Point(14, 529);
            this.fieldMassaQueries.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaQueries.Name = "fieldMassaQueries";
            this.fieldMassaQueries.Size = new System.Drawing.Size(323, 25);
            this.fieldMassaQueries.TabIndex = 47;
            this.fieldMassaQueries.Text = "Очередь сообщений весов:  ";
            this.fieldMassaQueries.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMassaQueries.Visible = false;
            // 
            // fieldMassaGet
            // 
            this.fieldMassaGet.AutoSize = true;
            this.fieldMassaGet.BackColor = System.Drawing.Color.Transparent;
            this.fieldMassaGet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaGet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaGet.Location = new System.Drawing.Point(343, 498);
            this.fieldMassaGet.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaGet.Name = "fieldMassaGet";
            this.fieldMassaGet.Size = new System.Drawing.Size(702, 25);
            this.fieldMassaGet.TabIndex = 46;
            this.fieldMassaGet.Text = "Сообщение взвешивания: ";
            this.fieldMassaGet.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMassaGet.Visible = false;
            // 
            // fieldMassaManager
            // 
            this.fieldMassaManager.AutoSize = true;
            this.fieldMassaManager.BackColor = System.Drawing.Color.Transparent;
            this.fieldMassaManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMassaManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMassaManager.Location = new System.Drawing.Point(14, 467);
            this.fieldMassaManager.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMassaManager.Name = "fieldMassaManager";
            this.fieldMassaManager.Size = new System.Drawing.Size(323, 25);
            this.fieldMassaManager.TabIndex = 44;
            this.fieldMassaManager.Text = "Менеджер весов";
            this.fieldMassaManager.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMassaManager.Visible = false;
            this.fieldMassaManager.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldPrintManager
            // 
            this.fieldPrintManager.AutoSize = true;
            this.fieldPrintManager.BackColor = System.Drawing.Color.Transparent;
            this.fieldPrintManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPrintManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldPrintManager.Location = new System.Drawing.Point(14, 436);
            this.fieldPrintManager.Margin = new System.Windows.Forms.Padding(3);
            this.fieldPrintManager.Name = "fieldPrintManager";
            this.fieldPrintManager.Size = new System.Drawing.Size(323, 25);
            this.fieldPrintManager.TabIndex = 43;
            this.fieldPrintManager.Text = "Менеджер принтера";
            this.fieldPrintManager.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldPrintManager.Visible = false;
            this.fieldPrintManager.DoubleClick += new System.EventHandler(this.FieldPrintManager_DoubleClick);
            this.fieldPrintManager.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldMemoryManager
            // 
            this.fieldMemoryManager.AutoSize = true;
            this.fieldMemoryManager.BackColor = System.Drawing.Color.Transparent;
            this.fieldMemoryManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldMemoryManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldMemoryManager.Location = new System.Drawing.Point(14, 560);
            this.fieldMemoryManager.Margin = new System.Windows.Forms.Padding(3);
            this.fieldMemoryManager.Name = "fieldMemoryManager";
            this.fieldMemoryManager.Size = new System.Drawing.Size(323, 25);
            this.fieldMemoryManager.TabIndex = 42;
            this.fieldMemoryManager.Text = "Менеджер памяти";
            this.fieldMemoryManager.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldMemoryManager.Visible = false;
            this.fieldMemoryManager.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldCountBox
            // 
            this.fieldCountBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fieldCountBox.Location = new System.Drawing.Point(1051, 436);
            this.fieldCountBox.Name = "fieldCountBox";
            this.fieldCountBox.Size = new System.Drawing.Size(112, 25);
            this.fieldCountBox.TabIndex = 30;
            this.fieldCountBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldResolution
            // 
            this.fieldResolution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fieldResolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldResolution.FormattingEnabled = true;
            this.fieldResolution.Items.AddRange(new object[] {
            "Максимальное",
            "1024х768",
            "1366х768",
            "1920х1080"});
            this.fieldResolution.Location = new System.Drawing.Point(1051, 34);
            this.fieldResolution.Name = "fieldResolution";
            this.fieldResolution.Size = new System.Drawing.Size(112, 24);
            this.fieldResolution.TabIndex = 29;
            this.fieldResolution.Visible = false;
            this.fieldResolution.SelectedIndexChanged += new System.EventHandler(this.FieldResolution_SelectedIndexChanged);
            this.fieldResolution.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldCurrentTime
            // 
            this.fieldCurrentTime.AutoSize = true;
            this.fieldCurrentTime.BackColor = System.Drawing.Color.Transparent;
            this.fieldCurrentTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldCurrentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldCurrentTime.Location = new System.Drawing.Point(14, 34);
            this.fieldCurrentTime.Margin = new System.Windows.Forms.Padding(3);
            this.fieldCurrentTime.Name = "fieldCurrentTime";
            this.fieldCurrentTime.Size = new System.Drawing.Size(323, 25);
            this.fieldCurrentTime.TabIndex = 25;
            this.fieldCurrentTime.Text = "Дата время";
            this.fieldCurrentTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldCurrentTime.Click += new System.EventHandler(this.FieldCurrentTime_Click);
            this.fieldCurrentTime.DoubleClick += new System.EventHandler(this.FieldDt_DoubleClick);
            this.fieldCurrentTime.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxClose.Image = global::ScalesUI.Properties.Resources.exit_2;
            this.pictureBoxClose.Location = new System.Drawing.Point(1051, 65);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(112, 93);
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
            this.labelWeightTare.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWeightTare.Location = new System.Drawing.Point(14, 164);
            this.labelWeightTare.Margin = new System.Windows.Forms.Padding(3);
            this.labelWeightTare.Name = "labelWeightTare";
            this.labelWeightTare.Size = new System.Drawing.Size(323, 112);
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
            this.labelKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelKneading.Location = new System.Drawing.Point(14, 288);
            this.labelKneading.Margin = new System.Windows.Forms.Padding(3);
            this.labelKneading.Name = "labelKneading";
            this.labelKneading.Size = new System.Drawing.Size(323, 74);
            this.labelKneading.TabIndex = 27;
            this.labelKneading.Text = "Замес";
            this.labelKneading.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelKneading.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // labelProductDate
            // 
            this.labelProductDate.AutoSize = true;
            this.labelProductDate.BackColor = System.Drawing.Color.Transparent;
            this.labelProductDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelProductDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelProductDate.Location = new System.Drawing.Point(14, 374);
            this.labelProductDate.Margin = new System.Windows.Forms.Padding(3);
            this.labelProductDate.Name = "labelProductDate";
            this.labelProductDate.Size = new System.Drawing.Size(323, 25);
            this.labelProductDate.TabIndex = 28;
            this.labelProductDate.Text = "Дата производства";
            this.labelProductDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelProductDate.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldProductDate
            // 
            this.fieldProductDate.AutoSize = true;
            this.fieldProductDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldProductDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldProductDate.Location = new System.Drawing.Point(343, 374);
            this.fieldProductDate.Margin = new System.Windows.Forms.Padding(3);
            this.fieldProductDate.Name = "fieldProductDate";
            this.fieldProductDate.Size = new System.Drawing.Size(702, 25);
            this.fieldProductDate.TabIndex = 31;
            this.fieldProductDate.Text = " Дата производства";
            this.fieldProductDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldProductDate.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldKneading
            // 
            this.fieldKneading.AutoSize = true;
            this.fieldKneading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldKneading.Location = new System.Drawing.Point(343, 288);
            this.fieldKneading.Margin = new System.Windows.Forms.Padding(3);
            this.fieldKneading.Name = "fieldKneading";
            this.fieldKneading.Size = new System.Drawing.Size(702, 74);
            this.fieldKneading.TabIndex = 32;
            this.fieldKneading.Text = " Замес";
            this.fieldKneading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldKneading.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldLabelsCount
            // 
            this.fieldLabelsCount.AutoSize = true;
            this.fieldLabelsCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldLabelsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fieldLabelsCount.Location = new System.Drawing.Point(343, 436);
            this.fieldLabelsCount.Margin = new System.Windows.Forms.Padding(3);
            this.fieldLabelsCount.Name = "fieldLabelsCount";
            this.fieldLabelsCount.Size = new System.Drawing.Size(702, 25);
            this.fieldLabelsCount.TabIndex = 37;
            this.fieldLabelsCount.Text = "Этикетки: 0 / 0";
            this.fieldLabelsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldLabelsCount.Visible = false;
            this.fieldLabelsCount.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
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
            this.fieldTitle.Size = new System.Drawing.Size(1174, 31);
            this.fieldTitle.TabIndex = 20;
            this.fieldTitle.Text = "ScalesUI";
            this.fieldTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldTitle.DoubleClick += new System.EventHandler(this.FieldTitle_DoubleClick);
            this.fieldTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // fieldPlu
            // 
            this.fieldPlu.AutoSize = true;
            this.fieldPlu.BackColor = System.Drawing.SystemColors.Control;
            this.fieldPlu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldPlu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.fieldPlu.Location = new System.Drawing.Point(343, 31);
            this.fieldPlu.Name = "fieldPlu";
            this.fieldPlu.Size = new System.Drawing.Size(702, 31);
            this.fieldPlu.TabIndex = 14;
            this.fieldPlu.Text = "PLU";
            this.fieldPlu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fieldPlu.Click += new System.EventHandler(this.ButtonSelectPlu_Click);
            this.fieldPlu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // labelPlu
            // 
            this.labelPlu.AutoSize = true;
            this.labelPlu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPlu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPlu.Location = new System.Drawing.Point(3, 285);
            this.labelPlu.Name = "labelPlu";
            this.labelPlu.Size = new System.Drawing.Size(5, 80);
            this.labelPlu.TabIndex = 33;
            this.labelPlu.Text = "PLU";
            this.labelPlu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelPlu.Click += new System.EventHandler(this.ButtonSelectPlu_Click);
            this.labelPlu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // buttonSettings
            // 
            this.buttonSettings.BackColor = System.Drawing.Color.Transparent;
            this.buttonSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSettings.Location = new System.Drawing.Point(405, 3);
            this.buttonSettings.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(120, 122);
            this.buttonSettings.TabIndex = 0;
            this.buttonSettings.Text = "Настройки";
            this.buttonSettings.UseVisualStyleBackColor = false;
            this.buttonSettings.Click += new System.EventHandler(this.ButtonSettings_Click);
            this.buttonSettings.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // flowLayoutPanelBottom
            // 
            this.flowLayoutPanelBottom.Controls.Add(this.buttonPrint);
            this.flowLayoutPanelBottom.Controls.Add(this.buttonSetKneading);
            this.flowLayoutPanelBottom.Controls.Add(this.buttonSelectPlu);
            this.flowLayoutPanelBottom.Controls.Add(this.buttonAddKneading);
            this.flowLayoutPanelBottom.Controls.Add(this.buttonNewPallet);
            this.flowLayoutPanelBottom.Controls.Add(this.buttonSettings);
            this.flowLayoutPanelBottom.Controls.Add(this.buttonSelectOrder);
            this.flowLayoutPanelBottom.Controls.Add(this.buttonScalesInit);
            this.flowLayoutPanelBottom.Controls.Add(this.buttonRunScalesTerminal);
            this.flowLayoutPanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelBottom.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelBottom.Location = new System.Drawing.Point(0, 622);
            this.flowLayoutPanelBottom.Name = "flowLayoutPanelBottom";
            this.flowLayoutPanelBottom.Size = new System.Drawing.Size(1180, 128);
            this.flowLayoutPanelBottom.TabIndex = 17;
            this.flowLayoutPanelBottom.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // buttonPrint
            // 
            this.buttonPrint.BackColor = System.Drawing.Color.Transparent;
            this.buttonPrint.Enabled = false;
            this.buttonPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPrint.Location = new System.Drawing.Point(1055, 3);
            this.buttonPrint.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(120, 122);
            this.buttonPrint.TabIndex = 6;
            this.buttonPrint.Text = "Печать";
            this.buttonPrint.UseVisualStyleBackColor = false;
            this.buttonPrint.Click += new System.EventHandler(this.ButtonPrint_Click);
            this.buttonPrint.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // buttonSetKneading
            // 
            this.buttonSetKneading.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSetKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSetKneading.Location = new System.Drawing.Point(925, 3);
            this.buttonSetKneading.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.buttonSetKneading.Name = "buttonSetKneading";
            this.buttonSetKneading.Size = new System.Drawing.Size(120, 122);
            this.buttonSetKneading.TabIndex = 5;
            this.buttonSetKneading.Text = "Ещё";
            this.buttonSetKneading.UseVisualStyleBackColor = false;
            this.buttonSetKneading.Click += new System.EventHandler(this.ButtonSetKneading_Click);
            this.buttonSetKneading.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // buttonSelectPlu
            // 
            this.buttonSelectPlu.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSelectPlu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSelectPlu.Location = new System.Drawing.Point(795, 3);
            this.buttonSelectPlu.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.buttonSelectPlu.Name = "buttonSelectPlu";
            this.buttonSelectPlu.Size = new System.Drawing.Size(120, 122);
            this.buttonSelectPlu.TabIndex = 3;
            this.buttonSelectPlu.Text = "Выбрать\r\nPLU";
            this.buttonSelectPlu.UseVisualStyleBackColor = false;
            this.buttonSelectPlu.Click += new System.EventHandler(this.ButtonSelectPlu_Click);
            this.buttonSelectPlu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // buttonAddKneading
            // 
            this.buttonAddKneading.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonAddKneading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddKneading.Location = new System.Drawing.Point(665, 3);
            this.buttonAddKneading.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.buttonAddKneading.Name = "buttonAddKneading";
            this.buttonAddKneading.Size = new System.Drawing.Size(120, 122);
            this.buttonAddKneading.TabIndex = 35;
            this.buttonAddKneading.Text = "Замес";
            this.buttonAddKneading.UseVisualStyleBackColor = false;
            this.buttonAddKneading.Click += new System.EventHandler(this.ButtonAddKneading_Click);
            this.buttonAddKneading.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // buttonNewPallet
            // 
            this.buttonNewPallet.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonNewPallet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonNewPallet.Location = new System.Drawing.Point(535, 3);
            this.buttonNewPallet.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.buttonNewPallet.Name = "buttonNewPallet";
            this.buttonNewPallet.Size = new System.Drawing.Size(120, 122);
            this.buttonNewPallet.TabIndex = 34;
            this.buttonNewPallet.Text = "Новая палета";
            this.buttonNewPallet.UseVisualStyleBackColor = false;
            this.buttonNewPallet.Click += new System.EventHandler(this.ButtonNewPallet_Click);
            this.buttonNewPallet.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // buttonSelectOrder
            // 
            this.buttonSelectOrder.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonSelectOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSelectOrder.Location = new System.Drawing.Point(275, 3);
            this.buttonSelectOrder.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.buttonSelectOrder.Name = "buttonSelectOrder";
            this.buttonSelectOrder.Size = new System.Drawing.Size(120, 122);
            this.buttonSelectOrder.TabIndex = 4;
            this.buttonSelectOrder.Text = "Выбрать\r\nзаказ";
            this.buttonSelectOrder.UseVisualStyleBackColor = false;
            this.buttonSelectOrder.Click += new System.EventHandler(this.ButtonSelectOrder_Click);
            this.buttonSelectOrder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // buttonScalesInit
            // 
            this.buttonScalesInit.BackColor = System.Drawing.Color.Transparent;
            this.buttonScalesInit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonScalesInit.Location = new System.Drawing.Point(145, 3);
            this.buttonScalesInit.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.buttonScalesInit.Name = "buttonScalesInit";
            this.buttonScalesInit.Size = new System.Drawing.Size(120, 122);
            this.buttonScalesInit.TabIndex = 1;
            this.buttonScalesInit.Text = "Инициали-\r\nзировать весы";
            this.buttonScalesInit.UseVisualStyleBackColor = false;
            this.buttonScalesInit.Visible = false;
            this.buttonScalesInit.Click += new System.EventHandler(this.ButtonScalesInit_Click);
            this.buttonScalesInit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            // 
            // buttonRunScalesTerminal
            // 
            this.buttonRunScalesTerminal.BackColor = System.Drawing.Color.Transparent;
            this.buttonRunScalesTerminal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRunScalesTerminal.Location = new System.Drawing.Point(15, 3);
            this.buttonRunScalesTerminal.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.buttonRunScalesTerminal.Name = "buttonRunScalesTerminal";
            this.buttonRunScalesTerminal.Size = new System.Drawing.Size(120, 122);
            this.buttonRunScalesTerminal.TabIndex = 36;
            this.buttonRunScalesTerminal.Text = "Scales Terminal";
            this.buttonRunScalesTerminal.UseVisualStyleBackColor = false;
            this.buttonRunScalesTerminal.Click += new System.EventHandler(this.ButtonRunScalesTerminal_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 750);
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
        private System.Windows.Forms.Label labelKneading;
        private System.Windows.Forms.Label labelProductDate;
        private System.Windows.Forms.ProgressBar fieldCountBox;
        private System.Windows.Forms.ComboBox fieldResolution;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBottom;
        private System.Windows.Forms.Button buttonScalesInit;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.Button buttonSetKneading;
        private System.Windows.Forms.Button buttonSelectOrder;
        private System.Windows.Forms.Button buttonSelectPlu;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Label fieldProductDate;
        private System.Windows.Forms.Label fieldKneading;
        private System.Windows.Forms.Label labelPlu;
        private System.Windows.Forms.Button buttonAddKneading;
        private System.Windows.Forms.Button buttonNewPallet;
        private System.Windows.Forms.Label fieldLabelsCount;
        private System.Windows.Forms.Label fieldMassaManager;
        private System.Windows.Forms.Label fieldPrintManager;
        private System.Windows.Forms.Label fieldMemoryManager;
        private System.Windows.Forms.Label fieldMassaGet;
        private System.Windows.Forms.Label fieldMassaQueries;
        private System.Windows.Forms.Label fieldMassaSet;
        private System.Windows.Forms.Button buttonRunScalesTerminal;
        private System.Windows.Forms.Label fieldMassaScalePar;
        private System.Windows.Forms.Label fieldMassaComPort;
        private System.Windows.Forms.Label fieldMassaSetCrc;
        private System.Windows.Forms.Label fieldMassaGetCrc;
        private System.Windows.Forms.Label fieldMemoryManagerTotal;
        private System.Windows.Forms.ProgressBar fieldMemoryProgress;
        private System.Windows.Forms.ProgressBar fieldMassaQueriesProgress;
        private System.Windows.Forms.ComboBox fieldLang;
    }
}

