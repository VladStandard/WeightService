// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
#nullable enable

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
            if (!Equals(UserSession.Scale.IdentityValueId, LastScaleId))
            {
                LastScaleId = UserSession.Scale.IdentityValueId;
                // Обновить кэш.
                UserSession.ContextCache.Load(WsSqlTableName.ViewPlusScales);
                UserSession.ContextCache.Load(WsSqlTableName.ViewPluStorageMethods);
                // Обновить метки.
                labelCurrentPage.Text = $@"{LocaleCore.Scales.PluPage} {UserSession.PageNumber}";
                buttonLeftRoll.Text = LocaleCore.Buttons.Previous;
                buttonRightRoll.Text = LocaleCore.Buttons.Next;
            }
            // Настроить контролы.
            SetupControls();
        });
    }

    private WsControlPluModel?[,] CreateControls()
    {
        List<WsSqlViewPluScaleModel> viewPlusScales = UserSession.GetCurrentViewPlusScales();
        WsControlPluModel?[,] controls = new WsControlPluModel?[0, 0];
        if (!viewPlusScales.Any()) return controls;
        controls = new WsControlPluModel[UserSession.PageColumnCount, UserSession.PageRowCount];
        WsActionUtils.ActionTryCatch(this, UserSession.Scale, () =>
        {
            for (ushort rowNumber = 0, buttonNumber = 0; rowNumber < UserSession.PageRowCount; ++rowNumber)
            {
                for (ushort columnNumber = 0; columnNumber < UserSession.PageColumnCount; ++columnNumber)
                {
                    if (buttonNumber >= viewPlusScales.Count) break;
                    WsControlPluModel control = NewControlGroup(viewPlusScales[buttonNumber], UserSession.PageNumber, buttonNumber);
                    controls[columnNumber, rowNumber] = control;
                    buttonNumber++;
                }
            }
        });
        return controls;
    }

    private WsControlPluModel NewControlGroup(WsSqlViewPluScaleModel viewPluScale, int pageNumber, ushort buttonNumber)
    {
        //int tabIndex = buttonNumber + pageNumber * UserSession.PageSize;
        Button buttonPlu = NewButtonPlu(viewPluScale);
        Label labelPluNumber = NewLabelPluNumber(viewPluScale, buttonPlu);
        Label labelPluType = NewLabelPluType(viewPluScale, buttonPlu);
        Label labelPluCode = NewLabelPluCode(viewPluScale, buttonPlu);
        Label labelPluValidate = NewPluValidLabel(viewPluScale, buttonPlu);
        Label labelPluTemplate = NewLabelPluTemplate(viewPluScale, buttonPlu);

        buttonPlu.Click += ActionPluSelect_Click;
        labelPluNumber.MouseClick += ActionPluSelect_Click;
        labelPluType.MouseClick += ActionPluSelect_Click;
        labelPluCode.MouseClick += ActionPluSelect_Click;
        labelPluValidate.MouseClick += ActionPluSelect_Click;
        labelPluTemplate.MouseClick += ActionPluSelect_Click;

        return new(viewPluScale, buttonPlu, labelPluNumber, labelPluType, labelPluCode, labelPluTemplate, labelPluValidate);
    }

    private Button NewButtonPlu(WsSqlViewPluScaleModel viewPluScale) =>
        new()
        {
            //Name = $@"buttonPlu{tabIndex}",
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
            //TabIndex = tabIndex,
        };

    private Label NewLabelPluNumber(WsSqlViewPluScaleModel viewPluScale, Control buttonPlu) =>
        new()
        {
            //Name = $@"labelPluNumber{tabIndex}",
            Font = FontsSettings.FontMinimum,
            AutoSize = false,
            Text = Text = Width > 1024 ? $@"{LocaleCore.Table.Number} {viewPluScale.PluNumber}" : $@"{viewPluScale.PluNumber}",
            TextAlign = ContentAlignment.MiddleCenter,
            Parent = buttonPlu,
            Dock = DockStyle.None,
            BackColor = Color.Transparent,
            BorderStyle = BorderStyle.FixedSingle,
            //TabIndex = tabIndex,
        };

    private Label NewPluValidLabel(WsSqlViewPluScaleModel viewPluScale, Control buttonPlu)
    {
        bool valid = WsSqlPluController.Instance.IsFullValid(viewPluScale);
        return new()
        {
            //Name = $@"labelPluNumber{tabIndex}",
            Font = FontsSettings.FontMinimum,
            AutoSize = false,
            Text = valid == false ? "!" : "OK",
            TextAlign = ContentAlignment.MiddleCenter,
            Parent = buttonPlu,
            Dock = DockStyle.None,
            BackColor = valid == false ? Color.Gold : Color.Transparent,
            BorderStyle = BorderStyle.FixedSingle,
            //TabIndex = tabIndex,
        };
    }

    private Label NewLabelPluType(WsSqlViewPluScaleModel viewPluScale, Control buttonPlu) =>
        new()
        {
            //Name = $@"labelPluType{tabIndex}",
            Font = FontsSettings.FontMinimum,
            AutoSize = false,
            Text = viewPluScale.PluIsWeight == false ? LocaleCore.Scales.PluIsPiece : LocaleCore.Scales.PluIsWeight,
            TextAlign = ContentAlignment.MiddleCenter,
            Parent = buttonPlu,
            Dock = DockStyle.None,
            BackColor = viewPluScale.PluIsWeight ? Color.LightGray : Color.Transparent,
            BorderStyle = BorderStyle.FixedSingle,
            //TabIndex = tabIndex,
        };

    private Label NewLabelPluCode(WsSqlViewPluScaleModel viewPluScale, Control buttonPlu) =>
        new()
        {
            //Name = $@"labelPluCode{tabIndex}",
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
            //TabIndex = tabIndex,
        };

    private Label NewLabelPluTemplate(WsSqlViewPluScaleModel viewPluScale, Control buttonPlu) =>
        new()
        {
            //Name = $@"labelPluTemplate{tabIndex}",
            Font = FontsSettings.FontMinimum,
            AutoSize = false,
            Text = !string.IsNullOrEmpty(viewPluScale.TemplateName) ? LocaleCore.Scales.PluTemplateSet : LocaleCore.Scales.PluTemplateNotSet,
            TextAlign = ContentAlignment.MiddleCenter,
            Parent = buttonPlu,
            Dock = DockStyle.None,
            BackColor = !string.IsNullOrEmpty(viewPluScale.TemplateName) ? Color.Transparent : Color.LightGray,
            Visible = true,
            BorderStyle = BorderStyle.FixedSingle,
            //TabIndex = tabIndex,
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
            if (sender is Control { Tag: WsSqlViewPluScaleModel viewPluScale })
            {
                if (UserSession.ContextCache.ViewPlusScalesDb.Any())
                {
                    UserSession.PluScale = UserSession.ContextManager.ContextPluScale.GetItem(
                        UserSession.Scale.IdentityValueId, viewPluScale.PluNumber);
                    Result = UserSession.PluScale.IsExists ? DialogResult.OK : DialogResult.Abort;
                }
            }
            ReturnBackAction();
        });
    }

    private void ButtonPreviousRoll_Click(object sender, EventArgs e)
    {
        int saveCurrentPage = UserSession.PageNumber;
        UserSession.PageNumber = UserSession.PageNumber > 0 ? UserSession.PageNumber - 1 : 0;
        if (UserSession.PageNumber == saveCurrentPage) return;
        WsActionUtils.ActionTryCatchFinally(this, SetupControls, () => { layoutPanel.Visible = true; }); 
    }

    private void ButtonNextRoll_Click(object sender, EventArgs e)
    {
        int saveCurrentPage = UserSession.PageNumber;
        int countPage = UserSession.ContextCache.ViewPlusScalesDb.Count / UserSession.PageSize;
        UserSession.PageNumber = UserSession.PageNumber < countPage ? UserSession.PageNumber + 1 : countPage;
        if (UserSession.PageNumber > countPage)
            UserSession.PageNumber = countPage - 1;
        if (UserSession.PageNumber == saveCurrentPage) return;
        WsActionUtils.ActionTryCatchFinally(this, SetupControls, () => { layoutPanel.Visible = true; });
    }

    private void SetupPanel(ushort columnCount, ushort rowCount)
    {
        layoutPanel.ColumnStyles.Clear();
        layoutPanel.RowStyles.Clear();
        layoutPanel.ColumnCount = 0;
        layoutPanel.RowCount = 0;
        if (columnCount > 0) AddColumns(layoutPanel, columnCount);
        if (rowCount > 0) AddRows(layoutPanel, rowCount);
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

    /// <summary>
    /// Очистить контролы.
    /// </summary>
    private void ClearControls()
    {
        //foreach (object control in layoutPanel.Controls)
        //{
        //    if (control is TableLayoutPanel subPanel)
        //    {
        //        layoutPanelActions = subPanel;
        //        layoutPanelActions.Parent = null;
        //    }
        //}
        layoutPanel.Controls.Clear();
    }

    /// <summary>
    /// Настроить контролы.
    /// </summary>
    private void SetupControls()
    {
        layoutPanel.Visible = false;
        labelCurrentPage.Text = $@"{LocaleCore.Scales.PluPage} {UserSession.PageNumber}";

        // Очистить контролы.
        ClearControls();
        
        WsControlPluModel?[,] controls = CreateControls();
        ushort columnCount = (ushort)(controls.GetUpperBound(0) + 1);
        ushort rowCount = (ushort)(controls.GetUpperBound(1) + 1);
        SetupPanel(columnCount, rowCount);
        // Проверка на наличие ПЛУ линии.
        if (!Equals(columnCount, (ushort)0) && !Equals(rowCount, (ushort)0))
        {
            for (ushort column = 0; column < columnCount; column++)
            {
                for (ushort row = 0; row < rowCount; row++)
                {
                    if (controls[column, row] is { } control)
                        layoutPanel.Controls.Add(control.ButtonPlu, column, row);
                }
            }
        }
        else { rowCount = 1; columnCount = 1; }

        //AddRows(layoutPanel, 1);
        layoutPanelActions.Parent = layoutPanel;
        layoutPanel.SetColumn(layoutPanelActions, 0);
        layoutPanel.SetRow(layoutPanelActions, rowCount - 1);
        layoutPanel.SetColumnSpan(layoutPanelActions, columnCount);
        layoutPanelActions.Dock = DockStyle.Fill;

        layoutPanel.Visible = true;

        foreach (WsControlPluModel? control in controls) control?.SetupSizes();
    }

    #endregion
}