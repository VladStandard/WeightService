using Phetch.Core;
using Ws.Desktop.Models;
using Ws.Desktop.Models.Features.PalletMen;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace ScalesDesktop.Source.Shared.Services;

public class PalletApi(IDesktopApi desktopApi)
{
    public ParameterlessEndpoint<PalletMan[]> PalletMenEndpoint { get; } = new(
        desktopApi.GetPalletMen,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });

    public Endpoint<Guid, PalletInfo[]> PiecePalletsEndpoint { get; } = new(
        desktopApi.GetPalletsByArm,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });

    public Endpoint<LabelEndpointArgs, LabelInfo[]> PalletLabelsEndpoint { get; } = new(
        value => desktopApi.GetPalletLabels(value.ArmUid, value.PalletUid),
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(30) });

    public void InsertPiecePallet(Guid armUid, PalletInfo data) =>
        PiecePalletsEndpoint.UpdateQueryData(armUid, q =>
        {
            if (q.Data == null) return q.Data!;
            IEnumerable<PalletInfo> newData = q.Data.Prepend(data);
            return newData.ToArray();
        });
}

public record LabelEndpointArgs (Guid ArmUid, Guid PalletUid);