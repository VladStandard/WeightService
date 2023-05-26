// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Bases;

/// <summary>
/// Базовый класс модели представления.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsViewModelBase : WsMvvmViewModelBase, INotifyPropertyChanged
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Кэш БД.
    /// </summary>
    protected WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    /// <summary>
    /// Прервать.
    /// </summary>
    public WsActionCommandModel ActionAbort { get; private set; }
    /// <summary>
    /// Отменить.
    /// </summary>
    public WsActionCommandModel ActionCancel { get; private set; }
    /// <summary>
    /// Настроить.
    /// </summary>
    public WsActionCommandModel ActionCustom { get; private set; }
    /// <summary>
    /// Игнорировать.
    /// </summary>
    public WsActionCommandModel ActionIgnore { get; private set; }
    /// <summary>
    /// Нет.
    /// </summary>
    public WsActionCommandModel ActionNo { get; private set; }
    /// <summary>
    /// Ок.
    /// </summary>
    public WsActionCommandModel ActionOk { get; private set; }
    /// <summary>
    /// Повторить.
    /// </summary>
    public WsActionCommandModel ActionRetry { get; private set; }
    /// <summary>
    /// Да.
    /// </summary>
    public WsActionCommandModel ActionYes { get; private set; }
    /// <summary>
    /// Список команд.
    /// </summary>
    public ObservableCollection<WsActionCommandModel> Commands { get; } = new();
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

    protected WsViewModelBase()
    {
        SetupActionsEmpty();
    }

    protected WsViewModelBase(WsActionCommandModel cmdAbort, WsActionCommandModel cmdCancel, WsActionCommandModel cmdCustom, WsActionCommandModel cmdIgnore,
        WsActionCommandModel cmdNo, WsActionCommandModel cmdOk, WsActionCommandModel cmdRetry, WsActionCommandModel cmdYes)
    {
        SetupCommands(cmdAbort, cmdCancel, cmdCustom, cmdIgnore, cmdNo, cmdOk, cmdRetry, cmdYes);
    }

    protected WsViewModelBase(Action actionAbort, Action actionCancel, Action actionCustom, Action actionIgnore,
        Action actionNo, Action actionOk, Action actionRetry, Action actionYes)
    {
        SetupActions(actionAbort, actionCancel, actionCustom, actionIgnore, actionNo, actionOk, actionRetry, actionYes);
    }

    #endregion

    #region Public and private methods - Commands

    public override string ToString() => Commands.Any() ? $"{string.Join(" | ", Commands.Select(item => item.Name))}" : "<Empty>";

    ///// <summary>
    ///// Прервать.
    ///// </summary>
    //[RelayCommand]
    //public void RelayAbort() => ActionAbort.Action?.Invoke();
    ///// <summary>
    ///// Отменить.
    ///// </summary>
    //[RelayCommand]
    //public void RelayCancel() => ActionCancel.Action?.Invoke();
    ///// <summary>
    ///// Настроить.
    ///// </summary>
    //[RelayCommand]
    //public void RelayCustom() => ActionCustom.Action?.Invoke();
    ///// <summary>
    ///// Игнорировать.
    ///// </summary>
    //[RelayCommand]
    //public void RelayIgnore() => ActionIgnore.Action?.Invoke();
    ///// <summary>
    ///// Нет.
    ///// </summary>
    //[RelayCommand]
    //public void RelayNo() => ActionNo.Action?.Invoke();
    ///// <summary>
    ///// Ок.
    ///// </summary>
    //[RelayCommand]
    //public void RelayOk() => ActionOk.Action?.Invoke();
    ///// <summary>
    ///// Повторить.
    ///// </summary>
    //[RelayCommand]
    //public void RelayRetry() => ActionRetry.Action?.Invoke();

    ///// <summary>
    ///// Да.
    ///// </summary>
    //[RelayCommand]
    //public void RelayYes() => ActionYes.Action?.Invoke();

    #endregion

    #region Public and private methods

    /// <summary>
    /// Настройка.
    /// </summary>
    /// <param name="cmdAbort"></param>
    /// <param name="cmdCancel"></param>
    /// <param name="cmdCustom"></param>
    /// <param name="cmdIgnore"></param>
    /// <param name="cmdNo"></param>
    /// <param name="cmdOk"></param>
    /// <param name="cmdRetry"></param>
    /// <param name="cmdYes"></param>
    protected void SetupCommands(WsActionCommandModel cmdAbort, WsActionCommandModel cmdCancel, WsActionCommandModel cmdCustom, WsActionCommandModel cmdIgnore, 
        WsActionCommandModel cmdNo, WsActionCommandModel cmdOk, WsActionCommandModel cmdRetry, WsActionCommandModel cmdYes)
    {
        ActionAbort = cmdAbort;
        ActionCancel = cmdCancel;
        ActionCustom = cmdCustom;
        ActionIgnore = cmdIgnore;
        ActionNo = cmdNo;
        ActionOk = cmdOk;
        ActionRetry = cmdRetry;
        ActionYes = cmdYes;
        UpdateCommands();
    }

    /// <summary>
    /// Настройка действий.
    /// </summary>
    /// <param name="actionAbort"></param>
    /// <param name="actionCancel"></param>
    /// <param name="actionCustom"></param>
    /// <param name="actionIgnore"></param>
    /// <param name="actionNo"></param>
    /// <param name="actionOk"></param>
    /// <param name="actionRetry"></param>
    /// <param name="actionYes"></param>
    /// <param name="actionBack"></param>
    /// <param name="controlWidth"></param>
    public void SetupActions(Action actionAbort, Action actionCancel, Action actionCustom, Action actionIgnore, Action actionNo,
        Action actionOk, Action actionRetry, Action actionYes, Action actionBack, int controlWidth)
    {
        actionAbort += actionBack;
        actionCancel += actionBack;
        actionCustom += actionBack;
        actionIgnore += actionBack;
        actionNo += actionBack;
        actionOk += actionBack;
        actionRetry += actionBack;
        actionYes += actionBack;
        SetupActions(actionAbort, actionCancel, actionCustom, actionIgnore, actionNo, actionOk, actionRetry, actionYes);
        SetupButtonsWidth(controlWidth);
    }

    /// <summary>
    /// Настройка действий.
    /// </summary>
    /// <param name="actionAbort"></param>
    /// <param name="actionCancel"></param>
    /// <param name="actionCustom"></param>
    /// <param name="actionIgnore"></param>
    /// <param name="actionNo"></param>
    /// <param name="actionOk"></param>
    /// <param name="actionRetry"></param>
    /// <param name="actionYes"></param>
    protected void SetupActions(Action actionAbort, Action actionCancel, Action actionCustom, Action actionIgnore,
        Action actionNo, Action actionOk, Action actionRetry, Action actionYes)
    {
        ActionAbort.Setup(nameof(ActionAbort), actionAbort, LocaleCore.Buttons.Abort, Visibility.Visible);
        ActionCancel.Setup(nameof(ActionCancel), actionCancel, LocaleCore.Buttons.Cancel, Visibility.Visible);
        ActionCustom.Setup(nameof(ActionCustom), actionCustom, LocaleCore.Buttons.Custom, Visibility.Visible);
        ActionIgnore.Setup(nameof(ActionIgnore), actionIgnore, LocaleCore.Buttons.Ignore, Visibility.Visible);
        ActionNo.Setup(nameof(ActionNo), actionNo, LocaleCore.Buttons.No, Visibility.Visible);
        ActionOk.Setup(nameof(ActionOk), actionOk, LocaleCore.Buttons.Ok, Visibility.Visible);
        ActionRetry.Setup(nameof(ActionRetry), actionRetry, LocaleCore.Buttons.Retry, Visibility.Visible);
        ActionYes.Setup(nameof(ActionYes), actionYes, LocaleCore.Buttons.Yes, Visibility.Visible);
        UpdateCommands();
    }

    /// <summary>
    /// Настройка Ок.
    /// </summary>
    /// <param name="actionOk"></param>
    protected void SetupActionsOk(Action actionOk)
    {
        ActionAbort.SetupEmpty(nameof(ActionAbort), LocaleCore.Buttons.Abort, Visibility.Hidden);
        ActionCancel.SetupEmpty(nameof(ActionCancel), LocaleCore.Buttons.Cancel, Visibility.Hidden);
        ActionCustom.SetupEmpty(nameof(ActionCustom), LocaleCore.Buttons.Custom, Visibility.Hidden);
        ActionIgnore.SetupEmpty(nameof(ActionIgnore), LocaleCore.Buttons.Ignore, Visibility.Hidden);
        ActionNo.SetupEmpty(nameof(ActionNo), LocaleCore.Buttons.No, Visibility.Hidden);
        ActionOk.Setup(nameof(ActionOk), actionOk, LocaleCore.Buttons.Ok, Visibility.Visible);
        ActionRetry.SetupEmpty(nameof(ActionRetry), LocaleCore.Buttons.Retry, Visibility.Hidden);
        ActionYes.SetupEmpty(nameof(ActionYes), LocaleCore.Buttons.Yes, Visibility.Hidden);
        UpdateCommands();
    }

    /// <summary>
    /// Настройка Нет/Да.
    /// </summary>
    /// <param name="actionNo"></param>
    /// <param name="actionYes"></param>
    private void SetupActionsNoYes(Action actionNo, Action actionYes)
    {
        ActionAbort.SetupEmpty(nameof(ActionAbort), LocaleCore.Buttons.Abort, Visibility.Hidden);
        ActionCancel.SetupEmpty(nameof(ActionCancel), LocaleCore.Buttons.Cancel, Visibility.Hidden);
        ActionCustom.SetupEmpty(nameof(ActionCustom), LocaleCore.Buttons.Custom, Visibility.Hidden);
        ActionIgnore.SetupEmpty(nameof(ActionIgnore), LocaleCore.Buttons.Ignore, Visibility.Hidden);
        ActionNo.Setup(nameof(ActionNo), actionNo, LocaleCore.Buttons.No, Visibility.Visible);
        ActionOk.SetupEmpty(nameof(ActionOk), LocaleCore.Buttons.Ok, Visibility.Hidden);
        ActionRetry.SetupEmpty(nameof(ActionRetry), LocaleCore.Buttons.Retry, Visibility.Hidden);
        ActionYes.Setup(nameof(ActionYes), actionYes, LocaleCore.Buttons.Yes, Visibility.Visible);
        UpdateCommands();
    }

    /// <summary>
    /// Настройка Нет/Да.
    /// </summary>
    /// <param name="actionCancel"></param>
    /// <param name="actionYes"></param>
    private void SetupActionsCancelYes(Action actionCancel, Action actionYes)
    {
        ActionAbort.SetupEmpty(nameof(ActionAbort), LocaleCore.Buttons.Abort, Visibility.Hidden);
        ActionCancel.SetupEmpty(nameof(ActionCancel), LocaleCore.Buttons.Cancel, Visibility.Hidden);
        ActionCustom.SetupEmpty(nameof(ActionCustom), LocaleCore.Buttons.Custom, Visibility.Hidden);
        ActionIgnore.SetupEmpty(nameof(ActionIgnore), LocaleCore.Buttons.Ignore, Visibility.Hidden);
        ActionCancel.Setup(nameof(ActionCancel), actionCancel, LocaleCore.Buttons.No, Visibility.Visible);
        ActionOk.SetupEmpty(nameof(ActionOk), LocaleCore.Buttons.Ok, Visibility.Hidden);
        ActionRetry.SetupEmpty(nameof(ActionRetry), LocaleCore.Buttons.Retry, Visibility.Hidden);
        ActionYes.Setup(nameof(ActionYes), actionYes, LocaleCore.Buttons.Yes, Visibility.Visible);
        UpdateCommands();
    }

    /// <summary>
    /// Настройка.
    /// </summary>
    private void SetupActionsEmpty()
    {
        ActionAbort = new(nameof(ActionAbort), LocaleCore.Buttons.Abort, Visibility.Hidden);
        ActionCancel = new(nameof(ActionCancel), LocaleCore.Buttons.Cancel, Visibility.Hidden);
        ActionCustom = new(nameof(ActionCustom), LocaleCore.Buttons.Custom, Visibility.Hidden);
        ActionIgnore = new(nameof(ActionIgnore), LocaleCore.Buttons.Ignore, Visibility.Hidden);
        ActionNo = new(nameof(ActionNo), LocaleCore.Buttons.No, Visibility.Hidden);
        ActionOk = new(nameof(ActionOk), LocaleCore.Buttons.Ok, Visibility.Hidden);
        ActionRetry = new(nameof(ActionRetry), LocaleCore.Buttons.Retry, Visibility.Hidden);
        ActionYes = new(nameof(ActionYes), LocaleCore.Buttons.Yes, Visibility.Hidden);
        UpdateCommands();
    }

    /// <summary>
    /// Обновить список команд из действий.
    /// </summary>
    public void UpdateCommands()
    {
        Commands.Clear();
        if (ActionAbort.Action is not null)
            Commands.Add(ActionAbort);
        if (ActionCustom.Action is not null)
            Commands.Add(ActionCustom);
        if (ActionIgnore.Action is not null)
            Commands.Add(ActionIgnore);
        if (ActionNo.Action is not null)
            Commands.Add(ActionNo);
        if (ActionOk.Action is not null)
            Commands.Add(ActionOk);
        if (ActionRetry.Action is not null)
            Commands.Add(ActionRetry);
        if (ActionYes.Action is not null)
            Commands.Add(ActionYes);
        if (ActionCancel.Action is not null)
            Commands.Add(ActionCancel);
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
                ActionCancel.Relay();
                break;
            case Key.Enter:
                ActionOk.Relay();
                break;
        }
    }

    /// <summary>
    /// Настройка ширины кнопок.
    /// </summary>
    /// <param name="controlWidth"></param>
    public void SetupButtonsWidth(int controlWidth) => ButtonWidth = !Commands.Any() ? controlWidth - 22 : controlWidth / Commands.Count - 22;

    /// <summary>
    /// Настройка кнопок Нет/Да.
    /// </summary>
    /// <param name="actionNo"></param>
    /// <param name="actionYes"></param>
    /// <param name="actionBack"></param>
    /// <param name="controlWidth"></param>
    protected void SetupButtonsNoYes(Action actionNo, Action actionYes, Action actionBack, int controlWidth)
    {
        actionNo += actionBack;
        actionYes += actionBack;
        SetupActionsNoYes(actionNo, actionYes);
        SetupButtonsWidth(controlWidth);
    }

    /// <summary>
    /// Настройка кнопок Отмена/Да.
    /// </summary>
    /// <param name="actionCancel"></param>
    /// <param name="actionYes"></param>
    /// <param name="actionBack"></param>
    /// <param name="controlWidth"></param>
    protected void SetupButtonsCancelYes(Action actionCancel, Action actionYes, Action actionBack, int controlWidth)
    {
        actionCancel += actionBack;
        actionYes += actionBack;
        SetupActionsCancelYes(actionCancel, actionYes);
        SetupButtonsWidth(controlWidth);
    }

    /// <summary>
    /// Настройка кнопки Ок.
    /// </summary>
    /// <param name="actionOk"></param>
    /// <param name="controlWidth"></param>
    public void SetupButtonsOk(Action actionOk, int controlWidth)
    {
        SetupActionsOk(actionOk);
        SetupButtonsWidth(controlWidth);
    }

    #endregion
}