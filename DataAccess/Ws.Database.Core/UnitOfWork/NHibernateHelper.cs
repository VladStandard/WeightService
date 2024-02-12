using System.Reflection;
using Microsoft.Extensions.Configuration;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Context;
using NHibernate.Dialect;
using NHibernate.Driver;
using Ws.Database.Core.Listeners;
using Ws.UnitOfWork.Abstractions.Exceptions;

namespace Ws.Database.Core.UnitOfWork;

public static class NHibernateHelper
{
    #region Vars

    public static ISessionFactory SessionFactory { get; set; } = null!;
    private static SqlSettingsModels SqlSettingsModels { get; set; } = new();
    private static Configuration SqlConfiguration { get; set; } = new();

    #endregion

    #region Private

    private static SqlSettingsModels LoadJsonConfig()
    {
        IConfigurationRoot sqlConfiguration = new ConfigurationBuilder()
            .AddJsonFile("sqlconfig.json", optional: false, reloadOnChange: false)
            .Build();

        SqlSettingsModels sqlSettingsModels = new();
        sqlConfiguration.GetSection("SqlSettings").Bind(sqlSettingsModels);
        return sqlSettingsModels;
    }
    
    private static void SetSqlConfiguration()
    {
        SqlSettingsModels = LoadJsonConfig();
        SqlConfiguration = new();
        SqlConfiguration.DataBaseIntegration(db => {
            db.ConnectionString = SqlSettingsModels.GetConnectionString();
            db.Dialect<MsSql2012Dialect>();
            db.Driver<SqlClientDriver>();
            db.LogSqlInConsole = SqlSettingsModels.IsShowSql;
        });
        SqlConfiguration.EventListeners.PreInsertEventListeners =
            [new SqlCreateDtListener()];
        SqlConfiguration.EventListeners.PreUpdateEventListeners =
            [new SqlChangeDtListener()];
    }
    
    private static void AddConfigurationMappings()
    {
        ModelMapper mapper = new();
        mapper.AddMappings(Assembly.GetExecutingAssembly().GetTypes());
        HbmMapping mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
        SqlConfiguration.AddMapping(mapping);
    }

    #endregion

    #region Public

    public static void SetSessionFactory()
    {
        SetSqlConfiguration();
        AddConfigurationMappings();
        SessionFactory = SqlConfiguration.BuildSessionFactory();
    }
    
    public static ISession GetSession()
    {
        if (CurrentSessionContext.HasBind(SessionFactory))
            return SessionFactory.GetCurrentSession();
        throw new DataBaseSessionException();
    }

    #endregion
}