using Ws.Desktop.Models;
using Ws.Desktop.Models.Features.PalletMen;
using Ws.Desktop.Models.Features.Pallets.Output;
using Ws.Shared.Extensions;

namespace ScalesDesktop.Source.Shared.Services.Endpoints;

public class PalletEndpoints(IDesktopApi desktopApi)
{
    public Endpoint<Guid, PalletMan[]> PalletMenEndpoint { get; } = new(
        desktopApi.GetPalletMenByArm,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });

    public Endpoint<PiecePalletsArgs, PalletInfo[]> PiecePalletsEndpoint { get; } = new(
        value => desktopApi.GetPalletsByArm(value.ArmUid, value.StartDt, value.EndDt),
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) });

    public Endpoint<LabelEndpointArgs, LabelInfo[]> PalletLabelsEndpoint { get; } = new(
        value => desktopApi.GetPalletLabels(value.ArmUid, value.PalletUid),
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(30) });

    public Endpoint<PiecePalletsNumberArgs, PalletInfo[]> PiecePalletsNumberEndpoint { get; } = new(
        value => desktopApi.GetPalletByNumber(value.ArmUid, value.Number),
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(5) }
    );

    public void InsertPiecePallet(PiecePalletsArgs args, PalletInfo data) =>
        PiecePalletsEndpoint.UpdateQueryData(args, q =>
        {
            if (q.Data == null) return q.Data!;
            IEnumerable<PalletInfo> newData = q.Data.Prepend(data);
            return newData.ToArray();
        });

    public void SwitchPiecePalletDeleteFlag(PiecePalletsArgs args, Guid palletId) =>
        PiecePalletsEndpoint.UpdateQueryData(args, q =>
        {
            if (q.Data == null) return q.Data!;
            PalletInfo? item = q.Data.FirstOrDefault(x => x.Id == palletId);
            if (item == null) return q.Data;
            PalletInfo newItem = item with { DeletedAt = item.DeletedAt == null ? DateTime.Now : null };
            return q.Data.ReplaceItemByKey(newItem, x => x.Id == palletId).ToArray();
        });
}

public record LabelEndpointArgs(Guid ArmUid, Guid PalletUid);

public record PiecePalletsArgs(Guid ArmUid, DateTime? StartDt, DateTime? EndDt);

public record PiecePalletsNumberArgs(Guid ArmUid, uint Number);