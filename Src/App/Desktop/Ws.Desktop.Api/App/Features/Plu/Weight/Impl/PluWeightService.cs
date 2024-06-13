using Ws.Database.EntityFramework;
using Ws.Desktop.Api.App.Features.Plu.Weight.Common;
using Ws.Desktop.Models.Features.Labels.Input;
using Ws.Desktop.Models.Features.Labels.Output;
using Ws.Desktop.Models.Features.Plus.Weight.Output;
using Ws.Domain.Services.Features.Arms;
using Ws.Domain.Services.Features.Plus;
using Ws.Labels.Service.Features.Generate;
using Ws.Labels.Service.Features.Generate.Features.Weight.Dto;

namespace Ws.Desktop.Api.App.Features.Plu.Weight.Impl;

public class PluWeightService(
    IPrintLabelService printLabelService,
    IPluService pluService,
    IArmService armService
    ) : IPluWeightService
{
    public List<PluWeight> GetAllWeightByArm(Guid uid)
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

    public WeightLabel GenerateLabel(Guid armId, Guid pluId, CreateWeightLabelDto dto)
    {
        var line = armService.GetItemByUid(armId);

        GenerateWeightLabelDto dtoToCreate = new()
        {
            Plu = pluService.GetItemByUid(pluId),
            Line = line,
            Weight = dto.WeightNet,
            Kneading = (short)dto.Kneading,
            ProductDt = dto.ProductDt
        };

        line.Counter += 1;
        armService.Update(line);

        var label = printLabelService.GenerateWeightLabel(dtoToCreate);
        return new() { ArmCounter = (uint)line.Counter, Zpl = label.Zpl };
    }
}