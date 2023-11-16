namespace Ws.LabelCore.Common;

/// <summary>
/// Базовый класс XAML модели представления.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public class XamlBaseViewModel : ViewModelBase, IViewModel
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Прервать.
    /// </summary>
    public ActionCommandModel CmdAbort { get; private set; }
    /// <summary>
    /// Отменить.
    /// </summary>
    public ActionCommandModel CmdCancel { get; private set; }
    /// <summary>
    /// Настроить.
    /// </summary>
    public ActionCommandModel CmdCustom { get; private set; }
    /// <summary>
    /// Игнорировать.
    /// </summary>
    public ActionCommandModel CmdIgnore { get; private set; }
    /// <summary>
    /// Нет.
    /// </summary>
    public ActionCommandModel CmdNo { get; private set; }
    /// <summary>
    /// Ок.
    /// </summary>
    public ActionCommandModel CmdOk { get; private set; }
    /// <summary>
    /// Повторить.
    /// </summary>
    public ActionCommandModel CmdRetry { get; private set; }
    /// <summary>
    /// Да.
    /// </summary>
    public ActionCommandModel CmdYes { get; private set; }
    /// <summary>e
    /// Список команд.
    /// </summary>
    public ObservableCollection<ActionCommandModel> Commands { get; private set; } = new();

    /// <summary>
    /// Список видимых команд без кастом.
    /// </summary>
    public ObservableCollection<ActionCommandModel> CommandsSmart => 
        new(Commands.Where(item => !item.Equals(CmdCustom) && item.Visibility.Equals(EnumVisibility.Visible)));

    /// <summary>
    /// Ширина кнопки.
    /// </summary>
    public int ButtonWidth { get; set; } = 100;
    /// <summary>
    /// Размер шрифта сообщения.
    /// </summary>
    public double FontSizeMessage => 26;
    /// <summary>
    /// Размер шрифта кнопок.
    /// </summary>
    public double FontSizeButton => 24;
    /// <summary>
    /// Сообщение.
    /// </summary>
    public string Message { get; set; } = "";
    /// <summary>
    /// Видимость сообщения.
    /// </summary>
    public Visibility MessageVisibility => string.IsNullOrEmpty(Message) ? Visibility.Hidden : Visibility.Visible;

    protected EnumNavigationPage FormUserControl { get; set; } = EnumNavigationPage.Default;

    public XamlBaseViewModel()
    {
        CmdAbort = new(nameof(CmdAbort), LocaleCore.Buttons.Abort, EnumVisibility.Hidden);
        CmdCancel = new(nameof(CmdCancel), LocaleCore.Buttons.Cancel, EnumVisibility.Hidden);
        CmdCustom = new(nameof(CmdCustom), LocaleCore.Buttons.Custom, EnumVisibility.Hidden);
        CmdIgnore = new(nameof(CmdIgnore), LocaleCore.Buttons.Ignore, EnumVisibility.Hidden);
        CmdNo = new(nameof(CmdNo), LocaleCore.Buttons.No, EnumVisibility.Hidden);
        CmdOk = new(nameof(CmdOk), LocaleCore.Buttons.Ok, EnumVisibility.Hidden);
        CmdRetry = new(nameof(CmdRetry), LocaleCore.Buttons.Retry, EnumVisibility.Hidden);
        CmdYes = new(nameof(CmdYes), LocaleCore.Buttons.Yes, EnumVisibility.Hidden);
        UpdateCommandsFromActions();
    }

    #endregion

    #region Public and private methods - Commands

    public override string ToString() =>
        $"{FormUserControl} | "+ 
        (Commands.Any() ? $"{string.Join(" | ", Commands.Select(item => item.ToString()))}" : $"{nameof(Commands)} is <Empty>") + 
        (string.IsNullOrEmpty(Message) ? string.Empty : $" | {Message}");

    #endregion

    #region Public and private methods

    /// <summary>
    /// Настройка действия Ок.
    /// </summary>
    private void AddActionsOk(Action actionOk)
    {
        HideCommandsVisibility();
        CmdOk.AddAction(actionOk);
        CmdOk.Visibility = EnumVisibility.Visible;
        UpdateCommandsFromActions();
    }

    /// <summary>
    /// Настройка кастом действия.
    /// </summary>
    private void AddActionsCustom(Action actionCustom)
    {
        HideCommandsVisibility();
        CmdCustom.AddAction(actionCustom);
        CmdCustom.Visibility = EnumVisibility.Hidden;
        UpdateCommandsFromActions();
    }

    /// <summary>
    /// Настройка действий Отмена/Да.
    /// </summary>
    private void AddActionsCancelYes(Action actionCancel, Action actionYes)
    {
        HideCommandsVisibility();
        CmdCancel.AddAction(actionCancel);
        CmdCancel.Visibility = EnumVisibility.Visible;
        CmdYes.AddAction(actionYes);
        CmdYes.Visibility = EnumVisibility.Visible;
        UpdateCommandsFromActions();
    }

    /// <summary>
    /// Настройка действий Отмена/Да.
    /// </summary>
    private void AddActionsCancelYes()
    {
        HideCommandsVisibility();
        CmdCancel.Visibility = EnumVisibility.Visible;
        CmdYes.Visibility = EnumVisibility.Visible;
        UpdateCommandsFromActions();
    }

    /// <summary>
    /// Настройка действий Нет/Да.
    /// </summary>
    private void AddActionsNoYes(Action actionNo, Action actionYes)
    {
        HideCommandsVisibility();
        CmdNo.AddAction(actionNo);
        CmdNo.Visibility = EnumVisibility.Visible;
        CmdYes.AddAction(actionYes);
        CmdYes.Visibility = EnumVisibility.Visible;
        UpdateCommandsFromActions();
    }

    /// <summary>
    /// Настройка действий Нет/Да.
    /// </summary>
    private void AddActionsNoYes()
    {
        HideCommandsVisibility();
        CmdNo.Visibility = EnumVisibility.Visible;
        CmdYes.Visibility = EnumVisibility.Visible;
        UpdateCommandsFromActions();
    }

    /// <summary>
    /// Скрыть видимость команд.
    /// </summary>
    private void HideCommandsVisibility()
    {
        CmdAbort.Visibility = EnumVisibility.Hidden;
        CmdCancel.Visibility = EnumVisibility.Hidden;
        CmdCustom.Visibility = EnumVisibility.Hidden;
        CmdIgnore.Visibility = EnumVisibility.Hidden;
        CmdNo.Visibility = EnumVisibility.Hidden;
        CmdOk.Visibility = EnumVisibility.Hidden;
        CmdRetry.Visibility = EnumVisibility.Hidden;
        CmdYes.Visibility = EnumVisibility.Hidden;
    }

    /// <summary>
    /// Обновить список команд из действий.
    /// </summary>
    public void UpdateCommandsFromActions()
    {
        Commands.Clear();
        if (CmdAbort.Action is not null) Commands.Add(CmdAbort);
        if (CmdCustom.Action is not null) Commands.Add(CmdCustom);
        if (CmdIgnore.Action is not null) Commands.Add(CmdIgnore);
        if (CmdNo.Action is not null) Commands.Add(CmdNo);
        if (CmdOk.Action is not null) Commands.Add(CmdOk);
        if (CmdRetry.Action is not null) Commands.Add(CmdRetry);
        if (CmdYes.Action is not null) Commands.Add(CmdYes);
        if (CmdCancel.Action is not null) Commands.Add(CmdCancel);
    }

    /// <summary>
    /// Обработчик нажатия кнопки.
    /// </summary>
    public void Button_KeyUp(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Escape:
                CmdCancel.Relay();
                break;
            case Key.Enter:
                CmdOk.Relay();
                break;
        }
    }

    /// <summary>
    /// Настройка ширины кнопок.
    /// </summary>
    public void SetupButtonsWidth(int controlWidth) => ButtonWidth = !Commands.Any() ? controlWidth - 15 : controlWidth / Commands.Count - 15;

    /// <summary>
    /// Настройка кнопок Отмена/Да.
    /// </summary>
    public void SetupButtonsCancelYes(string message, Action actionCancel, Action actionYes, Action actionBack, int controlWidth)
    {
        Message = message;
        AddActionsCancelYes(actionCancel, actionYes);
        AddActionsCancelYes(actionBack, actionBack);
        SetupButtonsWidth(controlWidth);
    }

    /// <summary>
    /// Настройка кнопок Отмена/Да.
    /// </summary>
    public void SetupButtonsCancelYes(string message, Action actionCancel, Action actionYes, int controlWidth)
    {
        Message = message;
        AddActionsCancelYes(actionCancel, actionYes);
        SetupButtonsWidth(controlWidth);
    }

    /// <summary>
    /// Настройка кнопок Нет/Да.
    /// </summary>
    public void SetupButtonsNoYes(string message, Action actionNo, Action actionYes, Action actionBack, int controlWidth)
    {
        Message = message;
        AddActionsCancelYes(actionNo, actionYes);
        AddActionsCancelYes(actionBack, actionBack);
        SetupButtonsWidth(controlWidth);
    }

    /// <summary>
    /// Настройка кнопок Нет/Да.
    /// </summary>
    public void SetupButtonsNoYes(string message, Action actionNo, Action actionYes, int controlWidth)
    {
        Message = message;
        AddActionsCancelYes(actionNo, actionYes);
        SetupButtonsWidth(controlWidth);
    }

    /// <summary>
    /// Настройка кнопок Отмена/Да.
    /// </summary>
    public void SetupButtonsCancelYes(int controlWidth)
    {
        Message = string.Empty;
        AddActionsCancelYes();
        SetupButtonsWidth(controlWidth);
    }

    /// <summary>
    /// Настройка кнопок Ок.
    /// </summary>
    public void SetupButtonsOk(string message, Action actionOk, int controlWidth)
    {
        Message = message;
        AddActionsOk(actionOk);
        SetupButtonsWidth(controlWidth);
    }

    /// <summary>
    /// Настройка кнопок Ок.
    /// </summary>
    public void SetupButtonsOk(string message, Action actionOk, Action actionBack, int controlWidth)
    {
        Message = message;
        AddActionsOk(actionOk);
        AddActionsOk(actionBack);
        SetupButtonsWidth(controlWidth);
    }

    /// <summary>
    /// Настройка кастом кнопки.
    /// </summary>
    public void SetupButtonsCustom(string message, Action actionCustom, int controlWidth)
    {
        Message = message;
        AddActionsCustom(actionCustom);
        SetupButtonsWidth(controlWidth);
    }

    /// <summary>
    /// Задать команды.
    /// </summary>
    public void SetCommands(ObservableCollection<ActionCommandModel> commands)
    {
        Commands = commands;
    }

    #endregion
}