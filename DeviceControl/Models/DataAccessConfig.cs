using System;
using Microsoft.Extensions.Configuration;

namespace BlazorDeviceControl.Models
{
    /// <summary>
    /// appsettings.json
    /// </summary>
    public class DataAccessConfig
    {
        public IConfiguration Configuration { get; }
        public string Server => Configuration["Sql:Server"];
        public string Db => Configuration["Sql:Db"];
        public bool Trusted => Convert.ToBoolean(Configuration["Sql:Trusted"]);
        public string Username => Configuration["Sql:Username"];
        public string Password => Configuration["Sql:Password"];

        public DataAccessConfig(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public override string ToString()
        {
            var strTrusted = Trusted ? 
                $"{nameof(Trusted)}: true." 
                : $"{nameof(Username)}: {Username}. {nameof(Password)}: {Password}.";
            return $"{nameof(Server)}: { Server}. " +
                   $"{nameof(Db)}: {Db}. " +
                   $"{strTrusted}";
        }
    }
}
