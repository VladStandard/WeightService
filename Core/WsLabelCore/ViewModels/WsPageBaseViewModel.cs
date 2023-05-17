// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

public class WsPageBaseViewModel : BaseViewModel
{
    #region Public and private fields, properties, constructor

    protected WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    protected WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;

    #endregion
}