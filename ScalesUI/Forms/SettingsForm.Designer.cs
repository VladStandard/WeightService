//namespace ScalesUI.Forms
//{
//    partial class SettingsForm
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components is not null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
//			this.fieldCurrentWeightFact = new System.Windows.Forms.TextBox();
//			this.fieldCurrentMKProp = new System.Windows.Forms.TextBox();
//			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
//			this.buttonClose = new System.Windows.Forms.Button();
//			this.buttonSaveOption = new System.Windows.Forms.Button();
//			this.buttonMassaParam = new System.Windows.Forms.Button();
//			this.buttonUploadResources = new System.Windows.Forms.Button();
//			this.buttonGenerateException = new System.Windows.Forms.Button();
//			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
//			this.buttonPrintOptions = new System.Windows.Forms.Button();
//			this.buttonPrintReset = new System.Windows.Forms.Button();
//			this.buttonPrintCancelAll = new System.Windows.Forms.Button();
//			this.buttonPrintCalibrate = new System.Windows.Forms.Button();
//			this.tableLayoutPanel3.SuspendLayout();
//			this.flowLayoutPanel1.SuspendLayout();
//			this.flowLayoutPanel2.SuspendLayout();
//			this.SuspendLayout();
//			// 
//			// tableLayoutPanel3
//			// 
//			this.tableLayoutPanel3.ColumnCount = 3;
//			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
//			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
//			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
//			this.tableLayoutPanel3.Controls.Add(this.fieldCurrentWeightFact, 0, 0);
//			this.tableLayoutPanel3.Controls.Add(this.fieldCurrentMKProp, 0, 5);
//			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Left;
//			this.tableLayoutPanel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//			this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
//			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
//			this.tableLayoutPanel3.RowCount = 11;
//			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
//			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
//			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
//			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
//			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
//			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
//			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
//			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
//			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.11111F));
//			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
//			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
//			this.tableLayoutPanel3.Size = new System.Drawing.Size(690, 431);
//			this.tableLayoutPanel3.TabIndex = 2;
//			// 
//			// fieldCurrentWeightFact
//			// 
//			this.tableLayoutPanel3.SetColumnSpan(this.fieldCurrentWeightFact, 3);
//			this.fieldCurrentWeightFact.Dock = System.Windows.Forms.DockStyle.Fill;
//			this.fieldCurrentWeightFact.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.150944F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//			this.fieldCurrentWeightFact.Location = new System.Drawing.Point(3, 3);
//			this.fieldCurrentWeightFact.Multiline = true;
//			this.fieldCurrentWeightFact.Name = "fieldCurrentWeightFact";
//			this.fieldCurrentWeightFact.ReadOnly = true;
//			this.tableLayoutPanel3.SetRowSpan(this.fieldCurrentWeightFact, 5);
//			this.fieldCurrentWeightFact.ScrollBars = System.Windows.Forms.ScrollBars.Both;
//			this.fieldCurrentWeightFact.Size = new System.Drawing.Size(684, 119);
//			this.fieldCurrentWeightFact.TabIndex = 22;
//			// 
//			// fieldCurrentMKProp
//			// 
//			this.tableLayoutPanel3.SetColumnSpan(this.fieldCurrentMKProp, 3);
//			this.fieldCurrentMKProp.Dock = System.Windows.Forms.DockStyle.Fill;
//			this.fieldCurrentMKProp.Location = new System.Drawing.Point(3, 128);
//			this.fieldCurrentMKProp.Multiline = true;
//			this.fieldCurrentMKProp.Name = "fieldCurrentMKProp";
//			this.tableLayoutPanel3.SetRowSpan(this.fieldCurrentMKProp, 5);
//			this.fieldCurrentMKProp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
//			this.fieldCurrentMKProp.Size = new System.Drawing.Size(684, 194);
//			this.fieldCurrentMKProp.TabIndex = 29;
//			// 
//			// flowLayoutPanel1
//			// 
//			this.flowLayoutPanel1.Controls.Add(this.buttonClose);
//			this.flowLayoutPanel1.Controls.Add(this.buttonSaveOption);
//			this.flowLayoutPanel1.Controls.Add(this.buttonMassaParam);
//			this.flowLayoutPanel1.Controls.Add(this.buttonUploadResources);
//			this.flowLayoutPanel1.Controls.Add(this.buttonGenerateException);
//			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
//			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
//			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 431);
//			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
//			this.flowLayoutPanel1.Size = new System.Drawing.Size(724, 130);
//			this.flowLayoutPanel1.TabIndex = 3;
//			// 
//			// buttonClose
//			// 
//			this.buttonClose.BackColor = System.Drawing.Color.Transparent;
//			this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//			this.buttonClose.Location = new System.Drawing.Point(581, 3);
//			this.buttonClose.Name = "buttonClose";
//			this.buttonClose.Size = new System.Drawing.Size(140, 120);
//			this.buttonClose.TabIndex = 1;
//			this.buttonClose.Text = "Закрыть";
//			this.buttonClose.UseVisualStyleBackColor = false;
//			this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
//			// 
//			// buttonSaveOption
//			// 
//			this.buttonSaveOption.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
//			this.buttonSaveOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//			this.buttonSaveOption.Location = new System.Drawing.Point(435, 3);
//			this.buttonSaveOption.Name = "buttonSaveOption";
//			this.buttonSaveOption.Size = new System.Drawing.Size(140, 120);
//			this.buttonSaveOption.TabIndex = 2;
//			this.buttonSaveOption.Text = "Сохранить настройки";
//			this.buttonSaveOption.UseVisualStyleBackColor = false;
//			this.buttonSaveOption.Click += new System.EventHandler(this.ButtonSaveOption_Click);
//			// 
//			// buttonMassaParam
//			// 
//			this.buttonMassaParam.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//			this.buttonMassaParam.Location = new System.Drawing.Point(289, 3);
//			this.buttonMassaParam.Name = "buttonMassaParam";
//			this.buttonMassaParam.Size = new System.Drawing.Size(140, 120);
//			this.buttonMassaParam.TabIndex = 20;
//			this.buttonMassaParam.Text = "Запрос параметров весового устройства";
//			this.buttonMassaParam.UseVisualStyleBackColor = true;
//			this.buttonMassaParam.Click += new System.EventHandler(this.ButtonMassaParam_Click);
//			// 
//			// buttonUploadResources
//			// 
//			this.buttonUploadResources.BackColor = System.Drawing.Color.Transparent;
//			this.buttonUploadResources.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//			this.buttonUploadResources.Location = new System.Drawing.Point(133, 3);
//			this.buttonUploadResources.Name = "buttonUploadResources";
//			this.buttonUploadResources.Size = new System.Drawing.Size(150, 120);
//			this.buttonUploadResources.TabIndex = 3;
//			this.buttonUploadResources.Text = "Выгрузить ресурсы для текущего шаблона";
//			this.buttonUploadResources.UseVisualStyleBackColor = false;
//			this.buttonUploadResources.Visible = false;
//			this.buttonUploadResources.Click += new System.EventHandler(this.ButtonUploadResources_Click);
//			// 
//			// buttonGenerateException
//			// 
//			this.buttonGenerateException.BackColor = System.Drawing.Color.Transparent;
//			this.buttonGenerateException.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//			this.buttonGenerateException.Location = new System.Drawing.Point(571, 129);
//			this.buttonGenerateException.Name = "buttonGenerateException";
//			this.buttonGenerateException.Size = new System.Drawing.Size(150, 120);
//			this.buttonGenerateException.TabIndex = 21;
//			this.buttonGenerateException.Text = "Генерировать тестовую ошибку";
//			this.buttonGenerateException.UseVisualStyleBackColor = false;
//			this.buttonGenerateException.Click += new System.EventHandler(this.ButtonGenerateException_Click);
//			// 
//			// flowLayoutPanel2
//			// 
//			this.flowLayoutPanel2.Controls.Add(this.buttonPrintOptions);
//			this.flowLayoutPanel2.Controls.Add(this.buttonPrintReset);
//			this.flowLayoutPanel2.Controls.Add(this.buttonPrintCancelAll);
//			this.flowLayoutPanel2.Controls.Add(this.buttonPrintCalibrate);
//			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
//			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
//			this.flowLayoutPanel2.Location = new System.Drawing.Point(581, 0);
//			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
//			this.flowLayoutPanel2.Size = new System.Drawing.Size(143, 431);
//			this.flowLayoutPanel2.TabIndex = 4;
//			// 
//			// buttonPrintOptions
//			// 
//			this.buttonPrintOptions.BackColor = System.Drawing.Color.Transparent;
//			this.buttonPrintOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//			this.buttonPrintOptions.Location = new System.Drawing.Point(3, 308);
//			this.buttonPrintOptions.Name = "buttonPrintOptions";
//			this.buttonPrintOptions.Size = new System.Drawing.Size(138, 120);
//			this.buttonPrintOptions.TabIndex = 6;
//			this.buttonPrintOptions.Text = "ZEBRA Print Configuration Label (~WC)";
//			this.buttonPrintOptions.UseVisualStyleBackColor = false;
//			this.buttonPrintOptions.Click += new System.EventHandler(this.ButtonPrintOptions_Click);
//			// 
//			// buttonPrintReset
//			// 
//			this.buttonPrintReset.BackColor = System.Drawing.Color.Transparent;
//			this.buttonPrintReset.Dock = System.Windows.Forms.DockStyle.Fill;
//			this.buttonPrintReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//			this.buttonPrintReset.Location = new System.Drawing.Point(3, 182);
//			this.buttonPrintReset.Name = "buttonPrintReset";
//			this.buttonPrintReset.Size = new System.Drawing.Size(138, 120);
//			this.buttonPrintReset.TabIndex = 11;
//			this.buttonPrintReset.Text = "ZEBRA Power On Reset (~JR)";
//			this.buttonPrintReset.UseVisualStyleBackColor = false;
//			this.buttonPrintReset.Click += new System.EventHandler(this.ButtonPrint_Click);
//			// 
//			// buttonPrintCancelAll
//			// 
//			this.buttonPrintCancelAll.BackColor = System.Drawing.Color.Transparent;
//			this.buttonPrintCancelAll.Dock = System.Windows.Forms.DockStyle.Fill;
//			this.buttonPrintCancelAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//			this.buttonPrintCancelAll.Location = new System.Drawing.Point(3, 56);
//			this.buttonPrintCancelAll.Name = "buttonPrintCancelAll";
//			this.buttonPrintCancelAll.Size = new System.Drawing.Size(138, 120);
//			this.buttonPrintCancelAll.TabIndex = 12;
//			this.buttonPrintCancelAll.Text = "ZEBRA Cancel All (~JA)";
//			this.buttonPrintCancelAll.UseVisualStyleBackColor = false;
//			this.buttonPrintCancelAll.Click += new System.EventHandler(this.ButtonPrintCancelAll_Click);
//			// 
//			// buttonPrintCalibrate
//			// 
//			this.buttonPrintCalibrate.BackColor = System.Drawing.Color.Transparent;
//			this.buttonPrintCalibrate.Dock = System.Windows.Forms.DockStyle.Fill;
//			this.buttonPrintCalibrate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
//			this.buttonPrintCalibrate.Location = new System.Drawing.Point(147, 308);
//			this.buttonPrintCalibrate.Name = "buttonPrintCalibrate";
//			this.buttonPrintCalibrate.Size = new System.Drawing.Size(0, 120);
//			this.buttonPrintCalibrate.TabIndex = 13;
//			this.buttonPrintCalibrate.Text = "Калибровка";
//			this.buttonPrintCalibrate.UseVisualStyleBackColor = false;
//			this.buttonPrintCalibrate.Click += new System.EventHandler(this.ButtonPrintCalibrate_Click);
//			// 
//			// SettingsForm
//			// 
//			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
//			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//			this.BackColor = System.Drawing.SystemColors.AppWorkspace;
//			this.ClientSize = new System.Drawing.Size(724, 561);
//			this.Controls.Add(this.flowLayoutPanel2);
//			this.Controls.Add(this.tableLayoutPanel3);
//			this.Controls.Add(this.flowLayoutPanel1);
//			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
//			this.MaximizeBox = false;
//			this.MinimizeBox = false;
//			this.MinimumSize = new System.Drawing.Size(740, 500);
//			this.Name = "SettingsForm";
//			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
//			this.Text = "Настройки";
//			this.Load += new System.EventHandler(this.ScaleOptionsForm_Load);
//			this.tableLayoutPanel3.ResumeLayout(false);
//			this.tableLayoutPanel3.PerformLayout();
//			this.flowLayoutPanel1.ResumeLayout(false);
//			this.flowLayoutPanel2.ResumeLayout(false);
//			this.ResumeLayout(false);

//        }

//        #endregion
//        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
//        private System.Windows.Forms.TextBox fieldCurrentWeightFact;
//        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
//        private System.Windows.Forms.Button buttonClose;
//        private System.Windows.Forms.Button buttonSaveOption;
//        private System.Windows.Forms.Button buttonUploadResources;
//        private System.Windows.Forms.Button buttonMassaParam;
//        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
//        private System.Windows.Forms.Button buttonPrintOptions;
//        private System.Windows.Forms.Button buttonPrintReset;
//        private System.Windows.Forms.Button buttonPrintCancelAll;
//        private System.Windows.Forms.Button buttonPrintCalibrate;
//        private System.Windows.Forms.TextBox fieldCurrentMKProp;
//        private System.Windows.Forms.Button buttonGenerateException;
//    }
//}