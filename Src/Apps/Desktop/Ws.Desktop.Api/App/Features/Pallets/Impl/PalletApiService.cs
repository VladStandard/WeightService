using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework;
using Ws.Desktop.Api.App.Features.Pallets.Common;
using Ws.Desktop.Api.App.Features.Pallets.Extensions;
using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;
using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Services.Features.Arms;
using Ws.Domain.Services.Features.PalletMen;
using Ws.Domain.Services.Features.Plus;
using Ws.Labels.Service.Generate;
using Ws.Labels.Service.Generate.Features.Piece.Dto;
using Ws.Shared.Extensions;

namespace Ws.Desktop.Api.App.Features.Pallets.Impl;

public class PalletApiService(
    WsDbContext dbContext,
    IPluService pluService,
    IArmService armService,
    IPalletManService palletManService,
    IPrintLabelService printLabelService
    ): IPalletApiService
{
    #region Quieries

    public List<LabelInfo> GetAllZplByArm(Guid armId, Guid palletId)
    {
        List<LabelInfo> labels = dbContext.Pallets
            .AsNoTracking()
            .Where(p => p.Arm.Id == armId && p.Id == palletId)
            .ToLabelInfo(dbContext.Labels)
            .ToList();
        return labels;
    }

    public List<PalletInfo> GetAllByDate(Guid armId, DateTime startTime, DateTime endTime)
    {
        bool dateCondition =
            startTime != DateTime.MinValue &&
            endTime != DateTime.MaxValue &&
            startTime < endTime;

        return dbContext.Pallets
            .AsNoTracking()
            .IfWhere(dateCondition, p => p.CreateDt > startTime && p.CreateDt < endTime)
            .Where(p => p.Arm.Id == armId)
            .OrderByDescending(p => p.CreateDt)
            .ToPalletInfo(dbContext.Labels).ToList();
    }

    public List<PalletInfo> GetByNumber(Guid armId, uint number)
    {
        string numberStr = $"{number}";
        return dbContext.Pallets
            .AsNoTracking()
            .Where(p => p.Arm.Id == armId && p.Number.ToString().Contains(numberStr))
            .ToPalletInfo(dbContext.Labels)
            .Take(10)
            .ToList();
    }


    #endregion

    #region Commands

    public async Task<PalletInfo> CreatePiecePallet(Guid armId, PalletPieceCreateDto dto)
    {
        var plu = pluService.GetItemByUid(dto.PluId);
        List<PluCharacteristic> characteristic = plu.CharacteristicsWithNesting.ToList();

        var data = new GeneratePiecePalletDto
        {
            Plu = pluService.GetItemByUid(dto.PluId),
            Line = armService.GetItemByUid(armId),
            PalletMan = palletManService.GetItemByUid(dto.PalletManId),
            PluCharacteristic = characteristic.Single(i => i.Uid == dto.CharacteristicId),
            Kneading = (short)dto.Kneading,
            Weight = dto.WeightTray,
            ProductDt = dto.ProdDt,
            ExpirationDt = dto.ProdDt.AddDays(plu.ShelfLifeDays),
        };
        Guid palletId = await printLabelService.GeneratePiecePallet(data, dto.LabelCount);

        return dbContext.Pallets
            .ToPalletInfo(dbContext.Labels)
            .Single(p => p.Id == palletId);
    }

    #endregion
}