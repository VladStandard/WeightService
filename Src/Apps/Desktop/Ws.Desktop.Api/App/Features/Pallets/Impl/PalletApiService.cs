using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework;
using Ws.Database.EntityFramework.Entities.Print.Pallets;
using Ws.Database.EntityFramework.Entities.Ref.Lines;
using Ws.Database.EntityFramework.Entities.Ref.PalletMen;
using Ws.Database.EntityFramework.Entities.Ref1C.Characteristics;
using Ws.Database.EntityFramework.Entities.Ref1C.Nestings;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;
using Ws.Desktop.Api.App.Features.Pallets.Common;
using Ws.Desktop.Api.App.Features.Pallets.Expressions;
using Ws.Desktop.Api.App.Shared.Helpers;
using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;
using Ws.Labels.Service.Generate;
using Ws.Labels.Service.Generate.Features.Piece;
using Ws.Labels.Service.Generate.Features.Piece.Dto;
using Ws.Labels.Service.Generate.Shared.Dto;
using Ws.Shared.Extensions;

namespace Ws.Desktop.Api.App.Features.Pallets.Impl;

internal sealed class PalletApiService(
    WsDbContext dbContext,
    IPrintLabelService printLabelService,
    UserHelper userHelper
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

    public List<PalletInfo> GetAllByDate(DateTime startTime, DateTime endTime)
    {
        bool dateCondition =
            startTime != DateTime.MinValue &&
            endTime != DateTime.MaxValue &&
            startTime < endTime;

        return dbContext.Pallets
            .AsNoTracking()
            .IfWhere(dateCondition, p => p.CreateDt > startTime && p.CreateDt < endTime)
            .Where(p => p.Arm.Id == userHelper.UserId)
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

    public async Task<PalletInfo> CreatePiecePallet(PalletPieceCreateDto dto)
    {
        uint palletCounter = (dbContext.Pallets.Any() ? dbContext.Pallets.Max(i => i.Counter) : 0) + 1;

        NestingForLabel nestingForLabel;

        if (dto.CharacteristicId == Guid.Empty)
        {
            NestingEntity data1 = await dbContext.Nestings
                .Include(i => i.Box)
                .SingleAsync(i => i.Id == dto.PluId);

            nestingForLabel = new(Guid.Empty, data1.Box.Weight, data1.BundleCount);
        }
        else
        {
            CharacteristicEntity data2 = await dbContext.Characteristics
                .Include(i => i.Box)
                .SingleAsync(i => i.Id == dto.CharacteristicId);

            nestingForLabel = new(dto.CharacteristicId, data2.Box.Weight, data2.BundleCount);
        }

        LineEntity line = await dbContext.Lines
            .Include(i => i.Warehouse)
            .ThenInclude(i => i.ProductionSite)
            .SingleAsync(i => i.Id == userHelper.UserId);

        PluEntity plu = await dbContext.Plus
            .Include(i => i.Clip)
            .Include(i => i.Bundle)
            .SingleAsync(i => i.Id == dto.PluId);

        PalletManEntity palletMan = await dbContext.PalletMen
            .SingleAsync(i => i.Id == dto.PalletManId);

        PalletEntity pallet = new()
        {
            Arm = line,
            PalletMan = palletMan,
            Counter = palletCounter,
            IsShipped = false,
            TrayWeight = dto.WeightTray,
            ProductDt = dto.ProdDt,
            Barcode = $"001460910023{palletCounter.ToString().PadLeft(7, '0')}",
        };

        GeneratePiecePalletDto data = new()
        {
            Plu = new(
                plu.Id,
                plu.TemplateId,
                plu.Gtin,
                plu.Number,
                plu.ShelfLifeDays,
                plu.Ean13,
                plu.FullName,
                plu.Description,
                plu.StorageMethod,
                plu.Weight,
                plu.Clip.Weight,
                plu.Bundle.Weight
            ),
            Line = new(
                line.Id,
                (short)line.Number,
                line.Name,
                line.Warehouse.ProductionSite.Address,
                line.Counter
                ),
            Nesting = nestingForLabel,
            Pallet = new(pallet.Barcode, line.Warehouse.Uid1C, palletMan.Uid1C),
            Kneading = (short)dto.Kneading,
            TrayWeight = dto.WeightTray,
            ProductDt = dto.ProdDt
        };
        PalletOutputData palletData = await printLabelService.GeneratePiecePallet(data, dto.LabelCount);

        pallet.Id = palletData.Id;
        pallet.Number = palletData.Number;

        line.Counter += dto.LabelCount;

        await dbContext.Pallets.AddAsync(pallet);
        await dbContext.Labels.AddRangeAsync(palletData.labels);

        await dbContext.SaveChangesAsync();

        return dbContext.Pallets
            .ToPalletInfo(dbContext.Labels)
            .Single(p => p.Id ==  palletData.Id);
    }

    #endregion
}