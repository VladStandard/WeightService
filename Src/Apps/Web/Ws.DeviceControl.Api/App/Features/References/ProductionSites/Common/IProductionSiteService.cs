using Ws.DeviceControl.Models.Features.References.ProductionSites.Commands.Create;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Commands.Update;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.ProductionSites.Common;

public interface IProductionSiteService : IGetApiService<ProductionSiteDto>, IDeleteService<Guid>
{
    #region Queries

    Task<ProxyDto> GetProxyByUser();
    Task<List<ProxyDto>> GetProxiesAsync();

    #endregion

    #region Commands

    Task<ProductionSiteDto> CreateAsync(ProductionSiteCreateDto dto);
    Task<ProductionSiteDto> UpdateAsync(Guid id, ProductionSiteUpdateDto dto);

    #endregion
}