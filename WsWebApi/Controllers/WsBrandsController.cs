// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

namespace WsWebApi.Controllers;

public sealed class WsBrandsController : WsContentBase
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsBrandsController _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsBrandsController Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    internal WsBrandsController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    private List<BrandModel> GetXmlBrandList(XElement xml) =>
        GetNodesListCore<BrandModel>(xml, LocaleCore.WebService.XmlItemBrand, (xmlNode, itemXml) =>
        {
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Code));
        });

    private void AddResponse1cBrand(WsResponse1cShortModel response, List<BrandModel> brandsDb, BrandModel brandXml)
    {
        try
        {
            // Find by Uid1C -> Update exists.
            BrandModel? brandDb = brandsDb.Find(item => Equals(item.Uid1c, brandXml.IdentityValueUid));
            if (UpdateBrandDb(response, brandXml, brandDb, true)) return;

            // Find by Code -> Update exists.
            brandDb = brandsDb.Find(item => Equals(item.Code, brandXml.Code));
            if (UpdateBrandDb(response, brandXml, brandDb, true)) return;

            // Find by Name -> Update exists.
            brandDb = brandsDb.Find(item => Equals(item.Name, brandXml.Name));
            if (UpdateBrandDb(response, brandXml, brandDb, true)) return;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, brandXml.Uid1c, brandXml, true);

            // Update db list.
            if (brandDb is not null && isSave && !brandsDb.Select(x => x.IdentityValueUid).Contains(brandDb.IdentityValueUid))
                brandsDb.Add(brandDb);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, brandXml.Uid1c, ex);
        }
    }

    public ContentResult NewResponse1cBrands(XElement xml, string formatString, bool isDebug, ISessionFactory sessionFactory) =>
        NewResponse1cCore<WsResponse1cShortModel>(response =>
        {
            List<BrandModel> itemsDb = WsDataContext.GetListNotNullableBrands(SqlCrudConfig);
            List<BrandModel> itemsXml = GetXmlBrandList(xml);
            foreach (BrandModel itemXml in itemsXml)
            {
                switch (itemXml.ParseResult.Status)
                {
                    case ParseStatus.Success:
                        AddResponse1cBrand(response, itemsDb, itemXml);
                        break;
                    case ParseStatus.Error:
                        AddResponse1cException(response, itemXml);
                        break;
                }
            }
        }, formatString, isDebug, sessionFactory);

    private bool UpdateBrandDb(WsResponse1cShortModel response, BrandModel brandXml, BrandModel? brandDb, bool isCounter)
    {
        if (brandDb is null || brandDb.IsNew) return false;
        brandDb.UpdateProperties(brandXml);
        SqlCrudResultModel dbUpdate = WsDataContext.DataAccess.UpdateForce(brandDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(brandXml.Uid1c));
        }
        else
            AddResponse1cException(response, brandXml.Uid1c, dbUpdate.Exception);
        return dbUpdate.IsOk;
    }

    #endregion
}