// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApi.Helpers;

public partial class ControllerHelper
{
    #region Public and private methods

    [Obsolete(@"Use GetNomenclatureList")]
    private List<NomenclatureModel> GetNomenclatureDeprecatedList(XElement xml)
    {
        List<NomenclatureModel> nomenclatures = new();
        XmlDocument xmlDocument = new();
        xmlDocument.LoadXml(xml.ToString());
        if (xmlDocument.DocumentElement is null) return nomenclatures;

        XmlNodeList list = xmlDocument.DocumentElement.GetElementsByTagName("Nomenclature");
        foreach (XmlNode node in list)
        {
            NomenclatureModel nomenclature = new();
            BrandModel brand = new();
            NomenclatureGroupModel nomenclatureGroup = new();
            try
            {
                nomenclature.ParseResult.Status = ParseStatus.Success;
                brand.ParseResult.Status = ParseStatus.Success;
                nomenclatureGroup.ParseResult.Status = ParseStatus.Success;
                // Set properties.
                SetItemPropertyFromXmlAttributeGuid(node, nomenclature, "Guid");
                SetItemPropertyFromXmlAttribute(node, nomenclature, nameof(nomenclature.IsMarked));
                //SetItemPropertyFromXmlAttribute(node, nomenclature, nameof(nomenclature.IsGroup));
                SetItemPropertyFromXmlAttribute(node, nomenclature, nameof(nomenclature.Name));
                SetItemPropertyFromXmlAttribute(node, nomenclature, nameof(nomenclature.Code));
                //SetItemPropertyFromXmlAttribute(node, nomenclature, nameof(nomenclature.FullName));
                SetItemPropertyFromXmlAttribute(node, nomenclature, nameof(nomenclature.Description));
                //SetItemPropertyFromXmlAttribute(node, brand, nameof(brand.Code));
                SetItemPropertyFromXmlAttribute(node, brand, "BrandGuid");
                SetItemPropertyFromXmlAttribute(node, nomenclatureGroup, "GroupGuid");
                //SetItemPropertyFromXmlAttribute(node, , "BoxTypeGuid");
                //SetItemPropertyFromXmlAttribute(node, , "PackageTypeGuid");
                //SetItemPropertyFromXmlAttribute(node, , "ClipTypeGuid");

                if (string.IsNullOrEmpty(nomenclature.ParseResult.Exception))
                    nomenclature.ParseResult.Message = "Is success";
            }
            catch (Exception ex)
            {
                nomenclature.ParseResult.Status = ParseStatus.Error;
                nomenclature.ParseResult.Exception = ex.Message;
                if (ex.InnerException is not null)
                    nomenclature.ParseResult.InnerException = ex.InnerException.Message;
            }
            nomenclatures.Add(nomenclature);
        }

        return nomenclatures;
    }

    private List<NomenclatureV2Model> GetNomenclatureList(XElement xml)
    {
        List<NomenclatureV2Model> itemsXml = new();
        XmlDocument xmlDocument = new();
        xmlDocument.LoadXml(xml.ToString());
        if (xmlDocument.DocumentElement is null) return itemsXml;

        XmlNodeList nodes = xmlDocument.DocumentElement.ChildNodes;
        if (nodes is null || nodes.Count <= 0) return itemsXml;
        foreach (XmlNode node in nodes)
        {
            NomenclatureV2Model itemXml = new();
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
                    SetItemPropertyFromXmlAttribute(node, itemXml, nameof(itemXml.MeasurementType));
                    SetItemPropertyFromXmlAttribute(node, itemXml, nameof(itemXml.ShelfLife));

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
    private void AddResponse1cNomenclature(Response1cShortModel response, 
        List<NomenclatureV2Model> itemsDb, NomenclatureV2Model itemXml)
    {
        try
        {
            NomenclatureV2Model? itemDb = itemsDb.Find(x => x.IdentityValueUid.Equals(itemXml.IdentityValueUid));

            // Find -> Update.
            if (itemDb is not null && itemDb.IsNotNew)
            {
                itemDb.UpdateProperties(itemXml);
                (bool IsOk, Exception? Exception) dbUpdate = DataContext.DataAccess.UpdateForce(itemDb);
                if (dbUpdate.IsOk)
                    response.Successes.Add(new(itemXml.IdentityValueUid));
                else
                    AddResponse1cException(response, itemXml.IdentityValueUid, dbUpdate.Exception);
                return;
            }

            // Find by Code -> Delete.
            itemDb = itemsDb.Find(x => x.Code.Equals(itemXml.Code));
            if (itemDb is not null && itemDb.IsNotNew)
            {
                (bool IsOk, Exception? Exception) dbDelete = DataContext.DataAccess.Delete(itemDb);
                // Delete was success. Duplicate field Code: {itemXml.Code}
                if (!dbDelete.IsOk)
                {
                    AddResponse1cException(response, itemDb.IdentityValueUid, dbDelete.Exception);
                    return;
                }
            }

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
    public ContentResult NewResponse1cNomenclatures(ISessionFactory sessionFactory, XElement request, string formatString)
    {
        return NewResponse1cCore<Response1cShortModel>(sessionFactory, response =>
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>(), true, false, false, true);
            List<NomenclatureV2Model> itemsDb = DataContext.GetListNotNullable<NomenclatureV2Model>(sqlCrudConfig);
            List<NomenclatureV2Model> itemsXml = GetNomenclatureList(request);
            foreach (NomenclatureV2Model itemXml in itemsXml)
            {
                switch (itemXml.ParseResult.Status)
                {
                    case ParseStatus.Success:
                        AddResponse1cNomenclature(response, itemsDb, itemXml);
                        break;
                    case ParseStatus.Error:
                        AddResponse1cException(response, itemXml.IdentityValueUid,
                            itemXml.ParseResult.Exception, itemXml.ParseResult.InnerException);
                        break;
                }
            }
        }, formatString, false);
    }

    #endregion
}