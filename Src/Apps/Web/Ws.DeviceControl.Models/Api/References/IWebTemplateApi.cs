namespace Ws.DeviceControl.Models.Api.References;

public interface IWebTemplateApi
{
    #region Queries

    [Get("/templates/proxy?plu={pluUid}")]
    Task<ProxyDto[]> GetProxyTemplatesByPlu(Guid pluUid);

    #endregion
}