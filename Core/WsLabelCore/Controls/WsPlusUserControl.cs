// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Controls;

#nullable enable
public sealed partial class WsPlusUserControl : WsBaseUserControl
{
    #region Public and private fields, properties, constructor

    public WsPlusViewModel ViewModel { get; }

    /// <summary>
    /// ID последней линии (для производительности).
    /// </summary>
    private long LastScaleId { get; set; }
    private int LastPageNumber { get; set; }

    public WsPlusUserControl()
    {
        InitializeComponent();

        ViewModel = new();
        LastScaleId = default;
        LastPageNumber = default;
    }

    #endregion

    #region Public and private methods

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        //// Настроить панель действий.
        //SetupLayoutPanelActions(rowCount, columnCount);
    }

    public override void RefreshAction()
    {
        WsWinFormNavigationUtils.ActionTryCatch(() =>
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

    /// <summary>
    /// Создать массив контролов ПЛУ.
    /// </summary>
    /// <returns></returns>
    private WsPluControl?[,] CreatePluUserControls()
    {
        List<WsSqlViewPluScaleModel> viewPlusScales = 
            UserSession.ContextCache.GetCurrentViewPlusScalesDb(UserSession.PlusPageNumber, WsUserSessionHelper.PlusPageSize);
        WsPluControl?[,] controls = new WsPluControl?[0, 0];
        if (!viewPlusScales.Any()) return controls;
        controls = new WsPluControl[WsUserSessionHelper.PlusPageColumnCount, WsUserSessionHelper.PlusPageRowCount];
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            for (ushort rowNumber = 0, counter = 0; rowNumber < WsUserSessionHelper.PlusPageRowCount; ++rowNumber)
            {
                for (ushort columnNumber = 0; columnNumber < WsUserSessionHelper.PlusPageColumnCount; ++columnNumber)
                {
                    if (counter >= viewPlusScales.Count) break;
                    controls[columnNumber, rowNumber] = new(viewPlusScales[counter], CreateLabelPlu(viewPlusScales[counter]),
                        CreateLabelPluTemplateCode(viewPlusScales[counter]), ActionPluSelect);
                    counter++;
                }
            }
        });
        return controls;
    }

    private Label CreateLabelPlu(WsSqlViewPluScaleModel viewPluScale) => new()
        {
            Font = FontsSettings.FontLabelsBlack,
            AutoSize = false,
            Text = $@"{viewPluScale.PluNumber} | {(viewPluScale.PluIsWeight ? LocaleCore.Scales.PluIsWeight : LocaleCore.Scales.PluIsPiece)} | {viewPluScale.PluName}",
            Visible = true,
            TextAlign = ContentAlignment.MiddleCenter,
            FlatStyle = FlatStyle.Flat,
            Dock = DockStyle.None,
            BackColor = Color.Transparent,
            BorderStyle = BorderStyle.FixedSingle,
        };

    private Label CreateLabelPluTemplateCode(WsSqlViewPluScaleModel viewPluScale)
    {
        string gtin = !string.IsNullOrEmpty(viewPluScale.PluGtin) ? @$"{viewPluScale.PluGtin}" : LocaleCore.Scales.PluGtinIsNotSet;
        string template = !string.IsNullOrEmpty(viewPluScale.TemplateName) ? LocaleCore.Scales.PluTemplateSet : LocaleCore.Scales.PluTemplateNotSet;
        return new()
        {
            Font = FontsSettings.FontMinimum,
            AutoSize = false,
            Text = $@"{gtin} | {template}",
            Visible = true,
            TextAlign = ContentAlignment.MiddleCenter,
            FlatStyle = FlatStyle.Flat,
            Dock = DockStyle.None,
            BackColor = !string.IsNullOrEmpty(viewPluScale.TemplateName) && WsSqlPluController.Instance.IsFullValid(viewPluScale) ? Color.Transparent : Color.Yellow,
            BorderStyle = BorderStyle.FixedSingle,
        };
    }

    /// <summary>
    /// Смена ПЛУ.
    /// </summary>
    private void ActionPluSelect(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            if (sender is Control { Tag: WsSqlViewPluScaleModel viewPluScale })
            {
                if (UserSession.ContextCache.CurrentViewPlusScales.Any())
                {
                    UserSession.PluScale = UserSession.ContextManager.ContextPluScale.GetItem(
                        UserSession.Scale.IdentityValueId, viewPluScale.PluNumber);
                }
            }

            if (UserSession.PluScale.IsExists)
                ViewModel.ActionReturnOk();
            else
                ViewModel.ActionReturnCancel();
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

    /// <summary>
    /// Настроить панель ПЛУ.
    /// </summary>
    /// <param name="columnCount"></param>
    /// <param name="rowCount"></param>
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
        // Скрыть и почтистить панель ПЛУ.
        layoutPanelPlus.Visible = false;
        layoutPanelPlus.Controls.Clear();
        labelCurrentPage.Text = $@"{LocaleCore.Scales.PluPage} {UserSession.PlusPageNumber}";
        // Создать массив контролов ПЛУ.
        WsPluControl?[,] pluUserControls = CreatePluUserControls();
        int columnSave = pluUserControls.GetUpperBound(0) + 1;     // -1 + 1 = 0
        int rowSave = pluUserControls.GetUpperBound(1) + 1;        // -1 + 1 = 0
        int columnCount = pluUserControls.GetUpperBound(0) + 1;    // -1 + 1 = 0
        int rowCount = pluUserControls.GetUpperBound(1) + 1;       // -1 + 1 = 0
        if (columnCount < 1) columnCount = 1;
        if (rowCount < 1) rowCount = 1;
        // Настроить панель ПЛУ.
        SetupLayoutPanelPlus(columnCount, rowCount);
        // Перебор контролов ПЛУ.
        for (ushort column = 0; column < columnCount; column++)
        {
            for (ushort row = 0; row < rowCount; row++)
                if (columnSave > 0 && rowSave > 0 && pluUserControls[column, row] is { } pluUserControl)
                    // Добавить контрол ПЛУ на панель ПЛУ в заданную ячейку.
                    layoutPanelPlus.Controls.Add(pluUserControl, column, row);
        }
        // Настроить панель действий.
        SetupLayoutPanelActions(rowCount, columnCount);
        // Настроить размеры контролов.
        foreach (WsPluControl? pluUserControl in pluUserControls) pluUserControl?.SetupSizes();
        // Отобразить панель ПЛУ.
        layoutPanelPlus.Visible = true;
    }

    /// <summary>
    /// Настроить панель действий.
    /// </summary>
    /// <param name="rowCount"></param>
    /// <param name="columnCount"></param>
    private void SetupLayoutPanelActions(int rowCount, int columnCount)
    {
        layoutPanelActions.Parent = layoutPanelPlus;
        layoutPanelPlus.SetColumn(layoutPanelActions, 0);
        layoutPanelPlus.SetRow(layoutPanelActions, rowCount);
        layoutPanelPlus.SetColumnSpan(layoutPanelActions, columnCount);
        layoutPanelActions.Dock = DockStyle.Fill;
    }

    #endregion
}