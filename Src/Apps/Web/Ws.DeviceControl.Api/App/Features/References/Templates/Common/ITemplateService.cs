namespace Ws.DeviceControl.Api.App.Features.References.Templates.Common;

public interface ITemplateService
{
    #region Queries

    Task<List<ProxyDto>> GetProxiesByPluAsync(Guid pluId);

    #endregion
}