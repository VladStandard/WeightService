// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.PlusCharacteristics;
using DataCore.Sql.TableScaleModels.PlusGroups;

namespace WsWebApi.Helpers;

/// <summary>
/// Web API Controller helper.
/// </summary>
public partial class ControllerHelper
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

    public ContentResult GetContentResult(Func<ContentResult> action, string format,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        try
        {
            return action();
        }
        catch (Exception ex)
        {
            filePath = Path.GetFileName(filePath);
            ServiceExceptionModel serviceException = new(filePath, lineNumber, memberName, ex);
            return DataFormatUtils.GetContentResult<ServiceExceptionModel>(serviceException, format, HttpStatusCode.OK);
        }
        finally
        {
            GC.Collect();
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private ContentResult NewResponse1cCore<T>(ISessionFactory sessionFactory, Action<T> action,
        string format, bool isTransaction, HttpStatusCode httpStatusCode = HttpStatusCode.OK) where T : SerializeBase, new()
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

        return DataFormatUtils.GetContentResult<T>(response, format, httpStatusCode);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public ContentResult NewResponse1cFromQuery(ISessionFactory sessionFactory, string query,
        SqlParameter? sqlParameter, string format, bool isTransaction)
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
        }, format, isTransaction);
    }

    [Obsolete(@"Deprecated method")]
    public ContentResult NewResponseBarcodeFromAction(ISessionFactory sessionFactory, DateTime start, DateTime end, string format, bool isTransaction)
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

        }, format, isTransaction);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cItem<T>(Response1cShortModel response, IReadOnlyCollection<T> listDb, T itemXml) where T : ISqlTable
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
            string itemInputCode;
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
                case var cls when cls == typeof(PluGroupModel):
                    if (itemXml is PluGroupModel nomenclatureGroupInput)
                    {
                        itemInputCode = nomenclatureGroupInput.Code;
                        PluGroupModel? itemCast = listDb.Cast<PluGroupModel>().FirstOrDefault(x => x.Code.Equals(itemInputCode));
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

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cException(Response1cShortModel response, Guid uid, string? exceptionMessage, string? innerExceptionMessage)
    {
        Response1cErrorModel responseRecord = new(uid,
            !string.IsNullOrEmpty(innerExceptionMessage) ? innerExceptionMessage : exceptionMessage ?? string.Empty);
        response.Errors.Add(responseRecord);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cException(Response1cShortModel response, BrandModel brand)
    {
        Response1cErrorModel responseRecord = new(brand.IdentityValueUid, brand.ParseResult.Exception ?? string.Empty);
        if (!string.IsNullOrEmpty(brand.ParseResult.InnerException))
            responseRecord.Message += " | " + brand.ParseResult.InnerException;
        response.Errors.Add(responseRecord);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private void AddResponse1cException(Response1cShortModel response, Guid uid, Exception? ex) =>
        AddResponse1cException(response, uid, ex?.Message, ex?.InnerException?.Message);

    private void SetItemPropertyFromXmlAttribute<T>(XmlNode xmlNode, T item, string xmlPropertyName) where T : ISqlTable
    {
        SetItemPropertyFromXmlAttributeForBase(xmlNode, item, xmlPropertyName);
        switch (item)
        {
            case BrandModel brand:
                SetItemPropertyFromXmlAttributeForBrand(xmlNode, brand, xmlPropertyName);
                break;
            case PluModel plu:
                SetItemPropertyFromXmlAttributeForPlu(xmlNode, plu, xmlPropertyName);
                break;
            case PluGroupModel pluGroup:
                SetItemPropertyFromXmlAttributeForPluGroup(xmlNode, pluGroup, xmlPropertyName);
                break;
            case PluCharacteristicModel pluCharacteristic:
                SetItemPropertyFromXmlAttributeForPluCharacteristic(xmlNode, pluCharacteristic, xmlPropertyName);
                break;
        }
    }

    private void SetItemParseResultException<T>(T item, string xmlPropertyName) where T : ISqlTable
    {
        item.ParseResult.Status = ParseStatus.Error;
        item.ParseResult.Exception = string.IsNullOrEmpty(item.ParseResult.Exception)
            ? $"{xmlPropertyName} {LocaleCore.WebService.IsEmpty}!"
            : $"{item.ParseResult.Exception} | {xmlPropertyName} {LocaleCore.WebService.IsEmpty}!";

    }

    private void SetItemPropertyFromXmlAttributeForBase<T>(XmlNode xmlNode, T item, string xmlPropertyName) where T : ISqlTable
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "GUID":
            case "IDENTITYVALUEUID":
                item.IdentityValueUid = GetXmlAttributeGuid(xmlNode, item, "Guid");
                break;
            case "ISMARKED":
                item.IsMarked = GetXmlAttributeBool(xmlNode, item, xmlPropertyName);
                break;
            case "NAME":
                item.Name = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
            case "DESCRIPTION":
                item.Description = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
        }
    }

    private void SetItemPropertyFromXmlAttributeForBrand(XmlNode xmlNode, BrandModel item, string xmlPropertyName)
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "CODE":
                item.Code = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
        }
    }

    private void SetItemPropertyFromXmlAttributeForPlu(XmlNode xmlNode, PluModel item, string xmlPropertyName)
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "ISGROUP":
                item.IsGroup = GetXmlAttributeBool(xmlNode, item, xmlPropertyName);
                break;
            case "FULLNAME":
                item.FullName = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
            case "CODE":
                item.Code = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
            case nameof(item.ShelfLifeDays):
                item.ShelfLifeDays = GetXmlAttributeUshort(xmlNode, item, "ShelfLife");
                break;
            case "ISCHECKWEIGHT":
                item.IsCheckWeight = GetXmlAttributeBool(xmlNode, item, "MeasurementType", "ШТ", "КГ");
                break;
            case "NUMBER":
                item.Number = GetXmlAttributeUshort(xmlNode, item, "PluNumber");
                break;
        }
    }

    private void SetItemPropertyFromXmlAttributeForPluGroup(XmlNode xmlNode, PluGroupModel item, string xmlPropertyName)
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "ISGROUP":
                item.IsGroup = GetXmlAttributeBool(xmlNode, item, xmlPropertyName);
                break;
            case "CODE":
                item.Code = GetXmlAttributeString(xmlNode, item, xmlPropertyName);
                break;
            case "GROUPGUID":
                item.GroupGuid = GetXmlAttributeGuid(xmlNode, item, xmlPropertyName);
                break;
        }
    }

    private void SetItemPropertyFromXmlAttributeForPluCharacteristic(XmlNode xmlNode, PluCharacteristicModel item, string xmlPropertyName)
    {
        switch (xmlPropertyName.ToUpper())
        {
            case "ATTACHMENTSCOUNT":
                item.AttachmentsCount = GetXmlAttributeDecimal(xmlNode, item, xmlPropertyName);
                break;
        }
    }

    public string GetXmlAttributeString<T>(XmlNode? xmlNode, T item, string attributeName) where T : ISqlTable
    {
        if (xmlNode?.Attributes is null) return string.Empty;
        foreach (XmlAttribute? attribute in xmlNode.Attributes)
        {
            if (attribute is not null)
            {
                if (attribute.Name.ToUpper().Equals(attributeName.ToUpper()))
                    return attribute.Value;
            }
        }
        SetItemParseResultException(item, attributeName);
        return string.Empty;
    }

    public bool GetXmlAttributeBool<T>(XmlNode? xmlNode, T item, string xmlPropertyName, 
        List<string> valuesFalse, List<string> valuesTrue) where T : ISqlTable
    {
        string value = GetXmlAttributeString(xmlNode, item, xmlPropertyName).ToUpper();
        if (Enumerable.Contains(valuesFalse, value)) return false;
        if (Enumerable.Contains(valuesTrue, value)) return true;
        return default;
    }

    public bool GetXmlAttributeBool<T>(XmlNode? xmlNode, T item, string xmlPropertyName) where T : ISqlTable =>
        GetXmlAttributeBool(xmlNode, item, xmlPropertyName, new List<string> { "0", "FALSE" }, new() { "1", "TRUE" });

    public bool GetXmlAttributeBool<T>(XmlNode? xmlNode, T item, string xmlPropertyName, 
        string valueFalse, string valueTrue) where T : ISqlTable =>
        GetXmlAttributeBool(xmlNode, item, xmlPropertyName, new List<string> { valueFalse }, new() { valueTrue });

    public Guid GetXmlAttributeGuid<T>(XmlNode? xmlNode, T item, string xmlPropertyName) where T : ISqlTable => 
        Guid.TryParse(GetXmlAttributeString(xmlNode, item, xmlPropertyName), out Guid uid) ? uid : Guid.Empty;

    public ushort GetXmlAttributeUshort<T>(XmlNode? xmlNode, T item, string xmlPropertyName) where T : ISqlTable => 
        ushort.TryParse(GetXmlAttributeString(xmlNode, item, xmlPropertyName), out ushort result) ? result : default;

    public decimal GetXmlAttributeDecimal<T>(XmlNode? xmlNode, T item, string xmlPropertyName) where T : ISqlTable => 
        decimal.TryParse(GetXmlAttributeString(xmlNode, item, xmlPropertyName), out decimal result) ? result : default;

    private List<T> GetNodesListCore<T>(XElement xml, string nodeIdentity, Action<XmlNode, T> action) where T : ISqlTable, new()
    {
        List<T> itemsXml = new();
        XmlDocument xmlDocument = new();
        xmlDocument.LoadXml(xml.ToString());
        if (xmlDocument.DocumentElement is null) return itemsXml;

        XmlNodeList xmlNodes = xmlDocument.DocumentElement.ChildNodes;
        if (xmlNodes.Count <= 0) return itemsXml;
        foreach (XmlNode xmlNode in xmlNodes)
        {
            T itemXml = new() { ParseResult = { Status = ParseStatus.Success, Exception = string.Empty } };
            if (xmlNode.Name.Equals(nodeIdentity, StringComparison.InvariantCultureIgnoreCase))
            {
                try
                {
                    action(xmlNode, itemXml);
                    // Fix ParseResult.
                    itemXml.ParseResult.Message = string.IsNullOrEmpty(itemXml.ParseResult.Exception) ? "Is success" : itemXml.ParseResult.Exception;
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
                itemXml.ParseResult.Exception = $"The node `{nodeIdentity}` with `{xmlNode.Name}` is not ident!";
            }
            itemsXml.Add(itemXml);
        }
        return itemsXml;
    }

    public ContentResult NewResponseBarCodes(ISessionFactory sessionFactory, DateTime dtStart, DateTime dtEnd, string format)
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
        }, format, false);
    }

    /// <summary>
    /// New response 1C.
    /// </summary>
    /// <param name="sessionFactory"></param>
    /// <param name="version"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public ContentResult NewResponse1cIsNotFound(ISessionFactory sessionFactory, string version, string format) =>
        NewResponse1cCore<Response1cModel>(sessionFactory, response =>
        {
            response.Infos.Add(new($"Version {version} is not found!"));
        }, format, false, HttpStatusCode.NotFound);

    /// <summary>
    /// Update a record in the database.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isUpdateIdentity"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    private bool UpdateItemDb<T>(Response1cShortModel response, T itemXml, T? itemDb, bool isUpdateIdentity, bool isCounter) where T : ISqlTable
    {
        if (itemDb is null || !itemDb.IsNotNew) return false;
        if (isUpdateIdentity)
            itemDb.Identity = itemXml.Identity;
        itemDb.UpdateProperties(itemXml);
        (bool IsOk, Exception? Exception) dbUpdate = DataContext.DataAccess.UpdateForce(itemDb);
        if (dbUpdate.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(itemXml.IdentityValueUid));
        }
        else
            AddResponse1cException(response, itemXml.IdentityValueUid, dbUpdate.Exception);
        return true;
    }

    /// <summary>
    /// Save the record to the database.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="itemXml"></param>
    /// <param name="isCounter"></param>
    private void SaveItemDb<T>(Response1cShortModel response, T itemXml, bool isCounter) where T : ISqlTable
    {
        (bool IsOk, Exception? Exception) dbSave = DataContext.DataAccess.Save(itemXml, itemXml.Identity);
        // Add was success.
        if (dbSave.IsOk)
        {
            if (isCounter)
                response.Successes.Add(new(itemXml.IdentityValueUid));
        }
        else
            AddResponse1cException(response, itemXml.IdentityValueUid, dbSave.Exception);
    }

    #endregion
}