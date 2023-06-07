// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;
using WsLocalizationCore.Utils;
using Control = System.Windows.Forms.Control;

namespace WsLabelCore.Pages;

/// <summary>
/// WinForms-контрол смены плу линии.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
#nullable enable
public sealed partial class WsXamlPlusLinesUserControl : WsFormBaseUserControl, IWsFormUserControl
{
    #region Public and private fields, properties, constructor

    public WsXamlPlusLineViewModel ViewModel => Page.ViewModel as WsXamlPlusLineViewModel ?? new();
    private WsFormPluControl?[,] PluUserControls { get; set; } = new WsFormPluControl?[0, 0];

    /// <summary>
    /// ID последней линии (для производительности).
    /// </summary>
    private long LastScaleId { get; set; }
    private int LastPageNumber { get; set; }

    public WsXamlPlusLinesUserControl() : base(WsEnumNavigationPage.PlusLine)
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить контрол.
    /// </summary>
    public void SetupUserConrol()
    {
        Page.SetupViewModel(ViewModel);

        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            // Обновить локальный кэш.
            ContextCache.LoadLocalViewPlusLines((ushort)LabelSession.Line.IdentityValueId);
            // ID линии.
            if (!LastScaleId.Equals(LabelSession.Line.IdentityValueId))
                LastScaleId = LabelSession.Line.IdentityValueId;
            // Настроить контролы.
            SetupFormControls();
        });
    }

    /// <summary>
    /// Создать массив контролов ПЛУ.
    /// </summary>
    /// <returns></returns>
    private void CreatePluUserControls()
    {
        List<WsSqlViewPluLineModel> viewPlusScales = 
            ContextCache.GetCurrentViewPlusScales(LabelSession.PlusPageNumber, LabelSession.PlusPageSize);
        if (!viewPlusScales.Any()) PluUserControls = new WsFormPluControl?[0, 0];

        PluUserControls = new WsFormPluControl[LabelSession.PlusPageColumnCount, LabelSession.PlusPageRowCount];
        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            for (ushort rowNumber = 0, counter = 0; rowNumber < LabelSession.PlusPageRowCount; ++rowNumber)
            {
                for (ushort columnNumber = 0; columnNumber < LabelSession.PlusPageColumnCount; ++columnNumber)
                {
                    if (counter >= viewPlusScales.Count) break;
                    PluUserControls[columnNumber, rowNumber] = new(viewPlusScales[counter], ActionPluSelect);
                    counter++;
                }
            }
        });
    }

    /// <summary>
    /// Смена ПЛУ.
    /// </summary>
    private void ActionPluSelect(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            if (sender is Control { Tag: WsSqlViewPluLineModel viewPluScale })
            {
                if (ContextCache.LocalViewPlusLines.Any())
                {
                    ViewModel.PluLine = ContextManager.ContextPlusLines.GetItem(viewPluScale.ScaleId, viewPluScale.PluNumber);
                }
            }

            if (ViewModel.PluLine.IsExists)
                ViewModel.CmdYes.Relay();
            else
                ViewModel.CmdCancel.Relay();
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
        SetupFormControls(); 
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
        SetupFormControls();
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
    private void SetupFormControls()
    {
        // Подготовить панель ПЛУ.
        PrepareLayoutPanelPlus();
        // Создать массив контролов ПЛУ.
        CreatePluUserControls();
        int columnSave = PluUserControls.GetUpperBound(0) + 1;     // -1 + 1 = 0
        int rowSave = PluUserControls.GetUpperBound(1) + 1;        // -1 + 1 = 0
        int columnCount = PluUserControls.GetUpperBound(0) + 1;    // -1 + 1 = 0
        int rowCount = PluUserControls.GetUpperBound(1) + 1;       // -1 + 1 = 0
        if (columnCount < 1) columnCount = 1;
        if (rowCount < 1) rowCount = 1;
        // Настроить панель ПЛУ.
        //SetupLayoutPanelPlus(columnCount, rowCount);
        // Перебор контролов ПЛУ.
        for (ushort column = 0; column < columnCount; column++)
        {
            for (ushort row = 0; row < rowCount; row++)
                if (columnSave > 0 && rowSave > 0 && PluUserControls[column, row] is { } pluUserControl)
                    // Добавить контрол ПЛУ на панель ПЛУ в заданную ячейку.
                    layoutPanelPlus.Controls.Add(pluUserControl, column, row);
        }
        // Настроить размеры контролов ПЛУ линий.
        SetupSizesForEachPlu();
    }
    
    /// <summary>
    /// Подготовить панель ПЛУ.
    /// </summary>
    private void PrepareLayoutPanelPlus()
    {
        MdInvokeControl.SetVisible(layoutPanelPlus, false);
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
    private void buttonCancel_Click(object sender, EventArgs e) => ViewModel.CmdCancel.Relay();
    
    /// <summary>
    /// Изменение размера.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void WsXamlPlusLinesUserControl_SizeChanged(object sender, EventArgs e)
    {
        // Настроить размеры контролов ПЛУ линий.
        SetupSizesForEachPlu();
    }

    /// <summary>
    /// Настроить размеры контролов ПЛУ линий.
    /// </summary>
    private void SetupSizesForEachPlu()
    {
        MdInvokeControl.SetVisible(layoutPanelPlus, false);
        foreach (WsFormPluControl? pluUserControl in PluUserControls)
            pluUserControl?.SetupSizes();
        // Отобразить панель ПЛУ.
        MdInvokeControl.SetVisible(layoutPanelPlus, true);
    }

    #endregion
}