namespace Ws.Labels.Dto
{
    public static partial class LabelDataDtoMapper
    {
        public static Ws.Labels.Models.WeightLabelModel AdaptToWeightLabelModel(this Ws.Labels.Dto.LabelDataDto p1)
        {
            return p1 == null ? null : new Ws.Labels.Models.WeightLabelModel()
            {
                Weight = p1.Weight,
                Kneading = p1.Kneading,
                ExpirationDtValue = p1.ExpirationDt,
                ProductDtValue = p1.ProductDt,
                LineNumber = p1.LineNumber,
                LineCounter = p1.LineCounter,
                LineName = p1.LineName,
                LineAddress = p1.Address,
                PluNumber = p1.PluNumber,
                PluGtin = p1.Gtin,
                PluFullName = p1.PluFullName,
                PluDescription = p1.PluDescription
            };
        }
        public static Ws.Labels.Models.LabelModel AdaptToLabelModel(this Ws.Labels.Dto.LabelDataDto p2)
        {
            return p2 == null ? null : new Ws.Labels.Models.LabelModel()
            {
                BundleCount = p2.BundleCount,
                Kneading = p2.Kneading,
                ExpirationDtValue = p2.ExpirationDt,
                ProductDtValue = p2.ProductDt,
                LineNumber = p2.LineNumber,
                LineCounter = p2.LineCounter,
                LineName = p2.LineName,
                LineAddress = p2.Address,
                PluNumber = p2.PluNumber,
                PluGtin = p2.Gtin,
                PluFullName = p2.PluFullName,
                PluDescription = p2.PluDescription
            };
        }
        public static Ws.Labels.Common.BaseLabelModel AdaptToBaseLabelModel(this Ws.Labels.Dto.LabelDataDto p3)
        {
            return p3 == null ? null : new Ws.Labels.Common.BaseLabelModel()
            {
                Kneading = p3.Kneading,
                ExpirationDtValue = p3.ExpirationDt,
                ProductDtValue = p3.ProductDt,
                LineNumber = p3.LineNumber,
                LineCounter = p3.LineCounter,
                LineName = p3.LineName,
                LineAddress = p3.Address,
                PluNumber = p3.PluNumber,
                PluGtin = p3.Gtin,
                PluFullName = p3.PluFullName,
                PluDescription = p3.PluDescription
            };
        }
    }
}