// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
namespace WsWebApiCore.Utils;

/// <summary>
/// Утилиты веб-ответов.
/// </summary>
public static class WsServiceResponseUtils
{
    #region Public and private fields, properties, constructor

    private static WsAppVersionHelper AppVersion => WsAppVersionHelper.Instance;

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

    private static WsResponseBarCodeModel CastBarCode(WsSqlBarCodeModel barCode) =>
        new WsResponseBarCodeModel().CloneCast(barCode);

    public static List<WsResponseBarCodeModel> CastBarCodes(IEnumerable<WsSqlBarCodeModel> barCodes) =>
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
            StrUtils.FormatDtEng(DateTime.Now, true),
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
        WsServiceResponseUtils.AddResponseExceptionString(response, uid, ex.Message, ex.InnerException?.Message);

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
    public static void RemoveResponseErrorFromSuccess(WsResponse1CShortModel response, WsResponse1CErrorModel responseRecord)
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

    #endregion
}