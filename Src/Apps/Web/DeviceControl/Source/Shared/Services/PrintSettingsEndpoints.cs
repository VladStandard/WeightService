using Phetch.Core;
using Ws.DeviceControl.Models;
using Ws.DeviceControl.Models.Features.References.Template.Queries;
using Ws.DeviceControl.Models.Features.References.TemplateResources.Queries;
using Ws.DeviceControl.Models.Shared;
using Ws.Shared.Extensions;

namespace DeviceControl.Source.Shared.Services;

public class PrintSettingsEndpoints(IWebApi webApi)
{
    # region Template

    public ParameterlessEndpoint<TemplateDto[]> TemplatesEndpoint { get; } = new(
        webApi.GetTemplates,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddTemplate(TemplateDto template, string body)
    {
        TemplatesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? query.Data! : query.Data.Prepend(template).ToArray());
        AddProxyTemplate(template.IsWeight, new() { Id = template.Id, Name = template.Name });
        AddTemplateBody(template.Id, body);
    }

    public void UpdateTemplate(TemplateDto template, string body)
    {
        TemplatesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? query.Data! : query.Data.ReplaceItemByKey(template, p => p.Id == template.Id).ToArray());
        UpdateProxyTemplate(template.IsWeight, new() { Id = template.Id, Name = template.Name });
        UpdateTemplateBody(template.Id, body);
    }

    public void DeleteTemplate(bool isWeight, Guid templateId)
    {
        TemplatesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? query.Data! : query.Data.Where(x => x.Id != templateId).ToArray());
        DeleteProxyTemplate(isWeight, templateId);
        DeleteTemplateBody(templateId);
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

    # region Template Body

    public Endpoint<Guid, string> TemplateBodyEndpoint { get; } = new(
        async value => (await webApi.GetTemplateBody(value)).Body,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddTemplateBody(Guid templateId, string body) =>
        TemplateBodyEndpoint.UpdateQueryData(templateId, query => query.Data == null ? query.Data! : body);

    public void UpdateTemplateBody(Guid templateId, string body) =>
        TemplateBodyEndpoint.UpdateQueryData(templateId, query => query.Data == null ? query.Data! : body);

    public void DeleteTemplateBody(Guid templateId) =>
        TemplateBodyEndpoint.Invalidate(templateId);

    # endregion

    # region Resource

    public ParameterlessEndpoint<TemplateResourceDto[]> ResourcesEndpoint { get; } = new(
        webApi.GetResources,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddResource(TemplateResourceDto resource, string body)
    {
        ResourcesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? query.Data! : query.Data.Prepend(resource).ToArray());
        AddResourceBody(resource.Id, body);
    }

    public void UpdateResource(TemplateResourceDto resource, string body)
    {
        ResourcesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? query.Data! : query.Data.ReplaceItemByKey(resource, p => p.Id == resource.Id).ToArray());
        UpdateResourceBody(resource.Id, body);
    }

    public void DeleteResource(Guid templateId)
    {
        ResourcesEndpoint.UpdateQueryData(new(), query =>
            query.Data == null ? query.Data! : query.Data.Where(x => x.Id != templateId).ToArray());
        DeleteResourceBody(templateId);
    }

    # endregion

    # region Resource Body

    public Endpoint<Guid, string> ResourceBodyEndpoint { get; } = new(
        async value => (await webApi.GetTemplateResourceBody(value)).Body,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void AddResourceBody(Guid resourceId, string body) =>
        ResourceBodyEndpoint.UpdateQueryData(resourceId, query => query.Data == null ? query.Data! : body);

    public void UpdateResourceBody(Guid resourceId, string body) =>
        ResourceBodyEndpoint.UpdateQueryData(resourceId, query => query.Data == null ? query.Data! : body);

    public void DeleteResourceBody(Guid resourceId) =>
        ResourceBodyEndpoint.Invalidate(resourceId);

    # endregion
}
