// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsWebApi.Controllers;

namespace WsWebApi.Base;

public class WsControllerBase : WsContentBase
{
    #region Public and private fields, properties, constructor

    public WsBrandsController BrandsControl { get; }
    public WsPlusCharacteristicsController PlusCharacteristicsControl { get; }
    public WsPlusController PlusControl { get; }
    public WsPlusGroupsController PlusGroupsControl { get; }

    protected WsControllerBase(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        BrandsControl = new(sessionFactory);
        PlusCharacteristicsControl = new(sessionFactory);
        PlusControl = new(sessionFactory);
        PlusGroupsControl = new(sessionFactory);
    }

    #endregion
}