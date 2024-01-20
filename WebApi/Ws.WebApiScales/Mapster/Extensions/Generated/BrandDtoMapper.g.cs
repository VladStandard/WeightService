namespace Ws.WebApiScales.Features.Brand.Dto
{
    public static partial class BrandDtoMapper
    {
        public static Ws.StorageCore.Entities.SchemaRef1c.Brands.SqlBrandEntity AdaptTo(this Ws.WebApiScales.Features.Brand.Dto.BrandDto p1, Ws.StorageCore.Entities.SchemaRef1c.Brands.SqlBrandEntity p2)
        {
            if (p1 == null)
            {
                return null;
            }
            Ws.StorageCore.Entities.SchemaRef1c.Brands.SqlBrandEntity result = p2 ?? new Ws.StorageCore.Entities.SchemaRef1c.Brands.SqlBrandEntity();
            
            result.Code = p1.Code;
            result.Uid1C = p1.Guid;
            result.Name = p1.Name;
            return result;
            
        }
    }
}