namespace ScalesHybrid.Services
{
    public static partial class LineContextMapper
    {
        public static Ws.Services.Dto.LabelInfoDto AdaptTo(this ScalesHybrid.Services.LineContext p1, Ws.Services.Dto.LabelInfoDto p2)
        {
            if (p1 == null)
            {
                return null;
            }
            Ws.Services.Dto.LabelInfoDto result = p2 ?? new Ws.Services.Dto.LabelInfoDto();
            
            result.WeightTare = funcMain1(p1.PluNesting == null ? null : (decimal?)p1.PluNesting.WeightTare, result.WeightTare);
            result.IsCheckWeight = funcMain2(p1.Plu == null ? null : (bool?)p1.Plu.IsCheckWeight, result.IsCheckWeight);
            result.Itf = p1.Plu == null ? null : p1.Plu.Itf14;
            result.Gtin = p1.Plu == null ? null : p1.Plu.Gtin;
            result.LineNumber = funcMain3(p1.Line == null ? null : (int?)p1.Line.Number, result.LineNumber);
            result.LineCounter = funcMain4(p1.Line == null ? null : (int?)p1.Line.LabelCounter, result.LineCounter);
            result.LineName = p1.Line == null ? null : p1.Line.Description;
            result.Plu1СGuid = funcMain5(p1.Plu == null ? null : (System.Guid?)p1.Plu.Uid1C, result.Plu1СGuid);
            result.PluNumber = funcMain6(p1.Plu == null ? null : (short?)p1.Plu.Number, result.PluNumber);
            result.PluFullName = p1.Plu == null ? null : p1.Plu.FullName;
            result.PluDescription = p1.Plu == null ? null : p1.Plu.Description;
            result.BundleCount = funcMain7(p1.PluNesting == null ? null : (short?)p1.PluNesting.BundleCount, result.BundleCount);
            result.Kneading = funcMain8(p1.KneadingModel == null ? null : (int?)p1.KneadingModel.KneadingCount, result.Kneading);
            result.Address = p1.Line == null ? null : (p1.Line.WorkShop == null ? null : (p1.Line.WorkShop.ProductionSite == null ? null : p1.Line.WorkShop.ProductionSite.Address));
            result.Template = p1.PluTemplate == null ? null : p1.PluTemplate.Data;
            result.ProductDt = ScalesHybrid.Mapster.LineContextConfigRegister.GetProductDt(p1.KneadingModel.ProductDate);
            result.ExpirationDt = ScalesHybrid.Mapster.LineContextConfigRegister.GetProductDt(p1.KneadingModel.ProductDate).AddDays((double)p1.Plu.ShelfLifeDays);
            return result;
            
        }
        
        private static decimal funcMain1(decimal? p3, decimal p4)
        {
            return p3 == null ? 0m : (decimal)p3;
        }
        
        private static bool funcMain2(bool? p5, bool p6)
        {
            return p5 == null ? false : (bool)p5;
        }
        
        private static int funcMain3(int? p7, int p8)
        {
            return p7 == null ? 0 : (int)p7;
        }
        
        private static int funcMain4(int? p9, int p10)
        {
            return p9 == null ? 0 : (int)p9;
        }
        
        private static System.Guid funcMain5(System.Guid? p11, System.Guid p12)
        {
            return p11 == null ? default(System.Guid) : (System.Guid)p11;
        }
        
        private static short funcMain6(short? p13, short p14)
        {
            return p13 == null ? ((short)0) : (short)p13;
        }
        
        private static short funcMain7(short? p15, short p16)
        {
            return p15 == null ? ((short)0) : (short)p15;
        }
        
        private static short funcMain8(int? p17, short p18)
        {
            return p17 == null ? ((short)0) : (short)(int)p17;
        }
    }
}