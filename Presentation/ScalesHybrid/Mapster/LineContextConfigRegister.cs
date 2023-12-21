using Mapster;
using ScalesHybrid.Services;
using Ws.Services.Dto;

namespace ScalesHybrid.Mapster;

public class LineContextConfigRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LineContext, LabelInfoDto>()
            .Map(d => d.Plu1СGuid, s => s.Plu.Uid1C)
            .Map(d => d.Kneading, s => s.KneadingModel.KneadingCount)
            .Map(d => d.WeightTare, s => s.PluNesting.WeightTare)
            .Map(d => d.LineCounter, s => s.Line.LabelCounter)
            .Map(d => d.BundleCount, s => s.PluNesting.BundleCount)
            .Map(d => d.IsCheckWeight, s => s.Plu.IsCheckWeight)
            .Map(d => d.Itf, s => s.Plu.Itf14)
            .Map(d => d.Gtin, s => s.Plu.Gtin)
            .Map(d => d.Address, s => s.Line.WorkShop.ProductionSite.Address)
            .Map(d => d.PluFullName, s => s.Plu.FullName)
            .Map(d => d.PluDescription, s => s.Plu.Description)
            .Map(d => d.LineNumber, s => s.Line.Number)
            .Map(d => d.LineName, s => s.Line.Description)
            .Map(d => d.Template, s => s.PluTemplate.Data)
            .Map(d => d.ProductDt, s => GetProductDt(s.KneadingModel.ProductDate))
            .Map(d => d.ExpirationDt, s => GetProductDt(s.KneadingModel.ProductDate)
                .AddDays(s.Plu.ShelfLifeDays))
            .IgnoreNonMapped(true)
            .GenerateMapper(MapType.MapToTarget);
    }
    
    public static DateTime GetProductDt(DateTime time) => 
        new(time.Year, time.Month, time.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
}