﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.Configuration;

namespace BlazorDeviceControl.Data
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
