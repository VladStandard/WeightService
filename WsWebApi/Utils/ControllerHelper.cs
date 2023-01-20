// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApi.Utils;

/// <summary>
/// Web API Controller helper.
/// </summary>
public class ControllerHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static ControllerHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static ControllerHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private DataContextModel DataContext { get; } = new();

    #endregion

    #region Public and private methods

    public ContentResult GetContentResult(Task<ContentResult> task, string formatString,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        try
        {
            task.Start();
            return task.GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            filePath = Path.GetFileName(filePath);
            ServiceExceptionModel serviceException = new(filePath, lineNumber, memberName, ex);
            return DataFormatUtils.GetContentResult<ServiceExceptionModel>(serviceException, formatString, HttpStatusCode.OK);
        }
        finally
        {
            GC.Collect();
        }
    }

    public delegate ContentResult GetContentResultDelegate();

    public ContentResult GetContentResult(GetContentResultDelegate getContentResult, string formatString,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        try
        {
            return getContentResult();
        }
        catch (Exception ex)
        {
            filePath = Path.GetFileName(filePath);
            ServiceExceptionModel serviceException = new(filePath, lineNumber, memberName, ex);
            return DataFormatUtils.GetContentResult<ServiceExceptionModel>(serviceException, formatString, HttpStatusCode.OK);
        }
        finally
        {
            GC.Collect();
        }
    }

    private ContentResult NewResponse1cCore<T>(ISessionFactory sessionFactory, Action<T> action,
        string formatString, bool isTransaction, HttpStatusCode httpStatusCode = HttpStatusCode.OK) where T : SerializeBase, new()
    {
        using ISession session = sessionFactory.OpenSession();
        using ITransaction transaction = session.BeginTransaction();
        T response = new();

        try
        {
            action(response);
            if (isTransaction)
                transaction.Commit();
        }
        catch (Exception ex)
        {
            httpStatusCode = HttpStatusCode.InternalServerError;
            if (response is Response1cModel response1c)
                response1c.Errors.Add(new(ex));
            if (isTransaction)
                transaction.Rollback();
        }

        return DataFormatUtils.GetContentResult<T>(response, formatString, httpStatusCode);
    }

    public ContentResult NewResponse1cFromQuery(ISessionFactory sessionFactory, string query,
        SqlParameter? sqlParameter, string formatString, bool isTransaction)
    {
        return NewResponse1cCore<Response1cModel>(sessionFactory, response =>
        {
            if (!string.IsNullOrEmpty(query))
            {
                if (response.ResponseQuery is not null)
                    response.ResponseQuery.Query = query;
                ISQLQuery sqlQuery = DataContext.Session.CreateSQLQuery(query);
                if (sqlParameter is not null)
                {
                    if (response.ResponseQuery is not null)
                        response.ResponseQuery.Parameters.Add(new(sqlParameter));
                    sqlQuery.SetParameter(sqlParameter.ParameterName, sqlParameter.Value);
                }

                IList? list = sqlQuery.List();
                object?[] result = new object?[list.Count];
                if (list.Count == 1 && list[0] is object[] records)
                {
                    result = records;
                }
                else
                {
                    for (int i = 0; i < result.Length; i++)
                    {
                        if (list[i] is object[] records2)
                            result[i] = records2;
                        else
                            result[i] = list[i];
                    }
                }
                string str = result[^1] as string ?? string.Empty;
                response.Infos.Add(new(str));
            }
            else
                response.Infos.Add(new("Empty query. Try to make some select from any table."));
        }, formatString, isTransaction);
    }

    public ContentResult NewResponse1cBrandsFromAction(ISessionFactory sessionFactory, XElement request, string formatString)
    {
        return NewResponse1cCore<Response1cShortModel>(sessionFactory, response =>
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>(), true, false, false, true);
            List<BrandModel> brandsDb = DataContext.GetListNotNullable<BrandModel>(sqlCrudConfig);
            List<BrandModel> brandsXml = GetBrandList(request);
            foreach (BrandModel brandXml in brandsXml)
            {
                switch (brandXml.ParseResult.Status)
                {
                    case ParseStatus.Success:
                        AddResponse1cBrand(response, brandsDb, brandXml);
                        break;
                    case ParseStatus.Error:
                        AddResponse1cException(response, brandXml);
                        break;
                }
            }
        }, formatString, false);
    }

    [Obsolete(@"Deprecated method")]
    public ContentResult NewResponseBarcodeFromAction(ISessionFactory sessionFactory, DateTime start, DateTime end, string formatString, bool isTransaction)
    {
        return NewResponse1cCore<Response1cModel>(sessionFactory, response =>
        {

            List<SqlFieldFilterModel> sqlFilters = new()
            {
                new(nameof(BarCodeModel.CreateDt), SqlFieldComparerEnum.MoreOrEqual, start),
                new(nameof(BarCodeModel.CreateDt), SqlFieldComparerEnum.LessOrEqual, end),
            };

            SqlCrudConfigModel sqlCrudConfig = new(sqlFilters, true, false, false, true);
            List<BarCodeModel> barcodesDb = DataContext.GetListNotNullable<BarCodeModel>(sqlCrudConfig);

            // ResponseBarCodeModels barCodes = new(barcodesDb);
            // response.SerializeAsXmlString<BarCodeModel>(false);

        }, formatString, isTransaction);
    }

    private void AddResponse1cBrand(Response1cShortModel response, List<BrandModel> brandsDb, BrandModel brandXml)
    {
        try
        {
            BrandModel? itemDb = brandsDb.Find(x => x.IdentityValueUid.Equals(brandXml.IdentityValueUid));

            // Find by UID -> Update.
            if (itemDb is not null && itemDb.IsNotNew)
            {
                itemDb.UpdateProperties(brandXml);
                (bool IsOk, Exception? Exception) dbUpdate = DataContext.DataAccess.Update(itemDb);
                if (dbUpdate.IsOk)
                    response.Successes.Add(new(brandXml.IdentityValueUid));
                else
                    AddResponse1cException(response, brandXml.IdentityValueUid, dbUpdate.Exception);
                return;
            }

            // Find by Code -> Delete.
            itemDb = brandsDb.Find(x => x.Code.Equals(brandXml.Code));
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
            (bool IsOk, Exception? Exception) dbSave = DataContext.DataAccess.Save(brandXml, brandXml.Identity);
            // Add was success.
            if (dbSave.IsOk)
                response.Successes.Add(new(brandXml.IdentityValueUid));
            else
                AddResponse1cException(response, brandXml.IdentityValueUid, dbSave.Exception);
        }
        catch (Exception ex)
        {
            AddResponse1cException(response, brandXml.IdentityValueUid, ex);
        }
    }

    private void AddResponse1cItem<T>(Response1cShortModel response, IReadOnlyCollection<T> listDb, T itemXml) where T : SqlTableBase, new()
    {
        try
        {
            T? itemDb = listDb.FirstOrDefault(x => x.IsIdentityId
                ? x.IdentityValueId.Equals(itemXml.IdentityValueId) : x.IdentityValueUid.Equals(itemXml.IdentityValueUid));

            // Find by Identity -> Update.
            if (itemDb is not null && itemDb.IsNotNew)
            {
                itemDb.UpdateProperties(itemXml);
                (bool IsOk, Exception? Exception) dbUpdate = DataContext.DataAccess.Update(itemDb);
                if (dbUpdate.IsOk)
                    response.Successes.Add(new(itemXml.IdentityValueUid));
                else
                    AddResponse1cException(response, itemXml.IdentityValueUid, dbUpdate.Exception);
                return;
            }

            // Find by Code -> Update.
            //itemDb = listDb.Where(x => x.Code.Equals(itemXml.Code)).FirstOrDefault();
            string itemInputCode = string.Empty;
            switch (typeof(T))
            {
                case var cls when cls == typeof(BrandModel):
                    if (itemXml is BrandModel brandInput)
                    {
                        itemInputCode = brandInput.Code;
                        BrandModel? itemCast = listDb.Cast<BrandModel>().FirstOrDefault(x => x.Code.Equals(itemInputCode));
                        if (itemCast is T itemT) itemDb = itemT;
                    }
                    break;
                case var cls when cls == typeof(NomenclatureGroupModel):
                    if (itemXml is NomenclatureGroupModel nomenclatureGroupInput)
                    {
                        itemInputCode = nomenclatureGroupInput.Code;
                        NomenclatureGroupModel? itemCast = listDb.Cast<NomenclatureGroupModel>().FirstOrDefault(x => x.Code.Equals(itemInputCode));
                        if (itemCast is T itemT) itemDb = itemT;
                    }
                    break;
            }
            if (itemDb is not null && itemDb.IsNotNew)
            {
                (bool IsOk, Exception? Exception) dbDelete = DataContext.DataAccess.Delete(itemDb);
                // Delete was success. Duplicate field Code: {itemInputCode}.
                if (!dbDelete.IsOk)
                {
                    AddResponse1cException(response, itemDb.IdentityValueUid, dbDelete.Exception);
                    return;
                }
            }

            // Not find the duplicate field "Code".
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

    private void AddResponse1cException(Response1cShortModel response, Guid uid, string? errorMessage, string? innerErrorMessage = null)
    {
        Response1cErrorModel responseRecord = new(uid, errorMessage ?? string.Empty);
        responseRecord.Message += " | " + innerErrorMessage;
        response.Errors.Add(responseRecord);
    }

    private void AddResponse1cException(Response1cShortModel response, BrandModel brand)
    {
        Response1cErrorModel responseRecord = new(brand.IdentityValueUid, brand.ParseResult.Exception ?? string.Empty);
        if (!string.IsNullOrEmpty(brand.ParseResult.InnerException))
            responseRecord.Message += " | " + brand.ParseResult.InnerException;
        response.Errors.Add(responseRecord);
    }

    private void AddResponse1cException(Response1cShortModel response, Guid uid, Exception? ex) =>
        AddResponse1cException(response, uid, ex?.Message, ex?.InnerException?.Message);

    private List<BrandModel> GetBrandList(XElement xml)
    {
        List<BrandModel> brands = new();
        XmlDocument xmlDocument = new();
        xmlDocument.LoadXml(xml.ToString());
        if (xmlDocument.DocumentElement is null) return brands;

        XmlNodeList nodes = xmlDocument.DocumentElement.ChildNodes;
        if (nodes is null || nodes.Count <= 0) return brands;
        foreach (XmlNode node in nodes)
        {
            BrandModel brand = new();
            if (node.Name.Equals("BRAND", StringComparison.InvariantCultureIgnoreCase))
            {
                try
                {
                    brand.ParseResult.Status = ParseStatus.Success;
                    // Set properties.
                    SetItemPropertyFromXmlAttributeGuid(node, brand, "Guid");
                    SetItemPropertyFromXmlAttribute(node, brand, nameof(brand.IsMarked));
                    SetItemPropertyFromXmlAttribute(node, brand, nameof(brand.Name));
                    SetItemPropertyFromXmlAttribute(node, brand, nameof(brand.Code));

                    if (string.IsNullOrEmpty(brand.ParseResult.Exception))
                        brand.ParseResult.Message = "Is success";
                }
                catch (Exception ex)
                {
                    brand.ParseResult.Status = ParseStatus.Error;
                    brand.ParseResult.Exception = ex.Message;
                    if (ex.InnerException is not null)
                        brand.ParseResult.InnerException = ex.InnerException.Message;
                }
            }
            else
            {
                brand.ParseResult.Status = ParseStatus.Error;
                brand.ParseResult.Exception = $"The node with name '{node.Name}' is not ident Brand!";
            }
            brands.Add(brand);
        }
        return brands;
    }

    private List<NomenclatureGroupModel> GetNomenclatureGroupList(XElement xml)
    {
        List<NomenclatureGroupModel> nomenclatureGroups = new();
        XmlDocument xmlDocument = new();
        xmlDocument.LoadXml(xml.ToString());
        if (xmlDocument.DocumentElement is null) return nomenclatureGroups;

        XmlNodeList list = xmlDocument.DocumentElement.GetElementsByTagName("NomenclatureGroup");
        foreach (XmlNode node in list)
        {
            NomenclatureGroupModel nomenclatureGroup = new();
            try
            {
                nomenclatureGroup.ParseResult.Status = ParseStatus.Success;
                // Set properties.
                SetItemPropertyFromXmlAttributeGuid(node, nomenclatureGroup, "Guid");
                SetItemPropertyFromXmlAttribute(node, nomenclatureGroup, nameof(nomenclatureGroup.IsMarked));
                SetItemPropertyFromXmlAttribute(node, nomenclatureGroup, nameof(nomenclatureGroup.Name));
                SetItemPropertyFromXmlAttribute(node, nomenclatureGroup, nameof(nomenclatureGroup.Code));

                if (string.IsNullOrEmpty(nomenclatureGroup.ParseResult.Exception))
                    nomenclatureGroup.ParseResult.Message = "Is success";
            }
            catch (Exception ex)
            {
                nomenclatureGroup.ParseResult.Status = ParseStatus.Error;
                nomenclatureGroup.ParseResult.Exception = ex.Message;
                if (ex.InnerException is not null)
                    nomenclatureGroup.ParseResult.InnerException = ex.InnerException.Message;
            }
            nomenclatureGroups.Add(nomenclatureGroup);
        }

        return nomenclatureGroups;
    }

    [Obsolete(@"Deprecated method")]
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

    private (object? Value, ParseResultModel ParseResult) GetItemPropertyFromXmlAttribute(XmlNode node, string propertyName)
    {
        object? value = null;
        ParseResultModel parseResult = new() { Status = ParseStatus.Success };
        switch (propertyName)
        {
            case "Guid":
                if (Guid.TryParse(GetXmlAttributeValue(node, "Guid"), out Guid uid))
                {
                    value = uid;
                }
                else
                {
                    parseResult.Status = ParseStatus.Error;
                    parseResult.Exception = $"{propertyName} is Empty!";
                }
                if (value is Guid guid && guid.Equals(Guid.Empty))
                {
                    parseResult.Status = ParseStatus.Error;
                    parseResult.Exception = $"{propertyName} is Empty!";
                }
                break;
            case nameof(SqlTableBase.IsMarked):
                string isMarkedStr = GetXmlAttributeValue(node, propertyName);
                switch (isMarkedStr)
                {
                    case "0":
                    case "false":
                        value = false;
                        break;
                    case "1":
                    case "true":
                        value = true;
                        break;
                    default:
                        parseResult.Status = ParseStatus.Error;
                        parseResult.Exception = $"{propertyName} is Empty!";
                        break;
                }
                break;
            case nameof(SqlTableBase.Name):
                value = GetXmlAttributeValue(node, propertyName);
                if (value is string name && string.IsNullOrEmpty(name))
                {
                    parseResult.Status = ParseStatus.Error;
                    parseResult.Exception = $"{propertyName} is Empty!";
                }
                break;
            case "Code":
                value = GetXmlAttributeValue(node, propertyName);
                if (value is string code && string.IsNullOrEmpty(code))
                {
                    parseResult.Status = ParseStatus.Error;
                    parseResult.Exception = $"{propertyName} is Empty!";
                }
                break;
        }
        return new(value, parseResult);
    }

    private void SetItemPropertyFromXmlAttributeGuid<T>(XmlNode node, T item, string propertyName) where T : SqlTableBase, new()
    {
        (object? Value, ParseResultModel ParseResult) property = GetItemPropertyFromXmlAttribute(node, propertyName);
        if (property.Value is Guid uid)
            item.IdentityValueUid = uid;
        item.ParseResult = property.ParseResult;
    }

    private void SetItemPropertyFromXmlAttribute<T>(XmlNode node, T item, string propertyName) where T : SqlTableBase, new()
    {
        (object? Value, ParseResultModel ParseResult) property = GetItemPropertyFromXmlAttribute(node, propertyName);
        item.ParseResult = property.ParseResult;
        switch (propertyName)
        {
            case nameof(item.IsMarked):
                if (property.Value is bool isMarked)
                    item.IsMarked = isMarked;
                break;
            case nameof(item.Name):
                if (property.Value is string name)
                    item.Name = name;
                break;
            case nameof(item.Description):
                if (property.Value is string description)
                    item.Description = description;
                break;
            case "Code":
                if (property.Value is string code)
                    switch (item)
                    {
                        case BrandModel brand:
                            brand.Code = code;
                            break;
                        case NomenclatureModel nomenclature:
                            nomenclature.Code = code;
                            break;
                        case NomenclatureGroupModel nomenclatureGroup:
                            nomenclatureGroup.Code = code;
                            break;
                    }
                break;
        }
    }

    [Obsolete(@"Deprecated method")]
    private void SetItemPropertyFromXmlAttributeDeprecated<T>(XmlNode node, T item, string propertyName) where T : SqlTableBase, new()
    {
        switch (propertyName)
        {
            case "Guid":
                if (Guid.TryParse(GetXmlAttributeValue(node, "Guid"), out Guid uid))
                {
                    item.IdentityValueUid = uid;
                }
                else
                {
                    item.ParseResult.Status = ParseStatus.Error;
                    item.ParseResult.Exception = "Guid is Empty!";
                }
                if (item.IdentityValueUid.Equals(Guid.Empty))
                {
                    item.ParseResult.Status = ParseStatus.Error;
                    item.ParseResult.Exception = "Guid is Empty!";
                }
                break;
            case nameof(item.IsMarked):
                string isMarkedStr = GetXmlAttributeValue(node, propertyName);
                switch (isMarkedStr)
                {
                    case "0":
                    case "false":
                        item.IsMarked = false;
                        break;
                    case "1":
                    case "true":
                        item.IsMarked = true;
                        break;
                    default:
                        item.ParseResult.Status = ParseStatus.Error;
                        item.ParseResult.Exception = "IsMarked is Empty!";
                        break;
                }
                break;
            case nameof(item.Name):
                item.Name = GetXmlAttributeValue(node, propertyName);
                if (string.IsNullOrEmpty(item.Name))
                {
                    item.ParseResult.Status = ParseStatus.Error;
                    item.ParseResult.Exception = "Name is Empty!";
                }
                break;
            case "Code":
                switch (item)
                {
                    case BrandModel brand:
                        brand.Code = GetXmlAttributeValue(node, propertyName);
                        if (string.IsNullOrEmpty(brand.Code))
                        {
                            brand.ParseResult.Status = ParseStatus.Error;
                            brand.ParseResult.Exception = "Code is Empty!";
                        }
                        break;
                    case NomenclatureGroupModel nomenclatureGroup:
                        nomenclatureGroup.Code = GetXmlAttributeValue(node, propertyName);
                        if (string.IsNullOrEmpty(nomenclatureGroup.Code))
                        {
                            nomenclatureGroup.ParseResult.Status = ParseStatus.Error;
                            nomenclatureGroup.ParseResult.Exception = "Code is Empty!";
                        }
                        break;
                }
                break;
        }
    }

    private string GetXmlAttributeValue(XmlElement? xmlElement, string nameAttribute)
    {
        string result = string.Empty;
        if (xmlElement is null) return result;

        foreach (XmlAttribute? attribute in xmlElement.Attributes)
        {
            if (attribute is not null)
            {
                if (attribute.Name.ToUpper().Equals(nameAttribute.ToUpper()))
                {
                    return attribute.Value;
                }
            }
        }
        return result;
    }

    private string GetXmlAttributeValue(XmlNode? xmlNode, string nameAttribute)
    {
        string result = string.Empty;
        if (xmlNode is null) return result;
        if (xmlNode.Attributes is null) return result;

        foreach (XmlAttribute? attribute in xmlNode.Attributes)
        {
            if (attribute is not null)
            {
                if (attribute.Name.ToUpper().Equals(nameAttribute.ToUpper()))
                {
                    return attribute.Value;
                }
            }
        }
        return result;
    }

    public ContentResult NewResponseBarCodes(ISessionFactory sessionFactory, DateTime dtStart, DateTime dtEnd, string formatString)
    {
        return NewResponse1cCore<ResponseBarCodeListModel>(sessionFactory, response =>
        {
            List<SqlFieldFilterModel> sqlFilters = new()
            {
                new(nameof(BarCodeModel.CreateDt), SqlFieldComparerEnum.MoreOrEqual, dtStart),
                new(nameof(BarCodeModel.CreateDt), SqlFieldComparerEnum.LessOrEqual, dtEnd),
            };
            SqlCrudConfigModel sqlCrudConfig = new(sqlFilters, true, false, false, true);
            List<BarCodeModel> barcodesDb = DataContext.GetListNotNullable<BarCodeModel>(sqlCrudConfig);
            response.ResponseBarCodes = WebResponseUtils.CastBarCodes(barcodesDb);
            response.StartDate = dtStart;
            response.EndDate = dtEnd;
            response.Count = response.ResponseBarCodes.Count;
        }, formatString, false);
    }

    public ContentResult NewResponse1cNomenclaturesGroups(ISessionFactory sessionFactory, XElement request, string formatString)
    {
        return NewResponse1cCore<Response1cShortModel>(sessionFactory, response =>
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>(), true, false, false, true);
            List<NomenclatureGroupModel> nomenclaturesGroupsDb = DataContext.GetListNotNullable<NomenclatureGroupModel>(sqlCrudConfig);

            List<NomenclatureGroupModel> nomenclaturesGroupsXml = GetNomenclatureGroupList(request);
            foreach (NomenclatureGroupModel nomenclatureGroupXml in nomenclaturesGroupsXml)
            {
                // string xml = brandInput.SerializeAsXmlString<BrandModel>(false);
                switch (nomenclatureGroupXml.ParseResult.Status)
                {
                    case ParseStatus.Success:
                        AddResponse1cItem(response, nomenclaturesGroupsDb, nomenclatureGroupXml);
                        break;
                    case ParseStatus.Error:
                        AddResponse1cException(response, nomenclatureGroupXml.IdentityValueUid,
                            nomenclatureGroupXml.ParseResult.Exception, nomenclatureGroupXml.ParseResult.InnerException);
                        break;
                }
            }
        }, formatString, false);
    }

    [Obsolete(@"Deprecated method")]
    public ContentResult NewResponse1cNomenclaturesDeprecated(ISessionFactory sessionFactory, XElement request, string formatString)
    {
        return NewResponse1cCore<Response1cShortModel>(sessionFactory, response =>
        {
            SqlCrudConfigModel sqlCrudConfig = new(new List<SqlFieldFilterModel>(), true, false, false, true);
            List<NomenclatureModel> nomenclaturesDb = DataContext.GetListNotNullable<NomenclatureModel>(sqlCrudConfig);

            List<NomenclatureModel> nomenclaturesInput = GetNomenclatureDeprecatedList(request);
            foreach (NomenclatureModel nomenclatureInput in nomenclaturesInput)
            {
                // string xml = brandInput.SerializeAsXmlString<BrandModel>(false);
                switch (nomenclatureInput.ParseResult.Status)
                {
                    case ParseStatus.Success:
                        AddResponse1cItem(response, nomenclaturesDb, nomenclatureInput);
                        break;
                    case ParseStatus.Error:
                        AddResponse1cException(response, nomenclatureInput.IdentityValueUid,
                            nomenclatureInput.ParseResult.Exception, nomenclatureInput.ParseResult.InnerException);
                        break;
                }
            }
        }, formatString, false);
    }

    /// <summary>
    /// New response 1C.
    /// </summary>
    /// <param name="sessionFactory"></param>
    /// <param name="version"></param>
    /// <param name="formatString"></param>
    /// <returns></returns>
    public ContentResult NewResponse1cIsNotFound(ISessionFactory sessionFactory, string version, string formatString) =>
        NewResponse1cCore<Response1cModel>(sessionFactory, response =>
        {
            response.Infos.Add(new($"Version {version} is not found!"));
        }, formatString, false, HttpStatusCode.NotFound);

    #endregion
}