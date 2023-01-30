// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Helpers;
using DataCore.Sql.TableScaleModels.Plus;
using System;

namespace WsWebApi.Helpers;

public partial class ControllerHelper
{
    #region Public and private methods

    [Obsolete(@"Use GetPluList")]
    private List<PluModel> GetPluListDeprecated(XElement xml)
    {
        List<PluModel> itemsXml = new();
        XmlDocument xmlDocument = new();
        xmlDocument.LoadXml(xml.ToString());
        if (xmlDocument.DocumentElement is null) return itemsXml;
        string nodeIdentity = "Nomenclature";

        XmlNodeList nodes = xmlDocument.DocumentElement.ChildNodes;
        if (nodes.Count <= 0) return itemsXml;
        foreach (XmlNode node in nodes)
        {
            PluModel itemXml = new();
            if (node.Name.Equals(nodeIdentity, StringComparison.InvariantCultureIgnoreCase))
            {
                try
                {
                    itemXml.ParseResult.Status = ParseStatus.Success;
                    // Set properties.
                    SetItemPropertyFromXmlAttributeGuid(node, itemXml, "Guid");
                    SetItemPropertyFromXmlAttribute(node, itemXml, nameof(itemXml.IsMarked));
                    SetItemPropertyFromXmlAttribute(node, itemXml, nameof(itemXml.Code));
                    SetItemPropertyFromXmlAttribute(node, itemXml, nameof(itemXml.Name));
                    SetItemPropertyFromXmlAttribute(node, itemXml, nameof(itemXml.FullName));
                    SetItemPropertyFromXmlAttribute(node, itemXml, nameof(itemXml.Description));
                    SetItemPropertyFromXmlAttribute(node, itemXml, nameof(itemXml.IsCheckWeight));
                    SetItemPropertyFromXmlAttribute(node, itemXml, nameof(itemXml.ShelfLifeDays));
                    SetItemPropertyFromXmlAttribute(node, itemXml, nameof(itemXml.Number));

                    if (string.IsNullOrEmpty(itemXml.ParseResult.Exception))
                        itemXml.ParseResult.Message = "Is success";
                }
                catch (Exception ex)
                {
                    itemXml.ParseResult.Status = ParseStatus.Error;
                    itemXml.ParseResult.Exception = ex.Message;
                    if (ex.InnerException is not null)
                        itemXml.ParseResult.InnerException = ex.InnerException.Message;
                }
            }
            else
            {
                itemXml.ParseResult.Status = ParseStatus.Error;
                itemXml.ParseResult.Exception = $"The node `{nodeIdentity}` with `{node.Name}` is not ident!";
            }
            itemsXml.Add(itemXml);
        }
        return itemsXml;
    }

    private List<PluModel> GetPluList(XElement xml) =>
        GetNodesListCore<PluModel>(xml, "Nomenclature", (node, itemXml) =>
        {
            SetItemPropertyFromXmlAttributeGuid(node, itemXml, "Guid");
            SetItemPropertyFromXmlAttribute(node, itemXml, nameof(itemXml.IsMarked));
            SetItemPropertyFromXmlAttribute(node, itemXml, nameof(itemXml.Code));
            SetItemPropertyFromXmlAttribute(node, itemXml, nameof(itemXml.Name));
            SetItemPropertyFromXmlAttribute(node, itemXml, nameof(itemXml.FullName));
            SetItemPropertyFromXmlAttribute(node, itemXml, nameof(itemXml.Description));
            SetItemPropertyFromXmlAttribute(node, itemXml, nameof(itemXml.IsCheckWeight));
            SetItemPropertyFromXmlAttribute(node, itemXml, nameof(itemXml.ShelfLifeDays));
            SetItemPropertyFromXmlAttribute(node, itemXml, nameof(itemXml.Number));
            // ParseResult.
            if (string.IsNullOrEmpty(itemXml.ParseResult.Exception))
                itemXml.ParseResult.Message = "Is success";
        });

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cPlus(Response1cShortModel response, List<PluModel> itemsDb, PluModel itemXml)
    {
        try
        {
            // Find by Identity -> Update.
            PluModel? itemDb = itemsDb.Find(x => x.IdentityValueUid.Equals(itemXml.IdentityValueUid));
            if (UpdateItemDb(response, itemXml, itemDb, false)) return;

            // Find by Code -> Update.
            itemDb = itemsDb.Find(x => x.Code.Equals(itemXml.Code));
            if (UpdateItemDb(response, itemXml, itemDb, true)) return;

            // Find by Name -> Update.
            itemDb = itemsDb.Find(x => x.Code.Equals(itemXml.Name));
            if (UpdateItemDb(response, itemXml, itemDb, true)) return;

            // Not find -> Add.
            itemXml.Nomenclature = DataAccessHelper.Instance.GetItemNewEmpty<NomenclatureModel>();
            SaveItemDb(response, itemXml);
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