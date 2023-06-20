// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Common;

/// <summary>
/// Базовый класс веб-контроллера.
/// </summary>
public class WsServiceControllerBase : ControllerBase
{
    #region Public and private fields, properties, constructor

    protected WsAppVersionHelper AppVersion { get; } = WsAppVersionHelper.Instance;
    protected ISessionFactory SessionFactory { get; }
    internal WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    internal WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    internal WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    private WsSqlCrudConfigModel SqlCrudConfig => new(new List<WsSqlFieldFilterModel>(),
        WsSqlEnumIsMarked.ShowAll, false, false, true, false);
    public WsServiceControllerBase(ISessionFactory sessionFactory) => SessionFactory = sessionFactory;

    #endregion

    #region Public and private methods

    internal ContentResult NewResponse1CCore<T>(Action<T> action, string format, bool isDebug, ISessionFactory sessionFactory,
        HttpStatusCode httpStatusCode = HttpStatusCode.OK) where T : SerializeBase, new()
    {
        T response = new();

        try
        {
            action(response);
            switch (typeof(T))
            {
                case var cls when cls == typeof(WsResponse1CShortModel):
                    if (response is WsResponse1CShortModel response1CShort)
                    {
                        response1CShort.IsDebug = isDebug;
                        if (response1CShort.IsDebug)
                            response1CShort.Info = WsServiceResponseUtils.NewServiceInfo(Assembly.GetExecutingAssembly(), sessionFactory);
                    }
                    break;
                case var cls when cls == typeof(WsResponse1CModel):
                    if (response is WsResponse1CModel response1C)
                    {
                        response1C.IsDebug = isDebug;
                        if (response1C.IsDebug)
                            response1C.Info = WsServiceResponseUtils.NewServiceInfo(Assembly.GetExecutingAssembly(), sessionFactory);
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            httpStatusCode = HttpStatusCode.InternalServerError;
            switch (typeof(T))
            {
                case var cls when cls == typeof(WsResponse1CShortModel):
                    if (response is WsResponse1CShortModel response1CShort)
                        response1CShort.Errors.Add(new(ex));
                    break;
                case var cls when cls == typeof(WsResponse1CModel):
                    if (response is WsResponse1CModel response1C)
                        response1C.Errors.Add(new(ex));
                    break;
            }
        }

        return WsDataFormatUtils.GetContentResult<T>(response, format, httpStatusCode);
    }

    public ContentResult NewResponse1CFromQuery(string url, SqlParameter? sqlParameter, string format, bool isDebug,
        ISessionFactory sessionFactory) =>
        NewResponse1CCore<WsResponse1CModel>(response =>
        {
            if (!string.IsNullOrEmpty(url))
            {
                if (SqlCore.SessionFactory is null)
                    throw new ArgumentException(nameof(SqlCore.SessionFactory));
                if (response.ResponseQuery is not null)
                    response.ResponseQuery.Query = url;
                //ISQLQuery sqlQuery = WsDataContext.Session.CreateSQLQuery(url);
                ISQLQuery sqlQuery = SqlCore.SessionFactory.OpenSession().CreateSQLQuery(url);
                if (sqlParameter is not null)
                {
                    if (response.ResponseQuery is not null)
                        response.ResponseQuery.Parameters.Add(new(sqlParameter));
                    sqlQuery.SetParameter(sqlParameter.ParameterName, sqlParameter.Value);
                }

                IList? list = sqlQuery.List();
                object?[] result = new object?[list.Count];
                if (list is [object[] records])
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
        }, format, isDebug, sessionFactory);

    public ContentResult NewResponseBarCodes(DateTime dtStart, DateTime dtEnd, string format, bool isDebug, ISessionFactory sessionFactory)
    {
        return NewResponse1CCore<WsResponseBarCodeListModel>(response =>
        {
            List<WsSqlFieldFilterModel> sqlFilters = new()
            {
                new() { Name = nameof(WsSqlBarCodeModel.CreateDt), Comparer = WsSqlEnumFieldComparer.MoreOrEqual, Value = dtStart },
                new() { Name = nameof(WsSqlBarCodeModel.CreateDt), Comparer = WsSqlEnumFieldComparer.LessOrEqual, Value = dtEnd },
            };
            WsSqlCrudConfigModel sqlCrudConfig = SqlCrudConfig;
            sqlCrudConfig.AddFilters(sqlFilters);
            List<WsSqlBarCodeModel> barcodesDb = ContextManager.ContextList.GetListNotNullableBarCodes(sqlCrudConfig);
            response.ResponseBarCodes = WsServiceResponseUtils.CastBarCodes(barcodesDb);
            response.StartDate = dtStart;
            response.EndDate = dtEnd;
            response.Count = response.ResponseBarCodes.Count;
        }, format, isDebug, sessionFactory);
    }

    /// <summary>
    /// Новый ответ 1С - не найдено.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="format"></param>
    /// <param name="isDebug"></param>
    /// <param name="sessionFactory"></param>
    /// <returns></returns>
    public ContentResult NewResponse1CIsNotFound(string message, string format, bool isDebug, ISessionFactory sessionFactory) =>
        NewResponse1CCore<WsResponse1CModel>(response =>
        {
            response.Infos.Add(new(message));
        }, format, isDebug, sessionFactory, HttpStatusCode.NotFound);

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
            WsServiceExceptionModel serviceException = new(filePath, lineNumber, memberName, ex);
            return WsDataFormatUtils.GetContentResult<WsServiceExceptionModel>(serviceException, format, HttpStatusCode.OK);
        }
        finally
        {
            GC.Collect();
        }
    }

    /// <summary>
    /// Get AcceptVersion from string value.
    /// </summary>
    /// <returns></returns>
    protected WsSqlEnumAcceptVersion GetAcceptVersion(string value) =>
        value.ToUpper() switch
        {
            "V2" => WsSqlEnumAcceptVersion.V2,
            "V3" => WsSqlEnumAcceptVersion.V3,
            _ => WsSqlEnumAcceptVersion.V1
        };

    #endregion

    #region Public and private methods - Update item

    /// <summary>
    /// Обновить запись 1C в БД.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal void UpdateItemDb<T>(WsResponse1CShortModel response, T itemXml, T? itemDb) where T : WsSqlTable1CBase
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        response.Successes.Add(new(itemXml.Uid1C));
    }

