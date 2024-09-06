using Ws.Desktop.Models.Features.Arms.Input;
using Ws.Desktop.Models.Features.Arms.Output;

namespace Ws.Desktop.Api.App.Features.Arms.Common;

public interface IArmService
{
    #region Queries

    public ArmValue? Get();

    #endregion

    #region Commands

    public bool Update(UpdateArmDto dto);

    #endregion
}