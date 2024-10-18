using Microsoft.AspNetCore.Components;
using Ws.Tablet.Models.Features.Pallets.Output;

namespace ScalesTablet.Source.Features.CreatePalletDialog;

public record CreateDialogProps
{
    public string DocumentNumber { get; init; } = string.Empty;
    public EventCallback<PalletDto> OnPalletCreated { get; init; }
}