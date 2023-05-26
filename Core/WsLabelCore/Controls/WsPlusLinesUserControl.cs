// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Controls;
#nullable enable
public sealed partial class WsPlusLinesUserControl : WsBaseUserControl
{
    #region Public and private fields, properties, constructor

    public WsPlusViewModel ViewModel { get; }

    /// <summary>
    /// ID последней линии (для производительности).
    /// </summary>
    private long LastScaleId { get; set; }
    private int LastPageNumber { get; set; }

    public WsPlusLinesUserControl()
    {
        InitializeComponent();

        ViewModel = new();
        LastScaleId = default;
        LastPageNumber = default;
    }

    #endregion

    #region Public and private methods

    public override void RefreshAction()
    {
        WsWinFormNavigationUtils.ActionTryCatchSimple(() =>
        {
            // Обновить локальный кэш.
            ContextCache.LoadLocalViewPlusLines((ushort)LabelSession.Line.IdentityValueId);
            // ID линии.
            if (!LastScaleId.Equals(LabelSession.Line.IdentityValueId))
                LastScaleId = LabelSession.Line.IdentityValueId;
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
        List<WsSqlViewPluLineModel> viewPlusScales = 
            ContextCache.GetCurrentViewPlusScales(LabelSession.PlusPageNumber, LabelSession.PlusPageSize);
        if (!viewPlusScales.Any()) return new WsPluControl?[0, 0];

        WsPluControl?[,] pluUserControls = new WsPluControl[LabelSession.PlusPageColumnCount, LabelSession.PlusPageRowCount];
        WsWinFormNavigationUtils.ActionTryCatchSimple(() =>
        {
            for (ushort rowNumber = 0, counter = 0; rowNumber < LabelSession.PlusPageRowCount; ++rowNumber)
            {
                for (ushort columnNumber = 0; columnNumber < LabelSession.PlusPageColumnCount; ++columnNumber)
                {
                    if (counter >= viewPlusScales.Count) break;
                    pluUserControls[columnNumber, rowNumber] = new(viewPlusScales[counter], ActionPluSelect);
                    counter++;
                }
            }
        });
        return pluUserControls;
    }

    /// <summary>
    /// Смена ПЛУ.
    /// </summary>
    private void ActionPluSelect(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatchSimple(() =>
        {
            if (sender is Control { Tag: WsSqlViewPluLineModel viewPluScale })
            {
                if (ContextCache.LocalViewPlusLines.Any())
                {
                    ViewModel.PluLine = ContextManager.ContextPlusLines.GetItem(viewPluScale.ScaleId, viewPluScale.PluNumber);
                }
            }

            if (ViewModel.PluLine.IsExists)
                ViewModel.ActionOk.Relay();
            else
                ViewModel.ActionCancel.Relay();
        });
    }

    /// <summary>
    /// Перейти на страницу назад.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonPreviousScroll_Click(object sender, EventArgs e)
    {
        LabelSession.PlusPageNumber = LabelSession.PlusPageNumber > 0 ? LabelSession.PlusPageNumber - 1: default;
        if (LabelSession.PlusPageNumber.Equals(LastPageNumber)) return;
        LastPageNumber = LabelSession.PlusPageNumber;
        SetupControls(); 
    }

    /// <summary>
    /// Перейти на страницу вперёд.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonNextScroll_Click(object sender, EventArgs e)
    {
        int countPage = LabelSession.GetPlusPageCount();
        LabelSession.PlusPageNumber = LabelSession.PlusPageNumber < countPage ? LabelSession.PlusPageNumber + 1: countPage;
        if (LabelSession.PlusPageNumber > countPage)
            LabelSession.PlusPageNumber = countPage - 1;
        if (LabelSession.PlusPageNumber.Equals(LastPageNumber)) return;
        LastPageNumber = LabelSession.PlusPageNumber;
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
        // Подготовить панель ПЛУ.
        PrepareLayoutPanelPlus();
        // Создать массив контролов ПЛУ.
        WsPluControl?[,] pluUserControls = CreatePluUserControls();
        int columnSave = pluUserControls.GetUpperBound(0) + 1;     // -1 + 1 = 0
        int rowSave = pluUserControls.GetUpperBound(1) + 1;        // -1 + 1 = 0
        int columnCount = pluUserControls.GetUpperBound(0) + 1;    // -1 + 1 = 0
        int rowCount = pluUserControls.GetUpperBound(1) + 1;       // -1 + 1 = 0
        if (columnCount < 1) columnCount = 1;
        if (rowCount < 1) rowCount = 1;
        // Настроить панель ПЛУ.
        //SetupLayoutPanelPlus(columnCount, rowCount);
        // Перебор контролов ПЛУ.
        for (ushort column = 0; column < columnCount; column++)
        {
            for (ushort row = 0; row < rowCount; row++)
                if (columnSave > 0 && rowSave > 0 && pluUserControls[column, row] is { } pluUserControl)
                    // Добавить контрол ПЛУ на панель ПЛУ в заданную ячейку.
                    layoutPanelPlus.Controls.Add(pluUserControl, column, row);
        }
        // Настроить размеры контролов.
        foreach (WsPluControl? pluUserControl in pluUserControls) pluUserControl?.SetupSizes();
        // Отобразить панель ПЛУ.
        layoutPanelPlus.Visible = true;
    }
    
    /// <summary>
    /// Подготовить панель ПЛУ.
    /// </summary>
    private void PrepareLayoutPanelPlus()
    {
        layoutPanelPlus.Visible = false;
        int i = 0;
        while (i < layoutPanelPlus.Controls.Count)
        {
            Control control = layoutPanelPlus.Controls[i];
            if (control.Name.StartsWith("WsPluControl"))
                layoutPanelPlus.Controls.RemoveByKey(control.Name);
            else i++;
        }
        // Обновить метки.
        buttonLeftScroll.Text = LocaleCore.Buttons.Back;
        buttonCurrentPage.Text = $@"{LocaleCore.Scales.PluPage} {LabelSession.PlusPageNumber + 1}";
        buttonRightScroll.Text = LocaleCore.Buttons.Forward;
        buttonCancel.Text = LocaleCore.Buttons.Cancel;
        buttonCancel.Select();
    }

    /// <summary>
    /// Отмена.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void buttonCancel_Click(object sender, EventArgs e) => ViewModel.ActionCancel.Relay();

    #endregion
}