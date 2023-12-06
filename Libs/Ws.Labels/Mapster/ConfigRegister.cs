using Mapster;
using Ws.Labels.Common;
using Ws.Labels.Dto;
using Ws.Labels.Models;

namespace Ws.Labels.Mapster;

public class ConfigRegister : IRegister
{

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LabelDataDto, BaseLabelModel>()
            .Include<LabelDataDto, LabelModel>()
            .Include<LabelDataDto, WeightLabelModel>()
            .Map(d => d.LineName, s => s.LineName)
            .Map(d => d.LineAddress, s => s.Address)
            .Map(d => d.LineNumber, s => s.LineNumber)
            .Map(d => d.LineCounter, s => s.LineCounter)
            
            .Map(d => d.ProductDtValue, s => s.ProductDt)
            .Map(d => d.ExpirationDtValue, s => s.ExpirationDt)
            
            .Map(d => d.PluGtin, s => s.Gtin)
            .Map(d => d.PluNumber, s => s.PluNumber)
            .Map(d => d.PluFullName, s => s.PluFullName)
            .Map(d => d.PluDescription, s => s.PluDescription)

            .Map(d => d.Kneading, s => s.Kneading)
            .GenerateMapper(MapType.Map);
            
        config.NewConfig<LabelDataDto, LabelModel>()
            .Map(d => d.BundleCount, s => s.BundleCount)
            .GenerateMapper(MapType.Map);

        config.NewConfig<LabelDataDto, WeightLabelModel>()
            .Map(d => d.Weight, s => s.Weight)
            .GenerateMapper(MapType.Map);
           
    }
}