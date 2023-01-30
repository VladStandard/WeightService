// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApi.Helpers;

public partial class ControllerHelper
{
    #region Public and private methods

    private List<NomenclatureGroupModel> GetNomenclatureGroupsList(XElement xml) =>
        GetNodesListCore<NomenclatureGroupModel>(xml, "Nomenclature", (xmlNode, itemXml) =>
        {
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, "Guid");
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.IsMarked));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Name));
            SetItemPropertyFromXmlAttribute(xmlNode, itemXml, nameof(itemXml.Code));
        });

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cNomenclatureGroups(Response1cShortModel response, List<NomenclatureGroupModel> itemsDb, NomenclatureGroupModel itemXml)
    {
        try
        {
            // Find by Identity -> Update.
            NomenclatureGroupModel? itemDb = itemsDb.Find(x => x.IdentityValueUid.Equals(itemXml.IdentityValueUid));
            if (UpdateItemDb(response, itemXml, itemDb, false)) return;

            // Find by Code -> Update.
            itemDb = itemsDb.Find(x => x.Code.Equals(itemXml.Code));
            if (UpdateItemDb(response, itemXml, itemDb, true)) return;

            // Find by Name -> Update.
            itemDb = itemsDb.Find(x => x.Code.Equals(itemXml.Name));
            if (UpdateItemDb(response, itemXml, itemDb, true)) return;

            // Not find -> Add.
            //itemXml.Nomenclature = DataAccessHelper.Instance.GetItemNewEmpty<NomenclatureGroupModel>();
            //SaveItemDb(response, itemXml);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, itemXml.IdentityValueUid, ex);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public ContentResult NewResponse1cNomenclatureGroups(ISessionFactory sessionFactory, XElement xml, string format) =>
        NewResponse1cCore<Response1cShortModel>(sessionFactory, response =>
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>(), true, false, false, true);
            List<NomenclatureGroupModel> itemsDb = DataContext.GetListNotNullable<NomenclatureGroupModel>(sqlCrudConfig);
            List<NomenclatureGroupModel> itemsXml = GetNomenclatureGroupsList(xml);
            foreach (NomenclatureGroupModel itemXml in itemsXml)
            {
                switch (itemXml.ParseResult.Status)
                {
                    case ParseStatus.Success:
                        AddResponse1cNomenclatureGroups(response, itemsDb, itemXml);
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