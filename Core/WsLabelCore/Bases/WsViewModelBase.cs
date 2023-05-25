// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Bases;

/// <summary>
/// Базовый класс модели представления.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public partial class WsViewModelBase : WsMvvmViewModelBase, INotifyPropertyChanged
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

    protected WsViewModelBase()
    {
        SetupEmpty();
    }

    protected WsViewModelBase(WsActionCommandModel cmdAbort, WsActionCommandModel cmdCancel, WsActionCommandModel cmdCustom, WsActionCommandModel cmdIgnore,
        WsActionCommandModel cmdNo, WsActionCommandModel cmdOk, WsActionCommandModel cmdRetry, WsActionCommandModel cmdYes)
    {
        Setup(cmdAbort, cmdCancel, cmdCustom, cmdIgnore, cmdNo, cmdOk, cmdRetry, cmdYes);
    }

    protected WsViewModelBase(Action actionAbort, Action actionCancel, Action actionCustom, Action actionIgnore,
        Action actionNo, Action actionOk, Action actionRetry, Action actionYes)
    {
        Setup(actionAbort, actionCancel, actionCustom, actionIgnore, actionNo, actionOk, actionRetry, actionYes);
    }

    protected WsViewModelBase(Action actionNo, Action actionYes)
    {
        SetupNoYes(actionNo, actionYes);
    }

    protected WsViewModelBase(Action actionOk)
    {
        SetupOk(actionOk);
    }

    #endregion

    #region Public and private methods - Commands

    /// <summary>
    /// Прервать.
    /// </summary>
    [RelayCommand]
    public void RelayAbort() => ActionAbort.Action?.Invoke();


    /// <summary>
    /// Отменить.
    /// </summary>
    [RelayCommand]
    public void RelayCancel() => ActionCancel.Action?.Invoke();


    /// <summary>
    /// Настроить.
    /// </summary>
    [RelayCommand]
    public void RelayCustom() => ActionCustom.Action?.Invoke();

    /// <summary>
    /// Игнорировать.
    /// </summary>
    [RelayCommand]
    public void RelayIgnore() => ActionIgnore.Action?.Invoke();

    /// <summary>
    /// Нет.
    /// </summary>
    [RelayCommand]
    public void RelayNo() => ActionNo.Action?.Invoke();

    /// <summary>
    /// Ок.
    /// </summary>
    [RelayCommand]
    public void RelayOk() => ActionOk.Action?.Invoke();

    /// <summary>
    /// Повторить.
    /// </summary>
    [RelayCommand]
    public void RelayRetry()
    {
        ActionRetry.Action?.Invoke();
    }

    /// <summary>
    /// Да.
    /// </summary>
    [RelayCommand]
    public void RelayYes()
    {
        ActionYes.Action?.Invoke();
    }

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
    protected void Setup(WsActionCommandModel cmdAbort, WsActionCommandModel cmdCancel, WsActionCommandModel cmdCustom, WsActionCommandModel cmdIgnore, 
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
        
        SetupCommands();
    }

    /// <summary>
    /// Настройка всех действий.
    /// </summary>
    /// <param name="actionAbort"></param>
    /// <param name="actionCancel"></param>
    /// <param name="actionCustom"></param>
    /// <param name="actionIgnore"></param>
    /// <param name="actionNo"></param>
    /// <param name="actionOk"></param>
    /// <param name="actionRetry"></param>
    /// <param name="actionYes"></param>
    protected void Setup(Action actionAbort, Action actionCancel, Action actionCustom, Action actionIgnore,
        Action actionNo, Action actionOk, Action actionRetry, Action actionYes)
    {
        ActionAbort = new(nameof(ActionAbort), actionAbort, LocaleCore.Buttons.Abort, Visibility.Visible);
        ActionCancel = new(nameof(ActionCancel), actionCancel, LocaleCore.Buttons.Cancel, Visibility.Visible);
        ActionCustom = new(nameof(ActionCustom), actionCustom, LocaleCore.Buttons.Custom, Visibility.Visible);
        ActionIgnore = new(nameof(ActionIgnore), actionIgnore, LocaleCore.Buttons.Custom, Visibility.Visible);
        ActionNo = new(nameof(ActionNo), actionNo, LocaleCore.Buttons.Custom, Visibility.Visible);
        ActionOk = new(nameof(ActionOk), actionOk, LocaleCore.Buttons.Custom, Visibility.Visible);
        ActionRetry = new(nameof(ActionRetry), actionRetry, LocaleCore.Buttons.Custom, Visibility.Visible);
        ActionYes = new(nameof(ActionYes), actionYes, LocaleCore.Buttons.Custom, Visibility.Visible);

        SetupCommands();
    }

    /// <summary>
    /// Настройка Ок.
    /// </summary>
    /// <param name="actionOk"></param>
    protected void SetupOk(Action actionOk)
    {
        ActionAbort = new(nameof(ActionAbort), LocaleCore.Buttons.Abort, Visibility.Hidden);
        ActionCancel = new(nameof(ActionCancel), LocaleCore.Buttons.Cancel, Visibility.Hidden);
        ActionCustom = new(nameof(ActionCustom), LocaleCore.Buttons.Custom, Visibility.Hidden);
        ActionIgnore = new(nameof(ActionIgnore), LocaleCore.Buttons.Custom, Visibility.Hidden);
        ActionNo = new(nameof(ActionNo), LocaleCore.Buttons.Custom, Visibility.Hidden);
        ActionOk = new(nameof(ActionOk), actionOk, LocaleCore.Buttons.Custom, Visibility.Visible);
        ActionRetry = new(nameof(ActionRetry), LocaleCore.Buttons.Custom, Visibility.Hidden);
        ActionYes = new(nameof(ActionYes), LocaleCore.Buttons.Custom, Visibility.Hidden);

        SetupCommands();
    }

    /// <summary>
    /// Настройка Нет/Да.
    /// </summary>
    /// <param name="actionNo"></param>
    /// <param name="actionYes"></param>
    protected void SetupNoYes(Action actionNo, Action actionYes)
    {
        ActionAbort = new(nameof(ActionAbort), LocaleCore.Buttons.Abort, Visibility.Hidden);
        ActionCancel = new(nameof(ActionCancel), LocaleCore.Buttons.Cancel, Visibility.Hidden);
        ActionCustom = new(nameof(ActionCustom), LocaleCore.Buttons.Custom, Visibility.Hidden);
        ActionIgnore = new(nameof(ActionIgnore), LocaleCore.Buttons.Custom, Visibility.Hidden);
        ActionNo = new(nameof(ActionNo), actionNo, LocaleCore.Buttons.Custom, Visibility.Visible);
        ActionOk = new(nameof(ActionOk), LocaleCore.Buttons.Custom, Visibility.Hidden);
        ActionRetry = new(nameof(ActionRetry), LocaleCore.Buttons.Custom, Visibility.Hidden);
        ActionYes = new(nameof(ActionYes), actionYes, LocaleCore.Buttons.Custom, Visibility.Visible);

        SetupCommands();
    }

    /// <summary>
    /// Настройка.
    /// </summary>
    private void SetupEmpty()
    {
        ActionAbort = new(nameof(ActionAbort), () => { }, LocaleCore.Buttons.Abort, Visibility.Hidden);
        ActionCancel = new(nameof(ActionCancel), () => { }, LocaleCore.Buttons.Cancel, Visibility.Hidden);
        ActionCustom = new(nameof(ActionCustom), () => { }, LocaleCore.Buttons.Custom, Visibility.Hidden);
        ActionIgnore = new(nameof(ActionIgnore), () => { }, LocaleCore.Buttons.Custom, Visibility.Hidden);
        ActionNo = new(nameof(ActionNo), () => { }, LocaleCore.Buttons.Custom, Visibility.Hidden);
        ActionOk = new(nameof(ActionOk), () => { }, LocaleCore.Buttons.Custom, Visibility.Hidden);
        ActionRetry = new(nameof(ActionRetry), () => { }, LocaleCore.Buttons.Custom, Visibility.Hidden);
        ActionYes = new(nameof(ActionYes), () => { }, LocaleCore.Buttons.Custom, Visibility.Hidden);

        SetupCommands();
    }

    /// <summary>
    /// Настройка списка команд.
    /// </summary>
    private void SetupCommands()
    {
        Commands.Clear();
        
        if (ActionAbort.Visibility.Equals(Visibility.Visible))
            Commands.Add(ActionAbort);
        if (ActionCustom.Visibility.Equals(Visibility.Visible))
            Commands.Add(ActionCustom);
        if (ActionIgnore.Visibility.Equals(Visibility.Visible))
            Commands.Add(ActionIgnore);
        if (ActionNo.Visibility.Equals(Visibility.Visible))
            Commands.Add(ActionNo);
        if (ActionOk.Visibility.Equals(Visibility.Visible))
            Commands.Add(ActionOk);
        if (ActionRetry.Visibility.Equals(Visibility.Visible))
            Commands.Add(ActionRetry);
        if (ActionYes.Visibility.Equals(Visibility.Visible))
            Commands.Add(ActionYes);
        if (ActionCancel.Visibility.Equals(Visibility.Visible))
            Commands.Add(ActionCancel);
    }

    #endregion
}