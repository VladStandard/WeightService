// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Utils;

public static class WsServiceResponseUtils
{
    #region Public and private fields, properties, constructor

    private static AppVersionHelper AppVersion => AppVersionHelper.Instance;

    #endregion

    #region Public and private methods

    public static async Task GetResponseAsync(string url, RestRequest request, Action<RestResponse> action)
    {
        RestClientOptions options = new(url)
        {
            UseDefaultCredentials = true,
            ThrowOnAnyError = true,
            MaxTimeout = 60_000,
            RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };
        using RestClient client = new(options);
        RestResponse response = await client.GetAsync(request);

        action(response);
    }

    public static WsResponseBarCodeModel CastBarCode(WsSqlBarCodeModel barCode) =>
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

    #endregion
}