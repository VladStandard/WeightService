using DeviceControl2.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.StorageMethod;
using Ws.Domain.Services.Features.Template;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.PrintSettings.StorageMethods;

public sealed partial class StorageMethodsCreateForm: SectionFormBase<StorageMethodEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStorageMethodService StorageMethodService { get; set; } = default!;

    #endregion

    protected override StorageMethodEntity CreateItemAction(StorageMethodEntity item) =>
        StorageMethodService.Create(item);
}

public class StorageMethodsCreateFormValidator : AbstractValidator<StorageMethodEntity>
{
    public StorageMethodsCreateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.Zpl).NotEmpty();
    }
}
