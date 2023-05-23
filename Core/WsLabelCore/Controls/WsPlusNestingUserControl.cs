// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Controls;

#nullable enable
public sealed partial class WsPlusNestingUserControl : WsBaseUserControl
{
    #region Public and private fields, properties, constructor

    private ElementHost ElementHost { get; }
    public WsPluNestingViewModel ViewModel { get; }
    private WsPluNestingPage Page { get; }

    public WsPlusNestingUserControl()
    {
        InitializeComponent();
        
        ViewModel = new();
        Page = new(ViewModel);
        ElementHost = new() { Dock = DockStyle.Fill };
        ElementHost.Child = Page;
        Controls.Add(ElementHost);
    }

    #endregion

    #region Public and private methods

    public override void RefreshAction()
    {
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            // Обновить локальный кэш.
            ContextCache.LoadLocalViewPlusNesting((ushort)LabelSession.PluScale.Plu.Number);
            // 
            ViewModel.PluNesting = LabelSession.ViewPluNesting;
            ViewModel.PlusNestings = ContextCache.LocalViewPlusNesting;
        });
    }

    #endregion
}