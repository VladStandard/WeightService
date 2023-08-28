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