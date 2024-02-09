namespace Ws.Labels.Service.Features.PrintLabel.Weight.Dto
{
    public static partial class LabelWeightDtoMapper
    {
        public static Ws.Labels.Service.Features.PrintLabel.Weight.Models.XmlWeightLabelModel AdaptToXmlWeightLabelModel(this Ws.Labels.Service.Features.PrintLabel.Weight.Dto.LabelWeightDto p1)
        {
            return p1 == null ? null : new Ws.Labels.Service.Features.PrintLabel.Weight.Models.XmlWeightLabelModel()
            {
                Weight = p1.Weight,
                Kneading = p1.Kneading,
                ExpirationDtValue = p1.ExpirationDt,
                ProductDtValue = p1.ProductDt,
                LineNumber = funcMain1(p1.Line == null ? null : (int?)p1.Line.Number),
                LineCounter = funcMain2(p1.Line == null ? null : (int?)p1.Line.Counter),
                LineName = p1.Line == null ? null : p1.Line.Name,
                LineAddress = p1.Line == null ? null : (p1.Line.Warehouse == null ? null : (p1.Line.Warehouse.ProductionSite == null ? null : p1.Line.Warehouse.ProductionSite.Address)),
                PluNumber = funcMain3(p1.Nesting == null ? null : (p1.Nesting.Plu == null ? null : (short?)p1.Nesting.Plu.Number)),
                PluGtin = p1.Nesting == null ? null : (p1.Nesting.Plu == null ? null : p1.Nesting.Plu.Gtin),
                PluFullName = p1.Nesting == null ? null : (p1.Nesting.Plu == null ? null : p1.Nesting.Plu.FullName),
                PluDescription = p1.Nesting == null ? null : (p1.Nesting.Plu == null ? null : p1.Nesting.Plu.Description)
            };
        }
        
        private static int funcMain1(int? p2)
        {
            return p2 == null ? 0 : (int)p2;
        }
        
        private static int funcMain2(int? p3)
        {
            return p3 == null ? 0 : (int)p3;
        }
        
        private static short funcMain3(short? p4)
        {
            return p4 == null ? ((short)0) : (short)p4;
        }
    }
}