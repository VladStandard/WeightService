// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
namespace ScalesUI.Forms;

/// <summary>
/// Select PLU form.
/// </summary>
public partial class PlusForm : Form
{
    #region Private fields and properties

    private const ushort ColumnCount = 4;
    private const ushort PageSize = 20;
    private const ushort RowCount = 5;
    private DebugHelper Debug => DebugHelper.Instance;
    private FontsSettingsHelper FontsSettings => FontsSettingsHelper.Instance;
    private List<PluScaleModel> PluScales { get; set; }
    private int PageNumber { get; set; }
    private UserSessionHelper UserSession => UserSessionHelper.Instance;

    /// <summary>
    /// Constructor.
    /// </summary>
    public PlusForm()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    private void PluListForm_Load(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatch(this, () =>
        {
            SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(UserSession.Scale, nameof(PluScaleModel.Scale), false, false);
            sqlCrudConfig.AddFilters(new() { new(nameof(PluScaleModel.IsActive), true) });
            //sqlCrudConfig.AddOrders(new(nameof(PluScaleModel.Plu.Number), SqlFieldOrderEnum.Asc));
            sqlCrudConfig.AddOrders(new(nameof(PluScaleModel.Plu), SqlFieldOrderEnum.Asc));
            sqlCrudConfig.IsResultOrder = true;
            PluScales = UserSession.DataContext.GetListNotNullable<PluScaleModel>(sqlCrudConfig);

            LoadFormControls();

            ControlPluModel[,] controls = CreateControls();
            Setup(tableLayoutPanelPlu, controls);
            SetupSizes(controls);
        });
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

    private ControlPluModel[,] CreateControls()
    {
        List<PluScaleModel> plus = GetCurrentPlus();
        ControlPluModel[,] controls = new ControlPluModel[ColumnCount, RowCount];
        try
        {
            for (ushort rowNumber = 0, buttonNumber = 0; rowNumber < RowCount; ++rowNumber)
            {
                for (ushort columnNumber = 0; columnNumber < ColumnCount; ++columnNumber)
                {
                    if (buttonNumber >= plus.Count) break;
                    ControlPluModel control = NewControlGroup(plus[buttonNumber], PageNumber, buttonNumber);
                    controls[columnNumber, rowNumber] = control;
                    buttonNumber++;
                }
            }
        }
        catch (Exception ex)
        {
            WpfUtils.CatchException(ex, this, true, true);
        }
        return controls;
    }

    private List<PluScaleModel> GetCurrentPlus()
    {
        IEnumerable<PluScaleModel> plusSkip = PluScales.Skip(PageNumber * PageSize);
        IEnumerable<PluScaleModel> plusTake = plusSkip.Take(PageSize);
        return plusTake.ToList();
    }

    private ControlPluModel NewControlGroup(PluScaleModel pluScale, int pageNumber, ushort buttonNumber)
    {
        int tabIndex = buttonNumber + pageNumber * PageSize;
        Button buttonPlu = NewButtonPlu(pluScale.Plu, tabIndex);
        Label labelPluNumber = NewLabelPluNumber(pluScale, tabIndex, buttonPlu);
        Label labelPluType = NewLabelPluType(pluScale.Plu, tabIndex, buttonPlu);
        Label labelPluCode = NewLabelPluCode(pluScale.Plu, tabIndex, buttonPlu);
        Label labelPluValidate = NewPluValidLabel(pluScale.Plu, tabIndex, buttonPlu);
        Label labelTemplate = NewLabelPluTemplate(pluScale, tabIndex, buttonPlu);

        return new(buttonPlu, labelPluNumber, labelPluType, labelPluCode, labelTemplate, labelPluValidate);
    }

    private Button NewButtonPlu(PluModel plu, int tabIndex)
    {
        const ushort buttonWidth = 150;
        const ushort buttonHeight = 30;

        Button buttonPlu = new()
        {
            Name = $@"{nameof(buttonPlu)}{tabIndex}",
            Font = FontsSettings.FontLabelsBlack,
            AutoSize = false,
            Text = plu.Name,
            Dock = DockStyle.Fill,
            Size = new(buttonWidth, buttonHeight),
            Visible = true,
            Parent = tableLayoutPanelPlu,
            FlatStyle = FlatStyle.Flat,
            Location = new(0, 0),
            UseVisualStyleBackColor = true,
            BackColor = System.Drawing.SystemColors.Control,
            TabIndex = tabIndex,
        };
        buttonPlu.Click += ButtonPlu_Click;
        return buttonPlu;
    }

    private Label NewLabelPluNumber(PluScaleModel pluScale, int tabIndex, Control buttonPlu)
    {
        Label labelPluNumber = new()
        {
            Name = $@"{nameof(labelPluNumber)}{tabIndex}",
            Font = FontsSettings.FontMinimum,
            AutoSize = false,
            Text = Text = Width > 1024 ? $@"{LocaleCore.Table.Number} {pluScale.Plu.Number}" : $@"{pluScale.Plu.Number}",
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

    private Label NewPluValidLabel(PluModel pluModel, int tabIndex, Control buttonPlu)
    {

        bool valid = PluController.IsFullValid(pluModel);

        Label labelPluNumber = new()
        {
            Name = $@"{nameof(labelPluNumber)}{tabIndex}",
            Font = FontsSettings.FontMinimum,
            AutoSize = false,
            Text = valid == false ? "!" : "OK",
            TextAlign = ContentAlignment.MiddleCenter,
            Parent = buttonPlu,
            Dock = DockStyle.None,
            BackColor = valid == false ? Color.Gold : Color.Transparent,
            BorderStyle = BorderStyle.FixedSingle,
            TabIndex = tabIndex,
        };
        labelPluNumber.MouseClick += ButtonPlu_Click;
        return labelPluNumber;
    }

    private Label NewLabelPluType(PluModel plu, int tabIndex, Control buttonPlu)
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

    private Label NewLabelPluCode(PluModel plu, int tabIndex, Control buttonPlu)
    {
        Label labelPluCode = new()
        {
            Name = $@"{nameof(labelPluCode)}{tabIndex}",
            Font = FontsSettings.FontMinimum,
            AutoSize = false,
            Text = Width > 1024
                ? !string.IsNullOrEmpty(plu.Gtin) ? @$"{LocaleCore.Scales.PluCode} {plu.Gtin}" : LocaleCore.Scales.PluCodeNotSet
                : !string.IsNullOrEmpty(plu.Gtin) ? @$"{plu.Gtin}" : LocaleCore.Scales.PluCodeNotSet,
            TextAlign = ContentAlignment.MiddleCenter,
            Parent = buttonPlu,
            Dock = DockStyle.None,
            BackColor = !string.IsNullOrEmpty(plu.Gtin) ? Color.Transparent : Color.LightGray,
            BorderStyle = BorderStyle.FixedSingle,
            TabIndex = tabIndex,
        };
        labelPluCode.MouseClick += ButtonPlu_Click;
        return labelPluCode;
    }

    //private Label NewLabelPluDescription(PluModel plu, int tabIndex, Control buttonPlu)
    //{
    //	Label labelPluDescription = new()
    //	{
    //		Name = $@"{nameof(labelPluDescription)}{tabIndex}",
    //		Font = FontsSettings.FontMinimum,
    //		AutoSize = false,
    //		Text = !string.IsNullOrEmpty(plu.Description) ? LocaleCore.Scales.PluDescriptionSet : LocaleCore.Scales.PluDescriptionNotSet,
    //		TextAlign = ContentAlignment.MiddleCenter,
    //		Parent = buttonPlu,
    //		Dock = DockStyle.None,
    //		BackColor = !string.IsNullOrEmpty(plu.Description) ? Color.Transparent : Color.LightGray,
    //		Visible = string.IsNullOrEmpty(plu.Description),
    //		BorderStyle = BorderStyle.FixedSingle,
    //		TabIndex = tabIndex,
    //	};
    //	labelPluDescription.MouseClick += ButtonPlu_Click;
    //	return labelPluDescription;
    //}

    private Label NewLabelPluTemplate(PluScaleModel pluScale, int tabIndex, Control buttonPlu)
    {
        //TemplateModel template = UserSession.DataAccess.GetItemNotNullable<TemplateModel>(pluScale.Plu.Template.IdentityValueId);
        TemplateModel template = UserSession.DataAccess.GetItemTemplateNotNullable(pluScale);
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
            WpfUtils.CatchException(ex, this, true, true);
        }
    }

    private void ButtonPlu_Click(object sender, EventArgs e)
    {
        try
        {
            //UserSession.SetCurrentPlu(PluScales[tabIndex]);
            ushort tabIndex = 0;
            if (sender is Control control)
                tabIndex = (ushort)control.TabIndex;
            if (PluScales?.Count >= tabIndex)
            {
                UserSession.PluScale = PluScales[tabIndex];
                //UserSession.Plu.LoadTemplate();
                DialogResult = DialogResult.OK;
            }
            Close();
        }
        catch (Exception ex)
        {
            WpfUtils.CatchException(ex, this, true, true);
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
            ControlPluModel[,] controls = CreateControls();
            Setup(tableLayoutPanelPlu, controls);
            SetupSizes(controls);
        }
        catch (Exception ex)
        {
            WpfUtils.CatchException(ex, this, true, true);
        }
        finally
        {
            tableLayoutPanelPlu.Visible = true;
        }
    }

    private void ButtonNextRoll_Click(object sender, EventArgs e)
    {
        int saveCurrentPage = PageNumber;
        int countPage = PluScales.Count / PageSize;
        PageNumber = PageNumber < countPage ? PageNumber + 1 : countPage;
        if (PageNumber > countPage)
            PageNumber = countPage - 1;
        if (PageNumber == saveCurrentPage)
            return;

        try
        {
            tableLayoutPanelPlu.Visible = false;
            labelCurrentPage.Text = $@"{LocaleCore.Scales.PluPage} {PageNumber}";
            ControlPluModel[,] controls = CreateControls();
            Setup(tableLayoutPanelPlu, controls);
            SetupSizes(controls);
        }
        catch (Exception ex)
        {
            WpfUtils.CatchException(ex, this, true, true);
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

    private void Setup(TableLayoutPanel panelPlu, ControlPluModel[,] controls)
    {
        panelPlu.Visible = false;
        ClearPanel(panelPlu);
        SetupPanel(panelPlu, (ushort)(controls.GetUpperBound(0) + 1), (ushort)(controls.GetUpperBound(1) + 1));

        for (ushort column = 0; column <= controls.GetUpperBound(0); column++)
        {
            for (ushort row = 0; row <= controls.GetUpperBound(1); row++)
            {
                ControlPluModel control = controls[column, row];
                if (control is not null)
                {
                    panelPlu.Controls.Add(control.ButtonPlu, column, row);
                }
            }
        }

        if (tableLayoutPanelActions is not null)
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

    private void SetupSizes(ControlPluModel[,] controls)
    {
        foreach (ControlPluModel control in controls)
        {
            control?.SetupSizes();
        }
    }

    #endregion
}