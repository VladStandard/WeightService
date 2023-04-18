// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsWebApiCore.Helpers;

namespace WsWebApiCore.Base;

public class WsControllerBase : WsContentBase
{
    #region Public and private fields, properties, constructor

    protected WsBrandsHelper WsBrands { get; }
    protected WsPlusCharacteristicsHelper WsPlusCharacteristics { get; }
    protected WsPlusHelper WsPlus { get; }
    protected WsPlusGroupsHelper WsPlusGroups { get; }

    public WsControllerBase(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        WsBrands = new(sessionFactory);
        WsPlusCharacteristics = new(sessionFactory);
        WsPlus = new(sessionFactory);
        WsPlusGroups = new(sessionFactory);
    }

    #endregion
}