using Ws.Database.EntityFramework;
using Ws.Desktop.Api.App.Features.Plu.Common;
using Ws.Desktop.Models.Features.Labels.Input;
using Ws.Desktop.Models.Features.Labels.Output;
using Ws.Desktop.Models.Features.Plus.Weight.Output;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Arms;
using Ws.Domain.Services.Features.Plus;
using Ws.Labels.Service.Generate;
using Ws.Labels.Service.Generate.Features.Weight.Dto;

namespace Ws.Desktop.Api.App.Features.Plu.Impl.Weight;

public class PluWeightApiService(
    IPrintLabelService printLabelService,
    IPluService pluService,
    IArmService armService,
    WsDbContext dbContext
    ) : IPluWeightService
{
    #region Queries

    public List<PluWeight> GetAllWeightByArm(Guid uid)
    {
        List<Guid> pluUidList = dbContext.Lines
            .Where(i => i.Id == uid)
            .SelectMany(i => i.Plus.Where(p => p.IsWeight == true).Select(p => p.Id))
            .DefaultIfEmpty()
            .ToList();

        List<PluWeight> data = dbContext.Plus
            .Where(p => pluUidList.Contains(p.Id))
            .OrderBy(result => result.Number)
            .Join(dbContext.Nestings,
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

        return data;
    }

    #endregion

    #region Commands

    public WeightLabel GenerateLabel(Guid armId, Guid pluId, CreateWeightLabelDto dto)
    {
        Arm line = armService.GetItemByUid(armId);

        GenerateWeightLabelDto dtoToCreate = new()
        {
            Plu = pluService.GetItemByUid(pluId),
            Line = line,
            Weight = dto.WeightNet,
            Kneading = (short)dto.Kneading,
            ProductDt = dto.ProductDt
        };
        (_, LabelZpl zpl) = printLabelService.GenerateWeightLabel(dtoToCreate);

        line.Counter += 1;
        armService.Update(line);

        return new() { ArmCounter = (uint)line.Counter, Zpl = zpl.Zpl };
    }

    #endregion
}