    /// <summary>
    /// Save the record to the database.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="item"></param>
    /// <param name="isCounter"></param>
    /// <param name="uid1C"></param>
    internal void SaveItemDb<T>(WsResponse1CShortModel response, T item, bool isCounter, Guid uid1C) where T : WsSqlTableBase
    {
        SqlCore.Save(item, item.Identity);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить бренд в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal void UpdateBrandDb(WsResponse1CShortModel response, Guid uid1C, WsSqlBrandModel itemXml, WsSqlBrandModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal void UpdatePluFkDb(WsResponse1CShortModel response, Guid uid1C, WsSqlPluFkModel itemXml, WsSqlPluFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь клипсы и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal void UpdatePluClipFkDb(WsResponse1CShortModel response, Guid uid1C, WsSqlPluClipFkModel itemXml, WsSqlPluClipFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить номенклатурную группу в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal void UpdatePluGroupDb(WsResponse1CShortModel response, Guid uid1C, WsSqlPluGroupModel itemXml, WsSqlPluGroupModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь номенклатурной группы и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal void UpdatePluGroupFkDb(WsResponse1CShortModel response, Guid uid1C, WsSqlPluGroupFkModel itemXml, WsSqlPluGroupFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь бренда и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal void UpdatePluBrandFkDb(WsResponse1CShortModel response, Guid uid1C, WsSqlPluBrandFkModel itemXml, WsSqlPluBrandFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь пакета и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal void UpdatePluBundleFkDb(WsResponse1CShortModel response, Guid uid1C, WsSqlPluBundleFkModel itemXml, WsSqlPluBundleFkModel? itemDb, bool isCounter)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь номенклатурной характеристики и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <returns></returns>
    internal void UpdatePluCharacteristicFk(WsResponse1CShortModel response, Guid uid1C, WsSqlPluCharacteristicsFkModel itemXml,
        WsSqlPluCharacteristicsFkModel? itemDb)
    {
        if (itemDb is null || itemDb.IsNew) return;
        itemDb.UpdateProperties(itemXml);
        SqlCore.Update(itemDb);
        //if (isCounter) response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Обновить связь вложенности и ПЛУ в БД. Не использовать вместе с UpdateItem1cDb.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid1C"></param>
    /// <param name="itemXml"></param>
    /// <param name="itemDb"></param>
    /// <param name="isCounter"></param>
    /// <returns></returns>
    internal void UpdatePluNestingFk(WsResponse1CShortModel response, Guid uid1C, WsSqlPluNestingFkModel itemXml,
        WsSqlViewPluNestingModel? itemDb, bool isCounter)
    {
        //if (itemDb is null || itemDb.IsNew);
        //itemDb.UpdateProperties(itemXml);
        //ContextManager.ContextPluNesting.Update(itemDb);
        //    if (isCounter)
        //        response.Successes.Add(new(uid1C));
    }

    /// <summary>
    /// Заполнить таблицу связей разрешённых для загрузки ПЛУ из 1С.
    /// </summary>
    public void FillPlus1CFksDb()
    {
        // Проверить наличие всех связей разрешённых для загрузки ПЛУ из 1С.
        if (WsServiceCheckUtils.CheckExistsAllPlus1CFksDb()) return;
        // Получить список ПЛУ.
        foreach (WsSqlPluModel plu in ContextManager.ContextPlus.GetList())
        {
            // Обновить данные записи в таблице связей обмена ПЛУ 1С.
            UpdatePlu1CFkDbCore(plu);
        }
    }

    /// <summary>
    /// Обновить таблицу связей ПЛУ для обмена.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="recordXml"></param>
    internal List<WsSqlPlu1CFkModel> UpdatePlus1CFksDb<T>(WsResponse1CShortModel response,
        WsXmlContentRecord<T> recordXml) where T : WsSqlTable1CBase, new()
    {
        List<WsSqlPlu1CFkModel> plus1CFksDb = new();
        switch (recordXml)
        {
            // ПЛУ.
            case WsXmlContentRecord<WsSqlPluModel> pluXml:
                plus1CFksDb = GetPlus1CFksByGuid1C(pluXml.Item.Uid1C);
                break;
            // Характеристика ПЛУ.
            case WsXmlContentRecord<WsSqlPluCharacteristicModel> pluCharacteristicXml:
                plus1CFksDb = GetPlus1CFksByGuid1C(pluCharacteristicXml.Item.NomenclatureGuid);
                break;
        }
        // Обновить таблицу связей ПЛУ для обмена.
        plus1CFksDb.ForEach(item => UpdatePlu1CFkDbCore(response, recordXml, item));
        return plus1CFksDb;
    }

    /// <summary>
    /// Получить список связей обмена ПЛУ 1С по GUID_1C.
    /// </summary>
    /// <param name="uid1C"></param>
    public List<WsSqlPlu1CFkModel> GetPlus1CFksByGuid1C(Guid uid1C)
    {
        List<WsSqlPlu1CFkModel> plus1CFks = new();
        ContextCache.Load(WsSqlEnumTableName.Plus1CFks);
        // Получить список ПЛУ по UID_1C.
        List<WsSqlPluModel> plusDb = ContextManager.ContextPlus.GetListByUid1C(uid1C);
        foreach (WsSqlPluModel plu in plusDb)
        {
            WsSqlPlu1CFkModel? plu1CFkCache =
                ContextCache.Plus1CFks.Find(item => item.Plu.IdentityValueUid.Equals(plu.IdentityValueUid));
            if (plu1CFkCache is not null)
                plus1CFks.Add(plu1CFkCache);
        }
        return plus1CFks;
    }

    /// <summary>
    /// Обновить данные записи в таблице связей обмена ПЛУ 1С.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="response"></param>
    /// <param name="recordXml"></param>
    /// <param name="plu1CFk"></param>
    private void UpdatePlu1CFkDbCore<T>(WsResponse1CShortModel response, WsXmlContentRecord<T> recordXml,
        WsSqlPlu1CFkModel plu1CFk) where T : WsSqlTable1CBase, new()
    {
        WsSqlPlu1CFkModel? plu1CFkCache =
            ContextCache.Plus1CFks.Find(item => item.Plu.IdentityValueUid.Equals(plu1CFk.Plu.IdentityValueUid));

        // В кэше не найдено - сохранить.
        if (plu1CFkCache is null)
        {
            plu1CFk.UpdateProperties(recordXml.Content);
            SqlCore.Save(plu1CFk);
            // Загрузить кэш.
            ContextCache.Load(WsSqlEnumTableName.Plus1CFks);
        }
        // В кэше найдено - обновить.
        else
        {
            plu1CFkCache.UpdateProperties(plu1CFk);
            plu1CFkCache.UpdateProperties(recordXml.Content);
            WsSqlPlu1CFkValidator validator = new();
            ValidationResult validation = validator.Validate(plu1CFkCache);
            if (!validation.IsValid)
            {
                if (recordXml is WsXmlContentRecord<WsSqlPluModel> pluXml)
                    WsServiceResponseUtils.AddResponseExceptionString(response, pluXml.Item.Uid1C,
                        string.Join(',', validation.Errors.Select(item => item.ErrorMessage).ToList()));
                else if (recordXml is WsXmlContentRecord<WsSqlPluCharacteristicModel> pluCharacteristicXml)
                    WsServiceResponseUtils.AddResponseExceptionString(response, pluCharacteristicXml.Item.NomenclatureGuid,
                        string.Join(',', validation.Errors.Select(item => item.ErrorMessage).ToList()));
            }
            else
            {
                SqlCore.Update(plu1CFkCache);
            }
        }
    }

    /// <summary>
    /// Обновить данные записи в таблице связей обмена ПЛУ 1С.
    /// </summary>
    /// <param name="plu"></param>
    private void UpdatePlu1CFkDbCore(WsSqlPluModel plu)
    {
        WsSqlPlu1CFkModel? plu1CFkCache =
            ContextCache.Plus1CFks.Find(item => item.Plu.IdentityValueUid.Equals(plu.IdentityValueUid));
        // В кэше не найдено - сохранить.
        if (plu1CFkCache is null)
        {
            SqlCore.Save(new WsSqlPlu1CFkModel(plu));
            // Загрузить кэш.
            ContextCache.Load(WsSqlEnumTableName.Plus1CFks);
        }
        // В кэше найдено - обновить.
        else
        {
            plu1CFkCache.UpdateProperties(plu);
            WsSqlPlu1CFkValidator validator = new();
            ValidationResult validation = validator.Validate(plu1CFkCache);
            if (!validation.IsValid)
                throw new($"Exception at UpdatePlu1CFkDbCore. Check PLU {plu1CFkCache}!");
            SqlCore.Update(plu1CFkCache);
        }
    }

    #endregion
}