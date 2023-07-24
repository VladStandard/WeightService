// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Utils;

/// <summary>
/// Утилиты веб-ответов.
/// </summary>
public static class WsServiceUtilsResponse
{
    #region Public and private fields, properties, constructor

    private static WsAppVersionHelper AppVersion => WsAppVersionHelper.Instance;
    private static WsSqlCrudConfigModel SqlCrudConfig => 
        new(new List<WsSqlFieldFilterModel>(), WsSqlEnumIsMarked.ShowAll, false, true, false);

    private static WsSqlBarcodeRepository BarcodeRepository { get; } = new();
    
    #endregion

    #region Public and private methods

    public static async Task GetResponseAsync(string url, RestRequest request, Action<RestResponse> action)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        RestClientOptions options = new(url)
        {
            UseDefaultCredentials = true,
            ThrowOnAnyError = true,
            MaxTimeout = 60_000,
            RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };
        using RestClient client = new(options);
        RestResponse response = await client.GetAsync(request).ConfigureAwait(true);
        action(response);
    }

    private static WsResponseBarCodeModel CastBarCode(WsSqlBarCodeModel barCode) => new(barCode);

    private static List<WsResponseBarCodeModel> CastBarCodes(IEnumerable<WsSqlBarCodeModel> barCodes) =>
        barCodes.Select(CastBarCode).ToList();

    public static WsServiceInfoModel NewServiceInfo(Assembly assembly, ISessionFactory sessionFactory)
    {
        AppVersion.Setup(assembly);

        using ISession session = sessionFactory.OpenSession();
        //using ITransaction transaction = session.BeginTransaction();
        ISQLQuery sqlQuery = session.CreateSQLQuery(WsWebSqlQueriesV2.GetDateTimeNow);
        sqlQuery.SetTimeout(session.Connection.ConnectionTimeout);
        string sqlCurrentDate = sqlQuery.UniqueResult<string>().ToString(CultureInfo.InvariantCulture);
        //transaction.Commit();

        return new(Environment.MachineName,
            AppVersion.App,
            AppVersion.Version,
            WsStrUtils.FormatDtEng(DateTime.Now, true),
            sqlCurrentDate,
            session.Connection.ConnectionString,
            session.Connection.ConnectionTimeout,
            session.Connection.DataSource,
            session.Connection.ServerVersion,
            session.Connection.Database,
            (ulong)Process.GetCurrentProcess().WorkingSet64 / 1048576,
            (ulong)Process.GetCurrentProcess().PrivateMemorySize64 / 1048576);
    }

    public static void AddResponseException(WsResponse1CShortModel response, WsSqlBrandModel brand)
    {
        WsResponse1CErrorModel responseRecord = new(brand.IdentityValueUid, brand.ParseResult.Exception);
        if (!string.IsNullOrEmpty(brand.ParseResult.InnerException))
            responseRecord.Message += " | " + brand.ParseResult.InnerException;
        if (response.Errors.Select(item => item.Uid).Contains(brand.Uid1C))
        {
            if (response.Errors.Find(item => Equals(item.Uid, brand.Uid1C)) is { } error)
                error.Message += $" | {responseRecord}";
        }
        else
            response.Errors.Add(responseRecord);
    }

    public static void AddResponseException(WsResponse1CShortModel response, Guid uid, Exception ex) =>
        AddResponseExceptionString(response, uid, ex.Message, ex.InnerException?.Message);

    /// <summary>
    /// Add error for response.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="uid"></param>
    /// <param name="exceptionMessage"></param>
    /// <param name="innerExceptionMessage"></param>
    public static void AddResponseExceptionString(WsResponse1CShortModel response, Guid uid, string exceptionMessage,
        string? innerExceptionMessage = "")
    {
        WsResponse1CErrorModel responseRecord = new(uid,
            !string.IsNullOrEmpty(innerExceptionMessage) ? innerExceptionMessage : exceptionMessage);
        if (response.Errors.Select(item => item.Uid).Contains(uid))
        {
            if (response.Errors.Find(item => Equals(item.Uid, uid)) is { } error)
                error.Message += $" | {responseRecord}";
        }
        else
            response.Errors.Add(responseRecord);

        RemoveResponseErrorFromSuccess(response, responseRecord);
    }

    /// <summary>
    /// Remove error from success for response.
    /// </summary>
    /// <param name="response"></param>
    /// <param name="responseRecord"></param>
    private static void RemoveResponseErrorFromSuccess(WsResponse1CShortModel response, WsResponse1CErrorModel responseRecord)
    {
        bool isFind;
        do
        {
            isFind = false;
            foreach (WsResponse1CSuccessModel success in response.Successes)
            {
                if (Equals(success.Uid, responseRecord.Uid))
                {
                    response.Successes.Remove(success);
                    isFind = true;
                    break;
                }
            }
        } while (isFind);
    }

    public static ContentResult NewResponse1CCore<T>(Action<T> action, string format, bool isDebug, ISessionFactory sessionFactory,
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
                            response1CShort.ServiceInfo = NewServiceInfo(Assembly.GetExecutingAssembly(), sessionFactory);
                    }
                    break;
                case var cls when cls == typeof(WsResponse1CModel):
                    if (response is WsResponse1CModel response1C)
                    {
                        response1C.IsDebug = isDebug;
                        if (response1C.IsDebug)
                            response1C.ServiceInfo = NewServiceInfo(Assembly.GetExecutingAssembly(), sessionFactory);
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

    public static ContentResult NewResponse1CFromQuery(string url, SqlParameter? sqlParameter, string format, bool isDebug,
        ISessionFactory sessionFactory) =>
        NewResponse1CCore<WsResponse1CModel>(response =>
        {
            if (!string.IsNullOrEmpty(url))
            {
                if (WsServiceUtils.SqlCore.SessionFactory is null)
                    throw new ArgumentException(nameof(WsServiceUtils.SqlCore.SessionFactory));
                if (response.ResponseQuery is not null)
                    response.ResponseQuery.Query = url;
                //ISQLQuery sqlQuery = WsDataContext.Session.CreateSQLQuery(url);
                ISQLQuery sqlQuery = WsServiceUtils.SqlCore.SessionFactory.OpenSession().CreateSQLQuery(url);
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

    public static ContentResult NewResponseBarCodes(DateTime dtStart, DateTime dtEnd, string format, bool isDebug, ISessionFactory sessionFactory)
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
            List<WsSqlBarCodeModel> barcodesDb = BarcodeRepository.GetList(sqlCrudConfig);
            response.ResponseBarCodes = CastBarCodes(barcodesDb);
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
    public static ContentResult NewResponse1CIsNotFound(string message, string format, bool isDebug, ISessionFactory sessionFactory) =>
        NewResponse1CCore<WsResponse1CModel>(response =>
        {
            response.Infos.Add(new(message));
        }, format, isDebug, sessionFactory, HttpStatusCode.NotFound);

    #endregion
}