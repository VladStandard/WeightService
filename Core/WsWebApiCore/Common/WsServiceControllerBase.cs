// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Common;

public class WsServiceControllerBase : ControllerBase
{
    #region Public and private fields, properties, constructor

    protected ISessionFactory SessionFactory { get; }
    public WsServiceControllerBase(ISessionFactory sessionFactory) => SessionFactory = sessionFactory;

    #endregion

    #region Public and private methods

    public void Dispose()
    {
        SessionFactory.Dispose();
    }

    #endregion
}