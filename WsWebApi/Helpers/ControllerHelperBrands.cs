// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApi.Helpers;

public partial class ControllerHelper
{
    #region Public and private methods

    private List<BrandModel> GetXmlBrandList(XElement xml) =>
        GetNodesListCore<BrandModel>(xml, "Brand", (xmlNode, itemXml) =>
        {
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Code));
        });

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cBrand(Response1cShortModel response, List<BrandModel> itemsDb, BrandModel itemXml)
    {
        try
        {
            // Find by UID -> Update exists.
            BrandModel? itemDb = itemsDb.Find(x => x.IdentityValueUid.Equals(itemXml.IdentityValueUid));
            if (UpdateItemDb(response, itemXml, itemDb, true)) return;

            // Find by Code -> Update exists.
            itemDb = itemsDb.Find(x => x.Code.Equals(itemXml.Code));
            if (UpdateItemDbWithNewUid(response, itemXml, itemDb, true)) return;

            // Not find -> Add new.
            bool isSave = SaveItemDb(response, itemXml, true);

            // Update db list.
            if (isSave && !itemsDb.Select(x => x.IdentityValueUid).Contains(itemXml.IdentityValueUid))
                itemsDb.Add(itemXml);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, itemXml.IdentityValueUid, ex);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public ContentResult NewResponse1cBrands(ISessionFactory sessionFactory, XElement request, string formatString) =>
        NewResponse1cCore<Response1cShortModel>(sessionFactory, response =>
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>(), true, false, false, true);
            List<BrandModel> itemsDb = DataContext.GetListNotNullable<BrandModel>(sqlCrudConfig);
            List<BrandModel> itemsXml = GetXmlBrandList(request);
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
        }, formatString, false);

    #endregion
}