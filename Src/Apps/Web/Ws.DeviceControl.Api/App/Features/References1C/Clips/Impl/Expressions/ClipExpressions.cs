using Ws.Database.EntityFramework.Entities.Ref1C.Clips;
using Ws.DeviceControl.Models.Shared;

namespace Ws.DeviceControl.Api.App.Features.References1C.Clips.Impl.Expressions;

public static class ClipExpressions
{
    public static Expression<Func<ClipEntity, PackageDto>> ToDto =>
        clip => new()
        {
            Id = clip.Id,
            Name = clip.Name,
            Weight = clip.Weight,
            CreateDt = clip.CreateDt,
            ChangeDt = clip.ChangeDt
        };
}