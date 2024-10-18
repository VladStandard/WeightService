using Ws.Desktop.Models.Features.PalletMen;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace ScalesDesktop.Source.Shared.Api.Desktop.Endpoints;

public class PalletEndpoints(IDesktopApi desktopApi)
{
    public Endpoint<PiecePalletsArgs, PalletInfo[]> PiecePalletsEndpoint { get; } = new(
        value => desktopApi.GetPalletsByArm(value.StartDt, value.EndDt),
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });

    public Endpoint<LabelEndpointArgs, LabelInfo[]> PalletLabelsEndpoint { get; } = new(
        value => desktopApi.GetPalletLabels(value.PalletUid),
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(30) });

    public Endpoint<PiecePalletsNumberArgs, PalletInfo[]> PiecePalletsNumberEndpoint { get; } = new(
        value => desktopApi.GetPalletByNumber(value.Number),
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) }
    );

    public void InsertPiecePallet(PiecePalletsArgs args, PalletInfo data) =>
        PiecePalletsEndpoint.UpdateQueryData(args, q =>
        {
            if (q.Data == null) return q.Data!;
            IEnumerable<PalletInfo> newData = q.Data.Prepend(data);
            return newData.ToArray();
        });
}

public record LabelEndpointArgs(Guid PalletUid);

public record PiecePalletsArgs(DateTime? StartDt, DateTime? EndDt);

public record PiecePalletsNumberArgs(uint Number);