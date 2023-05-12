// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesUI.Controls;

public sealed partial class PluUserControl : UserControlBase
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// ID последней линии (для производительности).
    /// </summary>
    private long LastScaleId { get; set; }

    public PluUserControl()
    {
        InitializeComponent();

        LastScaleId = 0;
    }

    #endregion

    #region Public and private methods


    public override void RefreshAction()
    {
        WsActionUtils.ActionTryCatch(this, UserSession.Scale, () =>
        {
            if (!UserSession.Scale.IdentityValueId.Equals(LastScaleId))
            {
                LastScaleId = UserSession.Scale.IdentityValueId;
                //UserSession.ContextCache.Load(WsSqlTableName.ViewPlusScales);
                UserSession.ContextCache.Load(WsSqlTableName.ViewPluStorageMethods);
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

    private WsControlPluModel[,] CreateControls()
    {
        List<WsSqlViewPluScaleModel> plus = UserSession.GetCurrentPlus();
        WsControlPluModel[,] controls = new WsControlPluModel[UserSession.PageColumnCount, UserSession.PageRowCount];
        WsActionUtils.ActionTryCatch(this, UserSession.Scale, () =>
        {
            for (ushort rowNumber = 0, buttonNumber = 0; rowNumber < UserSession.PageRowCount; ++rowNumber)
            {
                for (ushort columnNumber = 0; columnNumber < UserSession.PageColumnCount; ++columnNumber)
                {
                    if (buttonNumber >= plus.Count) break;
                    WsControlPluModel control = NewControlGroup(plus[buttonNumber], UserSession.PageNumber, buttonNumber);
                    controls[columnNumber, rowNumber] = control;
                    buttonNumber++;
                }
            }
        });
        return controls;
    }

    private WsControlPluModel NewControlGroup(WsSqlViewPluScaleModel viewPluScale, int pageNumber, ushort buttonNumber)
    {
        int tabIndex = buttonNumber + pageNumber * UserSession.PageSize;
        Button buttonPlu = NewButtonPlu(viewPluScale, tabIndex);
        Label labelPluNumber = NewLabelPluNumber(viewPluScale, tabIndex, buttonPlu);
        Label labelPluType = NewLabelPluType(viewPluScale, tabIndex, buttonPlu);
        Label labelPluCode = NewLabelPluCode(viewPluScale, tabIndex, buttonPlu);
        Label labelPluValidate = NewPluValidLabel(viewPluScale, tabIndex, buttonPlu);
        Label labelPluTemplate = NewLabelPluTemplate(viewPluScale, tabIndex, buttonPlu);

        buttonPlu.Click += ActionPluSelect_Click;
        labelPluNumber.MouseClick += ActionPluSelect_Click;
        labelPluType.MouseClick += ActionPluSelect_Click;
        labelPluCode.MouseClick += ActionPluSelect_Click;
        labelPluValidate.MouseClick += ActionPluSelect_Click;
        labelPluTemplate.MouseClick += ActionPluSelect_Click;

        return new(buttonPlu, labelPluNumber, labelPluType, labelPluCode, labelPluTemplate, labelPluValidate);
    }

    private Button NewButtonPlu(WsSqlViewPluScaleModel viewPluScale, int tabIndex) =>
        new()
        {
            Name = $@"buttonPlu{tabIndex}",
            Font = FontsSettings.FontLabelsBlack,
            AutoSize = false,
            Text = viewPluScale.PluName,
            Dock = DockStyle.Fill,
            Size = new(150, 30),
            Visible = true,
            Parent = layoutPanel,
            FlatStyle = FlatStyle.Flat,
            Location = new(0, 0),
            UseVisualStyleBackColor = true,
            BackColor = System.Drawing.SystemColors.Control,
            TabIndex = tabIndex,
        };

    private Label NewLabelPluNumber(WsSqlViewPluScaleModel viewPluScale, int tabIndex, Control buttonPlu) =>
        new()
        {
            Name = $@"labelPluNumber{tabIndex}",
            Font = FontsSettings.FontMinimum,
            AutoSize = false,
            Text = Text = Width > 1024 ? $@"{LocaleCore.Table.Number} {viewPluScale.PluNumber}" : $@"{viewPluScale.PluNumber}",
            TextAlign = ContentAlignment.MiddleCenter,
            Parent = buttonPlu,
            Dock = DockStyle.None,
            BackColor = Color.Transparent,
            BorderStyle = BorderStyle.FixedSingle,
            TabIndex = tabIndex,
        };

    private Label NewPluValidLabel(WsSqlViewPluScaleModel viewPluScale, int tabIndex, Control buttonPlu)
    {
        bool valid = WsSqlPluController.Instance.IsFullValid(viewPluScale);
        return new()
        {
            Name = $@"labelPluNumber{tabIndex}",
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
    }

    private Label NewLabelPluType(WsSqlViewPluScaleModel viewPluScale, int tabIndex, Control buttonPlu) =>
        new()
        {
            Name = $@"labelPluType{tabIndex}",
            Font = FontsSettings.FontMinimum,
            AutoSize = false,
            Text = viewPluScale.PluIsWeight == false ? LocaleCore.Scales.PluIsPiece : LocaleCore.Scales.PluIsWeight,
            TextAlign = ContentAlignment.MiddleCenter,
            Parent = buttonPlu,
            Dock = DockStyle.None,
            BackColor = viewPluScale.PluIsWeight ? Color.LightGray : Color.Transparent,
            BorderStyle = BorderStyle.FixedSingle,
            TabIndex = tabIndex,
        };

    private Label NewLabelPluCode(WsSqlViewPluScaleModel viewPluScale, int tabIndex, Control buttonPlu) =>
        new()
        {
            Name = $@"labelPluCode{tabIndex}",
            Font = FontsSettings.FontMinimum,
            AutoSize = false,
            Text = Width > 1024
                ? !string.IsNullOrEmpty(viewPluScale.PluGtin) ? @$"{LocaleCore.Scales.PluCode} {viewPluScale.PluGtin}" : LocaleCore.Scales.PluCodeNotSet
                : !string.IsNullOrEmpty(viewPluScale.PluGtin) ? @$"{viewPluScale.PluGtin}" : LocaleCore.Scales.PluCodeNotSet,
            TextAlign = ContentAlignment.MiddleCenter,
            Parent = buttonPlu,
            Dock = DockStyle.None,
            BackColor = !string.IsNullOrEmpty(viewPluScale.PluGtin) ? Color.Transparent : Color.LightGray,
            BorderStyle = BorderStyle.FixedSingle,
            TabIndex = tabIndex,
        };

    private Label NewLabelPluTemplate(WsSqlViewPluScaleModel viewPluScale, int tabIndex, Control buttonPlu) =>
        new()
        {
            Name = $@"labelPluTemplate{tabIndex}",
            Font = FontsSettings.FontMinimum,
            AutoSize = false,
            Text = !string.IsNullOrEmpty(viewPluScale.TemplateName) ? LocaleCore.Scales.PluTemplateSet : LocaleCore.Scales.PluTemplateNotSet,
            TextAlign = ContentAlignment.MiddleCenter,
            Parent = buttonPlu,
            Dock = DockStyle.None,
            BackColor = !string.IsNullOrEmpty(viewPluScale.TemplateName) ? Color.Transparent : Color.LightGray,
            Visible = true,
            BorderStyle = BorderStyle.FixedSingle,
            TabIndex = tabIndex,
        };

    /// <summary>
    /// Выбор ПЛУ.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ActionPluSelect_Click(object sender, EventArgs e)
    {
        WsActionUtils.ActionTryCatch(this, UserSession.Scale, () =>
        {
            ushort tabIndex = 0;
            if (sender is Control control)
                tabIndex = (ushort)control.TabIndex;
            if (UserSession.ContextCache.ViewPlusScalesDb.Count >= tabIndex)
            {
                UserSession.PluScale = UserSession.ContextManager.ContextPluScale.GetItem(
                    UserSession.ContextCache.ViewPlusScalesDb[tabIndex].PluNumber);
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

        WsActionUtils.ActionTryCatchFinally(this, UserSession.Scale, SetupLayoutPanel, () => { layoutPanel.Visible = true; }); 
    }

    private void ButtonNextRoll_Click(object sender, EventArgs e)
    {
        int saveCurrentPage = UserSession.PageNumber;
        int countPage = UserSession.ContextCache.ViewPlusScalesDb.Count / UserSession.PageSize;
        UserSession.PageNumber = UserSession.PageNumber < countPage ? UserSession.PageNumber + 1 : countPage;
        if (UserSession.PageNumber > countPage)
            UserSession.PageNumber = countPage - 1;
        if (UserSession.PageNumber == saveCurrentPage)
            return;

        WsActionUtils.ActionTryCatchFinally(this, UserSession.Scale, SetupLayoutPanel, () => { layoutPanel.Visible = true; });
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

    /// <summary>
    /// Выровнять панели.
    /// </summary>
    private void SetupLayoutPanel()
    {
        layoutPanel.Visible = false;
        labelCurrentPage.Text = $@"{LocaleCore.Scales.PluPage} {UserSession.PageNumber}";

        WsControlPluModel[,] controls = CreateControls();

        ClearPanel();
        ushort columnCount = (ushort)(controls.GetUpperBound(0) + 1);
        ushort rowCount = (ushort)(controls.GetUpperBound(1) + 1);
        SetupPanel(columnCount, rowCount);

        for (ushort column = 0; column < columnCount; column++)
        {
            for (ushort row = 0; row < rowCount; row++)
            {
                WsControlPluModel control = controls[column, row];
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

    private void SetupSizes(WsControlPluModel[,] controls)
    {
        foreach (WsControlPluModel control in controls)
        {
            control?.SetupSizes();
        }
    }

    #endregion
}