using System.Net.Http.Json;
using Ws.Desktop.Models.Common;
using Ws.Desktop.Models.Features.Arms.Output;
using Ws.Desktop.Models.Features.Labels.Input;
using Ws.Desktop.Models.Features.Labels.Output;
using Ws.Desktop.Models.Features.PalletMen;
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
        OutputDto<PluWeight[]> data = await httpClient.GetFromJsonAsync<OutputDto<PluWeight[]>>($"arms/{armUid}/plu/weight/") ?? throw new IOException("No PLU found");
        return data.Data;
    }

    public async Task<WeightLabel> CreatePluWeightLabel(Guid armUid, Guid pluUid, CreateWeightLabelDto createDto)
    {
        HttpResponseMessage response = await httpClient.PostAsJsonAsync($"arms/{armUid}/plu/weight/{pluUid}/label", createDto);
        if (!response.IsSuccessStatusCode) throw new IOException("Unable to create PLU label");
        OutputDto<WeightLabel> data = await response.Content.ReadFromJsonAsync<OutputDto<WeightLabel>>() ?? throw new IOException("Failed to deserialize response");
        return data.Data;
    }

    public async Task<PalletMan[]> GetPalletMen()
    {
        OutputDto<PalletMan[]> data = await httpClient.GetFromJsonAsync<OutputDto<PalletMan[]>>("pallet-men") ?? throw new IOException("No pallet men found");
        return data.Data;
    }
}