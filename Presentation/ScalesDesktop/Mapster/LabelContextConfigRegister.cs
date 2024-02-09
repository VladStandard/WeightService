using Mapster;
using ScalesDesktop.Services;
using Ws.Labels.Service.Features.PrintLabel.Weight.Dto;

namespace ScalesDesktop.Mapster;

public class LabelContextConfigRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LabelContext, LabelWeightDto>()
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