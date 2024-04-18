using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Localization;
using Ws.Domain.Models.Entities.Ref1c;

namespace ScalesDesktop.Source.Features.PluSelectDialog;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class PluSelectDialog : ComponentBase, IDialogContentComponent<PluDialogContent>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Parameter] public PluDialogContent Content { get; set; } = default!;
    [CascadingParameter] public FluentDialog Dialog { get; set; } = default!;
}

public record PluDialogContent
{
    public IQueryable<PluEntity> Data { get; init; } = new List<PluEntity>().AsQueryable();
}