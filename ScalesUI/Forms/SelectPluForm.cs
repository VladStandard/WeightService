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
    public partial class SelectPluForm : Form
    {
        #region Private fields and properties

        private DebugHelper Debug { get; } = DebugHelper.Instance;
        private FontsSettingsHelper FontsSettings { get; } = FontsSettingsHelper.Instance;
        private short CurrentPage { get; set; }
        private List<PluDirect> OrderList { get; set; }
        private List<PluDirect> PluList { get; set; }
        private static ushort ColumnCount => 4;
        private static ushort PageSize => 20;
        private static ushort RowCount => 5;
        private UserSessionHelper UserSession { get; } = UserSessionHelper.Instance;
        private decimal ScaleWeight { get; set; } = 0.19M;
        private decimal ScaleHeight => 0.085M;

        #endregion

        #region Constructor and destructor

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
                PluDirect[] pluEntities = PluList.Skip(CurrentPage * PageSize).Take(PageSize).ToArray();
                Control[,] controls = CreateControls(pluEntities);
                GridCustomizatorClass.PageBuilder(tableLayoutPanelPlu, controls);

                labelCurrentPage.Text = $@"{LocaleCore.Scales.PluPage} {CurrentPage}";
                buttonClose.Text = LocaleCore.Buttons.Close;
                buttonLeftRoll.Text = LocaleCore.Buttons.Previous;
                buttonRightRoll.Text = LocaleCore.Buttons.Next;
                TopMost = !Debug.IsDebug;
                Width = Owner.Width;
                Height = Owner.Height;
                Left = Owner.Left;
                Top = Owner.Top;
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private Control[,] CreateControls(IReadOnlyList<PluDirect> pluEntities)
        {
            Control[,] controls = new Control[ColumnCount, RowCount];
            try
            {
                for (ushort rowNumber = 0, buttonNumber = 0; rowNumber < RowCount; ++rowNumber)
                {
                    for (ushort columnNumber = 0; columnNumber < ColumnCount; ++columnNumber)
                    {
                        if (buttonNumber >= pluEntities.Count) break;
                        Control control = NewControl(pluEntities[buttonNumber], (ushort)CurrentPage, buttonNumber);
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

        private Control NewControl(PluDirect plu, ushort pageNumber, ushort buttonNumber)
        {
            Control buttonPlu = null;
            try
            {
                buttonPlu = NewControlButton(plu, pageNumber, buttonNumber);
                Control labelPluNumber = NewControlLabelPluNumber(plu, pageNumber, buttonNumber, buttonPlu);
                _ = NewControlLabelPluType(plu, pageNumber, buttonNumber, buttonPlu, labelPluNumber);
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            return buttonPlu;
        }

        private Control NewControlButton(PluDirect plu, ushort pageNumber, ushort buttonNumber)
        {
            const ushort buttonWidth = 150;
            const ushort buttonHeight = 30;
            Control button = new Button()
            {
                //Font = new Font("Arial", 18, FontStyle.Bold),
                Font = FontsSettings.FontLabelsBlack,
                Text = Environment.NewLine + plu.GoodsName,
                Name = "button" + buttonNumber,
                Dock = DockStyle.Fill,
                Size = new Size(buttonWidth, buttonHeight),
                Visible = true,
                Parent = tableLayoutPanelPlu,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(0, 0),
                UseVisualStyleBackColor = true,
                BackColor = SystemColors.Control,
                TabIndex = buttonNumber + pageNumber * PageSize,
            };
            button.Click += ButtonPlu_Click;
            return button;
        }

        private Control NewControlLabelPluNumber(PluDirect plu, ushort pageNumber, ushort buttonNumber, Control buttonPlu)
        {
            Control label = new Label()
            {
                //Font = new Font("Arial", 20, FontStyle.Bold),
                Font = FontsSettings.FontLabelsBlack,
                Text = plu.PLU.ToString(),
                TextAlign = ContentAlignment.MiddleCenter,
                Parent = buttonPlu,
                Size = new Size((ushort)(tableLayoutPanelPlu.Height * ScaleWeight), (ushort)(tableLayoutPanelPlu.Height * ScaleHeight)),
                Dock = DockStyle.None,
                Left = 3,
                Top = 3,
                BackColor = plu.IsCheckWeight == false
                    ? Color.FromArgb(255, 255, 92, 92)
                    : Color.FromArgb(255, 92, 255, 92),
                BorderStyle = BorderStyle.FixedSingle,
                TabIndex = buttonNumber + pageNumber * PageSize,
            };
            label.MouseClick += ButtonPlu_Click;
            return label;
        }

        private Control NewControlLabelPluType(PluDirect plu, ushort pageNumber, ushort buttonNumber, Control buttonPlu, Control labelPlu)
        {
            ScaleWeight = 0.11M;
            Label labelPluType = new()
            {
                //Font = new Font("Arial", 20, FontStyle.Bold),
                Font = FontsSettings.FontLabelsBlack,
                Text = plu.IsCheckWeight == false ? @"шт" : @"вес",
                TextAlign = ContentAlignment.MiddleCenter,
                Parent = buttonPlu,
                Size = new Size((ushort)(tableLayoutPanelPlu.Height * ScaleWeight), (ushort)(tableLayoutPanelPlu.Height * ScaleHeight)),
                Dock = DockStyle.None,
                Left = labelPlu.Width + 15,
                Top = 3,
                BackColor = plu.IsCheckWeight == false
                    ? Color.FromArgb(255, 255, 92, 92)
                    : Color.FromArgb(255, 92, 255, 92),
                BorderStyle = BorderStyle.FixedSingle,
                TabIndex = buttonNumber + pageNumber * PageSize,
            };
            labelPluType.MouseClick += ButtonPlu_Click;
            return labelPluType;
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
                    UserSession.Plu.LoadTemplate();
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
            try
            {
                tableLayoutPanelPlu.Visible = false;
                short saveCurrentPage = CurrentPage;
                CurrentPage = (short)(CurrentPage > 0 ? CurrentPage - 1 : 0);
                if (CurrentPage < 0) CurrentPage = 0;
                if (CurrentPage == saveCurrentPage)
                    return;

                PluDirect[] pluEntities = PluList.Skip(CurrentPage * PageSize).Take(PageSize).ToArray();
                Control[,] controls = CreateControls(pluEntities);
                GridCustomizatorClass.PageBuilder(tableLayoutPanelPlu, controls);

                labelCurrentPage.Text = $@"{LocaleCore.Scales.PluPage} {CurrentPage}";
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
            try
            {
                tableLayoutPanelPlu.Visible = false;
                short saveCurrentPage = CurrentPage;
                short countPage = (short)(PluList.Count / PageSize);
                CurrentPage = (short)(CurrentPage < countPage ? CurrentPage + 1 : countPage);
                if (CurrentPage > countPage) CurrentPage = (short)(countPage - 1);
                if (CurrentPage == saveCurrentPage)
                    return;

                PluDirect[] pluEntities = PluList.Skip(CurrentPage * PageSize).Take(PageSize).ToArray();
                Control[,] controls = CreateControls(pluEntities);
                GridCustomizatorClass.PageBuilder(tableLayoutPanelPlu, controls);

                labelCurrentPage.Text = $@"{LocaleCore.Scales.PluPage} {CurrentPage}";
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

        #endregion
    }
}
