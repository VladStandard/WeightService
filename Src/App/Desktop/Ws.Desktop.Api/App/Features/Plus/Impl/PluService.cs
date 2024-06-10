using Ws.Database.EntityFramework;
using Ws.Desktop.Api.App.Features.Plus.Common;
using Ws.Desktop.Models.Common;
using Ws.Desktop.Models.Features.Plus.Output;

namespace Ws.Desktop.Api.App.Features.Plus.Impl;

public class PluService : IPluService
{
    public OutputDto<List<PluWeight>> GetAllWeightByArm(Guid uid)
    {
        using var context = new WsDbContext();

        List<Guid> pluUidList = context.Lines
            .Where(i => i.Id == uid)
            .SelectMany(i => i.Plus.Where(p => p.IsWeight == true).Select(p => p.Id))
            .DefaultIfEmpty()
            .ToList();

        List<PluWeight> data = context.Plus
            .Where(p => pluUidList.Contains(p.Id))
            .Join(context.Nestings,
                plu => plu.Id,
                nesting => nesting.Id,
                (plu, nesting) => new PluWeight
                {
                    Id = plu.Id,
                    Name = plu.Name,
                    FullName = plu.FullName,
                    Number = (ushort)Math.Abs(plu.Number),
                    BundleCount = (byte)nesting.BundleCount,
                    Box = nesting.Box.Name,
                    Bundle = plu.Bundle.Name,
                    TareWeight = (decimal)Math.Round((double)(plu.Weight + plu.Clip.Weight + plu.Bundle.Weight) * nesting.BundleCount + (double)nesting.Box.Weight, 3)
                })
            .ToList();

        return new (data);
    }
}