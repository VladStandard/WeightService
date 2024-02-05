using Mapster;
using ScalesDesktop.Services;
using Ws.Labels.Service.Features.PrintLabel.Dto;

namespace ScalesDesktop.Mapster;

public class LabelContextConfigRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LabelContext, LabelDataDto>()
            .Map(d => d.Plu1СGuid, s => s.Plu.Uid1C)
            .Map(d => d.PluNumber, s => s.Plu.Number)
            .Map(d => d.Kneading, s => s.KneadingModel.KneadingCount)
            .Map(d => d.WeightTare, s => s.PluNesting.WeightTare)
            .Map(d => d.LineCounter, s => s.Line.Counter)
            .Map(d => d.BundleCount, s => s.PluNesting.BundleCount)
            .Map(d => d.Gtin, s => s.Plu.Gtin)
            .Map(d => d.Address, s => s.Line.Warehouse.ProductionSite.Address)
            .Map(d => d.PluFullName, s => s.Plu.FullName)
            .Map(d => d.PluDescription, s => s.Plu.Description)
            .Map(d => d.LineNumber, s => s.Line.Number)
            .Map(d => d.LineName, s => s.Line.Name)
            .Map(d => d.Template, s => s.PluTemplate.Data)
            .Map(d => d.ProductDt, s => GetProductDt(s.KneadingModel.ProductDate))
            .Map(d => d.ExpirationDt, s => GetProductDt(s.KneadingModel.ProductDate)
                .AddDays(s.Plu.ShelfLifeDays))
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.MapToTarget);
    }

    private static DateTime GetProductDt(DateTime time) => 
        new(time.Year, time.Month, time.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
}