// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApi.Helpers;

public partial class ControllerHelper
{
    #region Public and private methods

    private List<PluModel> GetPluList(XElement xml) =>
        GetNodesListCore<PluModel>(xml, "Nomenclature", (xmlNode, itemXml) =>
        {
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsGroup));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Code));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.FullName));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Description));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsCheckWeight));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.ShelfLifeDays));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Number));
        });

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cPlus(Response1cShortModel response, List<PluModel> itemsDb, PluModel itemXml)
    {
        try
        {
            // Add or update Foreign keys.
            // Must be refactoring!
            itemXml.Nomenclature = DataAccessHelper.Instance.GetItemNewEmpty<NomenclatureModel>();

            // Find by Identity -> Update exists.
            PluModel? itemDb = itemsDb.Find(x => x.IdentityValueUid.Equals(itemXml.IdentityValueUid));
            if (UpdateItemDb(response, itemXml, itemDb, false, true)) return;

            // Find by Code -> Update exists.
            itemDb = itemsDb.Find(x => x.Code.Equals(itemXml.Code));
            if (UpdateItemDb(response, itemXml, itemDb, true, true)) return;

            // Not find -> Add new.
            SaveItemDb(response, itemXml, true);

            // Update db list.
            if (!itemsDb.Select(x => x.IdentityValueUid).Contains(itemXml.IdentityValueUid))
                itemsDb.Add(itemXml);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, itemXml.IdentityValueUid, ex);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public ContentResult NewResponse1cPlus(ISessionFactory sessionFactory, XElement xml, string format) =>
        NewResponse1cCore<Response1cShortModel>(sessionFactory, response =>
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>(), true, false, false, true);
            List<PluModel> itemsDb = DataContext.GetListNotNullable<PluModel>(sqlCrudConfig);
            List<PluModel> itemsXml = GetPluList(xml);
            foreach (PluModel itemXml in itemsXml)
            {
                switch (itemXml.ParseResult.Status)
                {
                    case ParseStatus.Success:
                        AddResponse1cPlus(response, itemsDb, itemXml);
                        break;
                    case ParseStatus.Error:
                        AddResponse1cException(response, itemXml.IdentityValueUid,
                            itemXml.ParseResult.Exception, itemXml.ParseResult.InnerException);
                        break;
                }
            }
        }, format, false);

    #endregion
}