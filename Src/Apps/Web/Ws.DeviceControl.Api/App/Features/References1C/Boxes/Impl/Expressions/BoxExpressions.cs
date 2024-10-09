using Ws.Database.Entities.Ref1C.Boxes;

namespace Ws.DeviceControl.Api.App.Features.References1C.Boxes.Impl.Expressions;

public static class BoxExpressions
{
    public static Expression<Func<BoxEntity, PackageDto>> ToDto =>
        box => new()
        {
            Id = box.Id,
            Name = box.Name,
            Weight = box.Weight,
            CreateDt = box.CreateDt,
            ChangeDt = box.ChangeDt
        };
}