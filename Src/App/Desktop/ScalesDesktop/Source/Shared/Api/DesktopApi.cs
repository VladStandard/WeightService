using System.Net.Http.Json;
using Ws.Desktop.Models.Common;
using Ws.Desktop.Models.Features.Arms.Output;
using Ws.Desktop.Models.Features.Plus.Output;

namespace ScalesDesktop.Source.Shared.Api;

internal sealed class DesktopApi(HttpClient httpClient) : IDesktopApi
{
    public async Task<ArmValue> GetArmByName(string armName)
    {
        OutputDto<ArmValue> data = await httpClient.GetFromJsonAsync<OutputDto<ArmValue>>($"arms?name={armName}") ?? throw new IOException("No arm found");
        return data.Data;
    }

    public async Task<PluWeight[]> GetPlusByArm(Guid armUid)
    {
        OutputDto<PluWeight[]> data = await httpClient.GetFromJsonAsync<OutputDto<PluWeight[]>>($"plu/weight/{armUid}") ?? throw new IOException("No PLU found");
        return data.Data;
    }
}