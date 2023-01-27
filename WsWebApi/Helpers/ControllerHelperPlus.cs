// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Plus;

namespace WsWebApi.Helpers;

public partial class ControllerHelper
{
    #region Public and private methods

    private List<PluModel> GetPluList(XElement xml)
    {
        List<PluModel> itemsXml = new();
        XmlDocument xmlDocument = new();
        xmlDocument.LoadXml(xml.ToString());
        if (xmlDocument.DocumentElement is null) return itemsXml;

        XmlNodeList nodes = xmlDocument.DocumentElement.ChildNodes;
        if (nodes.Count <= 0) return itemsXml;
        foreach (XmlNode node in nodes)
        {
            PluModel itemXml = new();
            if (node.Name.Equals("Nomenclature", StringComparison.InvariantCultureIgnoreCase))
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
                itemXml.ParseResult.Exception = $"The node with name '{node.Name}' is not ident Brand!";
            }
            itemsXml.Add(itemXml);
        }
        return itemsXml;
    }

    // ReSharper disable once InconsistentNaming
    private void AddResponse1cPlus(Response1cShortModel response, List<PluModel> itemsDb, PluModel itemXml)
    {
        try
        {
            // Find -> Update.
            PluModel? itemDb = itemsDb.Find(x => x.IdentityValueUid.Equals(itemXml.IdentityValueUid));
            if (UpdateItemDb(response, itemXml, itemDb, false)) return;

            // Find by Code -> Update.
            itemDb = itemsDb.Find(x => x.Code.Equals(itemXml.Code));
            if (UpdateItemDb(response, itemXml, itemDb, true)) return;

            // Not find -> Add.
            (bool IsOk, Exception? Exception) dbSave = DataContext.DataAccess.Save(itemXml, itemXml.Identity);
            // Add was success.
            if (dbSave.IsOk)
                response.Successes.Add(new(itemXml.IdentityValueUid));
            else
                AddResponse1cException(response, itemXml.IdentityValueUid, dbSave.Exception);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, itemXml.IdentityValueUid, ex);
        }
    }

    // ReSharper disable once InconsistentNaming
    public ContentResult NewResponse1cPlus(ISessionFactory sessionFactory, XElement xml, string format)
    {
        return NewResponse1cCore<Response1cShortModel>(sessionFactory, response =>
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
    }

    #endregion
}