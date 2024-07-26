using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework;
using Ws.Database.EntityFramework.Entities.Print.Pallets;
using Ws.Desktop.Api.App.Features.Pallets.Common;
using Ws.Desktop.Api.App.Features.Pallets.Extensions;
using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;
using Ws.Domain.Models.Entities.Ref1c.Plus;
using Ws.Domain.Services.Features;
using Ws.Domain.Services.Features.Plus;
using Ws.Labels.Service.Generate;
using Ws.Labels.Service.Generate.Features.Piece.Dto;
using Ws.Shared.Extensions;

namespace Ws.Desktop.Api.App.Features.Pallets.Impl;

public class PalletApiService(
    WsDbContext dbContext,
    PluService pluService,
    ArmService armService,
    PalletManService palletManService,
    IPrintLabelService printLabelService
    ): IPalletApiService
{
    #region Quieries

    public List<LabelInfo> GetAllZplByPallet(Guid palletId)
    {
        List<LabelInfo> labels = dbContext.Pallets
            .AsNoTracking()
            .Where(p => p.Id == palletId)
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

    public async Task Delete(Guid id)
    {
        PalletEntity? pallet = await dbContext.Pallets.FindAsync(id);
        if (pallet != null)
        {
            bool isDelete = !(pallet.DeletedAt != null);
            await printLabelService.DeletePallet(pallet.Number, isDelete);
            pallet.DeletedAt = isDelete ? DateTime.Now : null;
            await dbContext.SaveChangesAsync();
        }
    }

    public List<PalletInfo> GetByNumber(string number)
    {
        return dbContext.Pallets
            .AsNoTracking()
            .Where(p => p.Number.ToString().Contains(number))
            .ToPalletInfo(dbContext.Labels)
            .Take(10)
            .ToList();
    }


    #endregion

    #region Commands

    public async Task<PalletInfo> CreatePiecePallet(Guid armId, PalletPieceCreateDto dto)
    {
        uint maxCounter = dbContext.Pallets.Any() ? dbContext.Pallets.Max(i => i.Counter) : 0;

        var plu = pluService.GetItemByUid(dto.PluId);
        List<PluCharacteristic> characteristic = plu.CharacteristicsWithNesting.ToList();

        GeneratePiecePalletDto data = new()
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
        Guid palletId = await printLabelService.GeneratePiecePallet(data, dto.LabelCount, maxCounter);

        return dbContext.Pallets
            .ToPalletInfo(dbContext.Labels)
            .Single(p => p.Id == palletId);
    }

    #endregion
}