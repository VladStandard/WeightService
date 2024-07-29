using Phetch.Core;
using Ws.DeviceControl.Models;
using Ws.DeviceControl.Models.Dto.References.Template.Queries;
using Ws.DeviceControl.Models.Dto.Shared;
using Ws.Shared.Extensions;

namespace DeviceControl.Source.Shared.Services;

public class PrintSettingsEndpoints(IWebApi webApi)
{
    # region Template

    public ParameterlessEndpoint<TemplateDto[]> TemplatesEndpoint { get; } = new(
        webApi.GetTemplates,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddTemplate(TemplateDto template)
    {
        TemplatesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? query.Data! : query.Data.Prepend(template).ToArray());
        AddProxyTemplate(template.IsWeight, new() { Id = template.Id, Name = template.Name });
    }

    public void UpdateTemplate(TemplateDto template)
    {
        TemplatesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? query.Data! : query.Data.ReplaceItemByKey(template, p => p.Id == template.Id).ToArray());
        UpdateProxyTemplate(template.IsWeight, new() { Id = template.Id, Name = template.Name });
    }

    public void DeleteTemplate(bool isWeight, Guid templateId)
    {
        TemplatesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? query.Data! : query.Data.Where(x => x.Id != templateId).ToArray());
        DeleteProxyTemplate(isWeight, templateId);
    }

    # endregion

    # region Proxy Template

    public Endpoint<bool, ProxyDto[]> ProxyTemplatesEndpoint { get; } = new(
        webApi.GetProxyTemplatesByPluType,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddProxyTemplate(bool isWeight, ProxyDto proxyTemplate) =>
        ProxyTemplatesEndpoint.UpdateQueryData(isWeight, query =>
            query.Data == null ? query.Data! : query.Data.Prepend(proxyTemplate).ToArray());

    public void UpdateProxyTemplate(bool isWeight, ProxyDto proxyTemplate) =>
        ProxyTemplatesEndpoint.UpdateQueryData(isWeight, query =>
            query.Data == null ? query.Data! : query.Data.ReplaceItemByKey(proxyTemplate, p => p.Id == proxyTemplate.Id).ToArray());

    public void DeleteProxyTemplate(bool isWeight, Guid proxyTemplateId) =>
        ProxyTemplatesEndpoint.UpdateQueryData(isWeight, query =>
            query.Data == null ? query.Data! : query.Data.Where(x => x.Id != proxyTemplateId).ToArray());

    # endregion
}
