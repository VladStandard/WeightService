// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;

namespace WebApiExample.Common;

public static class NHibernateExtensions
{

	public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
	{
		var mapper = new ModelMapper();
		mapper.AddMappings(typeof(NHibernateExtensions).Assembly.ExportedTypes);
		var domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

		var configuration = new Configuration();
		configuration.DataBaseIntegration(c =>
		{
			c.Dialect<MsSql2012Dialect>();
			c.ConnectionString = connectionString;
			c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
			c.SchemaAction = SchemaAutoAction.Validate;
			c.LogFormattedSql = true;
			c.LogSqlInConsole = true;
		});
		configuration.AddMapping(domainMapping);

		var sessionFactory = configuration.BuildSessionFactory();

		services.AddSingleton(sessionFactory);
		services.AddScoped(factory => sessionFactory.OpenSession());
		// services.AddScoped<IMapperSession, NHibernateMapperSession>();

		return services;
	}
}