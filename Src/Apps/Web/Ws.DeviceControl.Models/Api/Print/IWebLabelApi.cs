using Ws.DeviceControl.Models.Features.Print.Labels;

namespace Ws.DeviceControl.Models.Api.Print;

public interface IWebLabelApi
{
    #region Queries

    [Get("/labels")]
    Task<LabelDto[]> GetLabels();

    [Get("/labels/{uid}")]
    Task<LabelDto> GetLabelByUid(Guid uid);

    [Get("/labels/{uid}/zpl")]
    Task<ZplDto> GetLabelZplByUid(Guid uid);

    #endregion
}