// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Sql.TableDirectModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeightCore.Gui;
using WeightCore.Helpers;

namespace ScalesUI.Forms
{
    /// <summary>
    /// Select PLU form.
    /// </summary>
    public partial class SelectPluForm : Form
    {
        #region Private fields and properties

        private const ushort ColumnCount = 4;
        private const ushort PageSize = 20;
        private const ushort RowCount = 5;
        private DebugHelper Debug { get; } = DebugHelper.Instance;
        private FontsSettingsHelper FontsSettings { get; } = FontsSettingsHelper.Instance;
        private List<PluDirect> OrderList { get; set; }
        private List<PluDirect> PluList { get; set; }
        private int PageNumber { get; set; }
        private UserSessionHelper UserSession { get; } = UserSessionHelper.Instance;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SelectPluForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Public and private methods

        private void PluListForm_Load(object sender, EventArgs e)
        {
            try
            {
                PluList = new PluDirect().GetPluList(UserSession.Scale);
                OrderList = new PluDirect().GetPluList(UserSession.Scale);
                LoadFormControls();

                ControlPluEntity[,] controls = CreateControls();
                Setup(tableLayoutPanelPlu, controls);
                SetupSizes(controls);
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void LoadFormControls()
        {
            labelCurrentPage.Text = $@"{LocaleCore.Scales.PluPage} {PageNumber}";
            buttonClose.Text = LocaleCore.Buttons.Close;
            buttonLeftRoll.Text = LocaleCore.Buttons.Previous;
            buttonRightRoll.Text = LocaleCore.Buttons.Next;

            TopMost = !Debug.IsDebug;
            Width = Owner.Width;
            Height = Owner.Height;
            Left = Owner.Left;
            Top = Owner.Top;
        }

        private ControlPluEntity[,] CreateControls()
        {
            List<PluDirect> plus = GetCurrentPluList();
            ControlPluEntity[,] controls = new ControlPluEntity[ColumnCount, RowCount];
            try
            {
                for (ushort rowNumber = 0, buttonNumber = 0; rowNumber < RowCount; ++rowNumber)
                {
                    for (ushort columnNumber = 0; columnNumber < ColumnCount; ++columnNumber)
                    {
                        if (buttonNumber >= plus.Count) break;
                        ControlPluEntity control = NewControlGroup(plus[buttonNumber], PageNumber, buttonNumber);
                        controls[columnNumber, rowNumber] = control;
                        buttonNumber++;
                    }
                }
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            return controls;
        }

        private List<PluDirect> GetCurrentPluList()
        {
            IEnumerable<PluDirect> plusSkip = PluList.Skip(PageNumber * PageSize);
            IEnumerable<PluDirect> plusTake = plusSkip.Take(PageSize);
            return plusTake.ToList();
        }

        private ControlPluEntity NewControlGroup(PluDirect plu, int pageNumber, ushort buttonNumber)
        {
            int tabIndex = buttonNumber + pageNumber * PageSize;
            Button buttonPlu = NewButtonPlu(plu, tabIndex);
            Label labelPluNumber = NewLabelPluNumber(plu, tabIndex, buttonPlu);
            Label labelPluType = NewLabelPluType(plu, tabIndex, buttonPlu);
            Label labelPluCode = NewLabelPluCode(plu, tabIndex, buttonPlu);
            //Label labelPluDescription = NewLabelPluDescription(plu, tabIndex, buttonPlu);
            Label labelTemplate = NewLabelPluTemplate(plu, tabIndex, buttonPlu);
            return new(buttonPlu, labelPluNumber, labelPluType, labelPluCode, labelTemplate);
        }

        private Button NewButtonPlu(PluDirect plu, int tabIndex)
        {
            const ushort buttonWidth = 150;
            const ushort buttonHeight = 30;
            Button buttonPlu = new()
            {
                Name = $@"{nameof(buttonPlu)}{tabIndex}",
                Font = FontsSettings.FontLabelsBlack,
                AutoSize = false,
                Text = plu.GoodsName,
                Dock = DockStyle.Fill,
                Size = new(buttonWidth, buttonHeight),
                Visible = true,
                Parent = tableLayoutPanelPlu,
                FlatStyle = FlatStyle.Flat,
                Location = new(0, 0),
                UseVisualStyleBackColor = true,
                BackColor = SystemColors.Control,
                TabIndex = tabIndex,
            };
            buttonPlu.Click += ButtonPlu_Click;
            return buttonPlu;
        }

        private Label NewLabelPluNumber(PluDirect plu, int tabIndex, Control buttonPlu)
        {
            Label labelPluNumber = new()
            {
                Name = $@"{nameof(labelPluNumber)}{tabIndex}",
                Font = FontsSettings.FontMinimum,
                AutoSize = false,
                Text = Text = Width > 1024 ? $@"{LocaleCore.Table.Number} {plu.PLU}" : $@"{plu.PLU}",
                TextAlign = ContentAlignment.MiddleCenter,
                Parent = buttonPlu,
                Dock = DockStyle.None,
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.FixedSingle,
                TabIndex = tabIndex,
            };
            labelPluNumber.MouseClick += ButtonPlu_Click;
            return labelPluNumber;
        }

        private Label NewLabelPluType(PluDirect plu, int tabIndex, Control buttonPlu)
        {
            Label labelPluType = new()
            {
                Name = $@"{nameof(labelPluType)}{tabIndex}",
                Font = FontsSettings.FontMinimum,
                AutoSize = false,
                Text = plu.IsCheckWeight == false ? LocaleCore.Scales.PluIsPiece : LocaleCore.Scales.PluIsWeight,
                TextAlign = ContentAlignment.MiddleCenter,
                Parent = buttonPlu,
                Dock = DockStyle.None,
                BackColor = plu.IsCheckWeight ? Color.LightGray : Color.Transparent,
                BorderStyle = BorderStyle.FixedSingle,
                TabIndex = tabIndex,
            };
            labelPluType.MouseClick += ButtonPlu_Click;
            return labelPluType;
        }

        private Label NewLabelPluCode(PluDirect plu, int tabIndex, Control buttonPlu)
        {
            Label labelPluCode = new()
            {
                Name = $@"{nameof(labelPluCode)}{tabIndex}",
                Font = FontsSettings.FontMinimum,
                AutoSize = false,
                Text = Width > 1024 
                    ? !string.IsNullOrEmpty(plu.GTIN) ? @$"{LocaleCore.Scales.PluCode} {plu.GTIN}" : LocaleCore.Scales.PluCodeNotSet
                    : !string.IsNullOrEmpty(plu.GTIN) ? @$"{plu.GTIN}" : LocaleCore.Scales.PluCodeNotSet,
                TextAlign = ContentAlignment.MiddleCenter,
                Parent = buttonPlu,
                Dock = DockStyle.None,
                BackColor = !string.IsNullOrEmpty(plu.GTIN) ? Color.Transparent : Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle,
                TabIndex = tabIndex,
            };
            labelPluCode.MouseClick += ButtonPlu_Click;
            return labelPluCode;
        }

        private Label NewLabelPluDescription(PluDirect plu, int tabIndex, Control buttonPlu)
        {
            Label labelPluDescription = new()
            {
                Name = $@"{nameof(labelPluDescription)}{tabIndex}",
                Font = FontsSettings.FontMinimum,
                AutoSize = false,
                Text = !string.IsNullOrEmpty(plu.GoodsDescription) ? LocaleCore.Scales.PluDescriptionSet : LocaleCore.Scales.PluDescriptionNotSet,
                TextAlign = ContentAlignment.MiddleCenter,
                Parent = buttonPlu,
                Dock = DockStyle.None,
                BackColor = !string.IsNullOrEmpty(plu.GoodsDescription) ? Color.Transparent : Color.LightGray,
                Visible = string.IsNullOrEmpty(plu.GoodsDescription),
                BorderStyle = BorderStyle.FixedSingle,
                TabIndex = tabIndex,
            };
            labelPluDescription.MouseClick += ButtonPlu_Click;
            return labelPluDescription;
        }

        private Label NewLabelPluTemplate(PluDirect plu, int tabIndex, Control buttonPlu)
        {
            TemplateDirect template = plu.LoadTemplate();
            Label labelPluTemplate = new()
            {
                Name = $@"{nameof(labelPluTemplate)}{tabIndex}",
                Font = FontsSettings.FontMinimum,
                AutoSize = false,
                Text = !string.IsNullOrEmpty(template.Title) ? LocaleCore.Scales.PluTemplateSet : LocaleCore.Scales.PluTemplateNotSet,
                TextAlign = ContentAlignment.MiddleCenter,
                Parent = buttonPlu,
                Dock = DockStyle.None,
                BackColor = !string.IsNullOrEmpty(template.Title) ? Color.Transparent : Color.LightGray,
                Visible = true,
                BorderStyle = BorderStyle.FixedSingle,
                TabIndex = tabIndex,
            };
            labelPluTemplate.MouseClick += ButtonPlu_Click;
            return labelPluTemplate;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void ButtonPlu_Click(object sender, EventArgs e)
        {
            try
            {
                UserSession.Order = null;
                ushort tabIndex = 0;
                if (sender is Control control)
                    tabIndex = (ushort)control.TabIndex;
                if (OrderList?.Count >= tabIndex)
                {
                    UserSession.SetCurrentPlu(OrderList[tabIndex]);
                    //UserSession.Plu.LoadTemplate();
                    DialogResult = DialogResult.OK;
                }
                Close();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void ButtonPreviousRoll_Click(object sender, EventArgs e)
        {
            int saveCurrentPage = PageNumber;
            PageNumber = PageNumber > 0 ? PageNumber - 1 : 0;
            if (PageNumber == saveCurrentPage)
                return;
            try
            {
                tableLayoutPanelPlu.Visible = false;
                labelCurrentPage.Text = $@"{LocaleCore.Scales.PluPage} {PageNumber}";
                ControlPluEntity[,] controls = CreateControls();
                Setup(tableLayoutPanelPlu, controls);
                SetupSizes(controls);
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            finally
            {
                tableLayoutPanelPlu.Visible = true;
            }
        }

        private void ButtonNextRoll_Click(object sender, EventArgs e)
        {
            int saveCurrentPage = PageNumber;
            int countPage = PluList.Count / PageSize;
            PageNumber = PageNumber < countPage ? PageNumber + 1 : countPage;
            if (PageNumber > countPage)
                PageNumber = countPage - 1;
            if (PageNumber == saveCurrentPage)
                return;
            
            try
            {
                tableLayoutPanelPlu.Visible = false;
                labelCurrentPage.Text = $@"{LocaleCore.Scales.PluPage} {PageNumber}";
                ControlPluEntity[,] controls = CreateControls();
                Setup(tableLayoutPanelPlu, controls);
                SetupSizes(controls);
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            finally
            {
                tableLayoutPanelPlu.Visible = true;
            }
        }

        private void SetupPanel(TableLayoutPanel panel, ushort columnCount, ushort rowCount)
        {
            panel.ColumnStyles.Clear();
            panel.RowStyles.Clear();
            panel.ColumnCount = 0;
            panel.RowCount = 0;
            AddColumns(panel, columnCount);
            AddRows(panel, rowCount);
        }

        private void AddColumns(TableLayoutPanel panel, ushort columnCount)
        {
            panel.ColumnStyles.Clear();
            panel.ColumnCount += columnCount;
            ushort width = (ushort)(100 / columnCount);
            for (ushort i = 0; i < panel.ColumnCount; i++)
            {
                panel.ColumnStyles.Add(new(SizeType.Percent, width));
            }
        }

        private void AddRows(TableLayoutPanel panel, ushort rowCount)
        {
            panel.RowStyles.Clear();
            panel.RowCount += rowCount;
            ushort height = (ushort)(100 / panel.RowCount);
            for (ushort i = 0; i < panel.RowCount; i++)
            {
                panel.RowStyles.Add(new(SizeType.Percent, height));
            }
        }

        private void ClearPanel(TableLayoutPanel panel)
        {
            foreach (object control in panel.Controls)
            {
                if (control is TableLayoutPanel subPanel)
                {
                    tableLayoutPanelActions = subPanel;
                    tableLayoutPanelActions.Parent = null;
                }
            }
            panel.Controls.Clear();
        }

        private void Setup(TableLayoutPanel panelPlu, ControlPluEntity[,] controls)
        {
            panelPlu.Visible = false;
            ClearPanel(panelPlu);
            SetupPanel(panelPlu, (ushort)(controls.GetUpperBound(0) + 1), (ushort)(controls.GetUpperBound(1) + 1));

            for (ushort column = 0; column <= controls.GetUpperBound(0); column++)
            {
                for (ushort row = 0; row <= controls.GetUpperBound(1); row++)
                {
                    ControlPluEntity control = controls[column, row];
                    if (control != null)
                    {
                        panelPlu.Controls.Add(control.ButtonPlu, column, row);
                    }
                }
            }

            if (tableLayoutPanelActions != null)
            {
                AddRows(panelPlu, 1);
                tableLayoutPanelActions.Parent = panelPlu;
                panelPlu.SetColumn(tableLayoutPanelActions, 0);
                panelPlu.SetRow(tableLayoutPanelActions, panelPlu.RowCount - 1);
                panelPlu.SetColumnSpan(tableLayoutPanelActions, panelPlu.ColumnCount);
                tableLayoutPanelActions.Dock = DockStyle.Fill;
            }

            panelPlu.Refresh();
            panelPlu.Visible = true;
        }

        private void SetupSizes(ControlPluEntity[,] controls)
        {
            foreach (ControlPluEntity control in controls)
            {
                control?.SetupSizes();
            }
        }

        #endregion
    }
}
