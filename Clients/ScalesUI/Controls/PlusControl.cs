// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
#nullable enable

using WsLabelCore.Controls;
using WsLabelCore.Utils;

namespace ScalesUI.Controls;

public sealed partial class PlusControl : UserControlBase
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// ID последней линии (для производительности).
    /// </summary>
    private long LastScaleId { get; set; }
    private int LastPageNumber { get; set; }

    public PlusControl()
    {
        InitializeComponent();

        LastScaleId = default;
        LastPageNumber = default;
    }

    #endregion

    #region Public and private methods


    public override void RefreshAction()
    {
        WsActionUtils.ActionTryCatch(this, UserSession.Scale, () =>
        {
            if (!LastScaleId.Equals(UserSession.Scale.IdentityValueId))
            {
                LastScaleId = UserSession.Scale.IdentityValueId;
                // Обновить метки.
                labelCurrentPage.Text = $@"{LocaleCore.Scales.PluPage} {UserSession.PlusPageNumber}";
                buttonLeftScroll.Text = LocaleCore.Buttons.Previous;
                buttonRightScroll.Text = LocaleCore.Buttons.Next;
            }
            // Настроить контролы.
            SetupControls();
        });
    }

    private WsControlPluModel?[,] CreateControls()
    {
        List<WsSqlViewPluScaleModel> viewPlusScales = 
            UserSession.ContextCache.GetCurrentViewPlusScalesDb(UserSession.PlusPageNumber, WsUserSessionHelper.PlusPageSize);
        WsControlPluModel?[,] controls = new WsControlPluModel?[0, 0];
        if (!viewPlusScales.Any()) return controls;
        controls = new WsControlPluModel[WsUserSessionHelper.PlusPageColumnCount, WsUserSessionHelper.PlusPageRowCount];
        WsActionUtils.ActionTryCatch(this, UserSession.Scale, () =>
        {
            for (ushort rowNumber = 0, buttonNumber = 0; rowNumber < WsUserSessionHelper.PlusPageRowCount; ++rowNumber)
            {
                for (ushort columnNumber = 0; columnNumber < WsUserSessionHelper.PlusPageColumnCount; ++columnNumber)
                {
                    if (buttonNumber >= viewPlusScales.Count) break;
                    WsControlPluModel control = NewControlGroup(viewPlusScales[buttonNumber]);
                    controls[columnNumber, rowNumber] = control;
                    buttonNumber++;
                }
            }
        });
        return controls;
    }

    private WsControlPluModel NewControlGroup(WsSqlViewPluScaleModel viewPluScale)
    {
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
            Font = FontsSettings.FontLabelsBlack,
            AutoSize = false,
            Text = viewPluScale.PluName,
            Dock = DockStyle.Fill,
            Size = new(150, 30),
            Visible = true,
            Parent = layoutPanelPlus,
            FlatStyle = FlatStyle.Flat,
            Location = new(0, 0),
            UseVisualStyleBackColor = true,
            BackColor = System.Drawing.SystemColors.Control,
        };

    private Label NewLabelPluNumber(WsSqlViewPluScaleModel viewPluScale, Control buttonPlu) =>
        new()
        {
            Font = FontsSettings.FontMinimum,
            AutoSize = false,
            Text = Text = Width > 1024 ? $@"{LocaleCore.Table.Number} {viewPluScale.PluNumber}" : $@"{viewPluScale.PluNumber}",
            TextAlign = ContentAlignment.MiddleCenter,
            Parent = buttonPlu,
            Dock = DockStyle.None,
            BackColor = Color.Transparent,
            BorderStyle = BorderStyle.FixedSingle,
        };

    private Label NewPluValidLabel(WsSqlViewPluScaleModel viewPluScale, Control buttonPlu)
    {
        bool valid = WsSqlPluController.Instance.IsFullValid(viewPluScale);
        return new()
        {
            Font = FontsSettings.FontMinimum,
            AutoSize = false,
            Text = valid == false ? "!" : "OK",
            TextAlign = ContentAlignment.MiddleCenter,
            Parent = buttonPlu,
            Dock = DockStyle.None,
            BackColor = valid == false ? Color.Gold : Color.Transparent,
            BorderStyle = BorderStyle.FixedSingle,
        };
    }

    private Label NewLabelPluType(WsSqlViewPluScaleModel viewPluScale, Control buttonPlu) =>
        new()
        {
            Font = FontsSettings.FontMinimum,
            AutoSize = false,
            Text = viewPluScale.PluIsWeight == false ? LocaleCore.Scales.PluIsPiece : LocaleCore.Scales.PluIsWeight,
            TextAlign = ContentAlignment.MiddleCenter,
            Parent = buttonPlu,
            Dock = DockStyle.None,
            BackColor = viewPluScale.PluIsWeight ? Color.LightGray : Color.Transparent,
            BorderStyle = BorderStyle.FixedSingle,
        };

    private Label NewLabelPluCode(WsSqlViewPluScaleModel viewPluScale, Control buttonPlu) =>
        new()
        {
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
        };

    private Label NewLabelPluTemplate(WsSqlViewPluScaleModel viewPluScale, Control buttonPlu) =>
        new()
        {
            Font = FontsSettings.FontMinimum,
            AutoSize = false,
            Text = !string.IsNullOrEmpty(viewPluScale.TemplateName) ? LocaleCore.Scales.PluTemplateSet : LocaleCore.Scales.PluTemplateNotSet,
            TextAlign = ContentAlignment.MiddleCenter,
            Parent = buttonPlu,
            Dock = DockStyle.None,
            BackColor = !string.IsNullOrEmpty(viewPluScale.TemplateName) ? Color.Transparent : Color.LightGray,
            Visible = true,
            BorderStyle = BorderStyle.FixedSingle,
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
                if (UserSession.ContextCache.CurrentViewPlusScalesDb.Any())
                {
                    UserSession.PluScale = UserSession.ContextManager.ContextPluScale.GetItem(
                        UserSession.Scale.IdentityValueId, viewPluScale.PluNumber);
                    Result = UserSession.PluScale.IsExists ? DialogResult.OK : DialogResult.Abort;
                }
            }
            ReturnBackAction();
        });
    }

    private void ButtonPreviousScroll_Click(object sender, EventArgs e)
    {
        UserSession.PlusPageNumber = UserSession.PlusPageNumber > 0 ? UserSession.PlusPageNumber - 1: default;
        if (UserSession.PlusPageNumber.Equals(LastPageNumber)) return;
        LastPageNumber = UserSession.PlusPageNumber;
        SetupControls(); 
    }

    private void ButtonNextScroll_Click(object sender, EventArgs e)
    {
        int countPage = UserSession.GetPlusPageCount();
        UserSession.PlusPageNumber = UserSession.PlusPageNumber < countPage ? UserSession.PlusPageNumber + 1: countPage;
        if (UserSession.PlusPageNumber > countPage)
            UserSession.PlusPageNumber = countPage - 1;
        if (UserSession.PlusPageNumber.Equals(LastPageNumber)) return;
        LastPageNumber = UserSession.PlusPageNumber;
        SetupControls();
    }

    private void SetupLayoutPanelPlus(int columnCount, int rowCount)
    {
        layoutPanelPlus.ColumnStyles.Clear();
        layoutPanelPlus.ColumnCount = columnCount > 0 ? columnCount : 1;
        if (columnCount > 0)
        {
            int width = 100 / columnCount;
            for (ushort i = 0; i < layoutPanelPlus.ColumnCount; i++)
                layoutPanelPlus.ColumnStyles.Add(new(SizeType.Percent, width));
        }
        else
            layoutPanelPlus.ColumnStyles.Add(new(SizeType.Percent, 100));

        layoutPanelPlus.RowStyles.Clear();
        layoutPanelPlus.RowCount = rowCount > 0 ? rowCount : 1;
        if (rowCount > 0)
        {
            int height = 100 / layoutPanelPlus.RowCount;
            for (ushort i = 0; i < layoutPanelPlus.RowCount; i++)
                layoutPanelPlus.RowStyles.Add(new(SizeType.Percent, height));
        }
        else
            layoutPanelPlus.RowStyles.Add(new(SizeType.Percent, 100));

    }

    /// <summary>
    /// Настроить контролы.
    /// </summary>
    private void SetupControls()
    {
        layoutPanelPlus.Visible = false;
        layoutPanelPlus.Controls.Clear();
        labelCurrentPage.Text = $@"{LocaleCore.Scales.PluPage} {UserSession.PlusPageNumber}";

        WsControlPluModel?[,] controls = CreateControls();
        int columnSave = controls.GetUpperBound(0) + 1;    // -1 + 1 = 0
        int rowSave = controls.GetUpperBound(1) + 1;       // -1 + 1 = 0
        int columnCount = controls.GetUpperBound(0) + 1;    // -1 + 1 = 0
        int rowCount = controls.GetUpperBound(1) + 1;       // -1 + 1 = 0
        if (columnCount < 1) columnCount = 1;
        if (rowCount < 1) rowCount = 1;
        
        SetupLayoutPanelPlus(columnCount, rowCount);
        
        // Проверка на наличие ПЛУ линии.
        for (ushort column = 0; column < columnCount; column++)
        {
            for (ushort row = 0; row < rowCount; row++)
            {
                if (columnSave > 0 && rowSave > 0)
                    if (controls[column, row] is { } control)
                        layoutPanelPlus.Controls.Add(control.ButtonPlu, column, row);
            }
        }

        layoutPanelActions.Parent = layoutPanelPlus;
        layoutPanelPlus.SetColumn(layoutPanelActions, 0);
        layoutPanelPlus.SetRow(layoutPanelActions, rowCount);
        layoutPanelPlus.SetColumnSpan(layoutPanelActions, columnCount);
        layoutPanelActions.Dock = DockStyle.Fill;
        foreach (WsControlPluModel? control in controls) control?.SetupSizes();
        layoutPanelPlus.Visible = true;
    }

    #endregion
}