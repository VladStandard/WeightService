// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesUI.Controls;

public partial class PluUserControl : UserControlBase
{
    #region Public and private fields, properties, constructor

    private long PreviousScaleId { get; set; }

    public PluUserControl()
    {
        InitializeComponent();

        PreviousScaleId = 0;
    }

    #endregion

    #region Public and private methods


    public override void RefreshAction()
    {
        ActionUtils.ActionTryCatch(this, UserSession.Scale, () =>
        {
            if (!UserSession.Scale.IdentityValueId.Equals(PreviousScaleId))
            {
                PreviousScaleId = UserSession.Scale.IdentityValueId;
                UserSession.SetPluScales();
                UserSession.SetPluStorageMethodsFks();
                LoadFormControlsText();
            }
            SetupLayoutPanel();
        });
    }

    private void LoadFormControlsText()
    {
        labelCurrentPage.Text = $@"{LocaleCore.Scales.PluPage} {UserSession.PageNumber}";
        buttonLeftRoll.Text = LocaleCore.Buttons.Previous;
        buttonRightRoll.Text = LocaleCore.Buttons.Next;
    }

    private ControlPluModel[,] CreateControls()
    {
        List<PluScaleModel> plus = UserSession.GetCurrentPlus();
        ControlPluModel[,] controls = new ControlPluModel[UserSession.PageColumnCount, UserSession.PageRowCount];
        ActionUtils.ActionTryCatch(this, UserSession.Scale, () =>
        {
            for (ushort rowNumber = 0, buttonNumber = 0; rowNumber < UserSession.PageRowCount; ++rowNumber)
            {
                for (ushort columnNumber = 0; columnNumber < UserSession.PageColumnCount; ++columnNumber)
                {
                    if (buttonNumber >= plus.Count) break;
                    ControlPluModel control = NewControlGroup(plus[buttonNumber], UserSession.PageNumber, buttonNumber);
                    controls[columnNumber, rowNumber] = control;
                    buttonNumber++;
                }
            }
        });
        return controls;
    }

    private ControlPluModel NewControlGroup(PluScaleModel pluScale, int pageNumber, ushort buttonNumber)
    {
        int tabIndex = buttonNumber + pageNumber * UserSession.PageSize;
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
            Parent = layoutPanel,
            FlatStyle = FlatStyle.Flat,
            Location = new(0, 0),
            UseVisualStyleBackColor = true,
            BackColor = System.Drawing.SystemColors.Control,
            TabIndex = tabIndex,
        };
        buttonPlu.Click += ButtonPluSelect_Click;
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
        labelPluNumber.MouseClick += ButtonPluSelect_Click;
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
        labelPluNumber.MouseClick += ButtonPluSelect_Click;
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
        labelPluType.MouseClick += ButtonPluSelect_Click;
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
        labelPluCode.MouseClick += ButtonPluSelect_Click;
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
        TemplateModel template = UserSession.ContextManager.ContextItem.GetItemTemplateNotNullable(pluScale);
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
        labelPluTemplate.MouseClick += ButtonPluSelect_Click;
        return labelPluTemplate;
    }

    private void ButtonPluSelect_Click(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatch(this, UserSession.Scale, () =>
        {
            ushort tabIndex = 0;
            if (sender is Control control)
                tabIndex = (ushort)control.TabIndex;
            if (UserSession.PluScales.Count >= tabIndex)
            {
                UserSession.PluScale = UserSession.PluScales[tabIndex];
                Result = DialogResult.OK;
            }
            ReturnBackAction();
        });
    }

    private void ButtonPreviousRoll_Click(object sender, EventArgs e)
    {
        int saveCurrentPage = UserSession.PageNumber;
        UserSession.PageNumber = UserSession.PageNumber > 0 ? UserSession.PageNumber - 1 : 0;
        if (UserSession.PageNumber == saveCurrentPage)
            return;

        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, SetupLayoutPanel, () => { layoutPanel.Visible = true; }); 
    }

    private void ButtonNextRoll_Click(object sender, EventArgs e)
    {
        int saveCurrentPage = UserSession.PageNumber;
        int countPage = UserSession.PluScales.Count / UserSession.PageSize;
        UserSession.PageNumber = UserSession.PageNumber < countPage ? UserSession.PageNumber + 1 : countPage;
        if (UserSession.PageNumber > countPage)
            UserSession.PageNumber = countPage - 1;
        if (UserSession.PageNumber == saveCurrentPage)
            return;

        ActionUtils.ActionTryCatchFinally(this, UserSession.Scale, SetupLayoutPanel, () => { layoutPanel.Visible = true; });
    }

    private void SetupPanel(ushort columnCount, ushort rowCount)
    {
        layoutPanel.ColumnStyles.Clear();
        layoutPanel.RowStyles.Clear();
        layoutPanel.ColumnCount = 0;
        layoutPanel.RowCount = 0;
        AddColumns(layoutPanel, columnCount);
        AddRows(layoutPanel, rowCount);
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
        //ushort heightPanelActions = 0;
        ushort height = (ushort)(100 / panel.RowCount);
        for (ushort i = 0; i < panel.RowCount; i++)
        {
            panel.RowStyles.Add(new(SizeType.Percent, height));
        }
    }

    private void ClearPanel()
    {
        layoutPanel.Visible = false;
        foreach (object control in layoutPanel.Controls)
        {
            if (control is TableLayoutPanel subPanel)
            {
                layoutPanelActions = subPanel;
                layoutPanelActions.Parent = null;
            }
        }
        layoutPanel.Controls.Clear();
    }

    private void SetupLayoutPanel()
    {
        layoutPanel.Visible = false;
        labelCurrentPage.Text = $@"{LocaleCore.Scales.PluPage} {UserSession.PageNumber}";

        ControlPluModel[,] controls = CreateControls();

        ClearPanel();
        ushort columnCount = (ushort)(controls.GetUpperBound(0) + 1);
        ushort rowCount = (ushort)(controls.GetUpperBound(1) + 1);
        SetupPanel(columnCount, rowCount);

        for (ushort column = 0; column < columnCount; column++)
        {
            for (ushort row = 0; row < rowCount; row++)
            {
                ControlPluModel control = controls[column, row];
                if (control is not null)
                {
                    layoutPanel.Controls.Add(control.ButtonPlu, column, row);
                }
            }
        }

        if (layoutPanelActions is not null)
        {
            AddRows(layoutPanel, 1);
            layoutPanelActions.Parent = layoutPanel;
            layoutPanel.SetColumn(layoutPanelActions, 0);
            layoutPanel.SetRow(layoutPanelActions, layoutPanel.RowCount - 1);
            layoutPanel.SetColumnSpan(layoutPanelActions, layoutPanel.ColumnCount);
            layoutPanelActions.Dock = DockStyle.Fill;
        }

        layoutPanel.Visible = true;

        SetupSizes(controls);
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