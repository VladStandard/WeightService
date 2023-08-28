namespace WsLabelCore.Common;

/// <summary>
/// Базовый класс XAML модели представления.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public class WsXamlBaseViewModel : WsViewModelBase, IWsViewModel
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Прервать.
    /// </summary>
    public WsActionCommandModel CmdAbort { get; private init; }
    /// <summary>
    /// Отменить.
    /// </summary>
    public WsActionCommandModel CmdCancel { get; private init; }
    /// <summary>
    /// Настроить.
    /// </summary>
    public WsActionCommandModel CmdCustom { get; private init; }
    /// <summary>
    /// Игнорировать.
    /// </summary>
    public WsActionCommandModel CmdIgnore { get; private init; }
    /// <summary>
    /// Нет.
    /// </summary>
    public WsActionCommandModel CmdNo { get; private init; }
    /// <summary>
    /// Ок.
    /// </summary>
    public WsActionCommandModel CmdOk { get; private init; }
    /// <summary>
    /// Повторить.
    /// </summary>
    public WsActionCommandModel CmdRetry { get; private init; }
    /// <summary>
    /// Да.
    /// </summary>
    public WsActionCommandModel CmdYes { get; private init; }
    /// <summary>e
    /// Список команд.
    /// </summary>
    public ObservableCollection<WsActionCommandModel> Commands { get; private set; } = new();

    /// <summary>
    /// Список команд без кастом.
    /// </summary>
    public ObservableCollection<WsActionCommandModel> CommandsWithoutCustom => 
        new(Commands.Where(item => !item.Equals(CmdCustom)));

    /// <summary>
    /// Список видимых команд без кастом.
    /// </summary>
    public ObservableCollection<WsActionCommandModel> CommandsSmart => 
        new(Commands.Where(item => !item.Equals(CmdCustom) && item.Visibility.Equals(WsEnumVisibility.Visible)));

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

    protected WsEnumNavigationPage FormUserControl { get; init; } = WsEnumNavigationPage.Default;
    public bool IsLoaded { get; set; }

    public WsXamlBaseViewModel()
    {
        CmdAbort = new(nameof(CmdAbort), WsLocaleCore.Buttons.Abort, WsEnumVisibility.Hidden);
        CmdCancel = new(nameof(CmdCancel), WsLocaleCore.Buttons.Cancel, WsEnumVisibility.Hidden);
        CmdCustom = new(nameof(CmdCustom), WsLocaleCore.Buttons.Custom, WsEnumVisibility.Hidden);
        CmdIgnore = new(nameof(CmdIgnore), WsLocaleCore.Buttons.Ignore, WsEnumVisibility.Hidden);
        CmdNo = new(nameof(CmdNo), WsLocaleCore.Buttons.No, WsEnumVisibility.Hidden);
        CmdOk = new(nameof(CmdOk), WsLocaleCore.Buttons.Ok, WsEnumVisibility.Hidden);
        CmdRetry = new(nameof(CmdRetry), WsLocaleCore.Buttons.Retry, WsEnumVisibility.Hidden);
        CmdYes = new(nameof(CmdYes), WsLocaleCore.Buttons.Yes, WsEnumVisibility.Hidden);
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
    /// <param name="actionOk"></param>
    private void AddActionsOk(Action actionOk)
    {
        HideCommandsVisibility();
        CmdOk.AddAction(actionOk);
        CmdOk.Visibility = WsEnumVisibility.Visible;
        UpdateCommandsFromActions();
    }

    /// <summary>
    /// Настройка кастом действия.
    /// </summary>
    /// <param name="actionCustom"></param>
    private void AddActionsCustom(Action actionCustom)
    {
        HideCommandsVisibility();
        CmdCustom.AddAction(actionCustom);
        CmdCustom.Visibility = WsEnumVisibility.Hidden;
        UpdateCommandsFromActions();
    }

    /// <summary>
    /// Настройка действий Отмена/Да.
    /// </summary>
    /// <param name="actionCancel"></param>
    /// <param name="actionYes"></param>
    private void AddActionsCancelYes(Action actionCancel, Action actionYes)
    {
        HideCommandsVisibility();
        CmdCancel.AddAction(actionCancel);
        CmdCancel.Visibility = WsEnumVisibility.Visible;
        CmdYes.AddAction(actionYes);
        CmdYes.Visibility = WsEnumVisibility.Visible;
        UpdateCommandsFromActions();
    }

    /// <summary>
    /// Настройка действий Отмена/Да.
    /// </summary>
    private void AddActionsCancelYes()
    {
        HideCommandsVisibility();
        CmdCancel.Visibility = WsEnumVisibility.Visible;
        CmdYes.Visibility = WsEnumVisibility.Visible;
        UpdateCommandsFromActions();
    }

    /// <summary>
    /// Настройка действий Нет/Да.
    /// </summary>
    /// <param name="actionNo"></param>
    /// <param name="actionYes"></param>
    private void AddActionsNoYes(Action actionNo, Action actionYes)
    {
        HideCommandsVisibility();
        CmdNo.AddAction(actionNo);
        CmdNo.Visibility = WsEnumVisibility.Visible;
        CmdYes.AddAction(actionYes);
        CmdYes.Visibility = WsEnumVisibility.Visible;
        UpdateCommandsFromActions();
    }

    /// <summary>
    /// Настройка действий Нет/Да.
    /// </summary>
    private void AddActionsNoYes()
    {
        HideCommandsVisibility();
        CmdNo.Visibility = WsEnumVisibility.Visible;
        CmdYes.Visibility = WsEnumVisibility.Visible;
        UpdateCommandsFromActions();
    }

    /// <summary>
    /// Скрыть видимость команд.
    /// </summary>
    private void HideCommandsVisibility()
    {
        CmdAbort.Visibility = WsEnumVisibility.Hidden;
        CmdCancel.Visibility = WsEnumVisibility.Hidden;
        CmdCustom.Visibility = WsEnumVisibility.Hidden;
        CmdIgnore.Visibility = WsEnumVisibility.Hidden;
        CmdNo.Visibility = WsEnumVisibility.Hidden;
        CmdOk.Visibility = WsEnumVisibility.Hidden;
        CmdRetry.Visibility = WsEnumVisibility.Hidden;
        CmdYes.Visibility = WsEnumVisibility.Hidden;
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
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
    /// <param name="controlWidth"></param>
    public void SetupButtonsWidth(int controlWidth) => ButtonWidth = !Commands.Any() ? controlWidth - 15 : controlWidth / Commands.Count - 15;

    /// <summary>
    /// Настройка кнопок Отмена/Да.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="actionCancel"></param>
    /// <param name="actionYes"></param>
    /// <param name="actionBack"></param>
    /// <param name="controlWidth"></param>
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
    /// <param name="message"></param>
    /// <param name="actionCancel"></param>
    /// <param name="actionYes"></param>
    /// <param name="controlWidth"></param>
    public void SetupButtonsCancelYes(string message, Action actionCancel, Action actionYes, int controlWidth)
    {
        Message = message;
        AddActionsCancelYes(actionCancel, actionYes);
        SetupButtonsWidth(controlWidth);
    }

    /// <summary>
    /// Настройка кнопок Нет/Да.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="actionNo"></param>
    /// <param name="actionYes"></param>
    /// <param name="actionBack"></param>
    /// <param name="controlWidth"></param>
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
    /// <param name="message"></param>
    /// <param name="actionNo"></param>
    /// <param name="actionYes"></param>
    /// <param name="controlWidth"></param>
    public void SetupButtonsNoYes(string message, Action actionNo, Action actionYes, int controlWidth)
    {
        Message = message;
        AddActionsCancelYes(actionNo, actionYes);
        SetupButtonsWidth(controlWidth);
    }

    /// <summary>
    /// Настройка кнопок Отмена/Да.
    /// </summary>
    /// <param name="controlWidth"></param>
    public void SetupButtonsCancelYes(int controlWidth)
    {
        Message = string.Empty;
        AddActionsCancelYes();
        SetupButtonsWidth(controlWidth);
    }

    /// <summary>
    /// Настройка кнопок Ок.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="actionOk"></param>
    /// <param name="controlWidth"></param>
    public void SetupButtonsOk(string message, Action actionOk, int controlWidth)
    {
        Message = message;
        AddActionsOk(actionOk);
        SetupButtonsWidth(controlWidth);
    }

    /// <summary>
    /// Настройка кнопок Ок.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="actionOk"></param>
    /// <param name="actionBack"></param>
    /// <param name="controlWidth"></param>
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
    /// <param name="message"></param>
    /// <param name="actionCustom"></param>
    /// <param name="controlWidth"></param>
    public void SetupButtonsCustom(string message, Action actionCustom, int controlWidth)
    {
        Message = message;
        AddActionsCustom(actionCustom);
        SetupButtonsWidth(controlWidth);
    }

    /// <summary>
    /// Задать команды.
    /// </summary>
    /// <param name="commands"></param>
    public void SetCommands(ObservableCollection<WsActionCommandModel> commands)
    {
        Commands = commands;
    }

    #endregion
}