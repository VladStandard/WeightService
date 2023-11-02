using System.Windows.Forms;
using WsStorageCore.Entities.SchemaScale.PlusScales;
namespace WsLabelCore.Pages;

/// <summary>
/// WinForms-контрол смены плу линии.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
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
    public void SetupUserControl()
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
        List<WsSqlViewPluLineModel> viewPlusScales = ContextCache.GetCurrentViewPlusScales(LabelSession.PlusPageNumber, WsLabelSessionHelper.PlusPageSize);
        if (!viewPlusScales.Any())
        {
            PluUserControls = new WsFormPluControl?[0, 0];
            return;
        }

        PluUserControls = new WsFormPluControl[WsLabelSessionHelper.PlusPageColumnCount, WsLabelSessionHelper.PlusPageRowCount];
        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            for (ushort rowNumber = 0, counter = 0; rowNumber < WsLabelSessionHelper.PlusPageRowCount; ++rowNumber)
            {
                for (ushort columnNumber = 0; columnNumber < WsLabelSessionHelper.PlusPageColumnCount; ++columnNumber)
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
                    ViewModel.PluLine = new WsSqlPluLineRepository().GetItem(viewPluScale.ScaleId, viewPluScale.PluNumber);
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
        buttonLeftScroll.Text = WsLocaleCore.Buttons.Back;
        buttonCurrentPage.Text = $@"{WsLocaleCore.LabelPrint.PluPage} {LabelSession.PlusPageNumber + 1}";
        buttonRightScroll.Text = WsLocaleCore.Buttons.Forward;
        buttonCancel.Text = WsLocaleCore.Buttons.Cancel;
        buttonCancel.Select();
    }

    /// <summary>
    /// Отмена.
    /// </summary>
    private void buttonCancel_Click(object sender, EventArgs e) => ViewModel.CmdCancel.Relay();
    
    /// <summary>
    /// Изменение размера.
    /// </summary>
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