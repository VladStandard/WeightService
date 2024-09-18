using Ws.DeviceControl.Models.Features.References.Template.Commands.Create;
using Ws.DeviceControl.Models.Features.References.Template.Commands.Update;
using Ws.DeviceControl.Models.Features.References.Template.Queries;
using Ws.DeviceControl.Models.Features.References.Template.Universal;

namespace Ws.DeviceControl.Api.App.Features.References.Templates.Common;

public interface ITemplateService : IGetApiService<TemplateDto>, IDeleteService<Guid>
{
    #region Queries

    Task<List<ProxyDto>> GetProxiesByIsWeightAsync(bool isWeight);
    Task<TemplateBodyDto> GetBodyByIdAsync(Guid id);
    Task<List<BarcodeVarDto>> GetBarcodeVariables();
    Task<BarcodeItemWrapper> GetBarcodeTemplates(Guid id);

    #endregion

    #region Commands

    Task<TemplateDto> UpdateAsync(Guid id, TemplateUpdateDto dto);

    Task<TemplateDto> CreateAsync(TemplateCreateDto dto);

    #endregion
}